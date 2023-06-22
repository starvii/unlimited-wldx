using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

namespace wldx
{
	class ServiceWeb
	{
		public static readonly string TRUNK = "TMTitle";
		public static readonly string OPTION = "TMOption";

		/// <summary>
		/// 从页面中提取题目与选项列表
		/// </summary>
		/// <param name="html"></param>
		/// <returns></returns>
		static public IList<WebQuestion> ExtractQuestionFromHtml(HtmlDocument html)
		{
			if (null == html)
			{
				return null;
			}
			HtmlElementCollection c = html.GetElementsByTagName("div");
			if (0 == c.Count)
			{
				return null;
			}
			IList<HtmlElement> nodeList = new List<HtmlElement>();
			foreach (HtmlElement h in c)
			{
				//Console.WriteLine(className);
				if (TRUNK.Equals(h.GetAttribute("className")))
				{
					nodeList.Add(h.Parent);
				}
			}

			IList<WebQuestion> ret = new List<WebQuestion>();
			foreach (HtmlElement h in nodeList)
			{
				HtmlElement node = h.Parent;
				WebQuestion q = ExtractFromHtmlNode(node);
				ret.Add(q);
				Console.WriteLine(q.ToString());
			}
			Console.WriteLine(nodeList.Count);
			return ret;
		}

		/// <summary>
		/// 从页面元素中提取题目与选项
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		static private WebQuestion ExtractFromHtmlNode(HtmlElement node)
		{
			WebQuestion q = new WebQuestion() { Options = new List<KeyValuePair<string, bool>>(), WebElement = node };
			foreach (HtmlElement e in node.GetElementsByTagName("div"))
			{
				if (TRUNK.Equals(e.GetAttribute("className")))
				{
					q.Trunk = e.InnerText.Trim();
					break;
				}
			}
			foreach (HtmlElement e in node.GetElementsByTagName("span"))
			{
				if (OPTION.Equals(e.GetAttribute("className")))
				{
					q.Options.Add(new KeyValuePair<string, bool>(e.InnerText.Trim(), false));
				}
			}
			return q;
		}

		static public string DBQuestionToString(DBQuestion dq)
		{
			StringBuilder sb = new StringBuilder("<br/>");
			sb.Append(dq.Trunk);
			foreach (KeyValuePair<string, bool> kv in dq.Options)
			{
				sb.Append("<br/>");
				sb.Append(kv.Key);
				if (kv.Value)
				{
					sb.Append("√");
				}
			}
			return sb.ToString();
		}

		/// <summary>
		/// 对html元素进行渲染
		/// 标明题目正确与否
		/// </summary>
		/// <param name="q"></param>
		/// <param name="answers"></param>
		static public void RendererElement(WebQuestion q, ISet<int> answers, DBQuestion mostLike, int fuzzyIndex)
		{
			if (null == answers || 0 == answers.Count)
			{
				foreach (HtmlElement e in q.WebElement.GetElementsByTagName("div"))
				{
					if (TRUNK.Equals(e.GetAttribute("className")))
					{
						//string hint = "未找到一致的题目。最接近的题目可能如下：" + DBQuestionToString(mostLike);
						e.InnerHtml += string.Format(" <font size=\"2\" color=\"blue\"><strong>{0}{1}</strong></font>", "未找到一致的题目。最接近的题目可能如下（匹配指数[" + fuzzyIndex +"]）：", DBQuestionToString(mostLike));
						break;
					}
				}
			}
			else
			{
				int index = 0;
				foreach (HtmlElement e in q.WebElement.GetElementsByTagName("span"))
				{
					if (OPTION.Equals(e.GetAttribute("className")))
					{
						if (answers.Contains(index))
						{
							e.InnerHtml += " <font size=\"4\" color=\"red\"><strong>√</strong></font>";
						}
						index++;
					}
				}
			}
		}

		/// <summary>
		/// 检测页面是否符合提取特征
		/// </summary>
		/// <param name="html"></param>
		/// <returns></returns>
		static public bool CheckHtml(HtmlDocument html)
		{
			if (null == html)
			{
				return false;
			}
			HtmlElementCollection c1 = html.GetElementsByTagName("div");
			if (0 == c1.Count)
			{
				return false;
			}
			HtmlElementCollection c2 = html.GetElementsByTagName("span");
			if (0 == c2.Count)
			{
				return false;
			}
			bool r1 = false;
			foreach (HtmlElement h in c1)
			{
				//Console.WriteLine(className);
				if (TRUNK.Equals(h.GetAttribute("className")))
				{
					r1 = true;
					break;
				}
			}
			bool r2 = false;
			foreach (HtmlElement h in c2)
			{
				if (OPTION.Equals(h.GetAttribute("className")))
				{
					r2 = true;
					break;
				}
			}
			return r1 && r2;
		}
	}
}
