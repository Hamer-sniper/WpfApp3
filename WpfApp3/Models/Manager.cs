using WpfApp2.Data;
using WpfApp2.Interfaces;
using WpfApp2.Log;

namespace WpfApp2
{
    public class Manager : IEmployee
    {
        private readonly DataManager _dataManager = new DataManager();

        /// <summary>
        /// Меню Менеджера
        /// </summary>
        public void ShowMenu()
        {
            Console.WriteLine("Вы работаете как Менеджер.");
            Console.WriteLine("1 - Добавить данные");
            Console.WriteLine("2 - Вывод информации");
            Console.WriteLine("3 - Изменить информацию");
            Console.WriteLine("4 - Просмотреть лог изменений");
            Console.WriteLine("5 - Выход");

            bool exit = true;
            while (exit)
            {
                Program.Header();

                switch (Console.ReadLine())
                {
                    case "1": _dataManager.Create(); break;
                    case "2": _dataManager.GetAll(); break;
                    case "3": Update(); break;
                    case "4": Work_with_log.ShowChanges(); break;
                    case "5": exit = false; break;
                    default:
                        Console.WriteLine("Выберите корректное значение - от 1 до 3");
                        Program.Header();
                        break;
                }
            }
        }

        /// <summary>
        /// Изменить всю информацию
        /// </summary>
        public void Update()
        {
            var employees = _dataManager.ReadFromXml();

            Console.Write("У какого клиента следует изменить информацию?: ");

            var clientNumber = int.Parse(Console.ReadLine());
            var counter = 0;

            Console.WriteLine("Если оставить строку пустой, значение не изменится.");

            foreach (var employee in employees)
            {
                counter++;

                if (counter == clientNumber)
                {
                    employee.ShowInformation();

                    string input = string.Empty;
                    string changesString = string.Empty;

                    Console.Write("Ведите Фамилию: ");
                    input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input) == false)
                    {
                        changesString += $"Фамилия \"{employee.Surname}\" изменена на \"{input}\" ";
                        employee.Surname = input;
                        input = string.Empty;
                    }
                    Console.Write("Ведите Имя: ");
                    input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input) == false)
                    {
                        changesString += $"Имя \"{employee.Name}\" изменено на \"{input}\" ";
                        employee.Name = input;
                        input = string.Empty;
                    }

                    Console.Write("Ведите Отчество: ");
                    input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input) == false)
                    {
                        changesString += $"Отчество \"{employee.MiddleName}\" изменено на \"{input}\" ";
                        employee.MiddleName = input;
                        input = string.Empty;
                    }

                    Console.Write("Ведите Номер телефона: ");
                    input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input) == false)
                    {
                        changesString += $"Номер телефона \"{employee.TelephoneNumber}\" изменен на \"{input}\" ";
                        employee.TelephoneNumber = input;
                        input = string.Empty;
                    }

                    Console.Write("Ведите Паспорт: ");
                    input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input) == false)
                    {
                        changesString += $"Паспорт \"{employee.Pasport}\" изменен на \"{input}\" ";
                        employee.Pasport = input;
                        input = string.Empty;
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
            }
            _dataManager.AddToXmlFromList(employees);


            //     DateTimeChange = DateTime.Now.ToString(),
            //         DataChanged = "All",
            //         TypeOfChanges = "Creation",
            //         Changer = "Manager"
            //     };
            // Запись в лог
            //      var changesLog = Work_with_log.ReadFromLogXml();
            //     changesLog.Add(employee);
            //     Work_with_log.AddToLogXmlFromList(changesLog);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Информация изменена!");
            Console.ResetColor();
        }
    }
}
