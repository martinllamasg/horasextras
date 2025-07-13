using System.Threading.Tasks;

namespace HorasExtrasAppClean.Services
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        public Task SendAsync(string to, string subject, string body)
        {
            // Implementación real aquí...
            return Task.CompletedTask;
        }
    }
}
