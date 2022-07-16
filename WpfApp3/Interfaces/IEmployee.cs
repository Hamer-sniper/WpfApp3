using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Update(Employee emp);

        /// <summary>
        /// Удалить данные
        /// </summary>
        /// <param name="emp"></param>
        public void Delete(Employee emp);

        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="usurname"></param>
        /// <param name="uname"></param>
        /// <param name="umiddleName"></param>
        /// <param name="utelephoneNumber"></param>
        /// <param name="upasport"></param>
        public void Create(string usurname, string uname, string umiddleName, string utelephoneNumber, string upasport);

        /// <summary>
        /// Получить все данные
        /// </summary>
        /// <returns>List<Employee></returns>
        List<Employee> GetAll();
    }
}
