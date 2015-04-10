using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickPublish
{
    public partial class ApplicationUpdateForm : Form
    {
        readonly ClickOnceConfigFile config;
        public ApplicationUpdateForm(ClickOnceConfigFile config)
        {
            if (config == null)
                throw new ArgumentNullException("config", "config 不能为空。");

            this.config = config;
            InitializeComponent();
        }
    }
}
