using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace SVBU_Test.Tests
{
    class Elem
    {
        public Elem(string NameIs, string NameALGH)
        {
            this.NameIs = NameIs;
            this.NameALGH = NameALGH;
        }
        public string NameIs { get; set; }
        public string NameALGH { get; set; }
    }

    class IS_IN_OUT
    {
        public IS_IN_OUT(string Path, StreamWriter sw)
        {
            XDocument xdoc = XDocument.Load(Path);
            List<Elem> IS_OUT = new List<Elem>();
            List<string> IS_IN = new List<string>();
            bool IsError = false;

            #region Находим все элементы IS_OUT и IS_INP

            foreach (XElement ALGHs in xdoc.Element("LAES-2").Element("VERSION").Elements("ALGORITHM"))
            {
                foreach (var item in ALGHs.Descendants("IS_OUT"))
                {
                    IS_OUT.Add(new Elem(item.Attribute("name").Value, ALGHs.Attribute("name").Value));
                }
                foreach (var item in ALGHs.Descendants("IS_INP"))
                {
                    IS_IN.Add(item.Attribute("name").Value.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                }
            }

            #endregion

            sw.WriteLine("Тест №1: Поиск входных внутренних сигналов по выходным."); sw.WriteLine();

            #region Алгоритм проверки

            foreach (var Out in IS_OUT)
            {
                if (!IS_IN.Contains(Out.NameIs.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)[0]))
                {
                    IsError = true;
                    sw.WriteLine("\tОшибка. Отсутствует входной внутренний сигнал: {0}. Выходной внутренний сигнал присутствует в алгоритме: {1}", Out.NameIs, Out.NameALGH);
                }
            }
            if (!IsError)
            {
                sw.WriteLine("\tОшибок нет!");
            }

            #endregion

            sw.WriteLine();
        }
    }
}
