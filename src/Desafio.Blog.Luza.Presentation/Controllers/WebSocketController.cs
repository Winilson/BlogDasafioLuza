using Desafio.Blog.Luza.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Blog.Luza.Presentation.Controllers
{
    [ApiController]
    [Route("api/websocket")]
    public class WebSocketController : ControllerBase
    {
        private readonly IWebSocketNotifier _webSocketNotifier;

        public WebSocketController(IWebSocketNotifier webSocketNotifier)
        {
            _webSocketNotifier = webSocketNotifier;
        }

        [HttpGet("connect")]
        public async Task Connect()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await _webSocketNotifier.AddSocketAsync(socket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }
    }
}
