using System.Net;
using System.Net.WebSockets;
using System.Threading;

var httpListener = new HttpListener();
httpListener.Prefixes.Add("http://localhost:8000/");
httpListener.Start();

Console.WriteLine(httpListener.Prefixes.Single());

while (true)
{
  var context = await httpListener.GetContextAsync();
  if (context.Request.IsWebSocketRequest)
  {
    // Fire and forget to be able to accept more clients
    HandleSocket(await context.AcceptWebSocketAsync(null));
  }
  else
  {
    context.Response.StatusCode = 400;
    context.Response.Close();
  }
}

async void HandleSocket(HttpListenerWebSocketContext context)
{
  do
  {
    byte[] buffer = new byte[1024];
    var result = await context.WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    if (result.MessageType == WebSocketMessageType.Close)
    {
      await context.WebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
    }
    else
    {
      // Echo the input
      await context.WebSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
    }
  }
  while (context.WebSocket.State == WebSocketState.Open);
}
