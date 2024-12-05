using GameServer.Server;

var server = new Server(1234, 2);
await server.StartServerAsync();