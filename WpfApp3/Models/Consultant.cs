using WpfApp3.Data;
using WpfApp3.Interfaces;
using WpfApp3.Log;
using System;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace WpfApp3.Models
{
    public class Consultant : IEmployee
    {
        private readonly BitmapImage consul = new BitmapImage();

        /// <summary>
        /// Вернуть картинку
        /// </summary>
        /// <returns>BitmapImage</returns>
        public BitmapImage GetBitmap()
        {
            consul.BeginInit();
            consul.UriSource = new Uri(Environment.CurrentDirectory + @"\Pictures\Consultant.jpg");
            consul.EndInit();
            return consul;
        }           

        /// <summary>
        /// Вывод
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAll()
        {
            // Скрыть данные паспорта для консультанта
            var employees = DataManager.ReadFromXml();
            foreach (var pasp in employees)
            {
                pasp.Pasport = "******";
            }
            return employees;
        }

        /// <summary>
        /// Изменить только номер телефона
        /// </summary>        
        public void Update(Employee emp)
        {
            var employees = DataManager.ReadFromXml();

            foreach (var employee in employees)
            {
                if (employee.Id == emp.Id)
                {                    
                    string changesString = string.Empty;                  

                    if (emp.TelephoneNumber != employee.TelephoneNumber)
                    {
                        changesString += $"Номер телефона \"{employee.TelephoneNumber}\" изменен на \"{emp.TelephoneNumber}\" ";
                        employee.TelephoneNumber = emp.TelephoneNumber;                      
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
            DataManager.AddToXmlFromList(employees);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Номер телефона изменен!");
            Console.ResetColor();
        }
    }
}
