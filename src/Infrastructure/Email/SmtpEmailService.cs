using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CR_COMPUTER.Domain.Interfaces;

namespace CR_COMPUTER.Infrastructure.Email
{
    /// <summary>
    /// Email service implementation using SMTP
    /// </summary>
    public class SmtpEmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public SmtpEmailService(EmailSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
        {
            await SendEmailAsync(new[] { to }, subject, body, isHtml);
        }

        public async Task SendEmailAsync(string[] to, string subject, string body, bool isHtml = false)
        {
            using var client = new SmtpClient(_settings.SmtpServer, _settings.SmtpPort)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = _settings.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_settings.FromEmail, _settings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            foreach (var email in to)
            {
                mailMessage.To.Add(email);
            }

            await client.SendMailAsync(mailMessage);
        }

        public async Task SendProjectNotificationAsync(string to, string projectName, string message)
        {
            var subject = $"Project Update: {projectName}";
            var body = $@"
                <h2>Project Notification</h2>
                <p><strong>Project:</strong> {projectName}</p>
                <p>{message}</p>
                <p>This is an automated notification from the Workflow Management System.</p>
            ";

            await SendEmailAsync(to, subject, body, true);
        }

        public async Task SendTaskNotificationAsync(string to, string taskTitle, string projectName, string message)
        {
            var subject = $"Task Update: {taskTitle}";
            var body = $@"
                <h2>Task Notification</h2>
                <p><strong>Task:</strong> {taskTitle}</p>
                <p><strong>Project:</strong> {projectName}</p>
                <p>{message}</p>
                <p>This is an automated notification from the Workflow Management System.</p>
            ";

            await SendEmailAsync(to, subject, body, true);
        }
    }

    /// <summary>
    /// Email settings configuration
    /// </summary>
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = null!;
        public int SmtpPort { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool EnableSsl { get; set; }
        public string FromEmail { get; set; } = null!;
        public string FromName { get; set; } = null!;
    }
}
