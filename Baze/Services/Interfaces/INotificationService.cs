using System.Threading.Tasks;

namespace Todo.Services.Interfaces
{
    public interface INotificationService
    {
        public Task SendNotificationAsync(string target, string subject, string message);
    }
}
