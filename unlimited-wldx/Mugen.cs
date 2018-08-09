using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace wldx
{
    class WebElement
    {
        public const string AAA = "123";
        public const string BBB = "123";

        public IList<Question> ExtractQuestionFromHtml(string html)
        {
            return null;
        }
    }
    class Question
    {
        public string Trunk { get; set; } // 题干
        public IList<string> Options { get; set; } // 选项
    }

    class Mugen
    {
        public int[] FindAnswer(Question q)
        {
            return null;
        }

        public void GenerateSampleDatabase(string path)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + path))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE Demo(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = "DROP TABLE Demo";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
