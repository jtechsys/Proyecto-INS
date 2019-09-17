using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace doMain.Utils
{
    public class MailUtils
    {
        public void Send(string host, string port, string fromaddress, string fromPassword, string displayName, string toaddress, string subject, string body, List<Attachment> atachados, object userState)
        {
           


            var objfromAddress = new MailAddress(fromaddress, displayName);
            var objtoAddress = new MailAddress(toaddress);


            var smtp = new SmtpClient
            {
                Host = host,
                Port = Convert.ToInt32(port),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(objfromAddress.Address, fromPassword),
                Timeout = 20000
            };



            MailMessage message = new MailMessage(objfromAddress, objtoAddress);

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            //buscar atachados..
            //for (int at = 0; at < atachados.Count; at++)
            //{
            //    //if (atachados[at].Contains(codigo))
            //    //{
            //        Attachment data = new Attachment(atachados[at], MediaTypeNames.Image.Jpeg);
            //        message.Attachments.Add(data);
            //    //}
            //}

            if (atachados != null)
            {

                message.Attachments.Add(atachados[0]);
                message.Attachments[0].ContentId = "2.jpg";
                message.Attachments.Add(atachados[1]);
                message.Attachments[1].ContentId = "aqui.png";
            }

            
                        
            {
                try
                {
                    //smtp.SendCompleted += smtp_SendCompleted;
                    smtp.Send(message);

                    SendComplete(userState, null);

                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }
            }
            //message.Dispose();
        }
        public event EventHandler SendComplete;
        //void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        //{          

        //    if (e.Cancelled)
        //    {
                
        //    }
        //    if (e.Error != null)
        //    {

        //    }
        //    else
        //    {
        //        SendComplete(e.UserState, null);
        //    }

        //}


    }
}
