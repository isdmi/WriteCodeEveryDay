using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DILearn
{
    public interface IMessageWriter
    {
        void Write(string message);
    }

    public class MessageWriter : IMessageWriter
    {
        public void Write(string message)
        {
            Console.WriteLine("Message: " + message);
        }
    }
}
