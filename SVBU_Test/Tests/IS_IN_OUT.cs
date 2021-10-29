using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace SVBU_Test.Tests
{
    class IS_IN_OUT
    {
        public IS_IN_OUT(string Path, StreamWriter sw)
        {
            XDocument xdoc = XDocument.Load(Path);
            List<Elem> IS_OUT_Algh = new List<Elem>();
            List<Elem> IS_In_Algh = new List<Elem>();
            int CountErr = 0;

            #region Находим все элементы IS_OUT и IS_INP и названия их алгоритмов

            foreach (XElement ALGHs in xdoc.Element("LAES-2").Element("VERSION").Elements("ALGORITHM"))
            {
                foreach (var item in ALGHs.Descendants("IS_OUT"))
                {
                    if (item.Attribute("type") != null)
                    {
                        IS_OUT_Algh.Add(new Elem(item.Attribute("name").Value, ALGHs.Attribute("name").Value, item.Attribute("type").Value));
                    }
                    else
                    {
                        IS_OUT_Algh.Add(new Elem(item.Attribute("name").Value, ALGHs.Attribute("name").Value, "0"));
                    }
                    
                }
                foreach (var item in ALGHs.Descendants("IS_INP"))
                {
                    if (item.Attribute("type") != null)
                    {
                        IS_In_Algh.Add(new Elem(item.Attribute("name").Value, ALGHs.Attribute("name").Value, item.Attribute("type").Value));
                    }
                    else
                    {
                        IS_In_Algh.Add(new Elem(item.Attribute("name").Value, ALGHs.Attribute("name").Value, "0"));
                    }
                        
                }
            }

            #endregion

            #region Алгоритм проверки TEST4a

            sw.WriteLine("Тест №4a: Поиск входных внутренних сигналов (IS_OUT) по выходным (IS_INP)."); sw.WriteLine();

            bool IsExsist = false;
            foreach (var In in IS_In_Algh)
            {
                foreach (var Out in IS_OUT_Algh)
                {
                    if (In.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0].Contains(Out.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0]))
                    {
                        IsExsist = true;
                    }
                }
                if (IsExsist == false && int.Parse(In.Type) != 1)
                {
                    CountErr++;
                    sw.WriteLine("\tОшибка. Отсутствует входной внутренний сигнал (IS_OUT): {0}. Выходной внутренний сигнал (IS_INP) присутствует в алгоритме: {1}", In.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0], In.NameALGH);
                }
                IsExsist = false;
            }

            sw.WriteLine();
            sw.WriteLine($"Количество ошибок {CountErr}.");
            sw.WriteLine();

            #endregion

            #region Алгоритм проверки TEST4b

            CountErr = 0;
            sw.WriteLine("Тест №4b: Поиск выходных внутренних сигналов (IS_INP) по входным (IS_OUT)."); sw.WriteLine();
            foreach (var Out in IS_OUT_Algh)
            {
                foreach (var In in IS_In_Algh)
                {
                    if (In.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0].Contains(Out.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0]))
                    {
                        IsExsist = true;
                    }
                }
                if (IsExsist == false)
                {
                    CountErr++;
                    sw.WriteLine("\tПредупреждение. Отсутствует выходной внутренний сигнал (IS_INP): {0}. Входной внутренний сигнал (IS_OUT) присутствует в алгоритме: {1}", Out.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0], Out.NameALGH);
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
