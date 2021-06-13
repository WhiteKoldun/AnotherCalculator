using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherCalculator
{
    class MainCycle
    {
        public void Cycle()
        {
            for (bool isProgramWorkin = true; isProgramWorkin;)
            {
                Console.WriteLine("Введите новое выражение, введите \"q\" для завершения:");
                var rawInput = Console.ReadLine();
                if (rawInput.ToUpper() == "Q")
                {
                    isProgramWorkin = false;
                }
            }
        }
    }
}
