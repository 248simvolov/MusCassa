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
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Page
    {
        public login()
        {
            InitializeComponent();
        }

        private void polzvhod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            using (MusCassaEntities context = new MusCassaEntities())
            {
                    //обработка ошибок при некорректном вводе
                    StringBuilder mnogostrochie = new StringBuilder();
                    if (polzlog.Text.Length == 0)
                        mnogostrochie.Append("Введите логин\n");
                    if (polzpass.Password.Length == 0)
                        mnogostrochie.Append("Введите пароль\n");
                    if (mnogostrochie.Length <= 0)
                    {
                        //поиск посетителя с указанными данными
                        var ты = context.Посетитель.Where(a => a.Логин.Equals(polzlog.Text)).Where(a => a.Пароль.Equals(polzpass.Password)).ToList();
                        if (ты.Count() >= 1)
                        {
                            //открытие нового окна с передачей данных о посетителе
                            Посетитель этот = new Посетитель();
                            этот = ты.First();
                            osnova osn = new osnova(этот);
                            osn.Show();
                            Application.Current.MainWindow.Close();
                        }
                        else
                            mnogostrochie.Append("Пользователь не найден\n");
                    }
                    else
                        MessageBox.Show(mnogostrochie.ToString());
            }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void polzreg_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new PolzReg();
        }

        private void sotrvhod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MusCassaEntities context = new MusCassaEntities())
                {
                    StringBuilder mnogostrochie = new StringBuilder();
                    if (sotrlog.Text.Length == 0)
                        mnogostrochie.Append("Введите логин\n");
                    if (sotrpass.Password.Length == 0)
                        mnogostrochie.Append("Введите пароль\n");
                    if (mnogostrochie.Length <= 0)
                    {
                        var ты = context.Сотрудник.Where(a => a.Логин.Equals(sotrlog.Text)).Where(a => a.Пароль.Equals(sotrpass.Password)).ToList();
                        if (ты.Count() >= 1)
                        {
                            Сотрудник этот = new Сотрудник();
                            этот = ты.First();
                            Krisha osn = new Krisha(этот);
                            osn.Show();
                            Application.Current.MainWindow.Close();
                        }
                        else
                            mnogostrochie.Append("Пользователь не найден\n");
                    }
                    else
                        MessageBox.Show(mnogostrochie.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
