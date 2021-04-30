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
    /// Логика взаимодействия для CHANGEFILM.xaml
    /// </summary>
    public partial class CHANGEFILM : Page
    {
        public CHANGEFILM()
        {
            string req,req1;
            InitializeComponent();
            using (kursRabEntities db = new kursRabEntities())
            {
                int r = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x.IDFilm).FirstOrDefault();
                int r2 = db.DataUsers.Where(x => x.LoginUs == AUTH.test).Select(X => X.IDUser).FirstOrDefault();
                TextofF.Text = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x.NameofFilm).FirstOrDefault();
                TextofDATE.Text = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(x => x.DateofCreate).FirstOrDefault().ToString();
                var spisact = db.roleofactor.Where(y => y.IDFilm == r).Select(x => x).ToList();
                DataofRole.ItemsSource = spisact;
            }
            using (kursRabEntities db = new kursRabEntities())
            {
                var COMBOSPIS = db.GENRES.Select(x => x).ToList<GENRES>();
                foreach (GENRES gen in COMBOSPIS)
                {
                    COMBOBOXGENRE.Items.Add(gen);
                }
            }
            using (kursRabEntities db = new kursRabEntities())
            {
                var COMBOSPIS1 = db.Countries.Select(x => x).ToList<Countries>();
                foreach (Countries sen in COMBOSPIS1)
                { 
                    COMBOBOXCOUNTRY.Items.Add(sen);   
                }
                int z = db.Films.Where(x => x.NameofFilm == POISK.MEGATEST).Select(X => X.IDCountry).FirstOrDefault();
                int z1 = db.Films.Where(x => x.NameofFilm == POISK.MEGATEST).Select(X => X.IDgenre).FirstOrDefault();
                req = db.Countries.Where(x => x.IDCountry == z).Select(x => x.NameofCountry).FirstOrDefault();
                req1 = db.GENRES.Where(x => x.IDgenre == z1).Select(x => x.NameofGenre).FirstOrDefault();
            }
            Textofcountr.Text = req;
            Textofgenre.Text = req1;
        }

        private void backmanuforcechange(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void change(object sender, RoutedEventArgs e)
        {
            using (kursRabEntities db = new kursRabEntities()) 
            {
                string prov1 = COMBOBOXCOUNTRY.Text;
                string prov2 = COMBOBOXGENRE.Text;
                var fetch = db.Films.Where(x => x.NameofFilm == POISK.MEGATEST).Select(y => y).FirstOrDefault();
                fetch.NameofFilm = TextofF.Text;
                fetch.DateofCreate = Convert.ToDateTime(TextofDATE.Text);
                fetch.IDCountry = db.Countries.Where(x => x.NameofCountry == prov1).Select(y => y.IDCountry).FirstOrDefault();
                fetch.IDgenre = db.GENRES.Where(x => x.NameofGenre == prov2).Select(y => y.IDgenre).FirstOrDefault();
                fetch.AgeRestriction = Convert.ToInt32(Textofage.Text);
                db.SaveChanges();
                Textofcountr.Text = COMBOBOXCOUNTRY.Text;
                Textofgenre.Text = COMBOBOXGENRE.Text;
            }
        }

        private void addactor(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ADDACTOR());
        }
    }
}
