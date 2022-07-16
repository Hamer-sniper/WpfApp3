using System.Xml;
using System.Xml.Linq;
using WpfApp3.Models;
using WpfApp3.Log;
using System.Collections.Generic;
using System;
using System.IO;

namespace WpfApp3.Data
{
    public class DataManager
    {
        private readonly string _dataFilePath = Environment.CurrentDirectory + @"\Data\Databook.xml";

        public void Create(string usurname, string uname, string umiddleName, string utelephoneNumber, string upasport)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid().ToString(),
                Surname = usurname,
                Name = uname,
                MiddleName = umiddleName,                
                TelephoneNumber = utelephoneNumber,
                Pasport = string.IsNullOrWhiteSpace(upasport) ? "Паспорт не задан" : upasport,

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
                string aid = i.ToString();
                string asurname = "Ахвердов " + i;
                string aname = "Андрей " + i;
                string amiddleName = "Александрович " + i;
                string atelephoneNumber = "8918766937" + i;
                string apasport = "0708 10050" + i;

                var employee = new Employee
                {
                    Id = aid,
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
                XElement id = new XElement("id", consultant.Id);
                XElement surname = new XElement("surname", consultant.Surname);
                XElement name = new XElement("name", consultant.Name);
                XElement middleName = new XElement("middleName", consultant.MiddleName);
                XElement telephoneNumber = new XElement("telephoneNumber", consultant.TelephoneNumber);
                XElement pasport = new XElement("pasport", consultant.Pasport);

                person.Add(id, surname, name, middleName, telephoneNumber, pasport);
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
            string xid = "", xsurname = "", xname = "", xmiddleName = "", xtelephoneNumber = "", xpasport = "";

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
                        if (childnode.Name == "id") xid = childnode.InnerText;
                        if (childnode.Name == "surname") xsurname = childnode.InnerText;
                        if (childnode.Name == "name") xname = childnode.InnerText;
                        if (childnode.Name == "middleName") xmiddleName = childnode.InnerText;
                        if (childnode.Name == "telephoneNumber") xtelephoneNumber = childnode.InnerText;
                        if (childnode.Name == "pasport") xpasport = childnode.InnerText;
                    }

                    employees.Add(new Employee
                    {
                        Id = xid,
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
