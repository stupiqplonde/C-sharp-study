using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("путь до exe: ");
            string path = Console.ReadLine();

            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = false
            };

            try
            {
                using (Process p = Process.Start(info))
                {
                    Console.WriteLine("процесс" + p.Id + "запущен");
                    Console.WriteLine("1 - ждать завершения");
                    Console.WriteLine("2 - принудительно завершить");
                    Console.Write("выберите: ");
                    string mode = Console.ReadLine();

                    if (mode == "1")
                    {
                        Console.WriteLine("ждем конца");
                        p.WaitForExit();
                        Console.WriteLine("код завершения: " + p.ExitCode);
                    }
                    else if (mode == "2")
                    {
                        if (!p.HasExited)
                        {
                            p.Kill();

                            p.WaitForExit();
                            Console.WriteLine("процесс принудительно завершён");
                        }
                    }
                    else
                    {
                        Console.WriteLine("неверно");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ошибка: " + ex.Message);
            }

            Console.WriteLine("\nнажмите чтобы выйти");
            Console.ReadKey();
        }
    }
}
