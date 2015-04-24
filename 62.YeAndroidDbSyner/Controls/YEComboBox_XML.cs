using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace YEControl
{
    /// <summary>
    /// 能够读取和保存历史记录的下拉框控件。
    /// </summary>
    [ToolboxBitmap(typeof(ComboBox))]
    [Description("能够保持和删除任意输入的历史记录的ComboBox。存储格式为XML。")]
    public class YEComboBox_XML : ComboBox
    {
        const string comment = "此XML文件由YEControl中的YEComboBox生成的历史文件，\r\n【请不要修改！】以防加载该控件的程序出现异常！";

        public YEComboBox_XML()
        {
            this.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        #region 设计时  特性

        /// <summary>
        /// 保存日志文件的路径。
        /// </summary>
        private string _UserLogPath = @"History\Log.xml";

        /// <summary>
        /// 配置标签 
        /// </summary>
        private string _ControlLogKey = string.Empty;

        /// <summary>
        /// 获取或设置用户的历史记录文件路径。
        /// </summary>
        [Category("YE")]
        [Browsable(true)]
        [DefaultValue(@"History\Log.xml")]
        [Description("输入用于保存历史记录文件的文件路径，支持相对和绝对路径。"
           + "\r\n假如设置多个控件的此路径保持一致，则共用一个记录文件！")]
        public string UserLogPath
        {
            get
            {
                return _UserLogPath;
            }
            set
            {
                _UserLogPath = value;
            }
        }

        /// <summary>
        /// 配置标签 
        /// </summary>
        [Category("YE")]
        [Browsable(true)]
        [Description("指定配置文件中的根标签名，如ini文件中的[sectionName]，"
        + "\r\n默认使用此控件的Name属性以示区别。"
        + "\r\n假如多个控件设置相同的LogKey，则多个控件共用历史记录。")]
        public string ControlLogKey
        {
            get
            {
                return _ControlLogKey;
            }
            set
            {
                _ControlLogKey = value;
            }
        }

        #endregion

        #region 保存日志文件

        /// <summary>
        /// 保存历史记录到文件；此函数供外部程序调用，来决定何时保存历史记录。
        /// </summary>
        public void SaveHistory()
        {
            MySaveLog();
        }

        /// <summary>
        /// 在控件数据验证成功之后保存历史数据。
        /// </summary>
        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);

            MySaveLog();
        }

        /// <summary>
        /// 为保存历史记录前做准备。如检测文件夹以及便签签名。
        /// </summary>
        private void MySaveLog()
        {
            if (this.Text.Trim() == string.Empty)
                return;
            if (this.Items.Contains(this.Text))
                return;

            this.Items.Add(this.Text);

            SaveHistoryItems();
        }

        private void SaveHistoryItems()
        {
            //先检测日志所在文件夹是否存在。必须保证所在文件夹的存在，防止出现异常。
            string strFullPath = Path.GetFullPath(_UserLogPath);
            string strDirectory = Path.GetDirectoryName(strFullPath);

            if (!Directory.Exists(strDirectory))
                Directory.CreateDirectory(strDirectory);

            YESaveXmlLog(strFullPath, GetTheCotrolKey());
        }

        /// <summary>
        /// 将当前控件的Items值保存到历史记录文件中。
        /// </summary>
        /// <param name="logFullPath"></param>
        /// <param name="TagName"></param>
        private void YESaveXmlLog(string logFullPath, string TagName)
        {
            try
            {
                //创建一个XML对象
                XmlDocument xmlDoc = new XmlDocument();

                //已经存在现成文件，则加载数据。
                if (File.Exists(logFullPath))
                    xmlDoc.Load(logFullPath);

                XmlElement xmlRoot = xmlDoc.DocumentElement;

                if (xmlRoot == null)
                {
                    #region 初始化XML文件
                    //加入XML声明。
                    xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes"));

                    //加入XML注释。
                    xmlDoc.AppendChild(xmlDoc.CreateComment(comment));

                    //加入空行。
                    xmlDoc.AppendChild(xmlDoc.CreateWhitespace(" \n"));

                    //创建一个根节点(带命名空间)
                    xmlRoot = xmlDoc.CreateElement("YEControl", "ComboBox"
                        , "http://www.clesoft.cn");

                    xmlDoc.AppendChild(xmlRoot);
                    #endregion
                }

                //将原有的节点去掉，
                XmlNodeList xnl = xmlRoot.GetElementsByTagName(TagName);
                if (xnl.Count != 0)
                    xmlRoot.RemoveChild(xnl[0]);

                //然后再添加新的节点。
                XmlElement xeLog = xmlDoc.CreateElement(TagName);
                xmlRoot.AppendChild(xeLog);

                //遍历每一项写入配置。
                XmlElement xeTemp = null;
                foreach (object tmp in this.Items)
                {
                    xeTemp = xmlDoc.CreateElement("log");

                    xeTemp.InnerText = tmp.ToString();

                    xeLog.AppendChild(xeTemp);
                }

                //将配置写入到文件。
                xmlDoc.Save(logFullPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
            }
        }

        /// <summary>
        /// 获取根标签名，未定义，则使用空间的Name属性做标识。
        /// </summary>
        /// <returns></returns>
        private string GetTheCotrolKey()
        {
            return String.IsNullOrEmpty(_ControlLogKey) ? this.Name : _ControlLogKey;
        }
        #endregion

        #region 读取日志文件

        /// <summary>
        /// 每次控件首次显示的时候，开始读取历史记录。
        /// </summary>
        protected override void OnVisibleChanged(EventArgs e)
        {
            //方便首次输入就有Suggest的功能。
            if (base.Visible)
                myReadLog();

            base.OnVisibleChanged(e);
        }

        /// <summary>
        /// 每次控件下拉列表框打开时候，开始读取历史记录。
        /// </summary>
        protected override void OnDropDown(EventArgs e)
        {
            myReadLog();

            base.OnDropDown(e);
        }

        /// <summary>
        /// 读取所有的历史记录到Items里。
        /// </summary>
        private void myReadLog()
        {
            //读取指定的配置文件。
            string logFullPath = Path.GetFullPath(_UserLogPath);
            if (!File.Exists(logFullPath))
                return;

            XmlNodeList xnlTemp = YEReadXmlLog(logFullPath, GetTheCotrolKey());

            if (xnlTemp == null)
                return;

            //一切正常，开始加载历史记录。
            foreach (XmlNode var in xnlTemp)
            {
                if (!this.Items.Contains(var.InnerText))
                    this.Items.Add(var.InnerText);
            }
        }

        /// <summary>
        /// 返回指定XML文件的所有子节点。
        /// </summary>
        /// <param name="logFullPath"></param>
        /// <param name="logkey"></param>
        /// <returns></returns>
        private XmlNodeList YEReadXmlLog(string logFullPath, string logkey)
        {
            try
            {
                //加载可能存在的XML记录文档
                XmlDocument xmlDoc = new XmlDocument();
                if (File.Exists(logFullPath))
                    xmlDoc.Load(logFullPath);

                //不存在现成的记录文档
                XmlElement xmlRoot = xmlDoc.DocumentElement;
                if (xmlRoot == null)
                    return null;

                //虽然存在记录文件，但是可能没有记录有历史记录
                XmlNode xmlLog = xmlRoot.SelectSingleNode(logkey);
                if (xmlLog == null)
                    return null;

                //一切正常则返回历史记录集合。
                return xmlLog.ChildNodes;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return null;
            }
        }

        #endregion

        #region 删除历史记录

        /// <summary>
        /// 捕获键盘事件实现删除历史记录的功能。
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            //Alt+Del
            if (e.Alt && e.KeyCode == Keys.Delete)
                DeleteAllLog();
            else if (e.KeyCode == Keys.Delete)
                DeleteSelectedItem();

            base.OnKeyDown(e);
        }

        /// <summary>
        /// 删除单项。
        /// </summary>
        private void DeleteSelectedItem()
        {
            try
            {
                if (!this.Items.Contains(this.SelectedText))
                    return;

                string logFullPath = Path.GetFullPath(_UserLogPath);
                if (!File.Exists(logFullPath))
                    return;

                //从Items中删除
                this.Items.Remove(this.SelectedText);

                SaveHistoryItems();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
            }
        }

        /// <summary>
        /// 删除全部历史记录。
        /// </summary>
        private void DeleteAllLog()
        {
            if (MessageBox.Show("即将 删除[全部]“下拉历史记录”!\n\n是否继续？", ""//
                , MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) //
                == DialogResult.No)
                return;
            //直接将日志文件删除！似乎会影响别的控件的日志。
            //File.Delete(Path.GetFullPath(logPath));

            this.ResetText();

            this.Items.Clear();

            SaveHistoryItems();
        }

        #endregion

    }
}

