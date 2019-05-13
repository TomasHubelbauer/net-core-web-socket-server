using System.Net;
using System.Net.WebSockets;
using System.Threading;

var httpListener = new HttpListener();
httpListener.Prefixes.Add("http://localhost:8000/");
httpListener.Start();
Console.WriteLine("http://localhost:8000/");

while (true)
{
  var context = await httpListener.GetContextAsync();
  if (context.Request.IsWebSocketRequest)
  {
    // Fire and forget to be able to accept more clients
    var _ = Task.Run(async () =>
    {
      var socketContext = await context.AcceptWebSocketAsync(null);
      var socket = socketContext.WebSocket;
      while (true)
      {
        byte[] buffer = new byte[1024];
        var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        switch (result.MessageType)
        {
          case WebSocketMessageType.Close:
            {
              await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
              break;
            }
          case WebSocketMessageType.Text:
            {
              // Echo the input
              await socket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), WebSocketMessageType.Text, result.EndOfMessage, CancellationToken.None);
              break;
            }
          case WebSocketMessageType.Binary:
            {
              // Echo the input
              await socket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), WebSocketMessageType.Binary, result.EndOfMessage, CancellationToken.None);
              break;
            }
        }
      }
    });
  }
  else
  {
    context.Response.StatusCode = 400;
    context.Response.Close();
  }
}
