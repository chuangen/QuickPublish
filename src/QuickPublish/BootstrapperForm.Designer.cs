namespace QuickPublish
{
    partial class BootstrapperForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label1;
            this.chkBootstrapperEnabled = new System.Windows.Forms.CheckBox();
            this.chkListBoxBootstrappers = new System.Windows.Forms.CheckedListBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.cmbBootstrapperComponentsUrl = new System.Windows.Forms.ComboBox();
            this.radioComponentsLocationAbsolute = new System.Windows.Forms.RadioButton();
            this.radioComponentsLocationRelative = new System.Windows.Forms.RadioButton();
            this.radioComponentsLocationDefault = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 41);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(185, 12);
            label1.TabIndex = 1;
            label1.Text = "请选择要安装的系统必备组件(&W):";
            // 
            // chkBootstrapperEnabled
            // 
            this.chkBootstrapperEnabled.AutoSize = true;
            this.chkBootstrapperEnabled.Location = new System.Drawing.Point(12, 12);
            this.chkBootstrapperEnabled.Name = "chkBootstrapperEnabled";
            this.chkBootstrapperEnabled.Size = new System.Drawing.Size(246, 16);
            this.chkBootstrapperEnabled.TabIndex = 0;
            this.chkBootstrapperEnabled.Text = "创建用于安装系统必备组件的安装程序(&C)";
            this.chkBootstrapperEnabled.UseVisualStyleBackColor = true;
            // 
            // chkListBoxBootstrappers
            // 
            this.chkListBoxBootstrappers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkListBoxBootstrappers.FormattingEnabled = true;
            this.chkListBoxBootstrappers.Location = new System.Drawing.Point(12, 56);
            this.chkListBoxBootstrappers.Name = "chkListBoxBootstrappers";
            this.chkListBoxBootstrappers.Size = new System.Drawing.Size(492, 196);
            this.chkListBoxBootstrappers.TabIndex = 2;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 265);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(269, 12);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "检查 Microsoft Update 以获取更多可再发行组件";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.cmbBootstrapperComponentsUrl);
            this.groupBox1.Controls.Add(this.radioComponentsLocationAbsolute);
            this.groupBox1.Controls.Add(this.radioComponentsLocationRelative);
            this.groupBox1.Controls.Add(this.radioComponentsLocationDefault);
            this.groupBox1.Location = new System.Drawing.Point(14, 291);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 119);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "指定系统必备组件的安装位置";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(401, 83);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "浏览(&B)...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // cmbBootstrapperComponentsUrl
            // 
            this.cmbBootstrapperComponentsUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBootstrapperComponentsUrl.FormattingEnabled = true;
            this.cmbBootstrapperComponentsUrl.Location = new System.Drawing.Point(37, 85);
            this.cmbBootstrapperComponentsUrl.Name = "cmbBootstrapperComponentsUrl";
            this.cmbBootstrapperComponentsUrl.Size = new System.Drawing.Size(358, 20);
            this.cmbBootstrapperComponentsUrl.TabIndex = 3;
            // 
            // radioComponentsLocationAbsolute
            // 
            this.radioComponentsLocationAbsolute.AutoSize = true;
            this.radioComponentsLocationAbsolute.Location = new System.Drawing.Point(18, 63);
            this.radioComponentsLocationAbsolute.Name = "radioComponentsLocationAbsolute";
            this.radioComponentsLocationAbsolute.Size = new System.Drawing.Size(203, 16);
            this.radioComponentsLocationAbsolute.TabIndex = 2;
            this.radioComponentsLocationAbsolute.TabStop = true;
            this.radioComponentsLocationAbsolute.Text = "从下列位置下载系统必备组件(&A):";
            this.radioComponentsLocationAbsolute.UseVisualStyleBackColor = true;
            // 
            // radioComponentsLocationRelative
            // 
            this.radioComponentsLocationRelative.AutoSize = true;
            this.radioComponentsLocationRelative.Location = new System.Drawing.Point(18, 42);
            this.radioComponentsLocationRelative.Name = "radioComponentsLocationRelative";
            this.radioComponentsLocationRelative.Size = new System.Drawing.Size(293, 16);
            this.radioComponentsLocationRelative.TabIndex = 1;
            this.radioComponentsLocationRelative.TabStop = true;
            this.radioComponentsLocationRelative.Text = "从与我的应用程序相同的位置下载系统必备组件(&D)";
            this.radioComponentsLocationRelative.UseVisualStyleBackColor = true;
            // 
            // radioComponentsLocationDefault
            // 
            this.radioComponentsLocationDefault.AutoSize = true;
            this.radioComponentsLocationDefault.Location = new System.Drawing.Point(18, 20);
            this.radioComponentsLocationDefault.Name = "radioComponentsLocationDefault";
            this.radioComponentsLocationDefault.Size = new System.Drawing.Size(257, 16);
            this.radioComponentsLocationDefault.TabIndex = 0;
            this.radioComponentsLocationDefault.TabStop = true;
            this.radioComponentsLocationDefault.Text = "从组件供应商的网站上下载系统必备组件(&O)";
            this.radioComponentsLocationDefault.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(348, 425);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(429, 425);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // BootstrapperForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(516, 460);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.chkListBoxBootstrappers);
            this.Controls.Add(label1);
            this.Controls.Add(this.chkBootstrapperEnabled);
            this.Name = "BootstrapperForm";
            this.Text = "系统必备";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkBootstrapperEnabled;
        private System.Windows.Forms.CheckedListBox chkListBoxBootstrappers;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ComboBox cmbBootstrapperComponentsUrl;
        private System.Windows.Forms.RadioButton radioComponentsLocationAbsolute;
        private System.Windows.Forms.RadioButton radioComponentsLocationRelative;
        private System.Windows.Forms.RadioButton radioComponentsLocationDefault;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}