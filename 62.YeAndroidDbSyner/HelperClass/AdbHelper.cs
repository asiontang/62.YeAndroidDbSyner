using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace YeAndroidDbSyner
{
    /// <summary>
    /// Android Debug Bridge | Android Developers
    /// http://developer.android.com/tools/help/adb.html
    /// </summary>
    public class AdbHelper
    {
        /// <summary>
        /// adb.exe文件的路径，默认相对于当前应用程序目录取。
        /// </summary>
        public static string AdbExePath
        {
            get
            {
                return Path.Combine(Application.StartupPath, "AdbBin\\adb.exe");
            }
        }

        /// <summary>
        /// 当前ADB状态：
        /// adb get-state                - prints: offline | bootloader | device | unknown
        /// </summary>
        public enum AdbState
        {
            Offline, Bootloader, Device, Unknown, Error
        }

        /// <summary>
        /// 获取设备状态；多态设备时，获取的状态始终为unknwon
        /// adb get-state                - prints: offline | bootloader | device | unknown
        /// </summary>
        public static AdbState GetState()
        {
            //获取设备名称
            var result = ProcessHelper.Run(AdbExePath, "get-state");

            System.Diagnostics.Debug.WriteLine(result.ToString());

            if (result.Success)
            {
                switch (result.OutputString.Trim())
                {
                    case "offline":
                        return AdbState.Offline;
                    case "bootloader":
                        return AdbState.Bootloader;
                    case "device":
                        return AdbState.Device;
                    case "unknown":
                        return AdbState.Unknown;
                }
            }
            return AdbState.Error;
        }

        /// <summary>
        /// 启动ADB服务
        /// </summary>
        /// <returns></returns>
        public static bool StartServer()
        {
            return ProcessHelper.Run(AdbExePath, "start-server").Success;
        }

        /// <summary>
        /// 多态设备时，获取的状态始终为unknwon
        /// </summary>
        /// <returns></returns>
        public static string GetSerialNo()
        {
            return ProcessHelper.Run(AdbExePath, "get-serialno").OutputString.Trim();
        }

        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns></returns>
        public static string[] GetDevices()
        {
            var result = ProcessHelper.Run(AdbExePath, "devices");

            var itemsString = result.OutputString;
            var items = itemsString.Split(new[] { "$", "#", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var itemsList = new List<String>();
            foreach (var item in items)
            {
                var tmp = item.Trim();

                //第一行不含\t所以排除
                if (tmp.IndexOf("\t") == -1)
                    continue;
                var tmps = item.Split('\t');
                itemsList.Add(tmps[0]);
            }

            itemsList.Sort();

            return itemsList.ToArray();
        }

        /// <summary>
        /// 列举ls /data/data目录下的文件和目录
        /// </summary>
        /// <returns></returns>
        public static List<string> ListDataFolder(string deviceNo)
        {
            var moreArgs = new[] { "su", "ls /data/data", "exit", "exit" };
            var result = ProcessHelper.RunAsContinueMode(AdbExePath, string.Format("-s {0} shell", deviceNo), moreArgs);

            var itemsString = result.MoreOutputString[1];
            var items = itemsString.Split(new[] { "$", "#", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var itemsList = new List<String>();
            foreach (var item in items)
            {
                var tmp = item.Trim();
                //移除第一行，输入的命令
                if (tmp.Contains(moreArgs[1]))
                    continue;
                //移除空白行
                if (string.IsNullOrEmpty(tmp))
                    continue;
                //移除最后两行的root@android
                if (tmp.ToLower().Contains("root@"))
                    continue;
                itemsList.Add(tmp);
            }

            itemsList.Sort();

            return itemsList;
        }

        /// <summary>
        /// 指定“包名”后，就将其目录下的数据库文件遍历出来。
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public static List<string> ListDatabasesFolder(string deviceNo, string packageName)
        {
            var path = string.Format("ls /data/data/{0}/databases", packageName);

            var moreArgs = new[] { "su", path, "exit", "exit" };
            var result = ProcessHelper.RunAsContinueMode(AdbExePath, string.Format("-s {0} shell", deviceNo), moreArgs);

            var itemsString = result.MoreOutputString[1];
            var items = itemsString.Split(new[] { "$", "#", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var itemsList = new List<String>();
            foreach (var item in items)
            {
                var tmp = item.Trim();
                //移除第一行，输入的命令
                if (tmp.Contains(moreArgs[1]))
                    continue;
                //移除空白行
                if (string.IsNullOrEmpty(tmp))
                    continue;
                //移除最后两行的root@android
                if (tmp.ToLower().Contains("root@"))
                    continue;
                itemsList.Add(tmp);
            }

            itemsList.Sort();

            return itemsList;
        }

        /// <summary>
        /// 使用SU超级管理员模式拷贝文件到PC上
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <param name="devPath"></param>
        /// <param name="pcPath"></param>
        /// <returns></returns>
        public static bool CopyFromDevice(string deviceNo, string devPath, string pcPath)
        {
            var tmpPathInDevice = string.Format("/data/local/tmp/{0}", Path.GetFileName(devPath));

            //自动添加.DB后缀
            if (!Path.HasExtension(devPath))
                tmpPathInDevice += ".db";

            var moreArgs = new[] { "su", string.Format("cat {0} > {1}", devPath, tmpPathInDevice), "exit", "exit" };
            var result = ProcessHelper.RunAsContinueMode(AdbExePath, string.Format("-s {0} shell", deviceNo), moreArgs);
            if (!result.Success)
                return false;

            //使用Pull命令将数据库拷贝到Pc上
            //adb pull [-p] [-a] <remote> [<local>]
            result = ProcessHelper.Run(AdbExePath, string.Format("pull {0} {1}", tmpPathInDevice, pcPath));

            //假如成功则自动删除设备上临时文件
            if (result.Success)
                ProcessHelper.Run(AdbExePath, string.Format("shell rm {0}", tmpPathInDevice));

            return result.Success;
        }

        #region 获取设备相关信息
        /// <summary>
        /// -s 0123456789ABCDEF shell getprop ro.product.brand
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        public static string GetDeviceProp(string deviceNo, string propKey)
        {
            var result = ProcessHelper.Run(AdbExePath, string.Format("-s {0} shell getprop {1}", deviceNo, propKey));
            return result.OutputString.Trim();
        }
        /// <summary>
        /// 型号：[ro.product.model]: [Titan-6575]
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public static string GetDeviceModel(string deviceNo)
        {
            return GetDeviceProp(deviceNo, "ro.product.model");
        }
        /// <summary>
        /// 牌子：[ro.product.brand]: [Huawei]
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public static string GetDeviceBrand(string deviceNo)
        {
            return GetDeviceProp(deviceNo, "ro.product.brand");
        }
        /// <summary>
        /// 设备指纹：[ro.build.fingerprint]: [Huawei/U8860/hwu8860:2.3.6/HuaweiU8860/CHNC00B876:user/ota-rel-keys,release-keys]
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public static string GetDeviceFingerprint(string deviceNo)
        {
            return GetDeviceProp(deviceNo, "ro.build.fingerprint");
        }
        /// <summary>
        /// 系统版本：[ro.build.version.release]: [4.1.2]
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public static string GetDeviceVersionRelease(string deviceNo)
        {
            return GetDeviceProp(deviceNo, "ro.build.version.release");
        }
        /// <summary>
        /// SDK版本：[ro.build.version.sdk]: [16]
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <returns></returns>
        public static string GetDeviceVersionSdk(string deviceNo)
        {
            return GetDeviceProp(deviceNo, "ro.build.version.sdk");
        }
        #endregion
    }
}
