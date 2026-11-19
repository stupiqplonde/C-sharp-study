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
        private int peopleAtStop = 0;
        private readonly object gate = new object(); 
        private volatile bool running = false;

        private const int BusCount = 3;
        private const int BusCapacity = 20;
        private const int BusNumber = 175;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            running = true;
            peopleAtStop = 0;
            lstLog.Items.Clear();

            Thread peopleThread = new Thread(PeopleArrive);
            peopleThread.IsBackground = true;
            peopleThread.Start();

            for (int i = 0; i < BusCount; i++)
            {
                int busId = i + 1;
                Thread busThread = new Thread(() => BusLoop(busId));
                busThread.IsBackground = true;
                busThread.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            running = false;
        }

        private void PeopleArrive()
        {
            Random rnd = new Random();
            while (running)
            {
                int newPeople = rnd.Next(1, 10);
                lock (gate)
                {
                    peopleAtStop += newPeople;
                }
                Log($"Пришло {newPeople} человек, на остановке: {GetPeople()}");
                Thread.Sleep(500);
            }
        }

        private void BusLoop(int busId)
        {
            while (running)
            {
                Thread.Sleep(1000);

                int taken;
                lock (gate)
                {
                    taken = Math.Min(peopleAtStop, BusCapacity);
                    peopleAtStop -= taken;
                }

                Log($"Автобус {BusNumber} (рейс {busId}) забрал {taken}. Осталось: {GetPeople()}");
            }
        }
        private int GetPeople()
        {
            lock (gate) { return peopleAtStop; }
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
