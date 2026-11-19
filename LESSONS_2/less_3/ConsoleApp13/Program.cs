using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serv = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serv.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            serv.Bind(new IPEndPoint(IPAddress.Any, 8080));
            serv.Listen(1000); // прослушка порта 
            while (true)
            {
                Socket client = await serv.AcceptAsync(); // подключаем клиента к сокету
                try
                {
                    await HandleClient(client);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); Console.ReadKey();
                }
                finally
                {
                    client.Close();
                }
                //catch (SocketException ex) // отлов ошибок сокета
                //{
                //    Console.WriteLine(ex.ToString());
                //    Console.ReadLine();
                //}
            }
        }
        static async Task HandleClient(Socket client)
        {
            var buffer = new byte[4096]; // буфер
            var segmentR = new ArraySegment<byte>(buffer, 0, buffer.Length);

            while (true)
            {
                int recive = await client.ReceiveAsync(segmentR, SocketFlags.None); // получение данных от клиента
                string request = Encoding.UTF8.GetString(buffer, 0, recive); // переводим с двоичного в стринг
                Console.WriteLine("Запрос: " + request);

                string answer;
                switch (request)
                {
                    case "date":
                        answer = "Дата: " + DateTime.Now.ToString("dd.MM.yyyy");
                        break;
                    case "time":
                        answer = "Время: " + DateTime.Now.ToString("HH:mm:ss");
                        break;
                    default:
                        answer = "ошибка, используйте date или time";
                        break;
                }

                byte[] response = Encoding.UTF8.GetBytes(answer); // переводим со стринг в двоичный
                var segmentS = new ArraySegment<byte>(response, 0, response.Length);
                await client.SendAsync(segmentS, SocketFlags.None); // отправляем клиенту
            }
        }
    }
}
