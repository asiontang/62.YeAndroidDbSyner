namespace YEControl
{
    partial class YESelectPathBox_XML
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelect = new System.Windows.Forms.Button();
            this.yeComboBox1 = new YEControl.YEComboBox_XML();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelect.Location = new System.Drawing.Point(122, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(39, 20);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            this.btnSelect.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSelect_MouseDown);
            this.btnSelect.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSelect_MouseUp);
            // 
            // yeComboBox1
            // 
            this.yeComboBox1.AllowDrop = true;
            this.yeComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.yeComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.yeComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.yeComboBox1.ControlLogKey = "";
            this.yeComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.yeComboBox1.Location = new System.Drawing.Point(0, 0);
            this.yeComboBox1.Name = "yeComboBox1";
            this.yeComboBox1.Size = new System.Drawing.Size(121, 20);
            this.yeComboBox1.TabIndex = 2;
            this.yeComboBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.yeComboBox1_DragDrop);
            this.yeComboBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.yeComboBox1_DragEnter);
            this.yeComboBox1.Validating += new System.ComponentModel.CancelEventHandler(this.yeComboBox1_Validating);
            this.yeComboBox1.Validated += new System.EventHandler(this.yeComboBox1_Validated);
            // 
            // YESelectPathBox_XML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.yeComboBox1);
            this.Controls.Add(this.btnSelect);
            this.DoubleBuffered = true;
            this.Name = "YESelectPathBox_XML";
            this.Size = new System.Drawing.Size(161, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        public YEComboBox_XML yeComboBox1;

    }
}
