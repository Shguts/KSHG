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

namespace KSHG
{
    /// <summary>
    /// Логика взаимодействия для Profil.xaml
    /// </summary>
    public partial class Profil : Page
    {
        public Profil()
        {
            InitializeComponent();
            using (kursRabEntities db = new kursRabEntities())
            {
                string locstr = AUTH.test;
                Users u = new Users();
                int SPISUS = db.DataUsers.Where(x => x.LoginUs == locstr).Select(X => X.IDUser).FirstOrDefault();
                Name.Text = db.Users.Where(X=>X.IDUser==SPISUS).Select(x=>x.UName).FirstOrDefault();
                Name1.Text = db.Users.Where(X => X.IDUser == SPISUS).Select(x => x.ULastName).FirstOrDefault(); 
                Name2.Text = db.Users.Where(X => X.IDUser == SPISUS).Select(x => x.USecondName).FirstOrDefault();
                dATE.Text = db.Users.Where(X => X.IDUser == SPISUS).Select(x => x.DateofBirth).FirstOrDefault().ToString();
            }
        }

        private void Change_data(object sender, RoutedEventArgs e)
        {
            using (kursRabEntities db = new kursRabEntities()) 
            {
                string locstr = AUTH.test;
                int SPISUS = db.DataUsers.Where(x => x.LoginUs == locstr).Select(X => X.IDUser).FirstOrDefault();
                Users REP = db.Users.Where(x => x.IDUser == SPISUS).Select(X => X).FirstOrDefault();
                string PROV = Name.Text.Trim();
                string PROV1 = Name1.Text.Trim();
                string PROV2 = Name2.Text.Trim();
                string PROV3 = dATE.Text.Trim();
                REP.UName = PROV;
                REP.ULastName = PROV1;
                REP.USecondName = PROV2;
                REP.DateofBirth = Convert.ToDateTime(PROV3);
                db.SaveChanges();
                MessageBox.Show(AUTH.test);



            }

        }
        private void Backprofil(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
    }
}
