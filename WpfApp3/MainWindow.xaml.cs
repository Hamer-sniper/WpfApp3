using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.Models;
using WpfApp3.Interfaces;
using WpfApp3.Data;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IEmployee employee = new Consultant();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeeSelection.Items.Add("Консультант");
            EmployeeSelection.Items.Add("Менеджер");
            EmployeeSelection.SelectedIndex = 0;

            AddButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            Surname.IsEnabled = false;
            Name.IsEnabled = false;
            MiddleName.IsEnabled = false;
            Pasport.IsEnabled = false;

            UpdateButton.IsEnabled = false;
        }

        private void EmployeeSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeSelection.SelectedIndex == 0)
                employee = new Consultant();

            if (EmployeeSelection.SelectedIndex == 1)
                employee = new Manager();

            ClientsList.ItemsSource = employee.GetAll();
            EmployeeImage.Source = employee.GetBitmap();

            AddButton.IsEnabled = EmployeeSelection.SelectedIndex == 1 && !string.IsNullOrWhiteSpace(TelephoneNumber.Text);
            UpdateButton.IsEnabled = !string.IsNullOrWhiteSpace(TelephoneNumber.Text) && ClientsList.SelectedItem != null;
            DeleteButton.IsEnabled = EmployeeSelection.SelectedIndex == 1 && !string.IsNullOrWhiteSpace(TelephoneNumber.Text) && ClientsList.SelectedItem != null;

            Surname.IsEnabled = EmployeeSelection.SelectedIndex == 1;
            Name.IsEnabled = EmployeeSelection.SelectedIndex == 1;
            MiddleName.IsEnabled = EmployeeSelection.SelectedIndex == 1;
            Pasport.IsEnabled = EmployeeSelection.SelectedIndex == 1;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            employee.Create(Surname.Text, Name.Text, MiddleName.Text, TelephoneNumber.Text, Pasport.Text);
            ClientsList.ItemsSource = employee.GetAll();
            ClientsList.Items.Refresh();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var emp = (Employee)ClientsList.SelectedItem;
            ClientsList.Items.Refresh();
            employee.Update(emp);            
        }

        private void TelephoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Не сохранять запись без введеного номера телефона.
            AddButton.IsEnabled = EmployeeSelection.SelectedIndex == 1;

            if (string.IsNullOrWhiteSpace(TelephoneNumber.Text))
            {                
                UpdateButton.IsEnabled = false;
                AddButton.IsEnabled = false;
            }
            if (!string.IsNullOrWhiteSpace(TelephoneNumber.Text) && ClientsList.SelectedItem != null)
            {
                UpdateButton.IsEnabled = true;                
                DeleteButton.IsEnabled = EmployeeSelection.SelectedIndex == 1;
            }            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var emp = (Employee)ClientsList.SelectedItem;
            employee.Delete(emp);
            ClientsList.ItemsSource = employee.GetAll();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClientsList.ItemsSource = employee.GetAll();
        }
    }
}
