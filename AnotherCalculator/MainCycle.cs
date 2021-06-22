using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using AnotherCalculator.SequenceTypes;

namespace AnotherCalculator
{
    class MainCycle
    {
        TreeProcessing _tree = new TreeProcessing();
        bool isProgramWorkin;
        string forbiddenChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ,./?;:'\"\\|~`_=!@#$%%^&*";
        private string errorString;
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
                var nestedStringsTree = _tree.GenerateTree(rawInput);
                Console.WriteLine("Результат = " + result + " \ndebug: " +rawInput);
                ShowErrorText();
            }
        }

        private object GenerateNestedTree(string rawInput)
        {
            NestedStringElement nestedTree = new NestedStringElement();
            // строение узла: строка, уровень вложенности, лист экземпляров этого же типа
            

            nestedTree = _tree.GenerateTree(rawInput);
            
            return new List<NestedStringElement>();
        }

        
        private void CheckFormatOfInput(string rawInput)
        {
            rawInput = rawInput.ToUpper();
            char[] forbiddenCharsArray = forbiddenChars.ToCharArray();
            int checkResult = rawInput.LastIndexOfAny(forbiddenCharsArray);
            if (checkResult != -1)
            {
                isProgramWorkin = false;
                SaveError("Неправильный формат введенных данных");
            }
            int countOpens = rawInput.Count(f => f == '(');
            int countCloses = rawInput.Count(f => f == ')');
            if (countOpens != countCloses)
            {
                isProgramWorkin = false;
                SaveError("Количество закрывающих и открывающих скобок неодинаково, открывающих: " + countOpens + ", закрывающих: " + countCloses);
                isProgramWorkin = false;
            }
        }

        private string NormaliseRawInput(string rawInput)
        {
            rawInput = rawInput.Replace(" ", "");
            var rawInputNormalised = rawInput.ToUpper();
            return rawInputNormalised;
        }

        private void ShowErrorText()
        {
            if (errorString is null)
            {
                return;
            }
            else
            {
                Console.WriteLine(errorString);
            }
        }

        private void SaveError(string errorText)
        {
            errorString = errorText + " " + errorString;
        }
    }
}
