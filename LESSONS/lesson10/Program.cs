using System;

namespace ConsoleApp1
{
    enum TrafficLightColor
    {
        Red = 1,
        Yellow,
        Green
    }

    class Program
    {
        static void Main()
        {
            TrafficLightColor currentColor = TrafficLightColor.Red;

            while (true)
            {
                Console.WriteLine($"Текущий цвет светофора: {currentColor}");
                Console.WriteLine("Нажмите Enter, чтобы переключить цвет");
                Console.ReadLine();

                currentColor = SwitchColor(currentColor);
                Console.WriteLine($"Переключено на: {currentColor}");
                Console.WriteLine();
            }
        }

        static TrafficLightColor SwitchColor(TrafficLightColor color)
        {
            switch (color)
            {
                case TrafficLightColor.Red:
                    return TrafficLightColor.Yellow;
                case TrafficLightColor.Yellow:
                    return TrafficLightColor.Green;
                default:
                    return TrafficLightColor.Red;
            }
        }
    }
}