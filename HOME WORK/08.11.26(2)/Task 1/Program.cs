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

            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("нет пути");
                Console.ReadKey();
                return;
            }

            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = false
            };

            try
            {
                using (Process p = Process.Start(info))
                {
                    Console.WriteLine("процесс" + p.Id + "запущен ");
                    p.WaitForExit();
                    Console.WriteLine("код завершения: " + p.ExitCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ошибка:" + ex.Message);
            }

            Console.WriteLine("\nнажмите что-то для завершения");
            Console.ReadKey();
        }
    }
}
