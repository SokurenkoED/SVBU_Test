using SVBU_Test.Tests;
using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace SVBU_Test
{
    class Program
    {
        private static void GetInfo(string PathToXML, StreamWriter sw)
        {
            XDocument xdoc = XDocument.Load(PathToXML);
            foreach (XElement ALGHs in xdoc.Element("LAES-2").Elements("VERSION")) 
            {
                DateTime thisDay = DateTime.Now;
                sw.WriteLine("Время тестирования - {0} \t Версия - {1} \t Дата сдачи файла - {2} \t Разработчик - {3}", thisDay.ToString("yyyy/MM/dd HH:mm:ss"), ALGHs.Attribute("value").Value,
                    ALGHs.Attribute("data").Value, ALGHs.Attribute("developer").Value); sw.WriteLine();

            }
        }

        static void Main(string[] args)
        {
            string PathToXML = "!List_SVBU.xml"; // Путь до файла с данными

            #region Создаем дирректорию

            DirectoryInfo dirInfo = new DirectoryInfo("Report");
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            #endregion

            try
            {
                using (StreamWriter sw = new StreamWriter("report/report.txt", false, Encoding.Default))
                {
                    GetInfo(PathToXML, sw);

                    IS_IN_OUT Test4 = new IS_IN_OUT(PathToXML, sw);

                    DB_IN_OUT Test5 = new DB_IN_OUT(PathToXML, sw);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл {0} не был найден", PathToXML);
            }

            Console.ReadKey(true);
        }
    }
}
