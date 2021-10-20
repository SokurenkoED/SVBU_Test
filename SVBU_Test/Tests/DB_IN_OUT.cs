using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SVBU_Test.Tests
{
    class DB_IN_OUT
    {
        public DB_IN_OUT(string Path, StreamWriter sw)
        {
            XDocument xdoc = XDocument.Load(Path);
            List<Elem> DB_OUT = new List<Elem>();
            List<string> DB_IN = new List<string>();
            bool IsError = false;

            #region Находим все элементы DB_OUT и DB_INP

            foreach (XElement ALGHs in xdoc.Element("LAES-2").Element("VERSION").Elements("ALGORITHM"))
            {
                foreach (var item in ALGHs.Descendants("DB_OUT"))
                {
                    DB_OUT.Add(new Elem(item.Attribute("name").Value, ALGHs.Attribute("name").Value));
                }
                foreach (var item in ALGHs.Descendants("DB_INP"))
                {
                    DB_IN.Add(item.Attribute("name").Value.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                }
            }

            #endregion

            sw.WriteLine("Тест №2: Поиск входных сигналов БД по выходным."); sw.WriteLine();

            #region Алгоритм проверки

            foreach (var Out in DB_OUT)
            {
                if (!DB_IN.Contains(Out.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0]))
                {
                    IsError = true;
                    sw.WriteLine("\tОшибка. Отсутствует входной сигнал БД: {0}. Выходной сигнал БД присутствует в алгоритме: {1}", Out.NameIs, Out.NameALGH);
                }
            }
            if (!IsError)
            {
                sw.WriteLine("\tОшибок нет!");
            }
            sw.WriteLine();

            #endregion

            sw.WriteLine();
        }
    }
}
