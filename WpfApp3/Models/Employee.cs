namespace WpfApp2
{
    public class Employee
    {
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
        /// Лицо имзенившее данные
        /// </summary>
        public string Changer { get; set; } = string.Empty;

        /// <summary>
        /// Вывести данные о конкретном человеке
        /// </summary>
        public void ShowInformation()
        {
            Console.WriteLine(Surname);
            Console.WriteLine(Name);
            Console.WriteLine(MiddleName);
            Console.WriteLine(TelephoneNumber);
            Console.WriteLine(Pasport);
        }
        public void ShowLog()
        {
            Console.WriteLine(DateTimeChange);
            Console.WriteLine(DataChanged);
            Console.WriteLine(TypeOfChanges);
            Console.WriteLine(Changer);
        }
    }
}
