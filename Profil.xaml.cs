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
                if (db.Administrators.Where(x => x.Alogin == AUTH.test).Select(x => x).FirstOrDefault() == null)
                {
                    int SPISUS = db.DataUsers.Where(x => x.LoginUs == locstr).Select(X => X.IDUser).FirstOrDefault();
                    Name.Text = db.Users.Where(X => X.IDUser == SPISUS).Select(x => x.UName).FirstOrDefault();
                    Name1.Text = db.Users.Where(X => X.IDUser == SPISUS).Select(x => x.ULastName).FirstOrDefault();
                    Name2.Text = db.Users.Where(X => X.IDUser == SPISUS).Select(x => x.USecondName).FirstOrDefault();
                    dATE.Text = db.Users.Where(X => X.IDUser == SPISUS).Select(x => x.DateofBirth).FirstOrDefault().ToString();
                    log.Text = db.DataUsers.Where(X => X.IDUser == SPISUS).Select(x => x.LoginUs).FirstOrDefault().ToString();
                }
                else
                {
                    Name.Text = db.Administrators.Where(X => X.Alogin == locstr).Select(x => x.AName).FirstOrDefault();
                    Name1.Text = db.Administrators.Where(X => X.Alogin == locstr).Select(x => x.ALastName).FirstOrDefault();
                    Name2.Text = db.Administrators.Where(X => X.Alogin == locstr).Select(x => x.ASecondName).FirstOrDefault();
                    dATE.Text = db.Administrators.Where(X => X.Alogin == locstr).Select(x => x.DateofBirth).FirstOrDefault().ToString();
                    log.Text = db.Administrators.Where(X => X.Alogin == locstr).Select(x => x.Alogin).FirstOrDefault();
                }
            }
        }

        public static void shortrestriction(ref string provpassw, ref bool logbul,ref string provrestorepassw,ref string prolog)
        {
            if (provpassw != "")
            {
                if (provrestorepassw == prolog)
                {
                    if (provpassw != provrestorepassw)
                    {
                        prolog = provpassw;
                    }
                    else { MessageBox.Show("Такой пароль уже существует"); logbul = false; }
                }
                else { MessageBox.Show("Вы ввели неверный пароль"); logbul = false; }
            }
        }
        private void Change_data(object sender, RoutedEventArgs e)
        {
            using (kursRabEntities db = new kursRabEntities()) 
            {
                string locstr = AUTH.test;
                string prolog;
                string provpassw = par.Text;
                string provrestorepassw = provpar.Text;
                bool logbul = true;
                string PROV = Name.Text.Trim();
                string PROV1 = Name1.Text.Trim();
                string PROV2 = Name2.Text.Trim();
                string PROV3 = dATE.Text.Trim();
                string PROV4 = "NIKOGDANEDOGODAETESDOETOGO";
                string PROV5 = par.Text.Trim();
                Window1.generalrule(ref PROV, ref PROV1, ref PROV2, ref PROV3, ref PROV4, ref PROV5, ref logbul); 
                if (db.Administrators.Where(x => x.Alogin == AUTH.test).Select(x => x).FirstOrDefault() == null)
                {
                    int SPISUS = db.DataUsers.Where(x => x.LoginUs == locstr).Select(X => X.IDUser).FirstOrDefault();
                    prolog = db.DataUsers.Where(x => x.IDUser == SPISUS).Select(X => X.PasswordUs).FirstOrDefault();
                    Users REP = db.Users.Where(x => x.IDUser == SPISUS).Select(X => X).FirstOrDefault();
                    DataUsers REP1 = db.DataUsers.Where(x => x.IDUser == SPISUS).Select(X => X).FirstOrDefault(); 
                    REP.UName = PROV;
                    REP.ULastName = PROV1;
                    REP.USecondName = PROV2;
                    REP.DateofBirth = Convert.ToDateTime(PROV3);
                    shortrestriction(ref provpassw,ref logbul,ref provrestorepassw,ref prolog);
                    if (logbul)
                    {
                        REP1.PasswordUs = prolog;
                    }
                }
                else
                {
                    prolog = db.Administrators.Where(x => x.Alogin == AUTH.test).Select(x => x.Apassword).FirstOrDefault();
                    Administrators ad = db.Administrators.Where(x => x.Alogin == locstr).Select(y => y).FirstOrDefault();
                    shortrestriction(ref provpassw, ref logbul, ref provrestorepassw,ref prolog);
                    if (logbul)
                    {
                        ad.AName = PROV;
                        ad.ALastName = PROV1;
                        ad.ASecondName = PROV2;
                        ad.DateofBirth = Convert.ToDateTime(PROV3);
                        ad.Apassword = prolog;
                    }
                }
                if (logbul)
                {
                    db.SaveChanges();
                    MessageBox.Show(AUTH.test);
                } else MessageBox.Show("Были введены некорректные данные");
            }

        }
        private void Backprofil(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
    }
}
