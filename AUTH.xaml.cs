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

namespace KSHG
{
    /// <summary>
    /// Логика взаимодействия для AUTH.xaml
    /// </summary>
    public partial class AUTH : Page
    {
        public static string test;
        public AUTH()
        {
            InitializeComponent();
        }
        //Обработка кнопки выхода из приложения
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //Авторизация пользователя или даминистратора в приложении
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string provlogin = LOG.Text.Trim();
            test = LOG.Text;
            string provpassw = PASSW.Password.Trim();
            Administrators ad;
            DataUsers us = null;
            using (kursRabEntities context = new kursRabEntities())
            {
                us = context.DataUsers.Where(l => l.LoginUs == provlogin && l.PasswordUs == provpassw).FirstOrDefault();
                ad = context.Administrators.Where(l => l.Alogin == provlogin && l.Apassword == provpassw).FirstOrDefault();
            }
            if ((us != null) || (ad != null))
            {
                MessageBox.Show("Вы авторизованы");
                NavigationService.Navigate(new Menu());
            }
            else
            {
                MessageBox.Show("Вы ввели некорректные данные");
            }
  

        }
        //Переход к онкну регистрации
        private void BtgRegistr_Click(object sender, RoutedEventArgs e)
        {
            Window1 taskWindow = new Window1();
            taskWindow.Show();

        }
    }
}
