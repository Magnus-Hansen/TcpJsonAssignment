using System.Net.Sockets;

internal class Program
{
    private static void Main(string[] args)
    {
        TcpClient tcpClient = new TcpClient();

        bool keepSending = true;

        TcpClient socket = new TcpClient("127.0.0.1", 7);

        NetworkStream ns = socket.GetStream();
        StreamReader reader = new StreamReader(ns);
        StreamWriter writer = new StreamWriter(ns);

        string message = Console.ReadLine();

        writer.WriteLine(message);

        writer.Flush();

        while (keepSending)
        {

            string response = reader.ReadLine();

            Console.WriteLine(response);

            if (response == "Input numbers")
            {
                Console.WriteLine("First number");
                string firstNumber = Console.ReadLine();
                Console.WriteLine("Second number");
                string secondNumber = Console.ReadLine();
                writer.WriteLine(firstNumber + " " + secondNumber);
                writer.Flush();
            } 
            else
            {
                message = Console.ReadLine();

                writer.WriteLine(message);

                writer.Flush();
            }

        }
        socket.Close();
    }
}