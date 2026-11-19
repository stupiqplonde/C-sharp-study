using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ConsoleApp12
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Socket serv = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // инициализация
            serv.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            serv.Bind(new IPEndPoint(IPAddress.Loopback, 8080)); // привязка порта
            serv.Listen(100); // кол-во клиентов в оченреди
            while (true)
            {
                Socket client = await serv.AcceptAsync(); // новый сокет для подключения клиента, старый остается слушать
                HandleClient(client);
            }
        }
            static async Task HandleClient(Socket client)
            {
                using (client)
                {
                    var buffer = new byte[4096];
                    try
                    {
                        int recieve = await client.ReceiveAsync(buffer, SocketFlags.None);
                        Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, recieve));
                        string response = "Hello world";
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                        await client.SendAsync(responseBytes, SocketFlags.None);   
                    }
                    catch (SocketException ex)
                    {
                
                    }
                    client.Shutdown(SocketShutdown.Both);
            }
        }
    }
}
