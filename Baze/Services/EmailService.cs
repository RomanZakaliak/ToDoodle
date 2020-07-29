using System;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using MimeKit.IO;

namespace Todo.Services
{
    public class EmailService
    {
        public async Task SendEmaiAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Site administration", "admin@todo.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using(var client = new SmtpClient())
            {
                await client.ConnectAsync("localhost", 25, MailKit.Security.SecureSocketOptions.None);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }    
        }
    }
}
