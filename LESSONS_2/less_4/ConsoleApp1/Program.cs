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
        const int QuotesLimit = 10;

        static async Task Main(string[] args)
        {
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                await client.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
                int quotesReceived = 0;

                Console.Write("Логин: ");
                string login = Console.ReadLine();

                Console.Write("Пароль: ");
                string password = Console.ReadLine();

                string authResponse = await SendRequestAsync(client, login + " " + password);
                if (authResponse == null)
                {
                    Console.WriteLine("Сервер закрыл соединение");
                    return;
                }

                if (authResponse != "OK")
                {
                    Console.WriteLine("Сервер отклонил подключение: " + authResponse);
                    return;
                }

                Console.WriteLine("Авторизация успешна");

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("1 - получить случайную цитату");
                    Console.WriteLine("0 - выход");
                    Console.Write("Выберите пункт: ");

                    string choice = Console.ReadLine();
                    string request;

                    switch (choice)
                    {
                        case "1":
                            request = "quote";
                            break;
                        case "0":
                            Console.WriteLine("Выход");
                            return;
                        default:
                            Console.WriteLine("Неверный пункт меню, попробуйте еще");
                            continue;
                    }               

                    string response = await SendRequestAsync(client, request);
                    if (response == null)
                    {
                        Console.WriteLine("Сервер закрыл соединение");
                        break;
                    }

                    Console.WriteLine("Ответ сервера: " + response);
                    quotesReceived++;

                    if (quotesReceived >= QuotesLimit)
                    {
                        Console.WriteLine("Достигнут лимит в " + QuotesLimit + " цитат. Соединение закрывается");
                        break;
                    }
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

        static async Task<string> SendRequestAsync(Socket client, string request)
        {
            byte[] requestBytes = Encoding.UTF8.GetBytes(request);
            await client.SendAsync(new ArraySegment<byte>(requestBytes, 0, requestBytes.Length), SocketFlags.None);

            var buffer = new byte[4096];
            int recive = await client.ReceiveAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), SocketFlags.None);
            if (recive == 0)
                return null;

            return Encoding.UTF8.GetString(buffer, 0, recive);
        }
    }
}
