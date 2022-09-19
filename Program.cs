using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{


    private static async Task Main(string[] args)
    {
        using Socket client = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        await client.ConnectAsync(IPAddress.Parse("127.0.0.1"), 11122);
        Player player = new();

        while (true)
        {
            string message = "";
            if (!player.HasPosition)
            {
                message = TcpMessage.GET_POSITION;
            }

            if (message.Length > 0)
            {
                var messageBytes = Encoding.UTF8.GetBytes(message);
                _ = await client.SendAsync(messageBytes, SocketFlags.None);
                Console.WriteLine($"Socket client sent message: \"{message}\"");
            }
            Thread.Sleep(1000);
        }
        client.Shutdown(SocketShutdown.Both);
    }

}


class TcpMessage
{
    public const string GET_POSITION = "GET_POSITION";
}