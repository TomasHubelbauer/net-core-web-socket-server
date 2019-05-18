# .NET Core WebSocket Server

This repository demonstrates how to use the legacy `HttpListener` API to build a
naive WS server capable of accepting both text and binary connections from a JS
client running in a web browser.

To run the server, run `dotnet script main.csx`. You need `dotnet script` which
you can install by running `dotnet tool install -g dotnet-script`.

To run the client, open `index.html` and then use the developer tools to issue
a call of either `pingText` or `pingBinary` to see the echo.

This repository exists to demonstrate the WebSocket message preview capabilities
of the Google Chrome developer tools. I am however unable to demonstrate viewing
an outgoing binary frame, the content appears to be empty and the length is
mistakenly shown to be zero bytes. Incoming binary message is previewed
correctly.

I posted [a Stack Overflow question](https://stackoverflow.com/q/56114835/2715716)
inquiring about what could be the cause of the broken web socket frame display
in Chrome developer tools.

I made [a Chrome bug](https://crbug.com/962857) for this as well.

This turned out to be a Chrome bug as per the linked CRbug.
