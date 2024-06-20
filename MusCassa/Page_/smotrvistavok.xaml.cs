using MusCassa.DB;
using MusCassa.Window_;
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

namespace MusCassa.Page_
{
    /// <summary>
    /// Логика взаимодействия для smotrvistavok.xaml
    /// </summary>
    /// на тот случай если потеряется
    /// public int БилетовКуплено { get { using(var context = new MusCassaEntities()) { return context.Билет.Where(a => a.Выставка.Equals(this.Код)).Count(); } } }

    public partial class smotrvistavok : Page
    {
        Посетитель пос = new Посетитель();
        public smotrvistavok(Посетитель посетитель)
        {
            InitializeComponent();
            пос = посетитель;
            using (MusCassaEntities context = new MusCassaEntities())
            {
                dgrid.ItemsSource = context.Выставка.ToList();
            }
        }

        private void bexit_Click(object sender, RoutedEventArgs e)
        {
            Base osn = new Base();
            osn.Show();
        }

        private void bpodrobnee_Click(object sender, RoutedEventArgs e)
        {
            if (dgrid.SelectedItem != null)
            {
                this.NavigationService.Content = new smotrvistavki((Выставка)dgrid.SelectedItem, пос);
            }
            else
                MessageBox.Show("Выберите выставку");
        }
    }
}
