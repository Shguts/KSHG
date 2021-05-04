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
using System.IO;
using Microsoft.Win32;

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
                int r = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x.IDFilm).FirstOrDefault();
                int r2 = db.DataUsers.Where(x => x.LoginUs == AUTH.test).Select(X => X.IDUser).FirstOrDefault();
                Name_of_Film.Text = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x.NameofFilm).FirstOrDefault();
                DATEOC.Text = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x.DateofCreate).FirstOrDefault().ToString();
                AGE.Text = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x.AgeRestriction).FirstOrDefault().ToString();
                var spisact = db.roleofactor.Where(y => y.IDFilm == r).Select(x => x).ToList();
                DataofRole.ItemsSource = spisact;
                var helpforimage = db.Films.Where(y => y.IDFilm == POISK.MEGATEST).Select(x => x).FirstOrDefault();
                try
                {
                    Stream streamobg = new MemoryStream(helpforimage.Baner);
                    BitmapImage BitObj = new BitmapImage();
                    BitObj.BeginInit();
                    BitObj.StreamSource = streamobg;
                    BitObj.EndInit();
                    this.fotoImage.Source = BitObj;
                }
                catch { }
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
                int z = db.Films.Where(x => x.IDFilm == POISK.MEGATEST).Select(X => X.IDCountry).FirstOrDefault();
                int z1 = db.Films.Where(x => x.IDFilm == POISK.MEGATEST).Select(X => X.IDgenre).FirstOrDefault();
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
            string PROV1 = Name_of_Film.Text.Trim();
            string PROV2 = DATEOC.Text.Trim();
            string PROV3 = COMBOBOXGENRE.Text.Trim();
            string PROV4 = COMBOBOXCOUNTRY.Text.Trim();
            string PROV5 = AGE.Text.Trim();
            DateTime chekdatetime = DateTime.Now;
            bool necro = true;
            Spisokfilmov.generalfilms(ref PROV1, ref PROV2, ref PROV3, ref PROV4, ref PROV5, ref chekdatetime, ref necro);
            if (necro)
            {
                using (kursRabEntities db = new kursRabEntities())
                {
                    string prov1 = COMBOBOXCOUNTRY.Text;
                    string prov2 = COMBOBOXGENRE.Text;
                    var fetch = db.Films.Where(x => x.IDFilm == POISK.MEGATEST).Select(y => y).FirstOrDefault();
                    fetch.NameofFilm = Name_of_Film.Text;
                    fetch.DateofCreate = Convert.ToDateTime(DATEOC.Text);
                    if (prov1 != null)
                    {
                        fetch.IDCountry = db.Countries.Where(x => x.NameofCountry == prov1).Select(y => y.IDCountry).FirstOrDefault();
                    }
                    else { fetch.IDCountry = db.Countries.Where(x => x.NameofCountry == Textofcountr.Text).Select(y => y.IDCountry).FirstOrDefault(); }
                    if (prov2 != null)
                    {
                        fetch.IDgenre = db.GENRES.Where(x => x.NameofGenre == prov2).Select(y => y.IDgenre).FirstOrDefault();
                    }
                    else { fetch.IDgenre = db.GENRES.Where(x => x.NameofGenre == Textofgenre.Text).Select(y => y.IDgenre).FirstOrDefault(); }
                    try
                    {
                        fetch.AgeRestriction = Convert.ToInt32(AGE.Text);
                        fetch.Baner = Spisokfilmov.ImagetoByte;
                        db.SaveChanges();
                        Textofcountr.Text = COMBOBOXCOUNTRY.Text;
                        Textofgenre.Text = COMBOBOXGENRE.Text;
                        try
                        {
                            Stream streamobg = new MemoryStream(fetch.Baner);
                            BitmapImage BitObj = new BitmapImage();
                            BitObj.BeginInit();
                            BitObj.StreamSource = streamobg;
                            BitObj.EndInit();
                            this.fotoImage.Source = BitObj;
                        }
                        catch { MessageBox.Show("вы недобавили картинку для фильма но это не критично"); }
                    }
                    catch { MessageBox.Show("Вы ввели некорректные данные"); }
                }
            }
        }

        private void addactor(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ADDACTOR());
        }

        private void findimage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select a photo";
            ofd.Filter = "Image Files | *.BMP; *.JPG; *.PNG";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == true)
            {
                MessageBox.Show("Выбран файл " + ofd.FileName);
            }
            try
            {
                ImageSource III1 = new BitmapImage(new Uri(ofd.FileName));
                Spisokfilmov.ImagetoByte = File.ReadAllBytes(ofd.FileName);
            }
            catch
            {
                MessageBox.Show("Вы не выбрали фотографию");
            }
        }
    }
}
