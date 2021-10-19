using SVBU_Test.Tests;
using System;
using System.IO;

namespace SVBU_Test
{
    class Program
    {
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
                IS_IN_OUT Test1 = new IS_IN_OUT(PathToXML);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл {0} не был найден", PathToXML);
            }

            Console.ReadKey(true);
        }
    }
}
