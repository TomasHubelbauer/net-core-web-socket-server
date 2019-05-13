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

- [ ] Find out why the outgoing message is not previewed correctly
