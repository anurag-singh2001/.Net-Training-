using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CollegeMgmtSystem.Service
{
    public class EmailService
    {
        private readonly string _apiKey;
        public EmailService()
        {
            _apiKey = Environment.GetEnvironmentVariable("apiKeyEmail");

            // Check if API key is actually loaded
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new Exception("SendGrid API key is missing. Please set the 'apiKeyEmail' environment variable.");
            }
        }

        public async Task SendEmail(string subject, string toEmail, string username, string message, string filePath)
        //public async Task SendEmail(string subject, string toEmail, string username, string message)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("siddharthmishra@yopmail.com", "StudentID");
            var to = new EmailAddress(toEmail, username);
            var plainTextContent = message;
            var htmlContent = $"<strong>{message}</strong>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                var pdfBytes = await File.ReadAllBytesAsync(filePath);
                var pdfBase64 = Convert.ToBase64String(pdfBytes);

                msg.AddAttachment("StudentID.pdf", pdfBase64);
            }

            var response = await client.SendEmailAsync(msg);
        }
    }
}
