using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Interfaces
{
    /// <summary>
    /// Общий интерфейс сотрудника для любой роли (менеджер или рабочий)
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Отобразить меню
        /// </summary>
        void ShowMenu();

        /// <summary>
        /// Изменить данные
        /// </summary>
        void Update();
    }
}
