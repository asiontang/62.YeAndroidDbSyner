using System.Windows.Forms;

namespace YeAndroidDbSyner
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbDevices = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cbbPackage = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbDbName = new System.Windows.Forms.ComboBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txbDeviceInfo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbPcPath = new YEControl.YESelectPathBox_XML();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDownload.Location = new System.Drawing.Point(72, 104);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 32);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "↓";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Enabled = false;
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpload.Location = new System.Drawing.Point(261, 104);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 32);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "↑";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "程序包名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "PC 路径：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "设备序号：";
            // 
            // cbbDevices
            // 
            this.cbbDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbDevices.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbDevices.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDevices.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbDevices.FormattingEnabled = true;
            this.cbbDevices.Location = new System.Drawing.Point(72, 6);
            this.cbbDevices.Name = "cbbDevices";
            this.cbbDevices.Size = new System.Drawing.Size(265, 20);
            this.cbbDevices.TabIndex = 3;
            this.cbbDevices.SelectedIndexChanged += new System.EventHandler(this.cbbDevices_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.Location = new System.Drawing.Point(183, 104);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(42, 32);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "☯";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cbbPackage
            // 
            this.cbbPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbPackage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbPackage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbPackage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPackage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbPackage.FormattingEnabled = true;
            this.cbbPackage.Location = new System.Drawing.Point(72, 52);
            this.cbbPackage.Name = "cbbPackage";
            this.cbbPackage.Size = new System.Drawing.Size(265, 20);
            this.cbbPackage.TabIndex = 1;
            this.cbbPackage.SelectedIndexChanged += new System.EventHandler(this.cbbPackage_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "数据库名：";
            // 
            // cbbDbName
            // 
            this.cbbDbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbDbName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbbDbName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbDbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDbName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbDbName.FormattingEnabled = true;
            this.cbbDbName.Location = new System.Drawing.Point(72, 78);
            this.cbbDbName.Name = "cbbDbName";
            this.cbbDbName.Size = new System.Drawing.Size(265, 20);
            this.cbbDbName.TabIndex = 6;
            this.cbbDbName.SelectedIndexChanged += new System.EventHandler(this.cbbDbName_SelectedIndexChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(12, 169);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(325, 23);
            this.lblInfo.TabIndex = 9;
            this.lblInfo.Text = "Info";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txbDeviceInfo
            // 
            this.txbDeviceInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txbDeviceInfo.Location = new System.Drawing.Point(72, 32);
            this.txbDeviceInfo.Name = "txbDeviceInfo";
            this.txbDeviceInfo.Size = new System.Drawing.Size(265, 14);
            this.txbDeviceInfo.TabIndex = 10;
            this.txbDeviceInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "设备信息：";
            // 
            // cbbPcPath
            // 
            this.cbbPcPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbPcPath.ControlLogKey = "Pc_Path";
            this.cbbPcPath.Location = new System.Drawing.Point(72, 142);
            this.cbbPcPath.Name = "cbbPcPath";
            this.cbbPcPath.Size = new System.Drawing.Size(265, 20);
            this.cbbPcPath.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 194);
            this.Controls.Add(this.txbDeviceInfo);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cbbPcPath);
            this.Controls.Add(this.cbbDbName);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cbbDevices);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbPackage);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Android数据库同步工具[By:AsionTang 1.0]";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private ComboBox cbbPackage;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ComboBox cbbDevices;
        private Button btnRefresh;
        private Label label4;
        private ComboBox cbbDbName;
        private YEControl.YESelectPathBox_XML cbbPcPath;
        private Label lblInfo;
        private TextBox txbDeviceInfo;
        private Label label5;
    }
}

