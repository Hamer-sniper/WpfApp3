using WpfApp3.Data;
using WpfApp3.Interfaces;
using WpfApp3.Log;
using System;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace WpfApp3.Models
{
    public class Manager : IEmployee
    {
        private readonly BitmapImage manag = new BitmapImage();

        /// <summary>
        /// Вернуть картинку
        /// </summary>
        /// <returns>BitmapImage</returns>
        public BitmapImage GetBitmap()
        {
            manag.BeginInit();
            manag.UriSource = new Uri(Environment.CurrentDirectory + @"\Pictures\Manager.jpg");
            manag.EndInit();
            return manag;
        }

        /// <summary>
        /// Вывод
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAll()
        {
            return DataManager.ReadFromXml();
        }            

        /// <summary>
        /// Изменить всю информацию
        /// </summary>
        public void Update(Employee emp)
        {
            var employees = DataManager.ReadFromXml();

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
                    var pasp = string.IsNullOrWhiteSpace(emp.Pasport) ? "Паспорт не задан" : emp.Pasport;
                    changesString += $"Паспорт \"{employee.Pasport}\" изменен на \"{pasp}\" ";
                    employee.Pasport = pasp;
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
            DataManager.AddToXmlFromList(employees);
        }
    }
}
