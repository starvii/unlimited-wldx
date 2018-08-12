using System;
using System.Collections.Generic;
using System.Windows.Forms;
using wldx;

namespace unlimited_wldx
{
	public partial class FromMain : Form
	{
		private readonly Mugen mugen = new Mugen();

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
			if (null != WebBrowserMain.Url)
			{
				TextNav.Text = WebBrowserMain.Url.ToString();
			}
		}

		private void ExportStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ExampleSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				bool r = mugen.GenerateExample(ExampleSaveFileDialog.FileName);
				if (r)
				{
					MessageBox.Show("example is saved.", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
				} else
				{
					MessageBox.Show("save example failed.", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				string fn = DatabaseOpenFileDialog.FileName;
				Console.WriteLine(string.Format("open and read XML file: {0}", fn));
				if (!mugen.ReadAnswerFromXml(fn))
				{
					MessageBox.Show("parse XML error.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
				} else
				{
					LabelXml.Text = string.Format("[{0}] loaded.", fn);
					//MessageBox.Show("load XML success.", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (null == mugen.AnswerDatabase || 0 == mugen.AnswerDatabase.Count)
			{
				MessageBox.Show("please import answer database first.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (null == WebBrowserMain.Document)
			{
				MessageBox.Show("cannot find element. you may not enter the right url.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			IList<WebQuestion> list = WebHelper.ExtractQuestionFromHtml(WebBrowserMain.Document);
			if (null == list && 0 == list.Count)
			{
				MessageBox.Show("cannot find element. you may not enter the right url.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			foreach (WebQuestion wq in list)
			{
				ISet<int> answers = mugen.FindAnswer(wq);
				WebHelper.RendererElement(wq, answers);
			}
		}

		private void WebBrowserMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (WebHelper.CheckHtml(WebBrowserMain.Document))
			{
				LabelWeb.Text = "web element prepared.";
			}
		}
	}
}
