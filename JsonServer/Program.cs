using System.Net.Sockets;
using System.Net;
using System.ComponentModel;
using JsonClient;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Dear TCP server:");

        TcpListener listener = new TcpListener(IPAddress.Any, 8);

        listener.Start();
        while (true)
        {
            TcpClient socket = listener.AcceptTcpClient();
            IPEndPoint clientEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;
            Console.WriteLine("Client connected:" + clientEndPoint.Address);

            Task.Run(() => HandleClient(socket));
        }
        listener.Stop();

        void HandleClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);

            while (socket.Connected)
            {
                string serializedMessage = reader.ReadLine();

                Message deSerializedMessage = JsonSerializer.Deserialize<Message>(serializedMessage);
                
                switch (deSerializedMessage.Method)
                {
                    case "Random":
                        Random rnd = new Random();
                        int randNumb;
                        
                        writer.WriteLine(rnd.Next(deSerializedMessage.FirstNumber, deSerializedMessage.SecondNumber).ToString());
                        writer.Flush();
                        break;

                    case "Add":
                        writer.WriteLine((deSerializedMessage.FirstNumber + deSerializedMessage.SecondNumber).ToString());
                        writer.Flush();
                        break;

                    case "Subtract":
                        writer.WriteLine((deSerializedMessage.FirstNumber - deSerializedMessage.SecondNumber).ToString());
                        writer.Flush();
                        break;

                    default:
                        writer.WriteLine("No method by that name");
                        writer.Flush();
                        break;
                }
            }
        }
    }
}