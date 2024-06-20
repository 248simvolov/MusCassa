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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Net;
using ControlzEx.Standard;
using System.Data.Entity.Migrations;

namespace MusCassa.Page_
{
    /// <summary>
    /// Логика взаимодействия для LkPolz.xaml
    /// </summary>
    public partial class LkPolz : Page
    {
        Посетитель пос = new Посетитель();
        public LkPolz(Посетитель посетитель)
        {
            InitializeComponent();
            пос = посетитель;
            tbPochta.Text = пос.Email;
            try
            {
                using (var context = new MusCassaEntities())
                {
                    List<Билет> tablitsa = context.Билет.Where(a => a.Посетитель.Equals(пос.КодПосетителя)).ToList();
                    dgridbileti.ItemsSource = tablitsa;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        string code;
        int schetchik;
        private void bChangePochta_Click(object sender, RoutedEventArgs e)
        {
            tCode.Visibility = Visibility.Visible;
            tbCode.Visibility = Visibility.Visible;
            bAcceptPochta.Visibility = Visibility.Visible;
            pochta();
        }
        string newPochta;
        public void pochta()
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.outlook.com";
                //указание данных почтового ящика
                smtp.Credentials = new NetworkCredential("gai.museum@outlook.com", "80$$OfGym");
                smtp.EnableSsl = true;
                //что куда отправляется
                MailMessage m = new MailMessage("gai.museum@outlook.com", tbnewPochta.Text);
                //создание тела письма
                Random rnd = new Random();
                code = rnd.Next(10000, 99999).ToString();
                string data = $"Код смены почтового ящика: {code}";
                //запись тела письма
                m.Body = data;
                //отправка
                smtp.Send(m);
                schetchik = 5;
                newPochta = tbnewPochta.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bAcceptPochta_Click(object sender, RoutedEventArgs e)
        {
            if(tbCode.Text.Length==5)
            { 
                if(code==tbCode.Text)
                {
                    try
                    {
                        using(MusCassaEntities context = new MusCassaEntities())
                        {
                            пос.Email = newPochta;
                            context.Посетитель.AddOrUpdate(пос);
                            context.SaveChanges();
                        }
                    }
                    catch(Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
                else
                {
                    if (schetchik >= 0)
                    {
                        MessageBox.Show($"Введён неправильный код смены почты осталось попыток {schetchik}");
                        schetchik--;
                    }
                    else
                    {
                        MessageBox.Show("Попытки кончились отправлен новый код смены почты");
                        pochta();
                    }

                }
            }
            else
                    MessageBox.Show("Введите код подтверждения полностью");
        }

        private void bPassChanged_Click(object sender, RoutedEventArgs e)
        {
            tstarpass.Visibility = Visibility.Visible;
            pbStar.Visibility = Visibility.Visible;
            tnewpass.Visibility = Visibility.Visible;
            pbNew.Visibility = Visibility.Visible;
            bAcceptPass.Visibility = Visibility.Visible;
        }

        private void bAcceptPass_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stb = new StringBuilder();
            if (pbStar.Password.Length == 0)
                stb.Append($"Введите старый пароль\n");
            if (pbNew.Password.Length == 0)
                stb.Append("Введите новый пароль");
            if (stb.Length > 0)
                MessageBox.Show(stb.ToString());
            else
            {
                try
                {
                    using (MusCassaEntities context = new MusCassaEntities())
                    {
                        if (пос.Пароль == pbStar.Password)
                        {
                            пос.Пароль = pbNew.Password;
                            context.Посетитель.AddOrUpdate(пос);
                            context.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Введён неправильный старый пароль");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
