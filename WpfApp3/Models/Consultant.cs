using WpfApp2.Data;
using WpfApp2.Interfaces;
using WpfApp2.Log;

namespace WpfApp2
{
    public class Consultant : IEmployee
    {
        private readonly DataManager _dataManager = new DataManager();

        /// <summary>
        /// Меню консультанта
        /// </summary>
        public void ShowMenu()
        {
            Console.WriteLine("Вы работаете как Консультант.");
            Console.WriteLine("1 - Вывод информации");
            Console.WriteLine("2 - Изменить номер телефона");
            Console.WriteLine("3 - Выход");

            bool exit = true;
            while (exit)
            {
                Program.Header();
                switch (Console.ReadLine())
                {
                    case "1": _dataManager.GetAll(); break;
                    case "2": Update(); break;
                    case "3": exit = false; break;
                    default:
                        Console.WriteLine("Выберите корректное значение - от 1 до 3");
                        Program.Header();
                        break;
                }
            }
        }

        /// <summary>
        /// Изменить только номер телефона
        /// </summary>        
        public void Update()
        {
            var employees = _dataManager.ReadFromXml();

            Console.Write("У какого клиента следует изменить номер телефона?: ");

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

                    Console.Write("Введите новый номер телефона: ");
                    input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input) == false)
                    {
                        changesString += $"Номер телефона \"{employee.TelephoneNumber}\" изменен на \"{input}\" ";
                        employee.TelephoneNumber = input;
                        input = string.Empty;
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
