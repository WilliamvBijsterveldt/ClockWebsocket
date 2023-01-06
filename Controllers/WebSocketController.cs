using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ClockWebsocket.Controllers;

[ApiController]
[Route("[controller]")]
public class WebSocketController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ConnectWebSocket()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            while (true)
            {
                var data = new
                {
                    currentTime = DateTimeOffset.Now
                };

                Console.WriteLine(data.currentTime);
                await webSocket.SendAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)),
                    WebSocketMessageType.Text, true, CancellationToken.None);
                await Task.Delay(1000);
            }
        }

        return BadRequest();
    }
}