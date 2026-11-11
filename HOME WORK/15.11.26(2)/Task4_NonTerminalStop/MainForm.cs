using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task4_NonTerminalStop
{
    public class MainForm : Form
    {
        private const int BusCount = 5;
        private const int Capacity = 40;
        private const int SimulationTicks = 120;
        private readonly ListBox logBox = new() { Dock = DockStyle.Fill, HorizontalScrollbar = true };
        private readonly Button startButton = new() { Text = "Начать моделирование дня", AutoSize = true };
        private readonly Label status = new() { AutoSize = true, Text = "Автобусов: 5; вместимость каждого: 40 пассажиров." };
        private bool running;

        public MainForm()
        {
            Text = "Задание 4 — Не конечная автобусная остановка";
            Width = 850; Height = 540; MinimumSize = new Size(650, 400);
            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 55, Padding = new Padding(10), WrapContents = false };
            top.Controls.Add(startButton);
            top.Controls.Add(status);
            Controls.Add(logBox);
            Controls.Add(top);
            startButton.Click += async (_, _) => await StartSimulationAsync();
        }

        private void AddLog(string message)
        {
            if (IsDisposed || !IsHandleCreated) return;
            try
            {
                BeginInvoke(new Action(() =>
                {
                    if (!IsDisposed)
                    {
                        logBox.Items.Add(message);
                        logBox.TopIndex = logBox.Items.Count - 1;
                    }
                }));
            }
            catch (InvalidOperationException) { }
        }

        private async Task StartSimulationAsync()
        {
            if (running) return;
            running = true;
            startButton.Enabled = false;
            logBox.Items.Clear();
            status.Text = "Моделируется один день...";
            try
            {
                await Task.Run(() => SimulateDay(AddLog));
                status.Text = "Моделирование завершено.";
            }
            catch (Exception ex) { status.Text = "Ошибка: " + ex.Message; }
            finally
            {
                running = false;
                startButton.Enabled = true;
            }
        }

        private static void SimulateDay(Action<string> log)
        {
            var sync = new object();
            var waiting = new List<int>();
            using var stopEvent = new ManualResetEvent(false);
            using var passengerEvent = new AutoResetEvent(false);
            var randomSeed = Environment.TickCount;
            var randomLock = new object();
            var random = new Random(randomSeed);
            int totalArrived = 0;
            int totalBoarded = 0;

            int[] routes = new[] { 175, 12, 42 };
            log("Начало моделирования. Один условный день разбит на 120 временных интервалов.");
            log($"На остановке работают {BusCount} автобусов; вместимость каждого — {Capacity}.");

            Thread passengerThread = new(() =>
            {
                for (int tick = 1; tick <= SimulationTicks && !stopEvent.WaitOne(0); tick++)
                {
                    int added;
                    lock (randomLock) added = random.Next(0, 9);
                    lock (sync)
                    {
                        for (int i = 0; i < added; i++)
                        {
                            int route = routes[random.Next(routes.Length)];
                            waiting.Add(route);
                        }
                        totalArrived += added;
                    }
                    if (added > 0)
                    {
                        log($"Интервал {tick}: пришло пассажиров — {added}. На остановке всего: {CountWaiting(waiting, sync)}.");
                        passengerEvent.Set();
                    }
                    Thread.Sleep(100);
                }
                log("Поток прибытия пассажиров завершил работу.");
                stopEvent.Set();
                passengerEvent.Set();
            });

            Thread[] buses = Enumerable.Range(0, BusCount).Select(busIndex => new Thread(() =>
            {
                int route = routes[busIndex % routes.Length];
                int trip = 0;
                while (!stopEvent.WaitOne(0))
                {
                    int delay;
                    lock (randomLock) delay = random.Next(700, 1501);
                    int signal = WaitHandle.WaitAny(new WaitHandle[] { passengerEvent, stopEvent }, delay);
                    if (signal == 1 || stopEvent.WaitOne(0)) break;

                    trip++;
                    int boarded;
                    int waitingForRoute;
                    lock (sync)
                    {
                        waitingForRoute = waiting.Count(p => p == route);
                        int freeSeats = Capacity;
                        int onboard = random.Next(0, Capacity + 1); int alighted = random.Next(0, onboard + 1); onboard -= alighted; int freeSeats = Capacity - onboard;
                        boarded = Math.Min(freeSeats, waitingForRoute);
                        int removed = 0;
                        for (int i = waiting.Count - 1; i >= 0 && removed < boarded; i--)
                        {
                            if (waiting[i] == route) { waiting.RemoveAt(i); removed++; }
                        }
                        totalBoarded += boarded;
                    }
                    log($"Автобус №{route} (машина {busIndex + 1}), рейс {trip}: свободных мест было {Capacity}; пассажиров нужного маршрута ожидало {waitingForRoute}; вошло {boarded}; осталось на остановке: {CountWaiting(waiting, sync)}.");
                }
                log($"Автобус №{route}, машина {busIndex + 1}: завершил работу.");
            })).ToArray();

            passengerThread.Start();
            foreach (var bus in buses) bus.Start();
            passengerThread.Join();
            stopEvent.Set();
            passengerEvent.Set();
            foreach (var bus in buses) bus.Join();

            int remaining;
            lock (sync) remaining = waiting.Count;
            log("Итоги дня:");
            log($"Пассажиров пришло: {totalArrived}.");
            log($"Пассажиров уехало: {totalBoarded}.");
            log($"Пассажиров осталось ждать: {remaining}.");
        }

        private static int CountWaiting(List<int> waiting, object sync)
        {
            lock (sync) return waiting.Count;
        }
    }
}
