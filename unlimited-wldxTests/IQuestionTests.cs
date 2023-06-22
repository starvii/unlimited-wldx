using Microsoft.VisualStudio.TestTools.UnitTesting;
using wldx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wldx.Tests
{
	[TestClass()]
	public class IQuestionTests
	{
		[TestMethod()]
		public void ToSortTest()
		{
			WebQuestion wq = new WebQuestion()
			{
				Trunk = "服务请求管理流程与（__）流程等存在输入输出关系。",
				Options = new List<string>() { "需求管理、两票管理", "两票管理、配置与方式管理", "需求管理、缺陷管理", "服务级别管理、两票管理" }
			};

			DBQuestion dq = new DBQuestion()
			{
				Trunk = "服务请求管理流程与（C）流程等存在输入输出关系。",
				Options = new List<Option>()
			};
			dq.Options.Add(new Option() { OptionText = "需求管理、两票管理", Correct = false });
			dq.Options.Add(new Option() { OptionText = "两票管理、配置与方式管理", Correct = true });
			dq.Options.Add(new Option() { OptionText = "需求管理、缺陷管理", Correct = false });
			dq.Options.Add(new Option() { OptionText = "服务级别管理、两票管理", Correct = false });

			string a = wq.ToSort();
			string b = dq.ToSort();

			Console.WriteLine(a);
			Console.WriteLine(b);

			Console.WriteLine(Distance.Levenshtein(a, b));

			Assert.AreEqual(a, b);
		}
	}
}