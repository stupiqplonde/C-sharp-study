using System;
namespace ConsoleApp1
{
    internal class Program
    {

        class TransportDelivery
        {
            public string modelName = string.Empty;
            public double allowedWeightKg;
            public bool isAvailableNow;
            public int transportCount;
            public string transportType = string.Empty;
            public double coordX;
            public double coordY;

            private static readonly Random rng = new Random();

            protected void SetRandomCoords(double min = -1000, double max = 1000)
            {
                coordX = rng.NextDouble() * (max - min) + min;
                coordY = rng.NextDouble() * (max - min) + min;
            }

            public virtual void ShowInfo()
            {
                Console.WriteLine($"Тип транспорта: {transportType}");
                Console.WriteLine($"Название модели: {modelName}");
                Console.WriteLine($"Допустимый вес: {allowedWeightKg} кг");
                Console.WriteLine($"Доступен сейчас: {(isAvailableNow ? "Да" : "Нет")}");
                Console.WriteLine($"Количество транспорта: {transportCount}");
                Console.WriteLine($"Координаты: X={coordX:0.##}, Y={coordY:0.##}");
            }
        }

        class Truck : TransportDelivery
        {
            public Truck()
            {
                transportType = "Грузовик";
                modelName = "Volvo";
                allowedWeightKg = 25000;
                isAvailableNow = true;
                transportCount = 8;
                SetRandomCoords();
            }
        }

        class Ship : TransportDelivery
        {
            public Ship()
            {
                transportType = "Корабль";
                modelName = "Maersk";
                allowedWeightKg = 180000000;
                isAvailableNow = false;
                transportCount = 2;
                SetRandomCoords();
            }
        }

        class Drone : TransportDelivery
        {
            public Drone()
            {
                transportType = "Дрон";
                modelName = "DJI";
                allowedWeightKg = 40;
                isAvailableNow = true;
                transportCount = 15;
                SetRandomCoords();
            }
        }

        class Train : TransportDelivery
        {
            public Train()
            {
                transportType = "Поезд";
                modelName = "Siemens";
                allowedWeightKg = 600000;
                isAvailableNow = true;
                transportCount = 4;
                SetRandomCoords();
            }
        }

        static double Distance(TransportDelivery t, double x, double y)
        { 
            double dx = t.coordX - x;
            double dy = t.coordY - y;
            return Math.Sqrt(dx * dx + dy * dy);
        }


        static void Main(string[] args)
        {
            TransportDelivery truck = new Truck();
            TransportDelivery ship = new Ship();
            TransportDelivery drone = new Drone();
            TransportDelivery train = new Train();

            Console.WriteLine("Выберите транспорт для просмотра полной информации:");
            Console.WriteLine("1 - Truck");
            Console.WriteLine("2 - Ship");
            Console.WriteLine("3 - Drone");
            Console.WriteLine("4 - Train");
            Console.WriteLine("5 - Какой транспорт ближе к координатам");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    truck.ShowInfo();
                    break;
                case "2":
                    ship.ShowInfo();
                    break;
                case "3":
                    drone.ShowInfo();
                    break;
                case "4":
                    train.ShowInfo();
                    break;
                case "5":
                    Console.Write("Введите X координату места: ");
                    if (!double.TryParse(Console.ReadLine(), out double placeX))
                    {
                        Console.WriteLine("Некорректный ввод X.");
                        break;
                    }

                    Console.Write("Введите Y координату места: ");
                    if (!double.TryParse(Console.ReadLine(), out double placeY))
                    {
                        Console.WriteLine("Некорректный ввод Y.");
                        break;
                    }

                    double dTruck = Distance(truck, placeX, placeY);
                    double dShip = Distance(ship, placeX, placeY);
                    double dDrone = Distance(drone, placeX, placeY);
                    double dTrain = Distance(train, placeX, placeY);

                    TransportDelivery nearest = truck;
                    double minDist = dTruck;
                    if (dShip < minDist) { nearest = ship; minDist = dShip; }
                    if (dDrone < minDist) { nearest = drone; minDist = dDrone; }
                    if (dTrain < minDist) { nearest = train; minDist = dTrain; }

                    Console.WriteLine($"Ближе всего: {nearest.transportType} ({nearest.modelName})");
                    Console.WriteLine($"Расстояние: {minDist:0.##}");
                    Console.WriteLine();
                    nearest.ShowInfo();

                    Console.WriteLine();
                    Console.WriteLine("Расстояния до всех транспортов:");
                    Console.WriteLine($"Truck: {dTruck:0.##}");
                    Console.WriteLine($"Ship: {dShip:0.##}");
                    Console.WriteLine($"Drone: {dDrone:0.##}");
                    Console.WriteLine($"Train: {dTrain:0.##}");
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Введите число от 1 до 5.");
                    break;
            }
        }
    }
}
