using System.Net;
using System.Net.Mail;

namespace EcommerceApplication.Services
{
    public interface IEmailServices
    {
        string MailGenerator(string email, string message, string subject);
        void SendEmailMessage(string Toemail, string UserName);
        void SendOTP(string Toemail, string UserName, string otp);
    }
    public class EmailService:IEmailServices
    {    

        public string MailGenerator(string Toemail, string message, string subject)
        {

            string ToEmail = Toemail;
            var FromEmail = "flyingduchmen323@gmail.com";
            var password = "mbyndttyihulardn";


            var client = new SmtpClient("smtp.gmail.com", 587);

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(FromEmail, password);

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromEmail);
            mailMessage.To.Add(ToEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            client.Send(mailMessage);
            return "Email Sent";
        }
        public void SendEmailMessage(string Toemail,string UserName)
        {
            var Message = "Congratulations!  "+ UserName + " Your account is created successfully..........";
            var Subject="Hello " + UserName;
            MailGenerator(Toemail, Message, Subject);
        }
        public void SendOTP(string Toemail, string UserName,string otp)
        {
            var Message = "Dear  " + UserName + "  Welcome Back!  We have sent you One Time Password to confirm your login request.Your One-time-Password is "+otp.ToString();
            var Subject =otp+"  Verify with OTP";
            MailGenerator(Toemail, Message, Subject);

        }
    }
}





