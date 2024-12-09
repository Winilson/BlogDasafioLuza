namespace Desafio.Blog.Luza.Core.Domain.Interfaces
{
    public interface IWebSocketNotifier
    {
        Task NotifyAllAsync(string message);
        Task AddSocketAsync(System.Net.WebSockets.WebSocket socket);
    }
}
