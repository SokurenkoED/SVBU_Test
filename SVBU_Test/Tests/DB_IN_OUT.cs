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
            List<Elem> DB_OUT_Algh = new List<Elem>();
            List<Elem> DB_In_Algh = new List<Elem>();
            int CountErr = 0;

            #region Находим все элементы DB_OUT и DB_INP и названия их алгоритмов

            foreach (XElement ALGHs in xdoc.Element("LAES-2").Element("VERSION").Elements("ALGORITHM"))
            {
                foreach (var item in ALGHs.Descendants("DB_OUT"))
                {
                    if (item.Attribute("type") != null)
                    {
                        DB_OUT_Algh.Add(new Elem(item.Attribute("name").Value, ALGHs.Attribute("name").Value, item.Attribute("type").Value));
                    }
                    else
                    {
                        DB_OUT_Algh.Add(new Elem(item.Attribute("name").Value, ALGHs.Attribute("name").Value, "0"));
                    }

                }
                foreach (var item in ALGHs.Descendants("DB_INP"))
                {
                    if (item.Attribute("type") != null)
                    {
                        DB_In_Algh.Add(new Elem(item.Attribute("name").Value, ALGHs.Attribute("name").Value, item.Attribute("type").Value));
                    }
                    else
                    {
                        DB_In_Algh.Add(new Elem(item.Attribute("name").Value, ALGHs.Attribute("name").Value, "0"));
                    }

                }
            }

            #endregion

            #region Алгоритм проверки TEST5a

            sw.WriteLine("Тест №5a: Поиск входных сигналов БД (DB_OUT) по выходным (DB_INP)."); sw.WriteLine();

            bool IsExsist = false;
            foreach (var In in DB_In_Algh)
            {
                foreach (var Out in DB_OUT_Algh)
                {
                    if (In.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0].Contains(Out.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0]))
                    {
                        IsExsist = true;
                    }
                }
                if (IsExsist == false && int.Parse(In.Type) != 1)
                {
                    CountErr++;
                    sw.WriteLine("\tОшибка. Отсутствует входной сигнал БД (DB_OUT): {0}. Выходной сигнал БД (DB_INP) присутствует в алгоритме: {1}", In.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0], In.NameALGH);
                }
                IsExsist = false;
            }

            sw.WriteLine();
            sw.WriteLine($"Количество ошибок {CountErr}.");
            sw.WriteLine();

            #endregion

            #region Алгоритм проверки TEST5b

            CountErr = 0;
            sw.WriteLine("Тест №5b: Поиск входных сигналов БД (DB_INP) по входным (DB_OUT)."); sw.WriteLine();
            foreach (var Out in DB_OUT_Algh)
            {
                foreach (var In in DB_In_Algh)
                {
                    if (In.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0].Contains(Out.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0]))
                    {
                        IsExsist = true;
                    }
                }
                if (IsExsist == false)
                {
                    CountErr++;
                    sw.WriteLine("\tПредупреждение. Отсутствует выходной сигнал БД (DB_INP): {0}. Входной сигнал БД (DB_OUT) присутствует в алгоритме: {1}", Out.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0], Out.NameALGH);
                }
                IsExsist = false;
            }

            sw.WriteLine();
            sw.WriteLine($"Количество предупреждений {CountErr}.");
            sw.WriteLine();

            #endregion
        }
    }
}
