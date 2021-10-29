using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SVBU_Test.Tests
{
    class DB_OUT_Repeat
    {
        public DB_OUT_Repeat(string Path, StreamWriter sw)
        {
            XDocument xdoc = XDocument.Load(Path);
            HashSet<string> DB_OUT = new HashSet<string>();
            int CountErr = 0;

            sw.WriteLine("Тест №3: Поиск входных сигналов БД (DB_OUT) с повторяющимися именами."); sw.WriteLine();

            foreach (XElement Out in xdoc.Element("LAES-2").Element("VERSION").Elements("ALGORITHM").Descendants("DB_OUT"))
            {
                if (!DB_OUT.Contains(Out.Attribute("name").Value))
                {
                    DB_OUT.Add(Out.Attribute("name").Value);
                }
                else
                {
                    CountErr++;
                    sw.WriteLine("\tОшибка. Дублирование входного сигнала БД (DB_OUT): {0}", Out.Attribute("name").Value);
                }
            }
            sw.WriteLine();
            sw.WriteLine($"Количество ошибок {CountErr}.");
            sw.WriteLine();
        }
    }
}
