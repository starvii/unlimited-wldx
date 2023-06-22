using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml;

namespace wldx
{
	class ServiceFuzzy
	{
		public static readonly int THRESHOLD = 10;
		public static readonly int SPAMSUM_LENGTH = 64;
		public static readonly int FUZZY_MAX_RESULT = 2 * SPAMSUM_LENGTH + 20;

		/// <summary>
		/// 调用 ssdep fuzzy.dll 获取模糊hash
		/// </summary>
		/// <param name="buf"></param>
		/// <param name="bufLen"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		[DllImport("fuzzy.dll", CharSet = CharSet.Ansi, EntryPoint = "fuzzy_hash_buf", CallingConvention = CallingConvention.Cdecl)]
		public extern static int DllFuzzyHashBuf([MarshalAs(UnmanagedType.LPArray)] byte[] buf, UInt32 bufLen, [MarshalAs(UnmanagedType.LPArray)] byte[] result);

		[DllImport("fuzzy.dll", CharSet = CharSet.Ansi, EntryPoint = "fuzzy_compare", CallingConvention = CallingConvention.Cdecl)]
		public extern static int DllFuzzyCompare([MarshalAs(UnmanagedType.LPArray)] byte[] sig1, [MarshalAs(UnmanagedType.LPArray)] byte[] sig2);

		/// <summary>
		/// 获取 string 的模糊hash
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		static public string FuzzyString(string str)
		{
			byte[] result = new byte[FUZZY_MAX_RESULT];
			byte[] buf = Encoding.Default.GetBytes(str);
			int ret = DllFuzzyHashBuf(buf, (UInt32)buf.Length, result);
			if (ret == 0)
			{
				return Encoding.Default.GetString(result);
			}
			return "";
		}

		static public int FuzzyCompare(string sig1, string sig2)
		{
			byte[] b1 = Encoding.Default.GetBytes(sig1);
			byte[] b2 = Encoding.Default.GetBytes(sig2);
			return DllFuzzyCompare(b1, b2);
		}

		/// <summary>
		/// 内存数据库
		/// key: FuzzyHashSignature
		/// value: DBQuestion
		/// </summary>
		static public IDictionary<string, DBQuestion> QuestionDatabase { get; private set; } = null;


		static public DBQuestion FindQuestion(WebQuestion wq, out int fuzzyIndex)
		{
			if (null != QuestionDatabase && QuestionDatabase.Count > 0)
			{
				// 1 计算 WebQuestion Fuzzy Hash
				string sig = FuzzyString(wq.ToString());
				// 2 内存库中是否直接存在完全相同的题目
				if (QuestionDatabase.ContainsKey(sig))
				{
					fuzzyIndex = 100;
					return QuestionDatabase[sig];
				}
				else
				{
					DBQuestion dq = FindQuestionFuzzy(wq, out fuzzyIndex);
					return dq;
				}
			}
			throw new Exception("memory database is not prepared.");
		}

		static private DBQuestion FindQuestionFuzzy(WebQuestion wq, out int fuzzyIndex)
		{
			if (null != QuestionDatabase && QuestionDatabase.Count > 0)
			{
				string sig = FuzzyString(wq.ToString());
				List<KeyValuePair<int, DBQuestion>> list = new List<KeyValuePair<int, DBQuestion>>();
				foreach (DBQuestion dq in QuestionDatabase.Values)
				{
					int score = FuzzyCompare(sig, dq.FuzzyHash);
					list.Add(new KeyValuePair<int, DBQuestion>(score, dq));
				}
				list.Sort(delegate (KeyValuePair<int, DBQuestion> k1, KeyValuePair<int, DBQuestion> k2)
				{
					return k2.Key.CompareTo(k1.Key);//降序
				});
				// 显示前10个供调试
				for (int i = 0; i < THRESHOLD; i++)
				{
					Console.WriteLine(string.Format("{0} {1}", list[i].Key, list[i].Value.ToString()));
				}
				fuzzyIndex = list[0].Key;
				return list[0].Value;
			}
			throw new Exception("memory database is not prepared.");
		}

		static public ISet<int> FindAnswer(WebQuestion wq, DBQuestion dq)
		{
			if (wq.Options.Count != dq.Options.Count)
			{
				return null;
			}
			ISet<int> ret = new HashSet<int>();
			ISet<string> corrects = new HashSet<string>();
			foreach (KeyValuePair<string, bool> kv in dq.Options)
			{
				if (kv.Value)
				{
					corrects.Add(kv.Key);
				}
			}
			// 首先直接比较，是否存在一样的选项
			for (int i = 0; i < wq.Options.Count; i++)
			{
				string option = wq.Options[i].Key;
				if (corrects.Contains(option))
				{
					corrects.Remove(option);
					ret.Add(i);
				}
			}
			// 没有找全所有。可以使用模糊搜索，但这里简化为直接反馈找不到题目
			if (corrects.Count > 0)
			{
				return null;
			}
			return ret;
		}

		///// <summary>
		///// 从内存数据库中查找最相似的题目
		///// </summary>
		///// <param name="q"></param>
		///// <returns></returns>
		//static public ISet<int> FindAnswerFromDB(WebQuestion wq)
		//{
		//	if (null != QuestionDatabase && QuestionDatabase.Count > 0)
		//	{
		//		// 1 计算 WebQuestion Fuzzy Hash
		//		string sig = FuzzyString(wq.ToString());
		//		// 2 内存库中是否直接存在完全相同的题目
		//		if (QuestionDatabase.ContainsKey(sig))
		//		{
		//			DBQuestion dq = QuestionDatabase[sig];
		//			return FindAnswer(wq, dq);
		//		}
		//		else
		//		// 3 使用模糊哈希方法查找最相似的题目
		//		{
		//			List<KeyValuePair<int, DBQuestion>> list = new List<KeyValuePair<int, DBQuestion>>();
		//			foreach (DBQuestion _ in QuestionDatabase.Values)
		//			{
		//				KeyValuePair<int, DBQuestion> kv = new KeyValuePair<int, DBQuestion>(wq.CompareTo(_), _);
		//				list.Add(kv);
		//			}
		//			list.Sort(delegate (KeyValuePair<int, DBQuestion> k1, KeyValuePair<int, DBQuestion> k2)
		//			{
		//				return k1.Key.CompareTo(k2.Key);//升序
		//			}
		//			);
		//			Console.WriteLine(list);
		//			if (list[0].Key < Threshold)
		//			{
		//				DBQuestion dq = list[0].Value;
		//				if (wq.Options.Count == dq.Options.Count)
		//				{
		//					Console.WriteLine(string.Format("wq = {0}", wq));
		//					Console.WriteLine(string.Format("dq = {0}", dq));
		//					return FindAnswer(wq, dq);
		//				}
		//				else
		//				{
		//					Console.WriteLine(string.Format("wqcount {0} != dqcount {1}", wq.Options.Count, dq.Options.Count));
		//				}
		//			}
		//			else
		//			{
		//				Console.WriteLine(string.Format("distance {0} is out of threshold {1}", list[0].Key, Threshold));
		//			}

		//			//string a = wq.ToSort();
		//			//string b = dq.ToSort();
		//			//Console.WriteLine(a);
		//			//Console.WriteLine(b);

		//			//Console.WriteLine(list);
		//		}
		//	}
		//	return null;
		//}

		/// <summary>
		/// 对比题目
		/// 查找正确答案
		/// </summary>
		/// <param name="wq"></param>
		/// <param name="dq"></param>
		/// <returns></returns>
		//static private ISet<int> FindAnswer(WebQuestion wq, DBQuestion dq)
		//{
		//	if (wq.Options.Count != dq.Options.Count)
		//	{
		//		return null;
		//	}
		//	ISet<int> ret = new HashSet<int>();
		//	ISet<string> corrects = new HashSet<string>();
		//	foreach (Option o in dq.Options)
		//	{
		//		if (o.Correct)
		//		{
		//			corrects.Add(OnlyChar(o.OptionText));
		//		}
		//	}
		//	for (int i = 0; i < wq.Options.Count; i++)
		//	{
		//		string s = OnlyChar(wq.Options[i]);
		//		if (corrects.Contains(s))
		//		{
		//			ret.Add(i);
		//		}
		//	}
		//	return ret;
		//}

		/// <summary>
		/// 从path中读取XML
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		static public bool ReadAnswerFromXml(string path)
		{
			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load(path);
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e);
				return false;
			}
			try
			{
				QuestionDatabase = new Dictionary<string, DBQuestion>();
				XmlNode root = doc.SelectSingleNode("questions");
				foreach (XmlNode _node in root.ChildNodes)
				{
					XmlElement node = (XmlElement)_node;
					DBQuestion q = new DBQuestion() { Options = new List<KeyValuePair<string, bool>>() };
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
							q.Options.Add(new KeyValuePair<string, bool>(o, c));
						}
					}
					string sig = FuzzyString(q.ToString());
					q.FuzzyHash = sig;
					if (!QuestionDatabase.ContainsKey(sig))
					{
						QuestionDatabase.Add(sig, q);
					}
					else
					{
						Console.Error.WriteLine(string.Format("'{0}' exists.", q.Trunk));
					}
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
		static public bool GenerateExample(string path)
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
			catch (Exception e)
			{
				Console.Error.WriteLine(e);
				return false;
			}
		}

		///// <summary>
		///// 过滤容易产生不同的字符，方便比较
		///// </summary>
		///// <param name="str"></param>
		///// <returns></returns>
		//public string OnlyChar(string str)
		//{
		//	const string EXCLUED = " 　（）()，,。、\\/？?！!";
		//	string ret = (string)str.Clone();
		//	foreach (char c in EXCLUED.ToCharArray())
		//	{
		//		ret = ret.Replace(c.ToString(), "");
		//	}
		//	return ret;
		//}
	}
}
