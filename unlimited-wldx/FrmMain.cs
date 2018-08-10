using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                bool r = new Mugen().GenerateSampleDatabase(ExampleSaveFileDialog.FileName.ToString());
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
            HtmlElementCollection c = WebBrowserMain.Document.GetElementsByTagName("div");
            IList<HtmlElement> nodeList = new List<HtmlElement>();
            foreach(HtmlElement h in c)
            {
                //Console.WriteLine(className);
                if ("TMTitle".Equals(h.GetAttribute("className")))
                {
                    nodeList.Add(h.Parent);
                }
            }
            
            foreach(HtmlElement h in nodeList)
            {
                HtmlElement node = h.Parent;

                Console.WriteLine(ExtractFromHtmlNode(node).ToString());
            }
            Console.WriteLine(nodeList.Count);
            //MessageBox.Show(c.Count.ToString());
        }

        private Question ExtractFromHtmlNode(HtmlElement node) {
            Question q = new Question() { Options = new List<string>() };
            foreach (HtmlElement e in node.GetElementsByTagName("div"))
            {
                if ("TMTitle".Equals(e.GetAttribute("className")))
                {
                    q.Trunk = e.InnerText.Trim();
                    break;
                }
            }
            foreach (HtmlElement e in node.GetElementsByTagName("span"))
            {
                if ("TMOption".Equals(e.GetAttribute("className")))
                {
                    q.Options.Add(e.InnerText.Trim());
                }
            }
            return q;
        }
    }
}
