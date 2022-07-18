using System.Collections.Generic;
using System.Windows.Media.Imaging;
using WpfApp3.Models;

namespace WpfApp3.Interfaces
{
    /// <summary>
    /// Общий интерфейс сотрудника для любой роли (менеджер или рабочий)
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Вернуть картинку
        /// </summary>
        /// <returns>BitmapImage</returns>
        BitmapImage GetBitmap();

        /// <summary>
        /// Изменить данные
        /// </summary>
        /// /// <param name="emp"></param>
        void Update(Employee emp);

        /// <summary>
        /// Получить все данные
        /// </summary>
        /// <returns>List<Employee></returns>
        List<Employee> GetAll();
    }
}