using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace FirstWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainPage mainPage { get; set; }
        public MainWindow()
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            mainPage = new(UniversityDb);
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            MainFrame.Navigate(mainPage);
        }
    }
}
