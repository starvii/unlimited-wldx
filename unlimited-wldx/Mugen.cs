using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Forms;

namespace wldx
{
	public class WebHelper
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
			WebQuestion q = new WebQuestion() { Options = new List<string>(), Element = node };
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
					q.Options.Add(e.InnerText.Trim());
				}
			}
			return q;
		}

		/// <summary>
		/// 对html元素进行渲染
		/// 标明题目正确与否
		/// </summary>
		/// <param name="q"></param>
		/// <param name="answers"></param>
		static public void RendererElement(WebQuestion q, ISet<int> answers)
		{
			if (null == answers || 0 == answers.Count)
			{
				foreach (HtmlElement e in q.Element.GetElementsByTagName("div"))
				{
					if (TRUNK.Equals(e.GetAttribute("className")))
					{
						e.InnerHtml += " <font size=\"4\" color=\"red\"><strong>NOT FOUND</strong></font>";
						break;
					}
				}
			}
			else
			{
				int index = 0;
				foreach (HtmlElement e in q.Element.GetElementsByTagName("span"))
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
			foreach(HtmlElement h in c2)
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

	public class Mugen
	{
		public int Threshold { get; set; } = 10;

		/// <summary>
		/// 内存数据库
		/// </summary>
		public IDictionary<string, DBQuestion> AnswerDatabase { get; private set; } = null;

		/// <summary>
		/// 从内存数据库中查找最相似的题目
		/// </summary>
		/// <param name="q"></param>
		/// <returns></returns>
		public ISet<int> FindAnswer(WebQuestion wq)
		{
			if (null != AnswerDatabase && AnswerDatabase.Count > 0)
			{
				// 1 内存库中是否直接存在题干
				if (AnswerDatabase.ContainsKey(wq.Trunk))
				{
					DBQuestion dq = AnswerDatabase[wq.Trunk];
					return FindAnswer(wq, dq);
				} else
				// 2 使用 Levenshtein 比较方法查找最相似的题目
				{
					List<KeyValuePair<int, DBQuestion>> list = new List<KeyValuePair<int, DBQuestion>>();
					foreach (DBQuestion _ in AnswerDatabase.Values)
					{
						KeyValuePair<int, DBQuestion> kv = new KeyValuePair<int, DBQuestion>(wq.CompareTo(_), _);
						list.Add(kv);
					}
					list.Sort(delegate (KeyValuePair<int, DBQuestion> k1, KeyValuePair<int, DBQuestion> k2)
						{
							return k1.Key.CompareTo(k2.Key);//升序
						}
					);
					Console.WriteLine(list);
					if (list[0].Key < Threshold)
					{
						DBQuestion dq = list[0].Value;
						if (wq.Options.Count == dq.Options.Count)
						{
							Console.WriteLine(string.Format("wq = {0}", wq));
							Console.WriteLine(string.Format("dq = {0}", dq));
							return FindAnswer(wq, dq);
						}
						else
						{
							Console.WriteLine(string.Format("wqcount {0} != dqcount {1}", wq.Options.Count, dq.Options.Count));
						}
					}
					else
					{
						Console.WriteLine(string.Format("distance {0} is out of threshold {1}", list[0].Key, Threshold));
					}
					
					//string a = wq.ToSort();
					//string b = dq.ToSort();
					//Console.WriteLine(a);
					//Console.WriteLine(b);
					
					//Console.WriteLine(list);
				}
			}
			return null;
		}

		/// <summary>
		/// 对比题目
		/// 查找正确答案
		/// </summary>
		/// <param name="wq"></param>
		/// <param name="dq"></param>
		/// <returns></returns>
		private ISet<int> FindAnswer(WebQuestion wq, DBQuestion dq)
		{
			ISet<int> ret = new HashSet<int>();
			ISet<string> corrects = new HashSet<string>();
			foreach (Option o in dq.Options)
			{
				if (o.Correct)
				{
					corrects.Add(OnlyChar(o.OptionText));
				}
			}
			for (int i = 0; i < wq.Options.Count; i++)
			{
				string s = OnlyChar(wq.Options[i]);
				if (corrects.Contains(s))
				{
					ret.Add(i);
				}
			}
			return ret;
		}

		/// <summary>
		/// 从path中读取XML
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public bool ReadAnswerFromXml(string path)
		{
			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load(path);
			}
			catch(Exception e)
			{
				Console.Error.WriteLine(e);
				return false;
			}
			try
			{
				AnswerDatabase = new Dictionary<string, DBQuestion>();
				XmlNode root = doc.SelectSingleNode("questions");
				foreach (XmlNode _node in root.ChildNodes)
				{
					XmlElement node = (XmlElement)_node;
					DBQuestion q = new DBQuestion() { Options = new List<Option>() };
					bool toGetTrunk = true;
					foreach (XmlNode _opt in node.ChildNodes)
					{
						if (toGetTrunk && _opt.GetType() == typeof(XmlText))
						{
							XmlText opt = (XmlText)_opt;
							Console.WriteLine(opt.InnerText);
							q.Trunk = opt.InnerText.Trim();
							toGetTrunk = false;
						}
						else if (_opt.GetType() == typeof(XmlElement))
						{
							XmlElement opt = (XmlElement)_opt;
							string o = opt.InnerText.Trim();
							string b = opt.GetAttribute("correct").Trim();
							bool c = b.Equals("1") ? true : false;
							q.Options.Add(new Option() { OptionText = o, Correct = c });
						}
					}
					AnswerDatabase.Add(q.Trunk, q);
				}
				return true;
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e);
				return false;
			}
			
		}

		/// <summary>
		/// 根据path路径创建XML样本
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public bool GenerateExample(string path)
		{
			XmlDocument doc = new XmlDocument();
			XmlElement root = doc.CreateElement("questions");
			doc.AppendChild(root);
			for (int i = 1; i < 4; i++)
			{
				XmlElement q = doc.CreateElement("question");
				q.InnerText = "QUESTION_" + i;
				for (int j = 1; j < 5; j++)
				{
					XmlElement o = doc.CreateElement("option");
					o.SetAttribute("correct", "0");
					o.InnerText = "OPTION_" + i + "_" + j;
					q.AppendChild(o);
				}
				root.AppendChild(q);
			}
			try
			{
				doc.Save(path);
				return true;
			}
			catch(Exception e)
			{
				Console.Error.WriteLine(e);
				return false;
			}
		}

		/// <summary>
		/// 过滤容易产生不同的字符，方便比较
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string OnlyChar(string str)
		{
			const string EXCLUED = " 　（）()，,。、\\/？?！!";
			string ret = (string)str.Clone();
			foreach (char c in EXCLUED.ToCharArray())
			{
				ret = ret.Replace(c.ToString(), "");
			}
			return ret;
		}
	}
}
