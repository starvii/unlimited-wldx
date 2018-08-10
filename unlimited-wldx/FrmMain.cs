using System;
using System.Collections.Generic;
using System.Windows.Forms;
using wldx;

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
            DialogResult r = MessageBox.Show("exit unlimited-wldx?", "EXIT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        private void ExportStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ExampleSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bool r = new Mugen().GenerateExample(ExampleSaveFileDialog.FileName.ToString());
                if (r)
                {
                    MessageBox.Show("example is saved.", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {

                }
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form about = new FrmAboutBox();
            about.ShowDialog();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DatabaseOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(DatabaseOpenFileDialog.FileName.ToString());
            }
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (null == WebBrowserMain.Document)
            {
                MessageBox.Show("cannot find element. you may not enter the right url.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<Question> list = WebElement.ExtractQuestionFromHtml(WebBrowserMain.Document);
            if (null == list && 0 == list.Count)
            {
                MessageBox.Show("cannot find element. you may not enter the right url.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
