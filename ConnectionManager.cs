using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ConnectionManager
{
    private TcpListener? _server;
    private TcpClient? _client;
    private NetworkStream? _stream;

    public void Start()
    {
        _server = new TcpListener(IPAddress.Any, 6500);
        _server.Start();
        Logger.LogInfo("Servidor aguardando conexão na porta 6500");
        _client = _server.AcceptTcpClient();
        _stream = _client.GetStream();
        Send("#ok");
    }

    public string? ReceiveBarcode()
    {
        if (_stream == null) return null;
        byte[] buffer = new byte[255];
        if (_stream.DataAvailable)
        {
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            var input = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            return input.StartsWith("#") ? input[1..] : null;
        }
        return null;
    }

    public void Send(string message)
    {
        if (_stream == null) return;
        byte[] data = Encoding.ASCII.GetBytes(message);
        _stream.Write(data, 0, data.Length);
    }

    public void SendProduct(Product product)
    {
        Send("#" + product.Description + "|" + product.Price);
    }

    public void SendNotFound()
    {
        Send("#nfound");
    }
}
