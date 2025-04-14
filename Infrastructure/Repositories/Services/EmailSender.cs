using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace Infrastructure.Repositories.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            try
            {
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var port = int.Parse(_configuration["EmailSettings:Port"]!);
                var username = _configuration["EmailSettings:Username"];
                var password = _configuration["EmailSettings:Password"];

                var sender = new MailAddress(username!);

                var smtpClient = new SmtpClient(smtpServer)
                {
                    Port = port,
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                };

                var mailMessage = new MailMessage
                {
                    From = sender,
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (FormatException formatEx)
            {
                Console.WriteLine($"Format Exception: {formatEx.Message}");
                throw new InvalidOperationException($"Invalid email format: {formatEx.Message}");
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP Exception: {smtpEx.Message} - StatusCode: {smtpEx.StatusCode}");
                throw new InvalidOperationException($"Error sending email: {smtpEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
                throw new InvalidOperationException($"Unexpected error: {ex.Message}");
            }
        }
    }
}