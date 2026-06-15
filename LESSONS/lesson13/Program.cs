using System;
using System.Numerics;

namespace ConsoleApp1
{
    class Pair<T1, T2>
    {
        public T1 First { get; set; }
        public T2 Second { get; set; }
        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
        public override string ToString() => $"{First} -> {Second}";

        public static Pair<T, T> Sum<T>(Pair<T, T> left, Pair<T, T> right) where T : INumber<T>
            => new(left.First + right.First, left.Second + right.Second);
    }

    class Stack<T>
    {
        private readonly List<T> _items = new();

        public int Count => _items.Count;
        public bool IsEmpty => _items.Count == 0;

        public void Push(T item) => _items.Add(item);

        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Стек пуст.");
            int last = _items.Count - 1;
            T item = _items[last];
            _items.RemoveAt(last);
            return item;
        }

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Стек пуст.");
            return _items[^1];
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Pair<string, int> student = new Pair<string, int>("Иван", 20);
            // Console.WriteLine(student);
            // Pair<double, double> point1 = new Pair<double, double>(3.5, 4.5);
            // Pair<double, double> point2 = new Pair<double, double>(1.0, 0.5);
            // Console.WriteLine($"double: {point1} + {point2} = {Pair<double, double>.Sum(point1, point2)}");
            //
            // Pair<int, int> nums1 = new Pair<int, int>(10, 20);
            // Pair<int, int> nums2 = new Pair<int, int>(5, 3);
            // Console.WriteLine($"int: {nums1} + {nums2} = {Pair<int, int>.Sum(nums1, nums2)}");
            //
            // Console.WriteLine($"числа: 7 + 3 = {Add(7, 3)}");

            Stack<int> stack = new Stack<int>();
            Console.WriteLine($"стек пуст: {stack.IsEmpty}, элементов: {stack.Count}");

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            Console.WriteLine($"после Push(10, 20, 30): Count = {stack.Count}, IsEmpty = {stack.IsEmpty}");
            Console.WriteLine($"Peek = {stack.Peek()}");
            Console.WriteLine($"Pop = {stack.Pop()}");
            Console.WriteLine($"после Pop: Count = {stack.Count}, Peek = {stack.Peek()}");
            Console.WriteLine($"Pop = {stack.Pop()}, Pop = {stack.Pop()}");
            Console.WriteLine($"стек пуст: {stack.IsEmpty}, элементов: {stack.Count}");
        }

        static T Add<T>(T left, T right) where T : INumber<T> => left + right;
        
        
        
    }
}