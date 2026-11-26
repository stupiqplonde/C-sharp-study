using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var server = new CurrencyServer(8090);
            server.Log += Logger.Write;

            try
            {
                server.Start();
            }
            catch (SocketException e)
            {
                Console.WriteLine("Не удалось запустить сервер: " + e.Message);
                Console.WriteLine("Возможно, порт 8090 уже занят другой программой.");
                return;
            }

            Task serverTask = server.RunAsync();

            Console.WriteLine();
            Console.WriteLine("Пользователь по умолчанию: admin / 123456");
            PrintHelp();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == null) break;

                input = input.Trim();
                if (input.Length == 0) continue;

                string[] parts = input.Split(new[] { ' ' }, 3, StringSplitOptions.None);
                string command = parts[0].ToLowerInvariant();

                try
                {
                    switch (command)
                    {
                        case "help":
                            PrintHelp();
                            break;

                        case "maxclients":
                            server.MaxClients = ParsePositiveInt(parts, "maxclients");
                            Console.WriteLine("Максимум подключений: " + server.MaxClients);
                            break;

                        case "limit":
                            server.MaxRequests = ParsePositiveInt(parts, "limit");
                            Console.WriteLine("Максимум запросов на пользователя: " + server.MaxRequests);
                            break;

                        case "cooldown":
                            server.CooldownSeconds = ParsePositiveInt(parts, "cooldown");
                            Console.WriteLine("Кулдаун после лимита: " + server.CooldownSeconds + " сек");
                            break;

                        case "adduser":
                            if (parts.Length < 3)
                            {
                                Console.WriteLine("Формат: adduser <логин> <пароль>");
                                break;
                            }

                            Console.WriteLine(server.AddUser(parts[1], parts[2])
                                ? "Пользователь добавлен: " + parts[1]
                                : "Не удалось добавить пользователя (логин занят или пустой)");
                            break;

                        case "deluser":
                            if (parts.Length < 2)
                            {
                                Console.WriteLine("Формат: deluser <логин>");
                                break;
                            }

                            Console.WriteLine(server.RemoveUser(parts[1])
                                ? "Пользователь удалён: " + parts[1]
                                : "Пользователь не найден: " + parts[1]);
                            break;

                        case "users":
                            Console.WriteLine(server.GetUsersInfo());
                            break;

                        case "status":
                            Console.WriteLine(server.GetStatus());
                            break;

                        case "rates":
                            Console.WriteLine(Rates.GetTable());
                            break;

                        case "quit":
                        case "exit":
                            server.Stop();
                            try { await serverTask; } catch { }
                            return;

                        default:
                            Console.WriteLine("Неизвестная команда. Введите help для списка команд.");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка: " + e.Message);
                }
            }

            server.Stop();
            try { await serverTask; } catch { }
        }

        static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Команды сервера:");
            Console.WriteLine("  maxclients <N>           - максимум одновременных подключений");
            Console.WriteLine("  limit <N>                - максимум запросов на пользователя за сессию");
            Console.WriteLine("  cooldown <S>             - пауза после лимита, секунд");
            Console.WriteLine("  adduser <логин> <пароль> - добавить пользователя");
            Console.WriteLine("  deluser <логин>          - удалить пользователя");
            Console.WriteLine("  users                    - список пользователей");
            Console.WriteLine("  status                   - состояние сервера");
            Console.WriteLine("  rates                    - таблица курсов");
            Console.WriteLine("  help                     - эта справка");
            Console.WriteLine("  quit                     - остановить сервер");
            Console.WriteLine();
        }

        static int ParsePositiveInt(string[] parts, string command)
        {
            if (parts.Length < 2)
                throw new ArgumentException("Формат: " + command + " <число>");

            if (!int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
                throw new ArgumentException("Нужно целое число");

            if (value < 1)
                throw new ArgumentException("Число должно быть больше нуля");

            return value;
        }
    }

    internal static class Rates
    {
        // Сколько единиц валюты дают за 1 USD
        private static readonly Dictionary<string, decimal> UnitsPerUsd =
            new Dictionary<string, decimal>
            {
                { "USD", 1m },
                { "EUR", 0.92m },
                { "RUB", 92.5m },
                { "GBP", 0.79m },
                { "KZT", 450m }
            };

        public static bool TryGetRate(string from, string to, out decimal rate)
        {
            rate = 0m;
            if (!UnitsPerUsd.TryGetValue(from, out decimal fromUnits)) return false;
            if (!UnitsPerUsd.TryGetValue(to, out decimal toUnits)) return false;

            rate = toUnits / fromUnits;
            return true;
        }

        public static string Format(decimal value)
        {
            return value.ToString("0.######", CultureInfo.InvariantCulture);
        }

        public static string[] GetCodes()
        {
            var codes = new string[UnitsPerUsd.Count];
            UnitsPerUsd.Keys.CopyTo(codes, 0);
            return codes;
        }

        public static string GetTable()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Курсы (сколько единиц валюты дают за 1 USD):");

            foreach (KeyValuePair<string, decimal> pair in UnitsPerUsd)
                sb.AppendLine("  " + pair.Key + " = " + Format(pair.Value));

            return sb.ToString().TrimEnd();
        }
    }

    internal sealed class UserStore
    {
        private readonly Dictionary<string, string> _users =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private readonly object _sync = new object();

        public UserStore()
        {
            // Пользователь по умолчанию, чтобы можно было сразу подключиться.
            // Остальных добавляйте командой adduser.
            _users["admin"] = "123456";
        }

        public int Count
        {
            get { lock (_sync) return _users.Count; }
        }

        public bool TryValidate(string login, string password)
        {
            if (login == null || password == null) return false;

            lock (_sync)
            {
                return _users.TryGetValue(login, out string stored) && stored == password;
            }
        }

        public bool Add(string login, string password)
        {
            if (login == null) return false;

            login = login.Trim();
            if (login.Length == 0 || login.Contains(" ")) return false;
            if (password == null || password.Length == 0) return false;

            lock (_sync)
            {
                if (_users.ContainsKey(login)) return false;

                _users[login] = password;
                return true;
            }
        }

        public bool Remove(string login)
        {
            if (login == null) return false;

            lock (_sync) return _users.Remove(login.Trim());
        }

        public string GetInfo()
        {
            lock (_sync)
            {
                if (_users.Count == 0) return "Пользователей нет";
                return "Пользователи: " + string.Join(", ", _users.Keys);
            }
        }
    }

    internal sealed class SessionLimiter
    {
        private sealed class State
        {
            public int Requests;
            public DateTime BlockedUntilUtc;
        }

        private readonly Dictionary<string, State> _states =
            new Dictionary<string, State>(StringComparer.OrdinalIgnoreCase);

        private readonly object _sync = new object();

        public bool TryGetBlockSeconds(string login, out int seconds)
        {
            lock (_sync)
            {
                if (_states.TryGetValue(login, out State state) &&
                    state.BlockedUntilUtc > DateTime.UtcNow)
                {
                    seconds = (int)Math.Ceiling((state.BlockedUntilUtc - DateTime.UtcNow).TotalSeconds);
                    if (seconds < 1) seconds = 1;
                    return true;
                }

                seconds = 0;
                return false;
            }
        }

        public void StartSession(string login)
        {
            lock (_sync)
            {
                if (!_states.TryGetValue(login, out State state))
                {
                    state = new State();
                    _states[login] = state;
                }

                state.Requests = 0;

                if (state.BlockedUntilUtc <= DateTime.UtcNow)
                    state.BlockedUntilUtc = DateTime.MinValue;
            }
        }

        // Возвращает true, если лимит достигнут; тогда пользователь блокируется на cooldownSeconds.
        public bool RegisterRequest(string login, int maxRequests, int cooldownSeconds,
                                    out int used, out int cooldown)
        {
            lock (_sync)
            {
                State state = _states[login];
                state.Requests++;
                used = state.Requests;

                if (state.Requests >= maxRequests)
                {
                    state.BlockedUntilUtc = DateTime.UtcNow.AddSeconds(cooldownSeconds);
                    cooldown = cooldownSeconds;
                    return true;
                }

                cooldown = 0;
                return false;
            }
        }

        public int GetBlockedCount()
        {
            lock (_sync)
            {
                int count = 0;
                DateTime now = DateTime.UtcNow;

                foreach (State state in _states.Values)
                    if (state.BlockedUntilUtc > now) count++;

                return count;
            }
        }
    }

    internal sealed class CurrencyServer
    {
        private readonly TcpListener _listener;
        private readonly UserStore _users = new UserStore();
        private readonly SessionLimiter _limiter = new SessionLimiter();
        private readonly ConcurrentDictionary<TcpClient, byte> _clients =
            new ConcurrentDictionary<TcpClient, byte>();

        private int _activeClients;
        private int _maxClients = 5;
        private int _maxRequests = 5;
        private int _cooldownSeconds = 60;
        private volatile bool _stopping;

        public event Action<string> Log;

        public CurrencyServer(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
        }

        public int MaxClients
        {
            get { return Volatile.Read(ref _maxClients); }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxClients));
                Volatile.Write(ref _maxClients, value);
            }
        }

        public int MaxRequests
        {
            get { return Volatile.Read(ref _maxRequests); }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(MaxRequests));
                Volatile.Write(ref _maxRequests, value);
            }
        }

        public int CooldownSeconds
        {
            get { return Volatile.Read(ref _cooldownSeconds); }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(CooldownSeconds));
                Volatile.Write(ref _cooldownSeconds, value);
            }
        }

        public void Start()
        {
            _listener.Start();
            WriteLog("Сервер запущен на порту 8090");
            WriteLog("Лимит подключений: " + MaxClients +
                     ", лимит запросов: " + MaxRequests +
                     ", кулдаун: " + CooldownSeconds + " сек");
        }

        public async Task RunAsync()
        {
            while (!_stopping)
            {
                TcpClient client;
                try
                {
                    client = await _listener.AcceptTcpClientAsync();
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (SocketException e)
                {
                    if (_stopping) break;

                    WriteLog("Ошибка accept: " + e.Message);
                    continue;
                }

                int active = Interlocked.Increment(ref _activeClients);
                if (active > MaxClients)
                {
                    Interlocked.Decrement(ref _activeClients);
                    _ = RejectBusyAsync(client);
                    continue;
                }

                _clients.TryAdd(client, 0);
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }

        public void Stop()
        {
            if (_stopping) return;
            _stopping = true;

            try { _listener.Stop(); } catch { }

            foreach (TcpClient client in _clients.Keys)
            {
                try { client.Close(); } catch { }
            }

            WriteLog("Сервер остановлен");
        }

        public bool AddUser(string login, string password)
        {
            return _users.Add(login, password);
        }

        public bool RemoveUser(string login)
        {
            return _users.Remove(login);
        }

        public string GetUsersInfo()
        {
            return _users.GetInfo();
        }

        public string GetStatus()
        {
            return "Активных подключений: " + Volatile.Read(ref _activeClients) + " / " + MaxClients + Environment.NewLine +
                   "Лимит запросов: " + MaxRequests + ", кулдаун: " + CooldownSeconds + " сек" + Environment.NewLine +
                   "Пользователей: " + _users.Count + Environment.NewLine +
                   "Заблокированных пользователей: " + _limiter.GetBlockedCount();
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            string endpoint = "?";
            try { endpoint = client.Client.RemoteEndPoint.ToString(); } catch { }

            DateTime connectedAt = DateTime.Now;
            string login = null;
            int requests = 0;

            WriteLog("CONNECT " + endpoint + " (" + connectedAt.ToString("dd.MM.yyyy HH:mm:ss") + ")");

            try
            {
                client.NoDelay = true;

                using (client)
                using (NetworkStream stream = client.GetStream())
                using (var reader = new StreamReader(stream, new UTF8Encoding(false)))
                using (var writer = new StreamWriter(stream, new UTF8Encoding(false))
                {
                    AutoFlush = true,
                    NewLine = "\n"
                })
                {
                    // Приветствие: клиент сразу узнаёт, что сервер принял подключение,
                    // и получает список доступных валют.
                    await writer.WriteLineAsync("READY " + string.Join(" ", Rates.GetCodes()));

                    string line = await reader.ReadLineAsync();
                    if (line == null) return;

                    string[] auth = line.Split(new[] { ' ' }, 3, StringSplitOptions.None);
                    if (auth.Length != 3 ||
                        !auth[0].Equals("AUTH", StringComparison.OrdinalIgnoreCase))
                    {
                        await writer.WriteLineAsync("ERR BAD_REQUEST");
                        return;
                    }

                    login = auth[1].Trim();
                    string password = auth[2];

                    if (!_users.TryValidate(login, password))
                    {
                        WriteLog("AUTH FAIL " + login + " (" + endpoint + ")");
                        await writer.WriteLineAsync("ERR AUTH");
                        return;
                    }

                    if (_limiter.TryGetBlockSeconds(login, out int blockedSeconds))
                    {
                        WriteLog("BLOCKED " + login + " (" + endpoint +
                                 "), осталось " + blockedSeconds + " сек");
                        await writer.WriteLineAsync("ERR LIMIT " + blockedSeconds);
                        return;
                    }

                    _limiter.StartSession(login);
                    await writer.WriteLineAsync("OK");
                    WriteLog("AUTH OK " + login + " (" + endpoint + ")");

                    while (true)
                    {
                        line = await reader.ReadLineAsync();
                        if (line == null) break;

                        string trimmed = line.Trim();
                        if (trimmed.Equals("BYE", StringComparison.OrdinalIgnoreCase))
                        {
                            await writer.WriteLineAsync("BYE");
                            break;
                        }

                        string[] parts = trimmed.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length != 3 ||
                            !parts[0].Equals("RATE", StringComparison.OrdinalIgnoreCase))
                        {
                            await writer.WriteLineAsync("ERR BAD_REQUEST");
                            continue;
                        }

                        string from = parts[1].ToUpperInvariant();
                        string to = parts[2].ToUpperInvariant();

                        if (!Rates.TryGetRate(from, to, out decimal rate))
                        {
                            await writer.WriteLineAsync("ERR UNKNOWN_CURRENCY " + from + " " + to);
                            WriteLog("UNKNOWN " + login + " " + from + " " + to);
                            continue;
                        }

                        int maxRequests = MaxRequests;
                        bool limitReached = _limiter.RegisterRequest(
                            login, maxRequests, CooldownSeconds, out int used, out int cooldown);

                        requests = used;
                        string value = Rates.Format(rate);

                        if (limitReached)
                        {
                            // Последний разрешённый запрос: отдаём курс и сразу сообщаем о лимите.
                            await writer.WriteLineAsync(
                                "RATE " + from + " " + to + " " + value + " LIMIT " + cooldown);
                            WriteLog("RATE " + login + " " + from + "->" + to + " = " + value +
                                     " (" + used + "/" + maxRequests + ")");
                            WriteLog("LIMIT " + login + ": достигнут лимит " + maxRequests +
                                     " запросов, блокировка " + cooldown + " сек");
                            break;
                        }

                        await writer.WriteLineAsync("RATE " + from + " " + to + " " + value);
                        WriteLog("RATE " + login + " " + from + "->" + to + " = " + value +
                                 " (" + used + "/" + maxRequests + ")");
                    }
                }
            }
            catch (IOException)
            {
                // Клиент оборвал соединение — это штатная ситуация.
            }
            catch (ObjectDisposedException)
            {
                // Соединение закрыли через Stop().
            }
            catch (Exception e)
            {
                WriteLog("Ошибка клиента " + endpoint + ": " + e.Message);
            }
            finally
            {
                _clients.TryRemove(client, out _);
                Interlocked.Decrement(ref _activeClients);

                TimeSpan duration = DateTime.Now - connectedAt;
                string who = login == null ? endpoint : login + " (" + endpoint + ")";

                WriteLog("DISCONNECT " + who + " (" +
                         DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") +
                         ", запросов: " + requests +
                         ", длительность " + duration.ToString(@"hh\:mm\:ss") + ")");
            }
        }

        private async Task RejectBusyAsync(TcpClient client)
        {
            string endpoint = "?";
            try { endpoint = client.Client.RemoteEndPoint.ToString(); } catch { }

            try
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                using (var writer = new StreamWriter(stream, new UTF8Encoding(false))
                {
                    AutoFlush = true,
                    NewLine = "\n"
                })
                {
                    await writer.WriteLineAsync("ERR BUSY");
                }

                WriteLog("REJECT BUSY " + endpoint + " (активных: " +
                         Volatile.Read(ref _activeClients) + "/" + MaxClients + ")");
            }
            catch (Exception e)
            {
                WriteLog("Не удалось отклонить клиента " + endpoint + ": " + e.Message);
            }
        }

        private void WriteLog(string message)
        {
            Action<string> handler = Log;
            if (handler != null) handler(message);
        }
    }

    internal static class Logger
    {
        private static readonly object Sync = new object();
        private static readonly string LogPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.log");

        public static void Write(string message)
        {
            string line = "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + "] " + message;
            Console.WriteLine(line);

            try
            {
                lock (Sync) File.AppendAllText(LogPath, line + Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось записать лог: " + e.Message);
            }
        }
    }
}
