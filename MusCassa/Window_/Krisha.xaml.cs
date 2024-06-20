using MusCassa.DB;
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
using System.Windows.Shapes;

namespace MusCassa.Window_
{
    /// <summary>
    /// Логика взаимодействия для Krisha.xaml
    /// </summary>
    public partial class Krisha : Window
    {
        public Krisha(Сотрудник сотрудник)
        {
            InitializeComponent();
            lisichka.Content = new Page_.elfvistavok(сотрудник);
            //switch (сотрудник.Должность) 
            //{
            //    case 0:
            //        lisichka.Content=
            //        break;
            //        case 1:
            //        break;
            //}
        }

        private void bback_Click(object sender, RoutedEventArgs e)
        {
            Window_.Base basirovannoe = new Window_.Base();
            basirovannoe.Show();
            this.Close();
        }
    }
}
