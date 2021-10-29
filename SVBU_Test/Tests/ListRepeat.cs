using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SVBU_Test.Tests
{
    class ListRepeat
    {
        public ListRepeat(string Path, StreamWriter sw)
        {
            XDocument xdoc = XDocument.Load(Path);
            HashSet<string> ALGHs = new HashSet<string>();
            int CountErr = 0;

            sw.WriteLine("Тест №1: Поиск алгоритмов (ALGORITHM) с повторяющимися именами."); sw.WriteLine();

            foreach (XElement ALGH in xdoc.Element("LAES-2").Element("VERSION").Elements("ALGORITHM"))
            {
                if (!ALGHs.Contains(ALGH.Attribute("name").Value))
                {
                    ALGHs.Add(ALGH.Attribute("name").Value);
                }
                else
                {
                    CountErr++;
                    sw.WriteLine("\tОшибка. Дублирование алгоритма {0}", ALGH.Attribute("name").Value);
                }
            }
            sw.WriteLine();
            sw.WriteLine($"Количество ошибок {CountErr}.");
            sw.WriteLine();
        }
    }
}
