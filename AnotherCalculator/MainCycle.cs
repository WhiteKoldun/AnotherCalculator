using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherCalculator
{
    class MainCycle
    {
        bool isProgramWorkin;
        string forbiddenChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ,./?;:'\"\\|~`_=!@#$%%^&*";

        public void Cycle()
        {
            for (isProgramWorkin = true; isProgramWorkin;)
            {
                Console.WriteLine("Введите новое выражение, введите \"q\" для завершения:");
                var rawInput = Console.ReadLine();
                if (rawInput.ToUpper() == "Q")
                {
                    isProgramWorkin = false;
                }
                int result = new int();
                CheckFormatOfInput(rawInput);
                rawInput = NormaliseRawInput(rawInput);
                Console.WriteLine("Результат = " + result + "debug: " +rawInput);
            }
        }

        private void CheckFormatOfInput(string rawInput)
        {
            rawInput.ToUpper();
            char[] forbiddenCharsArray = forbiddenChars.ToCharArray();
            int checkResult = rawInput.LastIndexOfAny(forbiddenCharsArray);
            if (checkResult != -1)
            {
                isProgramWorkin = false;
            }


        }

        private string NormaliseRawInput(string rawInput)
        {
            rawInput = rawInput.Replace(" ", "");
            var rawInputNormalised = rawInput.ToUpper();
            return rawInputNormalised;
        }
    }
}
