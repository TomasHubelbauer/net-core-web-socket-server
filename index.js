const webSocket = new WebSocket("ws://localhost:8000");
window.webSocket = webSocket;

webSocket.addEventListener('open', () => {
  console.log('opened');
});

webSocket.addEventListener('message', event => {
  console.log('received', event.data, event.lastEventId, event.origin, event.ports, event.source);
});

webSocket.addEventListener('close', event => {
  console.log('closed', event.code, event.reason, event.wasClean);
});

webSocket.addEventListener('error', () => {
  console.log('errored');
});

function pingText() {
  console.log('sent TEXT');
  webSocket.send('TEXT');
}

function pingBinary() {
  const blob = new Blob(['BINARY']);
  console.log('sent', blob);
  webSocket.send(blob);
}
