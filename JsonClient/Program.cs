using JsonClient;
using System.Net.Sockets;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        TcpClient tcpClient = new TcpClient();

        bool keepSending = true;

        TcpClient socket = new TcpClient("127.0.0.1", 8);

        NetworkStream ns = socket.GetStream();
        StreamReader reader = new StreamReader(ns);
        StreamWriter writer = new StreamWriter(ns);

        while (keepSending)
        {
            Console.WriteLine("Methods: Random -- Add -- Subtract");
            string method = Console.ReadLine();
            Console.WriteLine("First number");
            int firstNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Second number");
            int secondNumber = int.Parse(Console.ReadLine());

            Message message = new Message(method, firstNumber, secondNumber);

            string serializedMessage = JsonSerializer.Serialize(message);
            writer.WriteLine(serializedMessage);
            writer.Flush();

            Console.WriteLine("Result: " + reader.ReadLine());
        }
        socket.Close();
    }
}