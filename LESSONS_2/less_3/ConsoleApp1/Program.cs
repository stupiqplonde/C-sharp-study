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
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));

                while (true)
                {
                    Console.WriteLine("1 - запросить дату");
                    Console.WriteLine("2 - запросить время");
                    Console.WriteLine("0 - выход");
                    Console.Write("Выберите пункт: ");

                    string choice = Console.ReadLine();
                    string request;

                    switch (choice)
                    {
                        case "1":
                            request = "date";
                            break;
                        case "2":
                            request = "time";
                            break;
                        case "0":
                            Console.WriteLine("Выход");
                            return;
                        default:
                            Console.WriteLine("Неверный пункт меню, попробуйте еще");
                            continue;
                    }

                    byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                    var buffer = new byte[4096];
                    var segmentR = new ArraySegment<byte>(buffer, 0, buffer.Length);
                    var segmentS = new ArraySegment<byte>(requestBytes, 0, requestBytes.Length);
                    await client.SendAsync(segmentS, SocketFlags.None);
                    
                    int recive = await client.ReceiveAsync(segmentR, SocketFlags.None);

                    Console.WriteLine("Ответ сервера: " + Encoding.UTF8.GetString(buffer, 0, recive));
                }
            } catch (SocketException e)
            {
                Console.WriteLine(e.SocketErrorCode + ": " + e.Message);
                Console.ReadKey();
            }
            finally
            {
                client.Close();
            }
        }
    }
}
