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
            string TaketheNameCountr, TaketheNameGenre;
            InitializeComponent();
            using (kursRabEntities db = new kursRabEntities())
            {
                Name_of_Film.Text = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(x => x.NameofFilm).FirstOrDefault();
                DATEOC.Text = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(x => x.DateofCreate).FirstOrDefault().ToString();
                AGE.Text = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(x => x.AgeRestriction).FirstOrDefault().ToString();
                ////////////////
                var result = (from actor in db.CREATORSOFFILMS
                              join role in db.roleofactor on actor.IDCreator equals role.IDCreator
                              where role.IDFilm == POISK.GenID
                              select new
                              {
                                  actor.AcName,
                                  actor.AcLastName,
                                  actor.AcSecondName,
                                  actor.DateofBirth,
                                  actor.Dateofcareer,
                                  role.RoleofActor1
                              });
                
                DataofRole.ItemsSource = result.ToList();
                //
                var helpforimage = db.Films.Where(y => y.IDFilm == POISK.GenID).Select(x => x).FirstOrDefault();
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
                var COMBOGEN= db.GENRES.Select(x => x).ToList<GENRES>();
                foreach (GENRES gen in COMBOGEN)
                {
                    COMBOBOXGENRE.Items.Add(gen);
                }
            }
            using (kursRabEntities db = new kursRabEntities())
            {
                var COMBOCOUNTRY = db.Countries.Select(x => x).ToList<Countries>();
                foreach (Countries sen in COMBOCOUNTRY)
                { 
                    COMBOBOXCOUNTRY.Items.Add(sen);   
                }
                int Getidcountry = db.Films.Where(x => x.IDFilm == POISK.GenID).Select(X => X.IDCountry).FirstOrDefault();
                int GetidGenre = db.Films.Where(x => x.IDFilm == POISK.GenID).Select(X => X.IDgenre).FirstOrDefault();
                TaketheNameCountr = db.Countries.Where(x => x.IDCountry == Getidcountry).Select(x => x.NameofCountry).FirstOrDefault();
                TaketheNameGenre = db.GENRES.Where(x => x.IDgenre == GetidGenre).Select(x => x.NameofGenre).FirstOrDefault();
                ///////////////////////////////////
                
            }
            Textofcountr.Text = TaketheNameCountr;
            Textofgenre.Text = TaketheNameGenre;
        }
        //Возвращение в основное меню
        private void backmanuforcechange(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Menu());
        }
        //Изменение данных фильма и сохранение
        private void change(object sender, RoutedEventArgs e)
        {
            Spisokfilmov.FILMBoolhelp = false;
            string GetName = Name_of_Film.Text.Trim();
            string GetDATE = DATEOC.Text.Trim();
            string GetGENRE = COMBOBOXGENRE.Text.Trim();
            string GetCOUNTR = COMBOBOXCOUNTRY.Text.Trim();
            string GetAGE = AGE.Text.Trim();
            DateTime chekdatetime = DateTime.Now;
            bool Gencheckbool= true;
            Spisokfilmov.generalfilms(ref GetName, ref GetDATE, ref GetGENRE, ref GetCOUNTR, ref GetAGE, ref chekdatetime, ref Gencheckbool);
            if (Gencheckbool)
            {
                using (kursRabEntities db = new kursRabEntities())
                {
                    var fetch = db.Films.Where(x => x.IDFilm == POISK.GenID).Select(y => y).FirstOrDefault();
                    fetch.NameofFilm = Name_of_Film.Text;
                    fetch.DateofCreate = Convert.ToDateTime(DATEOC.Text);
                    if (GetCOUNTR != "")
                    {
                        fetch.IDCountry = db.Countries.Where(x => x.NameofCountry == GetCOUNTR).Select(y => y.IDCountry).FirstOrDefault();
                    }
                    else { fetch.IDCountry = db.Countries.Where(x => x.NameofCountry == Textofcountr.Text).Select(y => y.IDCountry).FirstOrDefault(); }
                    if (GetGENRE != "")
                    {
                        fetch.IDgenre = db.GENRES.Where(x => x.NameofGenre == GetGENRE).Select(y => y.IDgenre).FirstOrDefault();
                    }
                    else { fetch.IDgenre = db.GENRES.Where(x => x.NameofGenre == Textofgenre.Text).Select(y => y.IDgenre).FirstOrDefault(); }
                    try
                    {
                        fetch.AgeRestriction = Convert.ToInt32(AGE.Text);
                        fetch.Baner = Spisokfilmov.ImagetoByte;
                        db.SaveChanges();
                        if (COMBOBOXCOUNTRY.Text != "")
                        {
                            Textofcountr.Text = COMBOBOXCOUNTRY.Text;
                        }
                        if (COMBOBOXGENRE.Text != "")
                        {
                            Textofgenre.Text = COMBOBOXGENRE.Text;
                        }
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
        //Переход к странице по добавлению актёров
        private void addactor(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ADDACTOR());
        }
        //нахождение фотографии и загрузка ее для фильма
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
