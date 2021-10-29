using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SVBU_Test.Tests
{
    class FindElemWithType
    {
        public FindElemWithType(string Path, StreamWriter sw)
        {

            XDocument xdoc = XDocument.Load(Path);

            #region Датчики

            List<Elem> ElemWithTypeOne = new List<Elem>();

            
            #region Поиск сигналов с датчиков

            foreach (XElement DB_INP in xdoc.Element("LAES-2").Element("VERSION").Elements("ALGORITHM").Descendants("DB_INP"))
            {
                if (DB_INP.Attribute("type") != null && DB_INP.Attribute("type").Value == "1")
                {
                    ElemWithTypeOne.Add(new Elem(DB_INP.Attribute("name").Value, " ", DB_INP.Attribute("type").Value));
                }
            }
            foreach (XElement DB_INP in xdoc.Element("LAES-2").Element("VERSION").Elements("ALGORITHM").Descendants("IS_INP"))
            {
                if (DB_INP.Attribute("type") != null && DB_INP.Attribute("type").Value == "1")
                {
                    ElemWithTypeOne.Add(new Elem(DB_INP.Attribute("name").Value, " ", DB_INP.Attribute("type").Value));
                }
            }

            #endregion

            #region Соритровка массива

            ElemWithTypeOne.Sort((p1, p2) =>
            {
                return p1.NameIs.CompareTo(p2.NameIs);
            });

            #endregion

            #region Запись датчиков

            sw.WriteLine($"Сигналы с датчиков ({ElemWithTypeOne.Count}):"); sw.WriteLine();
            foreach (var Sens in ElemWithTypeOne)
            {
                sw.WriteLine("\t"+Sens.NameIs);
            }
            sw.WriteLine();

            #endregion

            #endregion

            #region Сигналы с List_IC.txt

            List<Elem> ElemWithTypeTwo = new List<Elem>();

            #region Поиск сигналов с List_IC.txt

            foreach (XElement DB_INP in xdoc.Element("LAES-2").Element("VERSION").Elements("ALGORITHM").Descendants("DB_INP"))
            {
                if ((DB_INP.Attribute("type") != null && DB_INP.Attribute("type").Value == "2") || (!DB_INP.Attribute("name").Value.Contains("#") && DB_INP.Attribute("type") == null))
                {
                    ElemWithTypeTwo.Add(new Elem(DB_INP.Attribute("name").Value, " ", "2"));
                }
            }

            #endregion

            #region Соритровка массива

            ElemWithTypeTwo.Sort((p1, p2) =>
            {
                return p1.NameIs.CompareTo(p2.NameIs);
            });

            #endregion

            #region Запись сигналов с List_IC.txt

            sw.WriteLine($"Сигналы с List_IC ({ElemWithTypeTwo.Count}):"); sw.WriteLine();
            foreach (var Elem in ElemWithTypeTwo)
            {
                sw.WriteLine("\t" + Elem.NameIs);
            }

            #endregion

            #endregion

            sw.WriteLine();
        }
    }
}
