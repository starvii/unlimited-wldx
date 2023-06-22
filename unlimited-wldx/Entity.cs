using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace wldx
{
	public class Question
	{
		public string Trunk { get; set; }
		public IList<KeyValuePair<string, bool>> Options { get; set; }

		public override string ToString()
		{
			List<string> l = new List<string>();
			foreach (KeyValuePair<string, bool> kv in Options)
			{
				l.Add(kv.Key);
			}
			l.Sort();
			StringBuilder sb = new StringBuilder(Trunk);
			foreach (string option in l)
			{
				sb.Append('|');
				sb.Append(option);
			}
			return sb.ToString();
		}
	}

	public class WebQuestion: Question
	{
		public HtmlElement WebElement { get; set; }
	}

	public class DBQuestion: Question
	{
		public string FuzzyHash { get; set; }
	}
}
