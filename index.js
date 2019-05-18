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

function pingBinaryUint8Array() {
  const uint8Array = new TextEncoder().encode('BINARY');
  console.log('sent', uint8Array);
  webSocket.send(uint8Array);
}

function pingBinaryBlob() {
  const blob = new Blob(['BINARY']);
  console.log('sent', blob);
  webSocket.send(blob);
}
