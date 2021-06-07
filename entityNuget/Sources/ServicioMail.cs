using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Emails
{
    /// <summary>
    /// Esta clase te ayudara a crear objetos tipo EmailServicio,
    /// con los cuales puedes enviar un unico correo a un unico destinario.
    /// Constructor + 2 Sobrecargas
    /// Configura la primer sobrecarga para inicializarlo con un correo
    /// de pruebas que hayas creado previamente.
    /// </summary>
    class emailServicio
    {

        public string fromName { get; set; }
        private MailAddress fromAdress { get; set; }
        private string fromPassword { get; set; }
        private MailAddress toAdress { get; set; }
        private string subject { get; set; }
        private string body { get; set; }


        public emailServicio() { }

        /// <summary>
        /// Constructor
        /// Recibe tres parametros, en "you_mail" debes poner la direccion
        /// de tu correo electronico de GMAIL
        /// "your_password" pones tu contraseña.
        /// </summary>
        /// <param name="subject">Sujeto del mail</param>
        /// <param name="body">Contenido del mail</param>
        public emailServicio(string subject, string body)
        {
            this.fromAdress = new MailAddress("your_mail", this.fromName);
            this.fromPassword = "your_password";
            this.subject = subject;
            this.body = body;
        }


        public emailServicio(string emailDestino, string emailName, string subject, string body)
        {
            this.fromAdress = new MailAddress("your_mail", emailName);
            this.fromPassword = "your_password";
            this.subject = subject;
            this.body = body;

            //Correo del destinatario
            this.toAdress = new MailAddress(emailDestino);
        }

        //Unicamente asignas el correo del destinario.
        public void mailDestinatario(string mail)
        {
            this.toAdress = new MailAddress(mail);
        }

        //Se envia por el protocolo SMTP el correo.
        public void enviarEmail()
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(this.fromAdress.Address, this.fromPassword)
            };


            using (var message = new MailMessage(this.fromAdress, this.toAdress)
            {
                Subject = this.subject,
                Body = this.body
            })
            {
                smtp.Send(message);
            }
        }



    }
}