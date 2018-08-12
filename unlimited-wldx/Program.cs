using System;
using System.Windows.Forms;

namespace unlimited_wldx
{
	static class Program
	{
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FromMain());
        }

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="text"></param>
		static public void Log(string text)
		{
			// TODO: aaaa
			throw new NotImplementedException();
		}
    }
}
