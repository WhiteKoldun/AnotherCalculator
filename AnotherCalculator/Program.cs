using System;

namespace AnotherCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Приветствую в приложении \"Очередной калькулятор\"! Составляйте выражения из целых чисел, знаков сложения и вычитания и скобок!");
            MainCycle mainCycle = new MainCycle();
            mainCycle.Cycle();
            Console.ReadKey();
        }

        
    }
}
