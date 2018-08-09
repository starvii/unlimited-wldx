using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace unlimited_wldx
{
    public partial class FromMain : Form
    {
        public FromMain()
        {
            InitializeComponent();
        }

        protected void ToNav(string url)
        {
            WebBrowserMain.Navigate(url);
        }

        private void FromMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r = MessageBox.Show("Exit unlimited wldx?", "EIXT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            ToNav(TextNav.Text);
        }

        private void TextNav_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ToNav(TextNav.Text);
            }
        }

        private void WebBrowserMain_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            TextNav.Text = WebBrowserMain.Url.ToString();
        }
    }
}
