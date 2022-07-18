using System.Windows;
using WpfApp3.Log;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LogsList.ItemsSource = Work_with_log.ReadFromLogXml();
        }
    }
}
