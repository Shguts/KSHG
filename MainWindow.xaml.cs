﻿using System;
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
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;
namespace KSHG
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    ///  internal class ApplicationContext
    public partial class MainWindow : Window
    {
        public class ApplicationContext : DbContext
        {
            public DbSet<DataUsers> INFORMATION { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();

        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string provlogin = LOG.Text.Trim();
            string provpassw = PASSW.Text.Trim();
            DataUsers us = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                

            }
        }
        private void BtgRegistr_Click(object sender, RoutedEventArgs e)
        {
            Window1 taskWindow = new Window1();
            taskWindow.Show();

        }
    }

}
