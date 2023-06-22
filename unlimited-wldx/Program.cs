using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace wldx
{
	static class Program
	{
		static private StreamWriter f = null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
			f = new StreamWriter(new FileStream("log.txt", FileMode.Append));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FromMain());
			f.Close();
        }

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="text"></param>
		static public void Log(string text)
		{
			f.WriteLine(text);
			f.Flush();
		}
    }
}
