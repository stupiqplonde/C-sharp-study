using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
                byte[] request = Encoding.UTF8.GetBytes("Привет сервер");
                client.Send(request);
                var buffer = new byte[1024];
                int recive = client.Receive(buffer);
                Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, recive));
            } catch (SocketException e)
            {
                Console.WriteLine(e.SocketErrorCode + ": " + e.Message);
            }
        }
    }
}
