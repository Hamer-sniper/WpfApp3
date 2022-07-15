using WpfApp3.Data;
using WpfApp3.Interfaces;
using WpfApp3.Log;
using System;
using System.Windows.Media.Imaging;
using WpfApp3.Models;
using System.Collections.Generic;

namespace WpfApp3.Models
{
    public class Consultant : IEmployee
    {
        private readonly DataManager _dataManager = new DataManager();
        private readonly BitmapImage consul = new BitmapImage();

        /// <summary>
        /// Вернуть картинку
        /// </summary>
        /// <returns>BitmapImage</returns>
        public BitmapImage GetBitmap()
        {
            consul.BeginInit();
            consul.UriSource = new Uri(Environment.CurrentDirectory + @"\Consultant.jpg");
            consul.EndInit();
            return consul;
        }

        /// <summary>
        /// Удалить данные
        /// </summary>
        /// <param name="emp"></param>
        public void Delete(Employee emp)
        {

        }

        public void Create(string usurname, string uname, string umiddleName, string utelephoneNumber, string upasport)
        {
            //_dataManager.Create(usurname, uname, umiddleName, utelephoneNumber, upasport);
        }

        /// <summary>
        /// Вывод
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAll()
        {
            // Скрыть данные паспорта для консультанта
            var employees = _dataManager.ReadFromXml();
            foreach (var pasp in employees)
            {
                pasp.Pasport = "******";
            }
            return employees;
        }        

        /// <summary>
        /// Меню консультанта
        /// </summary>
        public void ShowMenu()
        {
            _dataManager.ReadFromXml();

            // Для консольного
            //Console.WriteLine("Вы работаете как Консультант.");
            //Console.WriteLine("1 - Вывод информации");
            //Console.WriteLine("2 - Изменить номер телефона");
            //Console.WriteLine("3 - Выход");

            //bool exit = true;
            //while (exit)
            //{
            //    Program.Header();
            //    switch (Console.ReadLine())
            //    {
            //        case "1": _dataManager.GetAll(); break;
            //        case "2": Update(); break;
            //        case "3": exit = false; break;
            //        default:
            //            Console.WriteLine("Выберите корректное значение - от 1 до 3");
            //            Program.Header();
            //            break;
            //    }
            //}
        }

        /// <summary>
        /// Изменить только номер телефона
        /// </summary>        
        public void Update(Employee emp)
        {
            var employees = _dataManager.ReadFromXml();

            //Console.Write("У какого клиента следует изменить номер телефона?: ");

            //var clientNumber = int.Parse(Console.ReadLine());
            var clientNumber = emp.Id;
            //var counter = 0;            

            //Console.WriteLine("Если оставить строку пустой, значение не изменится.");

            foreach (var employee in employees)
            {
                //counter++;                

                if (employee.Id == clientNumber)
                {
                    //employee.ShowInformation();

                    //input = string.Empty;
                    string changesString = string.Empty;

                    //Console.Write("Введите новый номер телефона: ");
                    //input = Console.ReadLine();
                    var input = emp.TelephoneNumber;

                    if (string.IsNullOrEmpty(input) == false)
                    {
                        changesString += $"Номер телефона \"{employee.TelephoneNumber}\" изменен на \"{input}\" ";
                        employee.TelephoneNumber = input;
                        //input = string.Empty;
                    }

                    if (string.IsNullOrEmpty(changesString) == false)
                    {
                        employee.DateTimeChange = DateTime.Now.ToString();
                        employee.DataChanged = changesString;
                        employee.TypeOfChanges = "Change";
                        employee.Changer = "Consultant";

                        var changesLog = Work_with_log.ReadFromLogXml();
                        changesLog.Add(employee);
                        Work_with_log.AddToLogXmlFromList(changesLog);
                    }
                    break;
                }
            }
            _dataManager.AddToXmlFromList(employees);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Номер телефона изменен!");
            Console.ResetColor();
        }
    }
}
