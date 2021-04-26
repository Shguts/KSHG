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
    /// Логика взаимодействия для ADDACTOR.xaml
    /// </summary>
    public partial class ADDACTOR : Page
    {
        public ADDACTOR()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            using (kursRabEntities db = new kursRabEntities())
            {
                CREATORSOFFILMS addactor = new CREATORSOFFILMS();
                addactor.AcName = Name.Text;
                addactor.AcLastName = Last_name.Text;
                addactor.AcSecondName = Second_name.Text;
                addactor.DateofBirth = Convert.ToDateTime(Date_of_birth.Text);
                addactor.Dateofcareer= Convert.ToDateTime(Date_of_career.Text);
                addactor.IDCreator = db.CREATORSOFFILMS.OrderByDescending(X => X.IDCreator).Select(x => x.IDCreator).FirstOrDefault() + 1 ;
                roleofactor roleofactor = new roleofactor();
                roleofactor.RoleofActor1 = role.Text;
                roleofactor.IDFilm = db.Films.Where(y => y.NameofFilm == POISK.MEGATEST).Select(X => X.IDFilm).FirstOrDefault();
                roleofactor.IDCreator = db.CREATORSOFFILMS.OrderByDescending(X => X.IDCreator).Select(x => x.IDCreator).FirstOrDefault() + 1;
                db.CREATORSOFFILMS.Add(addactor);
                db.roleofactor.Add(roleofactor);
                db.SaveChanges();
                MessageBox.Show("Актер бы успешно добавлен");
            }
        }
    }
}
