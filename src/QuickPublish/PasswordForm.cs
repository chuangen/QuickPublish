using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickPublish
{
    internal partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// PFX 文件名称。
        /// </summary>
        public string PfxName
        {
            get { return txtFileName.Text; }
            set { txtFileName.Text = value; }
        }
        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }
    }
}