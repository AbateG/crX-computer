using System.Threading.Tasks;

namespace CR_COMPUTER.Domain.Interfaces
{
    /// <summary>
    /// Email service interface
    /// </summary>
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, bool isHtml = false);
        Task SendEmailAsync(string[] to, string subject, string body, bool isHtml = false);
        Task SendProjectNotificationAsync(string to, string projectName, string message);
        Task SendTaskNotificationAsync(string to, string taskTitle, string projectName, string message);
    }
}
