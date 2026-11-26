using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new CurrencyClient())
            {
                try
                {
                    string greeting = await client.ConnectAsync("127.0.0.1", 8090);
                    if (greeting == null)
                    {
                        Console.WriteLine("Сервер закрыл соединение при подключении.");
                        return;
                    }

                    if (greeting.StartsWith("ERR BUSY", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Сервер под максимальной нагрузкой. Попробуйте подключиться позже.");
                        return;
                    }

                    if (!greeting.StartsWith("READY", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Неожиданный ответ сервера: " + greeting);
                        return;
                    }

                    string[] greetingParts = greeting.Split(' ');
                    if (greetingParts.Length > 1)
                    {
                        Console.WriteLine("Доступные валюты: " +
                            string.Join(", ", greetingParts, 1, greetingParts.Length - 1));
                    }

                    Console.Write("Логин: ");
                    string login = Console.ReadLine();
                    if (login == null) return;

                    login = login.Trim();
                    if (login.Length == 0 || login.Contains(" "))
                    {
                        Console.WriteLine("Логин не должен быть пустым или содержать пробелы.");
                        return;
                    }

                    Console.Write("Пароль: ");
                    string password = Console.ReadLine();
                    if (password == null) return;

                    string authResponse = await client.SendAsync("AUTH " + login + " " + password);
                    if (authResponse == null)
                    {
                        Console.WriteLine("Сервер закрыл соединение.");
                        return;
                    }

                    if (authResponse == "ERR AUTH")
                    {
                        Console.WriteLine("Неверный логин или пароль.");
                        return;
                    }

                    if (authResponse.StartsWith("ERR LIMIT", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] limitParts = authResponse.Split(' ');
                        string seconds = limitParts.Length >= 3 ? limitParts[2] : "60";
                        Console.WriteLine("Слишком много запросов. Следующая сессия через " +
                                          seconds + " сек.");
                        return;
                    }

                    if (authResponse != "OK")
                    {
                        Console.WriteLine("Сервер отклонил подключение: " + authResponse);
                        return;
                    }

                    Console.WriteLine("Авторизация успешна.");

                    while (true)
                    {
                        Console.WriteLine();
                        Console.Write("Введите две валюты (например, USD EUR) или exit: ");
                        string input = Console.ReadLine();

                        if (input == null ||
                            input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
                        {
                            await client.SendAsync("BYE");
                            break;
                        }

                        string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length != 2)
                        {
                            Console.WriteLine("Нужно ровно две валюты, например: USD EUR");
                            continue;
                        }

                        string response = await client.SendAsync(
                            "RATE " + parts[0].ToUpperInvariant() + " " + parts[1].ToUpperInvariant());

                        if (response == null)
                        {
                            Console.WriteLine("Сервер закрыл соединение.");
                            break;
                        }

                        if (!PrintResponse(response))
                            break;
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("Ошибка сети (" + e.SocketErrorCode + "): " + e.Message);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Ошибка ввода-вывода: " + e.Message);
                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine("Соединение закрыто.");
                }
            }
        }

        // Возвращает false, если сервер сообщил о лимите и сессия завершена.
        static bool PrintResponse(string response)
        {
            string[] parts = response.Split(' ');

            if (parts[0] == "RATE" && parts.Length >= 4)
            {
                Console.WriteLine("1 " + parts[1] + " = " + parts[3] + " " + parts[2]);

                if (parts.Length == 6 && parts[4] == "LIMIT")
                {
                    Console.WriteLine("Достигнут лимит запросов. Следующая сессия через " +
                                      parts[5] + " сек.");
                    return false;
                }

                return true;
            }

            if (parts[0] == "ERR")
            {
                if (parts.Length >= 4 && parts[1] == "UNKNOWN_CURRENCY")
                {
                    Console.WriteLine("Неизвестная валюта: " + parts[2] + " или " + parts[3]);
                }
                else
                {
                    Console.WriteLine("Ошибка сервера: " + response);
                }

                return true;
            }

            Console.WriteLine(response);
            return true;
        }
    }

    internal sealed class CurrencyClient : IDisposable
    {
        private TcpClient _tcp;
        private StreamReader _reader;
        private StreamWriter _writer;

        public async Task<string> ConnectAsync(string host, int port)
        {
            _tcp = new TcpClient();
            await _tcp.ConnectAsync(host, port);
            _tcp.NoDelay = true;

            NetworkStream stream = _tcp.GetStream();
            _reader = new StreamReader(stream, new UTF8Encoding(false));
            _writer = new StreamWriter(stream, new UTF8Encoding(false))
            {
                AutoFlush = true,
                NewLine = "\n"
            };

            return await _reader.ReadLineAsync();
        }

        public async Task<string> SendAsync(string line)
        {
            await _writer.WriteLineAsync(line);
            return await _reader.ReadLineAsync();
        }

        public void Dispose()
        {
            try { _writer?.Dispose(); } catch { }
            try { _reader?.Dispose(); } catch { }
            try { _tcp?.Close(); } catch { }
        }
    }
}
