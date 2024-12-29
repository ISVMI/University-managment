﻿using Microsoft.EntityFrameworkCore;
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

namespace FirstWPF
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void TableBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TableBox.SelectedIndex)
            {
                case 0: { await LoadGroupsGridAsync(); break; }
                case 1: { await LoadStudentsGridAsync(); break; }
            }
        }
        private async Task LoadStudentsGridAsync()
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            UniversityService universityService = new(UniversityDb);
            var students = await universityService.GetStudents();
            DbGrid.Columns.Clear();
            DbGrid.ItemsSource = students;
        }
        private async Task LoadGroupsGridAsync()
        {
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();
            UniversityContext UniversityDb = new(optionsBuilder.Options);
            UniversityService universityService = new(UniversityDb);
            var groups = await universityService.GetGroups();
            var students = await universityService.GetStudents();
            DbGrid.Columns.Clear();
            DbGrid.ItemsSource = groups;
            //foreach (var student in students)
            //{
            //    DbGrid.Columns[2]
            //}
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            switch (TableBox.SelectedIndex)
            {
                case -1: { MessageBox.Show("Выберите таблицу!"); return; }   
                case 0: { MainFrame.Navigate(new GroupPage()); break; }
                case 1: { MainFrame.Navigate(new StudentPage()); break; }
            }
        }
    }
}
