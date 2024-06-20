using Microsoft.Xaml.Behaviors.Core;
using MusCassa.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для osnova.xaml
    /// </summary>
    public partial class osnova : Window
    {
        Посетитель пос = new Посетитель();
        public osnova(Посетитель посетитель)
        {
            InitializeComponent();
            пос = посетитель;
            furion.Content = new Page_.smotrvistavok(пос);
            buser.Content = пос.Логин;
        }

        public void zakritosn()
        {
            this.Close();
        }
        bool osnovava = true;
        private void buser_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
            if (osnovava)
            {
                furion.Content = new Page_.LkPolz(пос);
                osnovava = false;
                buser.Content = "Назад";
            }
            else
            {
                furion.NavigationService.GoBack();
                osnovava = true;
                buser.Content = пос.Логин;
            }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
