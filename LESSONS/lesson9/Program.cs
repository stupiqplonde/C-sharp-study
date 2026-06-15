using System;

namespace ConsoleApp1
{
//     interface IFigure
//     {
//         double GetArea();
//         double GetPerimeter();
//     }
//
//     class Circle : IFigure
//     {
//         public double r;
//
//         public Circle(double r) { this.r = r; }
//
//         public double GetArea() => Math.PI * r * r;
//         public double GetPerimeter() => 2 * Math.PI * r;
//     }
//
//     class Square : IFigure
//     {
//         public double a;
//
//         public Square(double a) { this.a = a; }
//
//         public double GetArea() => a * a;
//         public double GetPerimeter() => 4 * a;
//     }
//     
//     class Triangle : IFigure
//     {
//         public double a, b, c;
//
//         public Triangle(double a, double b, double c)
//         {
//             this.a = a;
//             this.b = b;
//             this.c = c;
//         }
//
//         public double GetArea() => a * b / 2;
//         public double GetPerimeter() => a + b + c;
//     }
//
//     class Program
//     {
//         static void Main()
//         {
//             IFigure krug = new Circle(5);
//             IFigure kvadrat = new Square(4);
//             IFigure treugolnik = new Triangle(3, 4, 5);
//
//             Console.WriteLine(krug.GetArea());
//             Console.WriteLine(kvadrat.GetArea());
//             Console.WriteLine(treugolnik.GetArea());
//         }
//     }

    // interface IVehicle
    // {
    //     void Start();
    //     void Stop();
    //     double GetSpeed();
    // }
    //
    // class Car : IVehicle
    // {
    //     public double speed;
    //
    //     public void Start() { speed = 100; }
    //     public void Stop() { speed = 0; }
    //     public double GetSpeed() => speed;
    // }
    //
    // class Bicycle : IVehicle
    // {
    //     public double speed;
    //
    //     public void Start() { speed = 20; }
    //     public void Stop() { speed = 0; }
    //     public double GetSpeed() => speed;
    // }
    //
    // class Train : IVehicle
    // {
    //     public double speed;
    //
    //     public void Start() { speed = 200; }
    //     public void Stop() { speed = 0; }
    //     public double GetSpeed() => speed;
    // }
    //
    // class Airplane : IVehicle
    // {
    //     public double speed;
    //
    //     public void Start() { speed = 800; }
    //     public void Stop() { speed = 0; }
    //     public double GetSpeed() => speed;
    // }
    //
    // class Program
    // {
    //     static void Main()
    //     {
    //         IVehicle car = new Car();
    //         IVehicle bicycle = new Bicycle();
    //         IVehicle train = new Train();
    //         IVehicle airplane = new Airplane();
    //
    //         car.Start();
    //         bicycle.Start();
    //         train.Start();
    //         airplane.Start();
    //
    //         Console.WriteLine(car.GetSpeed());
    //         Console.WriteLine(bicycle.GetSpeed());
    //         Console.WriteLine(train.GetSpeed());
    //         Console.WriteLine(airplane.GetSpeed());
    //
    //         car.Stop();
    //         Console.WriteLine(car.GetSpeed());
    //     }
    // }

    // interface ISorteble
    // {
    //     void BubleSort();
    //     void SelectSort();
    // }
    //
    // class Array1D : ISorteble
    // {
    //     public int[] data;
    //
    //     public Array1D(int[] data) { this.data = data; }
    //
    //     public void BubleSort()
    //     {
    //         for (int i = 0; i < data.Length - 1; i++)
    //             for (int j = 0; j < data.Length - 1; j++)
    //                 if (data[j] > data[j + 1])
    //                 {
    //                     int t = data[j];
    //                     data[j] = data[j + 1];
    //                     data[j + 1] = t;
    //                 }
    //     }
    //
    //     public void SelectSort()
    //     {
    //         for (int i = 0; i < data.Length - 1; i++)
    //             for (int j = i + 1; j < data.Length; j++)
    //                 if (data[j] < data[i])
    //                 {
    //                     int t = data[i];
    //                     data[i] = data[j];
    //                     data[j] = t;
    //                 }
    //     }
    // }
    //
    // class Array2D : ISorteble
    // {
    //     public int[][] data;
    //
    //     public Array2D(int[][] data) { this.data = data; }
    //
    //     public void BubleSort()
    //     {
    //         int[] arr = data[0];
    //         for (int i = 0; i < arr.Length - 1; i++)
    //             for (int j = 0; j < arr.Length - 1; j++)
    //                 if (arr[j] > arr[j + 1])
    //                 {
    //                     int t = arr[j];
    //                     arr[j] = arr[j + 1];
    //                     arr[j + 1] = t;
    //                 }
    //     }
    //
    //     public void SelectSort()
    //     {
    //         int[] arr = data[0];
    //         for (int i = 0; i < arr.Length - 1; i++)
    //             for (int j = i + 1; j < arr.Length; j++)
    //                 if (arr[j] < arr[i])
    //                 {
    //                     int t = arr[i];
    //                     arr[i] = arr[j];
    //                     arr[j] = t;
    //                 }
    //     }
    // }
    //
    // class StringArray : ISorteble
    // {
    //     public string[] data;
    //
    //     public StringArray(string[] data) { this.data = data; }
    //
    //     public void BubleSort()
    //     {
    //         for (int i = 0; i < data.Length - 1; i++)
    //             for (int j = 0; j < data.Length - 1; j++)
    //                 if (data[j].CompareTo(data[j + 1]) > 0)
    //                 {
    //                     string t = data[j];
    //                     data[j] = data[j + 1];
    //                     data[j + 1] = t;
    //                 }
    //     }
    //
    //     public void SelectSort()
    //     {
    //         for (int i = 0; i < data.Length - 1; i++)
    //             for (int j = i + 1; j < data.Length; j++)
    //                 if (data[j].CompareTo(data[i]) < 0)
    //                 {
    //                     string t = data[i];
    //                     data[i] = data[j];
    //                     data[j] = t;
    //                 }
    //     }
    // }
    //
    // class Program
    // {
    //     static void Main()
    //     {
    //         ISorteble one = new Array1D(new int[] { 5, 2, 8, 1 });
    //         ISorteble two = new Array2D(new int[][] { new int[] { 9, 3 }, new int[] { 1, 7 } });
    //         ISorteble words = new StringaArray(new string[] { "яблоко", "ананас", "банан" });
    //
    //         one.BubleSort();
    //         two.SelectSort();
    //         words.BubleSort();
    //
    //         Array1D a = (Array1D)one;
    //         Array2D b = (Array2D)two;
    //         StringArray c = (StringArray)words;
    //
    //         foreach (int x in a.data) Console.Write(x + " ");
    //         Console.WriteLine();
    //         foreach (int x in b.data[0]) Console.Write(x + " ");
    //         Console.WriteLine();
    //         foreach (string s in c.data) Console.Write(s + " ");
    //     }
    // }

    interface IPayment
    {
        string Name { get; set; }
        double Balance { get; set; }
        void Deposit(double sum);   
        void Withdraw(double sum);  
    }

    class Card : IPayment
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Card(string name)
        {
            Name = name;
            Balance = 0;
        }

        public void Deposit(double sum) { Balance += sum; }
        public void Withdraw(double sum) { Balance -= sum; }
    }

    class Cash : IPayment
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Cash(string name)
        {
            Name = name;
            Balance = 0;
        }

        public void Deposit(double sum) { Balance += sum; }
        public void Withdraw(double sum) { Balance -= sum; }
    }

    class Crypto : IPayment
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Crypto(string name)
        {
            Name = name;
            Balance = 0;
        }

        public void Deposit(double sum) { Balance += sum; }
        public void Withdraw(double sum) { Balance -= sum; }
    }

    class Program
    {
        static void Main()
        {
            IPayment card = new Card("Карта");
            IPayment cash = new Cash("Наличные");
            IPayment crypto = new Crypto("Bitcoin");

            card.Deposit(1000);
            card.Withdraw(200);

            cash.Deposit(500);
            crypto.Deposit(0.5);

            Console.WriteLine(card.Name + " " + card.Balance);
            Console.WriteLine(cash.Name + " " + cash.Balance);
            Console.WriteLine(crypto.Name + " " + crypto.Balance);
        }
    }
}
