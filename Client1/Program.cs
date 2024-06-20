using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ChatClient
{
    private static TcpClient client;
    private static NetworkStream stream;

    static void Main(string[] args)
    {
        client = new TcpClient();
        client.Connect("192.168.1.44", 8888);
        stream = client.GetStream();

        Thread readThread = new Thread(new ThreadStart(ReadMessages));
        readThread.Start();

        while (true)
        {
            string message = Console.ReadLine();
            byte[] data = Encoding.ASCII.GetBytes(" Remote client: " + message);
            stream.Write(data, 0, data.Length);
        }
    }

    private static void ReadMessages()
    {
        byte[] data = new byte[4096];
        int bytesRead;

        while (true)
        {
            bytesRead = stream.Read(data, 0, data.Length);
            string message = Encoding.ASCII.GetString(data, 0, bytesRead);
            Console.WriteLine(DateTime.Now.ToString() + message);
        }
    }
}
