using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<int, int> waitingByBus = new Dictionary<int, int>();   
        private readonly Dictionary<int, int> onBoardByBus = new Dictionary<int, int>();   // в салоне автобуса 
        private readonly Dictionary<int, int> totalTransported = new Dictionary<int, int>(); // всего перевезено
        private readonly object gate = new object();

        private readonly int[] busNumbers = { 175, 42, 7 };
        private const int BusCapacity = 20;
        private const int BusCount = 3;

        private volatile bool running = false;
        private Thread peopleThread;
        private List<Thread> busThreads = new List<Thread>();

        public Form1()
        {
            InitializeComponent();
            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvState.Columns.Clear();
            dgvState.Columns.Add("num", "Автобус");
            dgvState.Columns.Add("wait", "Ждут");
            dgvState.Columns.Add("board", "В салоне");
            dgvState.Columns.Add("total", "Перевезено");
            dgvState.AllowUserToAddRows = false;
            dgvState.ReadOnly = true;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (running) return;
            running = true;

            lock (gate)
            {
                waitingByBus.Clear();
                onBoardByBus.Clear();
                totalTransported.Clear();
                foreach (int num in busNumbers)
                {
                    waitingByBus[num] = 0;
                    onBoardByBus[num] = 0;
                    totalTransported[num] = 0;
                }
            }
            lstLog.Items.Clear();
            dgvState.Rows.Clear();

            peopleThread = new Thread(PeopleLoop) { IsBackground = true };
            peopleThread.Start();

            busThreads.Clear();
            for (int i = 0; i < BusCount; i++)
            {
                int num = busNumbers[i];
                Thread t = new Thread(() => BusLoop(num)) { IsBackground = true };
                t.Start();
                busThreads.Add(t);
            }

            Log("Запущено");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            running = false;
            Log("остановка. Дождитесь завершения потоков");
        }

        private void PeopleLoop()
        {
            Random rnd = new Random();
            while (running)
            {
                int count = rnd.Next(1, 6);
                int busNum = busNumbers[rnd.Next(busNumbers.Length)];

                lock (gate)
                {
                    waitingByBus[busNum] += count;
                }

                Log($"Пришло {count} чел. на автобус {busNum}. Ждут {busNum}: {GetWaiting(busNum)}");
                SafeRefreshGrid();
                Thread.Sleep(500);
            }
        }

        private void BusLoop(int busNum)
        {
            Random rnd = new Random(busNum * 13 + Environment.TickCount);

            lock (gate)
            {
                onBoardByBus[busNum] = rnd.Next(0, BusCapacity + 1);
            }

            while (running)
            {
                Thread.Sleep(1500); 

                int exited = 0, taken = 0, onBoardNow = 0;

                lock (gate)
                {
                    exited = rnd.Next(0, onBoardByBus[busNum] + 1);
                    onBoardByBus[busNum] -= exited;

                    int freeSeats = BusCapacity - onBoardByBus[busNum];
                    taken = Math.Min(waitingByBus[busNum], freeSeats);

                    waitingByBus[busNum] -= taken;
                    onBoardByBus[busNum] += taken;
                    totalTransported[busNum] += taken;

                    onBoardNow = onBoardByBus[busNum];
                }

                Log($"Автобус {busNum}: вышло {exited}, село {taken}, " +
                    $"в салоне {onBoardNow}, ждут {busNum}: {GetWaiting(busNum)}");
                SafeRefreshGrid();
            }
        }

        private int GetWaiting(int busNum)
        {
            lock (gate) { return waitingByBus[busNum]; }
        }

        private void SafeRefreshGrid()
        {
            if (InvokeRequired)
                Invoke(new Action(RefreshGrid));
            else
                RefreshGrid();
        }

        private void RefreshGrid()
        {
            dgvState.Rows.Clear();
            lock (gate)
            {
                foreach (int num in busNumbers)
                {
                    dgvState.Rows.Add(num, waitingByBus[num], onBoardByBus[num], totalTransported[num]);
                }
            }
        }

        private void Log(string msg)
        {
            if (InvokeRequired)
                Invoke(new Action(() => lstLog.Items.Add(msg)));
            else
                lstLog.Items.Add(msg);
        }
    }
}
