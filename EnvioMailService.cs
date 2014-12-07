using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
namespace ServicioEnvioMailsProdeo
{
    class EnvioMailService
    {
        //public string enviarMailRegistro(string destinatario, string urlVerificacion)
        //{
        //    //Define instancia de la clase MailMessage
        //    MailMessage email = new MailMessage();
        //    email.To.Add(new MailAddress(destinatario));
        //    email.From = new MailAddress("prodeoteam@gmail.com");
        //    //email.Subject = "Asunto ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
        //    email.Subject = "Bienvenido a prodeo";
        //    email.Body = "Le damos la bienvenida a <b>PRODEO</b>. Para finalizar el registro, por favor haga clic en el siguiente link: <a href=\"" + urlVerificacion + "\">Finalizar registro</a><p>Si el link no funciona en su navegador, pegue la siguiente dirección en una nueva pestaña:</p><b>" + urlVerificacion + "</b><br/><p>Muchas gracias.</p>";
        //    email.IsBodyHtml = true;
        //    //email.Priority = MailPriority.Normal;

        //    //Define la instancia de SMTP
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = "smtp.gmail.com";
        //    smtp.Port = 587;
        //    smtp.EnableSsl = true;
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = new NetworkCredential("prodeoteam@gmail.com", "equipoprodeo");

        //    string output = null;

        //    //Envío del correo
        //    try
        //    {
        //        smtp.Send(email);
        //        email.Dispose();
        //        output = "Le env&iacute;amos un link a su correo para que pueda finalizar el registro.";
        //    }
        //    catch (Exception ex)
        //    {
        //        output = "No pudimos enviarle el mail de confirmaci&oacute;n, por favor chequee su conexi&oacute;n a Internet.";
        //        //output = "Error enviando correo electrónico: " + ex.Message;
        //    }

        //    return output;
        //}

        public Boolean enviarMail(Mails mail)
        {
                Boolean envioOk = true;
                //Define instancia de la clase MailMessage
                MailMessage email = new MailMessage();
                email.To.Add(new MailAddress(mail.destinatarios));
                email.From = new MailAddress("prodeoteam@gmail.com");
                //email.Subject = "Asunto ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
                email.Subject = mail.asunto;
                email.Body = mail.detalle;
                email.IsBodyHtml = true;
                //email.Priority = MailPriority.Normal;

                //Define la instancia de SMTP
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("prodeoteam@gmail.com", "equipoprodeo");

                string output = null;

                //Envío del correo
                try
                {
                    smtp.Send(email);
                    email.Dispose();
                    output = "Le env&iacute;amos un link a su correo para que pueda finalizar el registro.";
                }
                catch (Exception ex)
                {
                    envioOk = false;
                    output = "No pudimos enviarle el mail de confirmaci&oacute;n, por favor chequee su conexi&oacute;n a Internet.";
                    //output = "Error enviando correo electrónico: " + ex.Message;
                }

                return envioOk;
            
        }
    }
}
