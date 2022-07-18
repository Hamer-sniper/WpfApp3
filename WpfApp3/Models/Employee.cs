using System.Collections.Generic;

namespace WpfApp3.Models
{
    public class Employee
    {
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string TelephoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Паспорт
        /// </summary>
        public string Pasport { get; set; } = string.Empty;

        /// <summary>
        /// Дата и время изменения
        /// </summary>
        public string DateTimeChange { get; set; } = string.Empty;

        /// <summary>
        /// Изменяемые данные
        /// </summary>
        public string DataChanged { get; set; } = string.Empty;

        /// <summary>
        /// Вид изменения
        /// </summary>
        public string TypeOfChanges { get; set; } = string.Empty;

        /// <summary>
        /// Лицо, изменившее данные
        /// </summary>
        public string Changer { get; set; } = string.Empty;

        /// <summary>
        /// Перечисление критериев сортировки
        /// </summary>
        public enum SortedCriterion
        {
            Surname,
            Name
        }

        /// <summary>
        /// Сортировка по имени
        /// </summary>
        private class SortBySurname : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = (Employee)x;
                Employee Y = (Employee)y;

                return string.Compare(X.Surname, Y.Surname);
            }
        }

        /// <summary>
        /// Сортировка по фамилии
        /// </summary>
        private class SortByName : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = (Employee)x;
                Employee Y = (Employee)y;

                return string.Compare(X.Name, Y.Name);
            }
        }

        /// <summary>
        /// Сортировка по критерию
        /// </summary>
        /// <param name="criterion"></param>
        /// <returns>List<Employee></returns>
        public static IComparer<Employee> SortedBy(SortedCriterion criterion)
        {
            if (criterion == SortedCriterion.Name) return new SortByName();
            else return new SortBySurname();
        }
    }
}
