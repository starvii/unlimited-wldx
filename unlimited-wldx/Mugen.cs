using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace wldx
{
    public class WebElement
    {
        public const string AAA = "123";
        public const string BBB = "123";

        public IList<Question> ExtractQuestionFromHtml(string html)
        {
            return null;
        }
    }

    public class Question
    {
        public string Trunk { get; set; } // 题干
        
        public IList<string> Options { get; set; } // 选项


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

    public class Mugen
    {
        public int[] FindAnswer(Question q)
        {
            return null;
        }

        public bool GenerateSampleDatabase(string path)
        {
            string[] sqls = { @"
CREATE TABLE IF NOT EXISTS `trunk` (
    `id` INTEGER PRIMARY KEY,
    `trunk` TEXT NOT NULL,
    `type` INTEGER
);
", // 0 未定义，1 单选题，2 多选题， 3 判断题
@"
CREATE TABLE IF NOT EXISTS `options` (
    `id` INTEGER PRIMARY KEY,
    `tid` INTEGER NOT NULL,
    `option` TEXT NOT NULL,
    `result` INTEGER
);
",
@"
CREATE UNIQUE INDEX IF NOT EXISTS `idx_trunk`
ON `trunk` (`trunk_digest`);
",
@"
CREATE UNIQUE INDEX IF NOT EXISTS `idx_option`
ON `options` (`option_digest`);
"
                    };
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + path))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        foreach (string sql in sqls)
                        {
                            command.CommandText = sql;
                            command.ExecuteNonQuery();
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }
    }
}
