using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace YeAndroidDbSyner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadConfig();

            cbbPcPath.Text = mConfig[IndexOfPcPath];
            if (!string.IsNullOrEmpty(mConfig[IndexOfWaitTime]))
                nudWaitTime.Value = Convert.ToInt32(mConfig[IndexOfWaitTime]);

            //马上启动的话，减慢了界面打开速度。
            //GetInfo();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mConfig[IndexOfDevices] = Convert.ToString(cbbDevices.SelectedItem);
            mConfig[IndexOfPackages] = Convert.ToString(cbbPackage.SelectedItem);
            mConfig[IndexOfDbNames] = Convert.ToString(cbbDbName.SelectedItem);
            mConfig[IndexOfPcPath] = Convert.ToString(cbbPcPath.Text);
            mConfig[IndexOfWaitTime] = nudWaitTime.Value.ToString();

            SaveConfig();

            //自动清理adb进程	
            try
            {
                var processess = Process.GetProcessesByName("adb");
                foreach (var process in processess)
                    if (process.StartInfo.FileName.Contains(Application.StartupPath))
                        process.Kill();
            }
            catch
            {
                //ignore all
            }
        }


        private void cbbPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lst = AdbHelper.ListDatabasesFolder(Convert.ToString(cbbDevices.SelectedItem), Convert.ToString(cbbPackage.SelectedItem));
            if (lst.Count == 0)
                return;
            cbbDbName.Items.Clear();
            cbbDbName.Items.AddRange(lst.ToArray());
            cbbDbName.SelectedIndex = 0;
        }

        private void cbbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            var deviceNo = Convert.ToString(cbbDevices.SelectedItem);
            txbDeviceInfo.Text = string.Format("{0} {1} {2}({3})"
                , AdbHelper.GetDeviceBrand(deviceNo)
                , AdbHelper.GetDeviceModel(deviceNo)
                , AdbHelper.GetDeviceVersionRelease(deviceNo)
                , AdbHelper.GetDeviceVersionSdk(deviceNo));
        }

        private void cbbDbName_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(cbbPcPath.Text))
            {
                ShowInfo("Pc路径不存在！");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            lblInfo.Text = null;
            lblInfo.Refresh();
            try
            {
                var devPath = string.Format("/data/data/{0}/databases/{1}", cbbPackage.SelectedItem,
                                            cbbDbName.SelectedItem);
                if (AdbHelper.CopyFromDevice(Convert.ToString(cbbDevices.SelectedItem), devPath, cbbPcPath.Text))
                    ShowInfo("下载执行成功。");
                else
                    ShowInfo("下载执行失败！");
            }
            catch (Exception ex)
            {
                ShowInfo(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetInfo();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }


        private void GetInfo()
        {
            btnDownload.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            ShowInfo("获取信息中···");
            try
            {
                //启动服务
                if (!AdbHelper.StartServer())
                {
                    ShowInfo("服务启动失败！");
                    return;
                }

                //获取设备列表
                var devs = AdbHelper.GetDevices();
                if (devs.Length == 0)
                {
                    ShowInfo("没有连接设备！");
                    return;
                }

                cbbDevices.Items.Clear();
                cbbDevices.Items.AddRange(devs);
                cbbDevices.SelectedIndex = 0;

                //遍历指定目录
                var dataFolderList = AdbHelper.ListDataFolder(Convert.ToString(cbbDevices.SelectedItem));
                if (dataFolderList.Count == 0)
                {
                    ShowInfo("遍历Data目录失败！");
                    return;
                }

                //反转顺序，将系统的报名排到最后面
                dataFolderList.Reverse();

                cbbPackage.Items.Clear();
                cbbPackage.Items.AddRange(dataFolderList.ToArray());
                cbbPackage.SelectedIndex = 0;

                ShowInfo("信息获取完毕。");

                cbbDevices.SelectedItem = mConfig[IndexOfDevices];
                cbbPackage.SelectedItem = mConfig[IndexOfPackages];
                cbbDbName.SelectedItem = mConfig[IndexOfDbNames];

                btnDownload.Enabled = true;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ShowInfo(string info)
        {
            lblInfo.Text = info;
            lblInfo.Refresh();
        }

        #region 设置相关
        private const int IndexOfDevices = 0;
        private const int IndexOfPackages = 1;
        private const int IndexOfDbNames = 2;
        private const int IndexOfPcPath = 3;
        private const int IndexOfWaitTime = 4;
        private string[] mConfig = new[] { "", "", "", "", "" };
        private readonly string mConfigPath = Application.ExecutablePath.ToLower().Replace(".exe", ".ini");
        private void ReadConfig()
        {
            if (!File.Exists(mConfigPath))
                return;
            mConfig = File.ReadAllLines(mConfigPath);
            if (mConfig == null || mConfig.Length != 5)
                mConfig = new[] { "", "", "", "", "" };
        }
        private void SaveConfig()
        {
            File.WriteAllLines(mConfigPath, mConfig);
        }
        #endregion

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ProcessHelper.WaitTime = (int)nudWaitTime.Value;
        }
    }
}
