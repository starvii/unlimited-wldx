using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace wldx
{
	/// <summary>
	/// 选项类
	/// 包含选项文字与正确与否的类
	/// </summary>
	public class Option
	{
		public string OptionText { get; set; }
		public bool Correct { get; set; }

		public override string ToString()
		{
			return OptionText + ", " + Correct;
		}
	}

	/// <summary>
	/// 题目接口
	/// 包含主干与比较方法
	/// </summary>
	public interface IQuestion: IComparable<IQuestion>
	{
		string Trunk { get; set; } // 题干
		string ToSort();
	}

	/// <summary>
	/// 题目接口
	/// 包含选项
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IQuestion<T>: IQuestion
	{
		IList<T> Options { get; set; } // 选项
	}

	/// <summary>
	/// 题目基类
	/// 包含比较方法
	/// </summary>
	public abstract class BaseQuestion : IQuestion
	{
		public string Trunk { get; set; }
		public abstract string ToSort();

		public int CompareTo(IQuestion other)
		{
			string a = ToSort();
			string b = other.ToSort();
			return Distance.Levenshtein(a, b);
		}
	}

	/// <summary>
	/// 页面题目类
	/// 包含文本选项和页面元素
	/// </summary>
	public class WebQuestion: BaseQuestion, IQuestion<string>
	{
		public HtmlElement Element { get; set; }
		public IList<string> Options { get; set; }

		public override string ToSort()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Trunk);
			sb.Append("|");
			List<string> t = new List<string>();
			foreach (string o in Options)
			{
				t.Add(o);
			}
			t.Sort();
			foreach (string o in t)
			{
				sb.Append(o);
				sb.Append("|");
			}
			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Trunk);
			sb.Append("\r\n");
			foreach (string opt in Options)
			{
				sb.Append(opt);
				sb.Append("\r\n");
			}
			return sb.ToString();
		}
	}

	/// <summary>
	/// 内存题目类
	/// 包含选项类
	/// </summary>
	public class DBQuestion : BaseQuestion, IQuestion<Option>
	{
		public IList<Option> Options { get; set; }

		public override string ToSort()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Trunk);
			sb.Append("|");
			List<string> t = new List<string>();
			foreach (Option o in Options)
			{
				t.Add(o.OptionText);
			}
			t.Sort();
			foreach (string o in t)
			{
				sb.Append(o);
				sb.Append("|");
			}
			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Trunk);
			sb.Append("\r\n");
			foreach (Option opt in Options)
			{
				sb.Append(opt);
				sb.Append("\r\n");
			}
			return sb.ToString();
		}
	}
}
