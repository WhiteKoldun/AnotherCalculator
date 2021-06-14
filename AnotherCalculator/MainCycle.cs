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
                var nestedStringsTree = GenerateNestedTree(rawInput);
                Console.WriteLine("Результат = " + result + " \ndebug: " +rawInput);
                ShowErrorText();
            }
        }

        private object GenerateNestedTree(string rawInput)
        {
            NestedStringElement nestedTree = new NestedStringElement();
            // строение узла: строка, уровень вложенности, лист экземпляров этого же типа
            

            nestedTree = TreeProcessing(rawInput);
            
            return new List<NestedStringElement>();
        }

        private NestedStringElement TreeProcessing(string rawInput)
        {
            NestedStringElement nestedTree = new NestedStringElement();
            int nodeCount = rawInput.Count(f => f == '(');
            if (nodeCount == 0)
            {
                nestedTree.NestedString = rawInput;
                nestedTree.NestedLevel = 0;
                return nestedTree;
            }

            var bufferInput = rawInput;
            int nestedLevel = 0;
            while (bufferInput.Length > 0)
            {
                // удаляем вложенные выражения со скобками

                if (ContainsParenthesis(bufferInput))
                {
                    var currentInput = RemoveParenthesis(bufferInput);
                    NestedStringElement childInput = GetAllParenthesis(bufferInput);
                    nestedTree.NestedString = currentInput;
                    nestedTree.NestedLevel = nestedLevel;
                }
                else
                {

                }

            }

            return nestedTree;
        }

        private string RemoveParenthesis(string bufferInput)
        {
            int? openCoord = 0;
            int? closeCoord = 0;
            int iterationCoord = 0;
            int? parenthesisCount = 0;
            var processinput = bufferInput;
            while (IsContainsParenthesis(processinput))
            {
                iterationCoord = FindParenthesis(processinput, '(', iterationCoord);
                openCoord = iterationCoord;
                while (IsContainsParenthesis(processinput))
                {
                    
                }
            }

        }

        private bool IsContainsParenthesis(string stringe)
        {
            if (stringe.Contains("(") || stringe.Contains(")"))
            {
                return true;
            }

            return false;
        }

        private int FindParenthesis(string stringe, char parenthesis, int iterationCoord)
        {
            stringe = stringe.Remove(0, iterationCoord - 1);
            return stringe.IndexOf(parenthesis);
        }
        private int FindParenthesis(string stringe, int iterationCoord)
        {
            stringe = stringe.Remove(0, iterationCoord - 1);
            return stringe.IndexOf(parenthesis);
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
