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
        static async Task Main(string[] args)
        {
            // колиентская часть 
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(IPAddress.Loopback, 8080));
            byte[] buffer = new byte[4096];
            byte[] request = Encoding.UTF8.GetBytes("Привет сервер");
            await client.SendAsync(request, SocketFlags.None);
            int n = await client.ReceiveAsync(buffer, SocketFlags.None);
            Console.WriteLine($"Ответ от сервера {Encoding.UTF8.GetString(buffer, 0, n)}");
        }
    }
}
