using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;


namespace Practic6;

    public class Class1
    {

        public static ConsoleKeyInfo key;
        private static bool trigger = true;
        public static string Deserealizing = null;
        public static Figure_changes xmlType;
        public static List<Figure_changes> FigureList = new List<Figure_changes>();
        public static XmlSerializer xml = new XmlSerializer(typeof(List<Figure_changes>));

        public static Figure_changes figure1 = new Figure_changes();
        public static Figure_changes figure2 = new Figure_changes();
        public static int positionX = 3;
        public static int positionY = 0;
        public static void PrintSecond()
        {
            Console.Clear();
            string text = File.ReadAllText(Program.FileName);

            Console.WriteLine("нажмите F1, чтобы сохранить файл в формате json, txt или xml. Нажмите ESC чтобы закрыть программу");
            Console.WriteLine("Нажмите Backspace, чтобы выйти в меню выбор.");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------");

            
            if (Deserealizing == "json")
            {

                List<Figure_changes> deserialization = JsonConvert.DeserializeObject<List<Figure_changes>>(text);
                foreach (var item in deserialization)
                {
                    Console.WriteLine(item.Name);
                    Console.WriteLine(item.weight);
                    Console.WriteLine(item.length);
                }
                FigureList = JsonConvert.DeserializeObject<List<Figure_changes>>(File.ReadAllText(Program.FileName));
            }
            if (Deserealizing == "txt")
            {
                Console.WriteLine(File.ReadAllText(Program.FileName));

                figure1.Name = File.ReadLines(Program.FileName).Skip(0).Take(1).First();
                figure1.length = (File.ReadLines(Program.FileName).Skip(1).Take(1).First());
                figure1.weight = (File.ReadLines(Program.FileName).Skip(2).Take(1).First());

                figure2.Name = File.ReadLines(Program.FileName).Skip(3).Take(1).First();
                figure2.length = (File.ReadLines(Program.FileName).Skip(4).Take(1).First());
                figure2.weight = (File.ReadLines(Program.FileName).Skip(5).Take(1).First());

                FigureList.Add(figure1);
                FigureList.Add(figure2);
            }
            if (Deserealizing == "xml")
            {
                

                using (FileStream cw = new FileStream(Program.FileName, FileMode.Open))
                {
                    FigureList = (List<Figure_changes>)xml.Deserialize(cw);
                }
                foreach (var item in FigureList)
                {
                    Console.WriteLine(item.Name);
                    Console.WriteLine(item.weight);
                    Console.WriteLine(item.length);
                }
            }

            
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.F1)
                {
                    Console.WriteLine("Введите путь файла(с расширением). Kуда вы хотите сохранить текст?");
                    Console.WriteLine("------------------------------------------------------------------");
                    string way = Console.ReadLine();
                    Console.Clear();
                    ArrowMenu();
                    ConvertToJson(way);
                    trigger = false;
                }
                if (key.Key == ConsoleKey.Backspace)
                {
                    Console.Clear();
                    return;
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            } while (trigger = true);
            Console.Clear();
            Program.PrintFirst();
        }

        public static void ConvertToJson(string FilePath)
        {
            string secpath = FilePath;
            secpath = secpath.Substring(secpath.Length - 2, 2);
            if (secpath == "on")
            {
                string json = JsonConvert.SerializeObject(FigureList);
                File.WriteAllText(FilePath, json);
            }
            if (secpath == "ml")
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Figure_changes>));
                using (FileStream cw = new FileStream("test.xml", FileMode.OpenOrCreate))
                {
                    xml.Serialize(cw, FigureList);
                }
                string test = File.ReadAllText("test.xml");
                File.WriteAllText(FilePath, test);
                File.Delete("test.xml");
            }
            if (secpath == "xt")
            {
                string full = null;
                foreach (Figure_changes item in FigureList)
                {
                    full += $"{item.Name}\n{item.length}\n{item.weight}\n";
                }
                File.WriteAllText(FilePath, full);
            }
        }
        public static void ArrowMenu()
        {
            int reg = 0;
            while (true)
            {
                reg = 0;
                Console.SetCursorPosition(positionY, positionX);
                ConsoleKeyInfo key = Console.ReadKey();
                Console.SetCursorPosition(14, positionX);
                Console.Write("  ");

                if (key.Key == ConsoleKey.UpArrow)
                {
                    positionX = positionX - 1;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    positionX = positionX + 1;
                }
                if (positionX == 0)
                {
                    positionX = positionX + 1;
                }
                if (positionX >= 10)
                {
                    positionX = 9;
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                if (positionX == 3)
                {
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.SetCursorPosition(20, positionX);
                        figure1.Name = Console.ReadLine();

                    }
                }
                if (positionX == 4)
                {
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.SetCursorPosition(20, positionX);
                        figure1.weight = (Console.ReadLine());

                    }
                }
                if (positionX == 5)
                {
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.SetCursorPosition(20, positionX);
                        figure1.length = (Console.ReadLine());

                    }
                }
                if (positionX == 6)
                {
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.SetCursorPosition(20, positionX);
                        figure2.Name = Console.ReadLine();

                    }
                }
                if (positionX == 7)
                {
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.SetCursorPosition(20, positionX);
                        figure2.weight = (Console.ReadLine());

                    }
                }
                if (positionX == 8)
                {
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.SetCursorPosition(20, positionX);
                        figure2.length = (Console.ReadLine());

                    }
                }

                if (key.Key == ConsoleKey.Escape)
                {
                    FigureList.Add(figure1);
                    FigureList.Add(figure2);
                    break;
                }
            }
        }
    }

