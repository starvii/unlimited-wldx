﻿using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace wldx
{
    public class WebElement
    {
        public const string trunk = "TMTitle";
        public const string option = "TMOption";

        static public IList<Question> ExtractQuestionFromHtml(HtmlDocument html)
        {
            HtmlElementCollection c = html.GetElementsByTagName("div");
            if (0 == c.Count)
            {
                return null;
            }
            IList<HtmlElement> nodeList = new List<HtmlElement>();
            foreach (HtmlElement h in c)
            {
                //Console.WriteLine(className);
                if (trunk.Equals(h.GetAttribute("className")))
                {
                    nodeList.Add(h.Parent);
                }
            }

            IList<Question> ret = new List<Question>();
            foreach (HtmlElement h in nodeList)
            {
                HtmlElement node = h.Parent;
                Question q = ExtractFromHtmlNode(node);
                ret.Add(q);
                Console.WriteLine(q.ToString());
            }
            Console.WriteLine(nodeList.Count);
            return ret;
        }

        static private Question ExtractFromHtmlNode(HtmlElement node)
        {
            Question q = new Question() { Options = new List<string>(), Element = node };
            foreach (HtmlElement e in node.GetElementsByTagName("div"))
            {
                if (trunk.Equals(e.GetAttribute("className")))
                {
                    q.Trunk = e.InnerText.Trim();
                    break;
                }
            }
            foreach (HtmlElement e in node.GetElementsByTagName("span"))
            {
                if (option.Equals(e.GetAttribute("className")))
                {
                    q.Options.Add(e.InnerText.Trim());
                }
            }
            return q;
        }
    }

    public class Question
    {
        public string Trunk { get; set; } // 题干
        
        public IList<string> Options { get; set; } // 选项

        public HtmlElement Element { get; set; }

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

        public IDictionary<string, Question> AnswerFromXml(string path)
        {
            return null;
        }

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
                Console.Error.Write(e);
                return false;
            }
        }
    }
}
