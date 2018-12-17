using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace wldx
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
			if (null != WebBrowserMain.Url)
			{
				TextNav.Text = WebBrowserMain.Url.ToString();
			}
		}

		private void ExportStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ExampleSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				bool r = ServiceFuzzy.GenerateExample(ExampleSaveFileDialog.FileName);
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
				if (!ServiceFuzzy.ReadAnswerFromXml(fn))
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
			if (null == ServiceFuzzy.QuestionDatabase || 0 == ServiceFuzzy.QuestionDatabase.Count)
			{
				MessageBox.Show("please import answer database first.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (null == WebBrowserMain.Document)
			{
				MessageBox.Show("cannot find element. you may not enter the right url.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			IList<WebQuestion> list = ServiceWeb.ExtractQuestionFromHtml(WebBrowserMain.Document);
			if (null == list && 0 == list.Count)
			{
				MessageBox.Show("cannot find element. you may not enter the right url.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Thread t = new Thread(new ParameterizedThreadStart(FindAnswerThread));
			t.Start(list);
			
		}

		private void FindAnswerThread(object param)
		{
			ProgressChangedHandler progressChangedHandler = new ProgressChangedHandler(SetProgress);
			RendererWebHandler rendererWebHandler = new RendererWebHandler(ServiceWeb.RendererElement);

			object[] progressParams = new object[3] { 0, 100, 0 };
			object[] rendererParams = new object[4];
			try
			//if (param.GetType() == typeof(IList<WebQuestion>))
			{
				IList<WebQuestion> list = (IList<WebQuestion>)param;
				progressParams[1] = list.Count;
				int c = 0;
				foreach (WebQuestion wq in list)
				{
					int fi = -1;
					DBQuestion dq = ServiceFuzzy.FindQuestion(wq, out fi);
					ISet<int> answers = ServiceFuzzy.FindAnswer(wq, dq);
					rendererParams[0] = wq;
					rendererParams[1] = answers;
					rendererParams[2] = dq;
					rendererParams[3] = fi;
					Invoke(rendererWebHandler, rendererParams);
					//WebHelper.RendererElement(wq, answers);

					progressParams[2] = ++c;
					Invoke(progressChangedHandler, progressParams);
				}
				//Invoke()
			}
			catch (Exception e)
			{
				Console.Error.WriteLine("param is not typeof IList.");
				Console.Error.WriteLine(e);
			}
		}

		private delegate void RendererWebHandler(WebQuestion wq, ISet<int> answers, DBQuestion dq, int fuzzyIndex);
		private delegate void ProgressChangedHandler(int min, int max, int value);
		private void SetProgress(int min, int max, int value)
		{
			ProgressBar.Minimum = min;
			ProgressBar.Maximum = max;
			ProgressBar.Value = value;
		}

		private void WebBrowserMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (ServiceWeb.CheckHtml(WebBrowserMain.Document))
			{
				LabelWeb.Text = "web element prepared.";
			}
		}

		private void FromMain_Load(object sender, EventArgs e)
		{
			LabelWeb.Text = "";
			LabelXml.Text = "";
		}
	}
}
