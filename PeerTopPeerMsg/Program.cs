using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PeerTopPeerMsg;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: dotnet run <port> [peerAddress:peerPort]");
            return;
        }

        var port = int.Parse(args[0]);
        var peer = new Peer(port);

        if (args.Length == 3)
        {
            var peerInfo = args[2].Split(':');
            var peerAddress = peerInfo[0];
            var peerPort = int.Parse(peerInfo[1]);
            peer.ConnectToPeer(peerAddress, peerPort);
        }
        
        _ = Task.Run(() => peer.StartAsync());

        while (true)
        {
            var message = Console.ReadLine();
            if (message != null) peer.SendMessage(message);
        }
    }
}

internal class Peer
{
    private readonly int _port;
    private TcpListener _listener;
    private List<TcpClient> _clients;

    public Peer(int port)
    {
        _port = port;
        _clients = new List<TcpClient>();
    }

    public async Task StartAsync()
    {
        _listener = new TcpListener(IPAddress.Any, _port);
        _listener.Start();
        Console.WriteLine($"Listening on port {_port}...");

        while (true)
        {
            var client = await _listener.AcceptTcpClientAsync();
            _clients.Add(client);
            _ = Task.Run(() => HandleClientAsync(client));
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        var stream = client.GetStream();
        var buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received: {message}");
            BroadcastMessage(message);
        }
    }

    public void SendMessage(string message)
    {
        BroadcastMessage(message);
    }

    private void BroadcastMessage(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);

        foreach (var stream in from client in _clients where client.Connected select client.GetStream())
        {
            stream.Write(buffer, 0, buffer.Length);
        }
    }

    public void ConnectToPeer(string address, int port)
    {
        var client = new TcpClient(address, port);
        _clients.Add(client);
        _ = Task.Run(() => HandleClientAsync(client));
    }
}