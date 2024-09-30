using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonClient
{
    public class Message
    {
        public string Method { get; set; }
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }

        public Message(string method, int firstNumber, int secondNumber)
        {
            Method = method;
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
        }
        public Message()
        {
            
        }
    }
}
