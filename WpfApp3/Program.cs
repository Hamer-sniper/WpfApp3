using WpfApp3.Interfaces;
using WpfApp3.Models;
using System.Collections.Generic;
using System;

namespace WpfApp3
{
    class Program
    {
        static void Menu()
            // static void Main(string[] args)
        {
            // Авто создание xml
            //Consultant.AutoCreation();

            bool ex = true;
            while (ex)
            {
                Console.WriteLine("1 - Работать как \"Консультант\"");
                Console.WriteLine("2 - Работать как \"Менеджер\"");
                Console.WriteLine("3 - Выйти");
                Header();

                IEmployee employee = new Consultant();

                switch (Console.ReadLine())
                {
                    case "1": 
                        employee = new Consultant();
                        employee.ShowMenu();
                        break;
                    case "2": 
                        employee = new Manager();
                        employee.ShowMenu();
                        break;
                    case "3":                        
                        ex = false;
                        break;
                    default:
                        Console.WriteLine("Выберите корректное значение - от 1 до 3");
                        Header();
                        break;
                }
            }
            Console.WriteLine("Вы вышли из программы");
            Console.ReadKey();
        }

        public static void Header()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nВведите команду: ");
            Console.ResetColor();
        }
    }
}

