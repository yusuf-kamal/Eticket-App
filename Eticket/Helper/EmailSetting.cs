using Eticket.Data.ViewModels;
using System.Net.Mail;
using System.Net;

namespace Eticket.Helper
{
	public class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("yusufkamal2030@gmail.com", "vckipelyemkzqcoy");
			client.Send("yusufkamal2030@gmail.com", email.To, email.Subject, email.Body);



			//MailMessage mail = new MailMessage();
			//mail.To.Add(email.To);
			//mail.From = new MailAddress("yusukamal2030@gmail.com");
			//mail.Subject = email.Subject;
			//mail.Body = email.Body;
			//mail.IsBodyHtml = true;
			//SmtpClient smtp = new SmtpClient();
			//smtp.Host = "smtp.gmail.com";
			//smtp.Port = 587;
			//smtp.Credentials = new NetworkCredential("yusufkamal2030@gmail.com", "ockdjwzbqawhnfgx");

			//smtp.EnableSsl = true;
			//smtp.Send(mail);
		}
	}
}
