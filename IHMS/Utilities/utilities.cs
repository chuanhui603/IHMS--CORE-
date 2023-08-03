using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HealthyLifeApp
{
    class utilities
    {
        public static string Smtp = "smtp.gmail.com";
        public static  int Port = 587;

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz234567890!~@#$%^&*";
            var random = new Random();
            Thread.Sleep(1);
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string getCryptPWD(string pwd, string userName)
        {

            char[] passwordChar = pwd.ToCharArray();//string convert to char
            string str1 = new string(passwordChar);//convert char to string 
            //==================================================
            //byte[] passwordAscii = Encoding.ASCII.GetBytes(pwd);//convert string to ascii
            byte[] passwordUnicode = Encoding.Unicode.GetBytes(pwd);//convert string to ascii
            string str_password = "";
            for (int i = 0; i < passwordUnicode.Length; i++)//pwd convert to char add( i+1)*2  salt define
            {
                char cha1 = (char)(passwordUnicode[i]);
                char cha = (char)(passwordUnicode[i] + (i + 1) * 2);
                str_password += cha1.ToString() + cha.ToString();
            }
            //==================================================
            byte[] userNameAscii = Encoding.Unicode.GetBytes(userName);//userName
            string str_userName = "";
            for (int j = 0; j < userNameAscii.Length; j++)
            {
                char cha_userName = (char)(userNameAscii[j]);
                char cha_userNameSalt = (char)(userNameAscii[j] + (j + 2) * 3);
                str_userName += cha_userName.ToString() + cha_userNameSalt.ToString();
            }
             Cryptographys cryp = new Cryptographys();
            string pwdCryp = cryp.SHA256Encode(str_password + str_userName);
            return pwdCryp;
        }
        public  bool CheckedPassword(string password)//with Regex to check password
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+"); 
            var hasLowerChar = new Regex(@"[a-z]+"); 
            var hasSymbol = new Regex(@"[!@#~$%^&*]+"); 
            if (password.Length >= 6 && password.Length <= 15)
            {
                var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasLowerChar.IsMatch(password);
                  return true;
            }
            else {  return false; };
        }
        public static void sendMail(string userName,string newPassword,string userMail,string DateSend)
        {
            MailMessage mm = new MailMessage("charleschou54138@gmail.com", userMail);
            mm.Subject = $"{userName} 您的密碼已重設, 請登入後,重新修改密碼";
            mm.Body = $"<p style='font-size:16px'>{userName}您好</p> " +
                $"<p style='font-size:16px'> 新的密碼: </p><p style='color: red; font-size:20px;font-weight:bolder'>{newPassword}</p> "+
                $"<p style='font-size:16px'>重設密碼的時間為</p><p style='color:green; font-size:16px;font-weight: bold'>{DateSend}</p> "+
                $"<p style='font-size:16px'>請盡快重設您的密碼,以保障您帳號的使用安全</p> " +
                $"<p style='font-size:16px'>此信為系統自動寄出, 不用回覆</p> " +
                $"<p style='font-size:16px'> iHealthyLife 敬上</p> ";
            mm.IsBodyHtml = true;
            //mm.Attachments.Add(new Attachment(""));//attachment
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            //smtp.Host = utilities.Smtp;
            //smtp.Port = utilities.Port;
            System.Net.NetworkCredential nc = new NetworkCredential("charleschou54138@gmail.com", "dhcepudpchktcjrf");
            smtp.Credentials = nc;
            smtp.EnableSsl = true;
            smtp.Send(mm);
        }
    }
}
