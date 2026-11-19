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
        static void Main(string[] args)
        {
            var serv = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serv.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            serv.Bind(new IPEndPoint(IPAddress.Any, 8080));
            serv.Listen(1000); // прослушка порта 
            while (true)
            {
                Socket client = serv.Accept(); // подключаем клиента к сокету
                try
                {
                    HandleClient(client);
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
        static void HandleClient(Socket client)
        {
            var buffer = new byte[1024]; // буфер
            var segment = new ArraySegment<byte>(buffer, 0, buffer.Length); // сегмент хранит в себе первый арг
            int recive = client.Receive(buffer); // получение данных от клиента
            string request = Encoding.UTF8.GetString(buffer, 0, recive); // переводим с двоичного в стринг
            Console.WriteLine(request);
            byte[] response = Encoding.UTF8.GetBytes("Привет клиент"); // переводим со стринг в двоичный
            var segmentS = new ArraySegment<byte>(response, 0, response.Length); 
            client.Send(response); // отправляем клиенту
            client.Shutdown(SocketShutdown.Both); // мягкое закрытие
        }
    }
}
