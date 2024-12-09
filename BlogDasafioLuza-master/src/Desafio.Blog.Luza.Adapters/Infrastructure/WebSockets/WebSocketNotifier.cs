using Desafio.Blog.Luza.Core.Domain.Interfaces;
using System.Net.WebSockets;
using System.Text;

namespace Desafio.Blog.Luza.Adapters.Infrastructure.WebSockets
{
    public class WebSocketNotifier : IWebSocketNotifier
    {
        private readonly List<WebSocket> _sockets = new();

        public async Task AddSocketAsync(WebSocket socket)
        {
            lock (_sockets)
            {
                _sockets.Add(socket);
            }

            var buffer = new byte[1024 * 4];
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    lock (_sockets)
                    {
                        _sockets.Remove(socket);
                    }
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                }
            }
        }

        public async Task NotifyAllAsync(string message)
        {
            var data = Encoding.UTF8.GetBytes(message);

            lock (_sockets)
            {
                _sockets.RemoveAll(s => s.State != WebSocketState.Open);
            }

            foreach (var socket in _sockets)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
