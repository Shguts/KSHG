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
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;

namespace KSHG
{
    /// <summary>
    /// Логика взаимодействия для TEST_SPISFILM.xaml
    /// </summary>
    public partial class TEST_SPISFILM : Window
    {
        public TEST_SPISFILM()
        {
            InitializeComponent();
            using (kursRabEntities db = new kursRabEntities())
            {
                var spisfilms = db.Films.Select(x => x).ToList() ;
                Films t = new Films();
                SPISOKF1.DataContext = spisfilms;
                //foreach (Films f in spisfilms)
                //{
                  //  SPISOKF1.Items.Add(t);
                //}
            }
        }
    }
}
