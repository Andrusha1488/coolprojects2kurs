using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Security.Principal;

namespace Practic6
{
    public class Program
    {
        public static string FileName;
        static void Main(string[] args)
        {
            PrintFirst();
        }
        public static void PrintFirst()
        {
            do
            {
                string top = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Введите путь файла, который вы хотите октрыть");
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("----------------------------------------------   ");
                string path = Console.ReadLine();
                Console.SetCursorPosition(0, 2);
                Console.WriteLine("                                                 ");
                FileName = path;
                string temppath = path;
                temppath = temppath.Substring(temppath.Length - 2, 2);
                Class1.Deserealizing = temppath;
                if (System.IO.File.Exists(Convert.ToString(Path.GetFullPath(path))) == false)
                {
                    Console.SetCursorPosition(13, 1);
                    Console.WriteLine("Файл не найден");
                }
                if (System.IO.File.Exists(Convert.ToString(Path.GetFullPath(path))) == true)
                {
                    Class1.PrintSecond();
                }
            } while (true);
        }
    }



}