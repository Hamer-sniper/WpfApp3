using WpfApp3.Data;
using WpfApp3.Interfaces;
using WpfApp3.Log;
using System;
using System.Windows.Media.Imaging;
using WpfApp3.Models;
using System.Collections.Generic;

namespace WpfApp3.Models
{
    public class Manager : IEmployee
    {
        private readonly DataManager _dataManager = new DataManager();
        private readonly BitmapImage manag = new BitmapImage();

        /// <summary>
        /// Вернуть картинку
        /// </summary>
        /// <returns>BitmapImage</returns>
        public BitmapImage GetBitmap()
        {
            manag.BeginInit();
            manag.UriSource = new Uri(Environment.CurrentDirectory + @"\Manager.jpg");
            manag.EndInit();
            return manag;
        }

        /// <summary>
        /// Вывод
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAll()
        {
            return _dataManager.ReadFromXml();
        }

        public void Create(string usurname, string uname, string umiddleName, string utelephoneNumber, string upasport)
        {
            _dataManager.Create(usurname, uname, umiddleName, utelephoneNumber, upasport);
        }

        /// <summary>
        /// Меню Менеджера
        /// </summary>
        public void ShowMenu()
        {
            _dataManager.ReadFromXml();

            // Консольный вывод
            //public void ShowMenu()
            //Console.WriteLine("Вы работаете как Менеджер.");
            //Console.WriteLine("1 - Добавить данные");
            //Console.WriteLine("2 - Вывод информации");
            //Console.WriteLine("3 - Изменить информацию");
            //Console.WriteLine("4 - Просмотреть лог изменений");
            //Console.WriteLine("5 - Выход");

            //bool exit = true;
            //while (exit)
            //{
            //    Program.Header();

            //    switch (Console.ReadLine())
            //    {
            //        case "1": _dataManager.Create(); break;
            //        case "2": _dataManager.GetAll(); break;
            //        case "3": Update(); break;
            //        case "4": Work_with_log.ShowChanges(); break;
            //        case "5": exit = false; break;
            //        default:
            //            Console.WriteLine("Выберите корректное значение - от 1 до 3");
            //            Program.Header();
            //            break;
            //    }
            //}
        }

        /// <summary>
        /// Удалить данные
        /// </summary>
        /// <param name="emp"></param>
        public void Delete(Employee emp)
        {
            var employees = _dataManager.ReadFromXml();
            var tempEmployee = emp;

            foreach (var employee in employees)
            {
                if (employee.Id == emp.Id)
                {
                    tempEmployee = employee;
                    employees.Remove(tempEmployee);
                    break;
                }
            }                
            _dataManager.AddToXmlFromList(employees);
        }

        /// <summary>
        /// Изменить всю информацию
        /// </summary>
        public void Update(Employee emp)
        {
            var employees = _dataManager.ReadFromXml();

            //Console.Write("У какого клиента следует изменить информацию?: ");

            //var clientNumber = int.Parse(Console.ReadLine());
            //var counter = 0;           

            //Console.WriteLine("Если оставить строку пустой, значение не изменится.");

            foreach (var employee in employees)
            {
                if (employee.Id != emp.Id)
                    continue;

                string changesString = string.Empty;

                if (emp.Surname != employee.Surname)
                {
                    changesString += $"Фамилия \"{employee.Surname}\" изменена на \"{emp.Surname}\" ";
                    employee.Surname = emp.Surname;
                }

                if (emp.Name != employee.Name)
                {
                    changesString += $"Имя \"{employee.Name}\" изменено на \"{emp.Name}\" ";
                    employee.Name = emp.Name;
                }

                if (emp.MiddleName != employee.MiddleName)
                {
                    changesString += $"Отчество \"{employee.MiddleName}\" изменено на \"{emp.MiddleName}\" ";
                    employee.MiddleName = emp.MiddleName;                    
                }

                if (emp.TelephoneNumber != employee.TelephoneNumber)
                {
                    changesString += $"Номер телефона \"{employee.TelephoneNumber}\" изменен на \"{emp.TelephoneNumber}\" ";
                    employee.TelephoneNumber = emp.TelephoneNumber;
                }

                if (emp.Pasport != employee.Pasport)
                {
                    changesString += $"Паспорт \"{employee.Pasport}\" изменен на \"{emp.Pasport}\" ";
                    employee.Pasport = emp.Pasport;
                }

                if (string.IsNullOrEmpty(changesString) == false)
                {
                    employee.DateTimeChange = DateTime.Now.ToString();
                    employee.DataChanged = changesString;
                    employee.TypeOfChanges = "Change";
                    employee.Changer = "Manager";

                    var changesLog = Work_with_log.ReadFromLogXml();
                    changesLog.Add(employee);
                    Work_with_log.AddToLogXmlFromList(changesLog);
                }
                break;
            }
            _dataManager.AddToXmlFromList(employees);
        }
    }
}
