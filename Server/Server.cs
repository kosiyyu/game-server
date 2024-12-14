using System.Net.Sockets;
using GameServer.Server.Library;
using ValueType = GameServer.Server.Library.ValueType;


namespace GameServer.Server
{
    public class Server
    {
        private readonly int _port;
        private readonly MakeAtomic<UdpClient> _atomicUdpClient;
        private readonly int _numberOfThreads;

        public Server(int port, int? numberOfThreads)
        {
            _port = port;
            _numberOfThreads = numberOfThreads ?? Environment.ProcessorCount;
            _atomicUdpClient = new MakeAtomic<UdpClient>(new UdpClient(_port));
        }
        
        public async Task StartServerAsync()
        {
            ThreadPool.SetMinThreads(_numberOfThreads, _numberOfThreads);
            ThreadPool.SetMaxThreads(_numberOfThreads, _numberOfThreads);
            
            Console.WriteLine($"Server is running... on port {_port}");
            
            while (true)
            {
                await _atomicUdpClient.ExecuteAsync
                (async client =>
                {
                    UdpReceiveResult result = await client.ReceiveAsync();
                    var bytes = ProcessInput(result.Buffer);
                    await client.SendAsync(bytes, bytes.Length, result.RemoteEndPoint);
                });
            }
        }
        
        private static byte[] ProcessInput(byte[] data)
        {
            var command = CommandUtils.Deserialize(data);
            
            // if (!CommandUtils.IsCommand(command))
            // {
            //     // incorrect command
            //     Console.WriteLine($"Received unknown command: {command.Name}");
            // }

            // To refactor
            // if (command.Name == "msg")
            // {
            //     Console.WriteLine($"Received command value: {command.Value}");
            // }
            
            if (command is { Name: "tel", ValueType: ValueType.Telemetry })
            {
                var telemetry = CommandUtils.GetTelemetry(command);
                Console.WriteLine(telemetry);
            }

            return [];
        }
    }
}