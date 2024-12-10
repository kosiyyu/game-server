using System.Net.Sockets;


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
                    var bytes = (await ProcessInput(result.Buffer)).ToBytes();
                    await client.SendAsync(bytes, bytes.Length, result.RemoteEndPoint);
                        
                });
            }
        }
        
        private static async Task<InputData> ProcessInput(byte[] data)
        {
            InputData inputData = InputData.FromBytes(data);
            var command = CommandUtils.Deserialize(data);
            
            if (!CommandUtils.IsCommand(command))
            {
                // incorrect command
            }

            // To refactor
            if (command.Name == "msg")
            {
                Console.WriteLine($"Received command value: {command.Value}");
            }

            return inputData;
        }
    }
}