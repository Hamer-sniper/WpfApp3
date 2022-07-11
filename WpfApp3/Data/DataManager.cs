using System.Xml;
using System.Xml.Linq;
using WpfApp2.Models;
using WpfApp2.Log;

namespace WpfApp2
{
    public class DataManager
    {
        private readonly string _dataFilePath = Environment.CurrentDirectory + @"\Databook.xml";

        /// <summary>
        /// Вывести всю информацию на экран
        /// </summary>
        public void GetAll()
        {
            var employees = ReadFromXml();
            var counter = 0;

            foreach (var employee in employees)
            {
                counter++;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("_____________________________");
                Console.WriteLine("№" + counter);
                Console.ResetColor();

                employee.ShowInformation();
            }
        }

        public void Create()
        {
            Console.Write("Ведите Фамилию: ");
            string usurname = Console.ReadLine();
            Console.Write("Ведите Имя: ");
            string uname = Console.ReadLine();
            Console.Write("Ведите Отчество: ");
            string umiddleName = Console.ReadLine();
            Console.Write("Ведите Номер телефона: ");
            string utelephoneNumber = Console.ReadLine();
            Console.Write("Ведите паспорт: ");
            string upasport = Console.ReadLine();

            var employee = new Employee()
            {
                Surname = usurname,
                Name = uname,
                MiddleName = umiddleName,

                //Если в переменной utelephoneNumber будет Null то запишутся 1111111
                TelephoneNumber = utelephoneNumber ?? "1111111",
                Pasport = upasport ?? "Паспорт не задан",

                DateTimeChange = DateTime.Now.ToString(),
                DataChanged = "All",
                TypeOfChanges = "Creation",
                Changer = "Manager"
            };
            // Запись в лог
            var changesLog = Work_with_log.ReadFromLogXml();
            changesLog.Add(employee);
            Work_with_log.AddToLogXmlFromList(changesLog);

            // Запись данных
            var employees = ReadFromXml();
            employees.Add(employee);
            AddToXmlFromList(employees);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Клиент добавлен!");
            Console.ResetColor();
        }

        /// <summary>
        /// Автосоздание данных
        /// </summary>        
        /// <returns>consultants</returns>
        private void AutoCreation()
        {
            var employees = new List<Employee>();

            for (int i = 1; i < 10; i++)
            {
                string asurname = "Ахвердов " + i;
                string aname = "Андрей " + i;
                string amiddleName = "Александрович " + i;
                string atelephoneNumber = "8918766937" + i;
                string apasport = "0708 10050" + i;

                var employee = new Employee
                {
                    Surname = asurname,
                    Name = aname,
                    MiddleName = amiddleName,
                    TelephoneNumber = atelephoneNumber,
                    Pasport = apasport
                };

                employees.Add(employee);
            }

            AddToXmlFromList(employees);
        }

        /// <summary>
        /// Запись в xml
        /// </summary>
        public void AddToXmlFromList(List<Employee> employees)
        {
            XElement persons = new XElement("Persons");

            foreach (var consultant in employees)
            {
                XElement person = new XElement("Person");
                XElement surname = new XElement("surname", consultant.Surname);
                XElement name = new XElement("name", consultant.Name);
                XElement middleName = new XElement("middleName", consultant.MiddleName);
                XElement telephoneNumber = new XElement("telephoneNumber", consultant.TelephoneNumber);
                XElement pasport = new XElement("pasport", consultant.Pasport);

                person.Add(surname, name, middleName, telephoneNumber, pasport);
                persons.Add(person);
            }

            persons.Save(_dataFilePath);
        }

        /// <summary>
        /// Чтение из xml
        /// </summary>        
        /// <returns>consultants</returns>
        public List<Employee> ReadFromXml()
        {
            var employees = new List<Employee>();
            string xsurname = "", xname = "", xmiddleName = "", xtelephoneNumber = "", xpasport = "";

            if (!File.Exists(_dataFilePath)) AutoCreation();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(_dataFilePath);

            // получим корневой элемент
            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xnode in xRoot)
                {
                    // обходим все дочерние узлы элемента
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "surname") xsurname = childnode.InnerText;
                        if (childnode.Name == "name") xname = childnode.InnerText;
                        if (childnode.Name == "surname") xmiddleName = childnode.InnerText;
                        if (childnode.Name == "telephoneNumber") xtelephoneNumber = childnode.InnerText;
                        if (childnode.Name == "pasport") xpasport = childnode.InnerText;
                    }

                    employees.Add(new Employee
                    {
                        Surname = xsurname,
                        Name = xname,
                        MiddleName = xmiddleName,
                        TelephoneNumber = xtelephoneNumber,
                        Pasport = xpasport
                    });
                }
            }

            return employees;
        }
    }
}
