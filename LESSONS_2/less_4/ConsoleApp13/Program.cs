using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    internal class Program
    {
        static readonly string[] Quotes =
        {
            "«Быть или не быть — вот в чём вопрос.» — Уильям Шекспир",
            "«Знание — сила.» — Фрэнсис Бэкон",
            "«Я мыслю, следовательно, существую.» — Рене Декарт",
            "«Жизнь — это то, что происходит с тобой, пока ты строишь другие планы.» — Джон Леннон",
            "«Единственный способ делать великую работу — любить то, что ты делаешь.» — Стив Джобс",
            "«Дорогу осилит идущий.» — Латинская пословица",
            "«Сложнее всего начать действовать, всё остальное зависит от упорства.» — Амелия Эрхарт",
            "«Всё, что нас не убивает, делает нас сильнее.» — Фридрих Ницше",
            "«Век живи — век учись.» — Народная мудрость",
            "«Делу время — потехе час.» — Русская пословица"
        };

        const int QuotesLimit = 10;
        const int MaxClients = 5;

        static readonly Random rnd = new Random();
        static readonly string LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.log");
        static readonly SemaphoreSlim clientsSemaphore = new SemaphoreSlim(MaxClients, MaxClients);
        static readonly object rndLock = new object();
        static readonly object logLock = new object();

        static async Task Main(string[] args)
        {
            var serv = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serv.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            serv.Bind(new IPEndPoint(IPAddress.Any, 8080));
            serv.Listen(1000); // прослушка порта
            Log("Доступно цитат: " + Quotes.Length);
            Log("Максимум подключений: " + MaxClients);

            while (true)
            {
                Socket client = await serv.AcceptAsync(); // подключаем клиента к сокету

                if (!clientsSemaphore.Wait(0))
                {
                    Log("Достигнут лимит подключений (" + MaxClients + "). Клиент " + client.RemoteEndPoint + " отклонён");
                    try
                    {
                        byte[] reject = Encoding.UTF8.GetBytes("Достигнут лимит подключений (" + MaxClients + "). Попробуйте позже.");
                        await client.SendAsync(new ArraySegment<byte>(reject), SocketFlags.None);
                    }
                    catch (Exception e)
                    {
                        Log("Не удалось отправить отказ клиенту: " + e.Message);
                    }
                    finally
                    {
                        client.Close();
                    }

                    continue;
                }

                _ = ProcessClientAsync(client);
            }
        }

        static async Task ProcessClientAsync(Socket client)
        {
            try
            {
                await HandleClient(client);
            }
            catch (Exception e)
            {
                Log("Ошибка при обслуживании клиента: " + e.Message);
            }
            finally
            {
                client.Close();
                clientsSemaphore.Release();
            }
        }

        static async Task HandleClient(Socket client)
        {
            string clientInfo = client.RemoteEndPoint.ToString();
            Log("Подключен клиент" + clientInfo);

            int quotesSent = 0;

            var buffer = new byte[4096]; // буфер
            var segmentR = new ArraySegment<byte>(buffer, 0, buffer.Length);

            // Первое сообщение клиента 
            int authReceived = await client.ReceiveAsync(segmentR, SocketFlags.None);
            if (authReceived == 0)
            {
                Log("Отключился клиент" + clientInfo);
                return;
            }

            string authRequest = Encoding.UTF8.GetString(buffer, 0, authReceived).Trim();

            if (authRequest != "admin 123456" && authRequest != "user 123456")
            {
                Log("Неудачная попытка входа от " + clientInfo);
                await SendTextAsync(client, "Неверный логин или пароль");
                return;
            }

            string login = authRequest.Split(' ')[0];
            Log("Успешная авторизация: " + login + " (" + clientInfo + ")");
            await SendTextAsync(client, "OK");

            while (true)
            {
                int recive = await client.ReceiveAsync(segmentR, SocketFlags.None); // получение данных от клиента
                if (recive == 0)
                {
                    Log("Отключился клиент" + clientInfo);
                    break;
                }

                string request = Encoding.UTF8.GetString(buffer, 0, recive); // переводим с двоичного в стринг
                Log("Запрос от " + clientInfo + ": " + request);

                string answer;
                switch (request.ToLowerInvariant())
                {
                    case "quote":
                    {
                        int index;
                        lock (rndLock)
                        {
                            index = rnd.Next(Quotes.Length);
                        }

                        quotesSent++;
                        answer = "Цитата " + (index + 1) + ": " + Quotes[index];
                        Log("Отправлена цитата " + (index + 1) + " клиенту " + clientInfo + " (" + quotesSent + " / " + QuotesLimit + ")");
                        break;
                    }
                    default:
                        answer = "Неизвестный запрос";
                        Log("Неизвестный запрос от " + clientInfo + ": " + request);
                        break;
                }

                await SendTextAsync(client, answer); // отправляем клиенту

                if (quotesSent >= QuotesLimit)
                {
                    Log("Клиент " + clientInfo + " получил " + QuotesLimit + " цитат. Соединение закрывается");
                    break;
                }
            }
        }

        static async Task SendTextAsync(Socket client, string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            await client.SendAsync(new ArraySegment<byte>(data, 0, data.Length), SocketFlags.None);
        }

        static void Log(string message)
        {
            string line = "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "] " + message;
            Console.WriteLine(line);

            try
            {
                lock (logLock)
                {
                    File.AppendAllText(LogPath, line + Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось записать лог: " + e.Message);
            }
        }
    }
}
