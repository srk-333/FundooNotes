using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Models
{
    public class MsmqModel
    {
        //Obj of MessageQueue class.
        MessageQueue messageQueue = new MessageQueue();
        //Sending token on Mail.
        public void Sender(string token)
        { 
            //system private msmq server path
            messageQueue.Path = @".\private$\Tokens";
            try
            {
                //Checking Path Exists or Not
                if (!MessageQueue.Exists(messageQueue.Path))
                {
                    //Creating Path
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                //Delegate Method Called
                messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Delegate Method for Sending Mail.
        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = messageQueue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com") {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("saurav.kr.192.168.1.1@gmail.com", "jkliop89")
                };
                mailMessage.From = new MailAddress("saurav.kr.192.168.1.1@gmail.com");
                mailMessage.To.Add(new MailAddress("saurav.kr.192.168.1.1@gmail.com"));
                mailMessage.Body = token;
                mailMessage.Subject = "Forgot Password Link";
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                messageQueue.BeginReceive();
            }
        }
    }
}
