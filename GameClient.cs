
using System.Net.Sockets;
class ClientTCP
{
    private static TcpClient tcp = new TcpClient();
    private static int port = 111222;
    private static String received = "";
    public static void InitNetwork(ref bool IsConnected)
    {
        IsConnected = false;
        try
        {

            tcp.Connect("localhost", 11122);
            IsConnected = true;
        }
        catch (Exception e)
        {
            Console.WriteLine("Could not connect to server. ");
        }
    }

    ///
    ///
    ///
    public static void SendData(String text)
    {
        if (!tcp.Connected)
        {
            return;
        }
        using (var stream = tcp.GetStream())
        using (var writer = new StreamWriter(stream))
        {
            writer.WriteLine(text);
            writer.Flush();
        }
    }

    ///
    ///
    ///
    public static void Pong()
    {
        Console.SetCursorPosition(33, 0);
        Console.WriteLine("Got ping! Sending pong!");
        SendData("pong");
    }

    ///
    ///
    ///
    public static void ReceiveData()
    {
        if (!tcp.Connected)
        {
            return;
        }

        // If we get this far it means the user is connected, 
        // but has not sent any data for a while
        // Let's send a ping to the client.
        using (var stream = tcp.GetStream())
        using (var reader = new StreamReader(stream))
        {
            string? response = reader.ReadLine();
            if (response != null && response.Length > 0)
            {
                switch (response)
                {
                    case "ping":
                        Pong();
                        break;
                    default:
                        Console.SetCursorPosition(0, 32);
                        Console.WriteLine("Received: " + received);
                        break;
                }
            }
        }
    }



}
