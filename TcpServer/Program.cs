using System.Net.Sockets;
using System.Net;
using System.ComponentModel;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Dear TCP server:");

        TcpListener listener = new TcpListener(IPAddress.Any, 7);

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
                string? message = reader.ReadLine();
                string number;
                string[] numbers;
                int firstNumber;
                int secondNumber;

                switch (message) 
                {
                    case "Random":
                        numbers = GetNumbers();

                        Random rnd = new Random();
                        int randNumb;

                        if ((int.TryParse(numbers[0], out firstNumber) && int.TryParse(numbers[1], out secondNumber)) && (firstNumber <= secondNumber)) 
                        { 
                            writer.WriteLine(rnd.Next(firstNumber, secondNumber).ToString());
                            writer.Flush();
                        }
                        else
                        {
                            writer.WriteLine("need 2 numbers with space between and first number needs to be smaller than second");
                            writer.Flush();
                        }
                        break;

                    case "Add":
                        numbers = GetNumbers();

                        if (int.TryParse(numbers[0], out firstNumber) && int.TryParse(numbers[1], out secondNumber))
                        {
                            writer.WriteLine((firstNumber + secondNumber).ToString());
                            writer.Flush();
                        }
                        else
                        {
                            writer.WriteLine("need 2 numbers with space between");
                            writer.Flush();
                        }
                        break;

                    case "Subtract":
                        numbers = GetNumbers();

                        if (int.TryParse(numbers[0], out firstNumber) && int.TryParse(numbers[1], out secondNumber))
                        {
                            writer.WriteLine((firstNumber - secondNumber).ToString());
                            writer.Flush();
                        }
                        else
                        {
                            writer.WriteLine("need 2 numbers with space between");
                            writer.Flush();
                        }
                        break;

                    default:
                        writer.WriteLine("The 3 commands are: Random -- Add -- Subtract");
                        writer.Flush();
                        break;
                }
            }

            string[] GetNumbers()
            {
                writer.WriteLine("Input numbers");
                writer.Flush();

                string number = reader.ReadLine();
                return number.Split(" ");
            }

        }
    }
}