using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace YEControl
{
    /// <summary>
    /// 支持选择并保存文件（或文件夹）路径的自定义控件。
    /// </summary>
    [DefaultEvent("SelectCompleted")]
    [Description("在YEComboBox的基础上自定义的，可以选择文件（夹）的集成控件。")]
    public partial class YESelectPathBox_XML : UserControl
    {
        #region 自定义方法
        /// <summary>
        /// 打开文件夹选择对话框。
        /// </summary>
        private void ShowFolderDialog()
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog(this) != DialogResult.OK)
                    return;

                this.Text = fbd.SelectedPath;

                this.OnSelectCompleted(new SelectCompletedEventArgs(this.Text));
            }
        }

        /// <summary>
        /// 是否使用警示文本。
        /// </summary>
        /// <param name="enable"></param>
        private void WaringText(bool enable)
        {
            //加粗加红警示
            if (enable)
            {
                yeComboBox1.ForeColor = Color.Red;
                yeComboBox1.Font = new Font(yeComboBox1.Font, FontStyle.Bold);
            }
            else
            {
                yeComboBox1.ForeColor = Color.Black;
                yeComboBox1.Font = new Font(yeComboBox1.Font, FontStyle.Regular);
            }
        }

        /// <summary>
        /// 使用指定的文件名筛选字符串来构建并打开文件对话框。
        /// </summary>
        /// <param name="fileFilter"></param>
        private void ShowFileDialog(string fileFilter)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = GetFileDialogFilter(fileFilter);
                if (ofd.ShowDialog(this) != DialogResult.OK)
                    return;

                this.Text = ofd.FileName;

                this.OnSelectCompleted(new SelectCompletedEventArgs(this.Text));
            }
        }

        /// <summary>
        /// 转换自定义的文件名筛选器为FileDialog的Filter。
        /// </summary>
        /// <param name="filter"></param>
        private string GetFileDialogFilter(string yeFilter)
        {
            //OpenFileDialog默认格式为：XX文件名|*.xxx|OO|*.ooo;*.txt|
            //自定义控件的格式为：*.xxx|*.ooo
            string filter = string.Format("支持文件（{0}）|{1}|"
            , yeFilter.Replace('|', '，')
            , yeFilter.Replace('|', ';'));

            foreach (string var in yeFilter.Split('|'))
            {
                //*.txt ==> TXT|*.txt
                filter += string.Format("{0}|{1}|", var.Remove(0, 2).ToUpperInvariant(), var);
            }
            return filter.TrimEnd('|');
        }
        #endregion

        #region 设计时属性的定义区

        #region 重现YEComboBox_XML的公开属性
        /// <summary>
        /// 获取或设置用户的历史记录文件路径。
        /// </summary>
        [Category("YE")
        , Browsable(true)
        , DefaultValue(@"History\Log.xml")
        , Description("输入用于保存历史记录文件的文件路径，支持相对和绝对路径。"
            + "\r\n假如设置多个控件的此路径保持一致，则共用一个记录文件！")]
        public string UserLogPath
        {
            get
            {
                return yeComboBox1.UserLogPath;
            }
            set
            {
                yeComboBox1.UserLogPath = value;
            }
        }

        /// <summary>
        /// 获取或设置历史记录文件配置中的根标签名。
        /// </summary>
        [Category("YE")
        , Browsable(true)
        , Description("指定配置文件中的根标签名，如ini文件中的[sectionName]，"
        + "\r\n默认使用此控件的Name属性以示区别。"
        + "\r\n假如多个控件设置相同的LogKey，则多个控件共用历史记录。")]
        public string ControlLogKey
        {
            get
            {
                return yeComboBox1.ControlLogKey;
            }
            set
            {
                yeComboBox1.ControlLogKey = value;
            }
        }
        #endregion

        /// <summary>
        /// 获取或设置下拉框（ComboBox）控件的文本。
        /// </summary>
        [Category("YE")]
        public new string Text
        {
            get
            {
                return yeComboBox1.Text;
            }
            set
            {
                yeComboBox1.Text = value;
            }
        }

        /// <summary>
        /// 获取或设置“选择”按钮的文本。
        /// </summary>
        [Category("YE")
        , Browsable(true)
        , DefaultValue("选择")
        , Description("“选择”按钮的文本显示。")]
        public string ButtonText
        {
            get
            {
                return btnSelect.Text;
            }
            set
            {
                btnSelect.Text = value;
            }
        }

        //必须声明一个私有变量来保存数据，不然直接调用
        //FileFilter属性，将是Null类型，使得出错很多。
        string _FileFilter = string.Empty;

        /// <summary>
        /// 获取或设置支持拖动的文件类型筛选器。
        /// </summary>
        [Category("YE")
        , Browsable(true)
        , DefaultValue("")
        , Description("支持拖动到框中显示的文件扩展名筛选器,"
            + "\r\n如：“*.txt|*.csv|*.*”；留空则表示支持“文件夹”路径显示。")]
        public string FileFilter
        {
            get
            {
                return _FileFilter;
            }
            set
            {
                _FileFilter = value.ToUpper();
            }
        }
        #endregion

        #region 设计时事件的定义区

        public delegate void SelectCompletedEventHandler(object sender, SelectCompletedEventArgs e);

        [Category("YE")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Description("当点击“选择”按钮完成后引发该事件。"
            + "\r\n或者当ComboBox控件“数据验证完成后”引发该事件。")]
        public event SelectCompletedEventHandler SelectCompleted;

        protected virtual void OnSelectCompleted(SelectCompletedEventArgs e)
        {
            try
            {
                //使用异常捕捉，因为委托调用别人的代码，可能会出现异常，
                //在其没有对异常捕获的情况下，为了防止程序出错，在此将其捕获。
                if (SelectCompleted != null)
                    SelectCompleted(this, e);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
            }
        }

        #endregion

        #region 手动填写路径的时候的数据验证
        private void yeComboBox1_Validating(object sender, CancelEventArgs e)
        {
            //为空的时候不能验证，不然会出现无法正常退出的问题。
            if (yeComboBox1.Text == "")
                return;

            this.Text = this.Text.Trim(new[] { '"', '\'', ' ' });

            if (!string.IsNullOrEmpty(FileFilter))
            {
                #region 验证文件路径
                //文件格式不支持或文件不存在的时候验证失败。
                string extension = Path.GetExtension(this.Text);
                extension = extension.ToUpper();
                if ((!FileFilter.Contains("*.*") && !FileFilter.Contains(extension))
                    || !File.Exists(this.Text))
                {
                    e.Cancel = true;
                    WaringText(true);
                }
                else
                    WaringText(false);
                #endregion
            }
            else
            {
                #region 验证文件夹路径
                if (!Directory.Exists(this.Text))
                {
                    e.Cancel = true;
                    WaringText(true);
                }
                else
                    WaringText(false);
                #endregion
            }
        }

        private void yeComboBox1_Validated(object sender, EventArgs e)
        {
            //在控件验证成功之后，就引发SelectCompleted事件
            if (yeComboBox1.Text != "")
                OnSelectCompleted(new SelectCompletedEventArgs(yeComboBox1.Text));
        }
        #endregion

        #region 支持拖动的功能
        private void yeComboBox1_DragEnter(object sender, DragEventArgs e)
        {
            //获取拖动传递过来的文件（夹）路径。
            string[] fdStrings = e.Data.GetData(DataFormats.FileDrop) as string[];

            //拖动数据为空则退出
            if (fdStrings == null || fdStrings.Length == 0)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            e.Effect = DragDropEffects.All;
        }

        private void yeComboBox1_DragDrop(object sender, DragEventArgs e)
        {
            //获取拖动传递过来的文件（夹）路径。
            string[] fdStrings = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fdStrings != null && fdStrings.Length != 0)
                this.Text = fdStrings[0];

            //手动将历史记录保存。
            yeComboBox1.SaveHistory();

            //在拖动完成后，也引发SelectCompleted事件
            if (yeComboBox1.Text != "")
                OnSelectCompleted(new SelectCompletedEventArgs(yeComboBox1.Text));
        }
        #endregion

        #region 右键按钮能够打开选择的文件或文件夹
        private void btnSelect_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right)
                return;
            ((Button)sender).Font = new Font(((Button)sender).Font, FontStyle.Bold);
        }

        private void btnSelect_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right)
                return;

            ((Button)sender).Font = new Font(((Button)sender).Font, FontStyle.Regular);

            try
            {
                if (yeComboBox1.Text != "")
                    System.Diagnostics.Process.Start(yeComboBox1.Text);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("SelectPathBox控件右键打开异常：\r\n" + ex.ToString());
            }
        }
        #endregion

        public YESelectPathBox_XML()
        {
            InitializeComponent();

            yeComboBox1.AllowDrop = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileFilter))
                ShowFolderDialog();
            else
                ShowFileDialog(FileFilter);

            //手动将历史记录保存。
            yeComboBox1.SaveHistory();
        }

        private void yeComboBox1_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
        }

    }
}
