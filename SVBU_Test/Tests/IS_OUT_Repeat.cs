using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SVBU_Test.Tests
{
    class IS_OUT_Repeat
    {
        public IS_OUT_Repeat(string Path, StreamWriter sw)
        {
            XDocument xdoc = XDocument.Load(Path);
            HashSet<string> IS_OUT = new HashSet<string>();

            sw.WriteLine("Тест №2: Поиск внутренних входных сигналов (IS_OUT) с повторяющимися именами."); sw.WriteLine();

            foreach (XElement Out in xdoc.Element("LAES-2").Element("VERSION").Elements("ALGORITHM").Descendants("IS_OUT"))
            {
                if (!IS_OUT.Contains(Out.Attribute("name").Value))
                {
                    IS_OUT.Add(Out.Attribute("name").Value);
                }
                else
                {
                    sw.WriteLine("\tОшибка. Дублирование внутреннего входного сигнала (IS_OUT): {0}", Out.Attribute("name").Value);
                }
            }
            sw.WriteLine();
        }
    }
}
