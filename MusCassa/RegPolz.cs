using MusCassa.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusCassa
{
    public class RegPolz
    {
        public string registration(string login, string email, string password)
        {

            StringBuilder sb = new StringBuilder();
            if (login.Length == 0)
                sb.Append($"Введите логин\n");
            if (email.Length == 0)
                sb.Append($"Введите почту\n");
            if (password.Length == 0)
                sb.Append($"Введите пароль\n");
            if (sb.Length == 0)
                try
                {
                    using (MusCassaEntities context = new MusCassaEntities())
                    {
                        if (context.Посетитель.Where(a => a.Email.Equals(email)).Count() == 0)
                        {
                            if (context.Посетитель.Where(a => a.Логин.Equals(login)).Count() == 0)
                            {
                                return "accept";
                            }
                            else
                                return "Пользователь с таким логином уже существует";
                        }
                        else
                            return "Эта почта уже используется";
                    }
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
            else
                return(sb.ToString());
            
        }
    }
}
