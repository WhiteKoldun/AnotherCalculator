using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using AnotherCalculator.Dto;
using Microsoft.VisualBasic;

namespace AnotherCalculator
{
    public class TreeProcessing
    {
        public string TransformString(string input)
        {
            string result = input;
            var positionTest = input.Remove(5);
            var processString = input;
            do
            {
                var maxNested = GetMaxNestedExpression(input);
                input = ReplaceExpression(input, maxNested);
            } while (IsContainsParenthesis(processString));

            return result;
        }

        private string ReplaceExpression(string changingString, CuttedExpression replaceData)
        {
            string calculationResult = Calculate(replaceData.Expression);
            changingString = changingString.Remove(replaceData.StartPosition, replaceData.EndPosition - replaceData.StartPosition);
            changingString = changingString.Insert(replaceData.StartPosition, calculationResult);
            return changingString;
        }

        private string Calculate(string expression)
        {
            throw new NotImplementedException();
        }

        private CuttedExpression GetMaxNestedExpression(string nestedString)
        {
            int openCoord = 0;
            int closeCoord = 0;
            int iterationCoord = 0;
            int parenthesisCount = 0;
            int deltaStart = 0;
            string oldNestedString;

            // двигаемся вглубь скобок, пока скобок не останется
            do
            {
                oldNestedString = nestedString;
                // находим первую скобку
                if (IsContainsParenthesis(nestedString) && IsOpenedParenthesis(nestedString))
                {
                    parenthesisCount = 0;
                }
                else
                {
                    return null;
                }

                var processInput = nestedString;
                iterationCoord = FindParenthesis(processInput);
                openCoord = iterationCoord;
                // цикл получения координат скобок 
                do
                {
                    if (IsOpenedParenthesis(processInput))
                    {
                        parenthesisCount = parenthesisCount + 1;
                    }
                    else
                    {
                        parenthesisCount = parenthesisCount - 1;
                    }

                    if (parenthesisCount == 0)
                    {
                        closeCoord = iterationCoord + 1;
                    }

                    processInput = processInput.Remove(iterationCoord, 1);
                    processInput = processInput.Insert(iterationCoord, " ");

                    iterationCoord = FindParenthesis(processInput);
                } while (parenthesisCount > 0);

                // отрезаем то, что снаружи скобок
                nestedString = nestedString.Remove(closeCoord, nestedString.Length - closeCoord);
                nestedString = nestedString.Remove(0, openCoord);
                deltaStart = deltaStart + openCoord;
                
                // проверка на неизменяемость строки
                if (oldNestedString.Length == nestedString.Length)
                {
                    nestedString = nestedString.Remove(0, 1).Insert(0,"");
                    nestedString = nestedString.Remove(nestedString.Length - 1, 1).Insert(nestedString.Length - 1, "");
                    deltaStart += 1;
                }
            } while (IsContainsParenthesis(nestedString));

            var returnExpression = new CuttedExpression();
            returnExpression.Expression = nestedString;
            returnExpression.StartPosition = deltaStart - 1;
            returnExpression.EndPosition = deltaStart + nestedString.Length;
            return returnExpression;
        }

        private bool IsContainsParenthesis(string stringe)
        {
            if (stringe.Contains("(") || stringe.Contains(")"))
            {
                return true;
            }

            return false;
        }

        private int FindParenthesis(string input)
        {
            char openParenth = '(';
            char closenParenth = ')';
            int positionOpenParenthesis = input.IndexOf(openParenth);
            int positionCloseParenthesis = input.IndexOf(closenParenth);
            if (positionOpenParenthesis == -1)
            {
                return positionCloseParenthesis;
            }

            if (positionOpenParenthesis > positionCloseParenthesis)
            {
                return positionCloseParenthesis;
            }

            return positionOpenParenthesis;
        }

        private bool IsOpenedParenthesis(string input)
        {
            int position = FindParenthesis(input);
            if (position >= 0)
            {
                if (input.Substring(position, 1) == "(")
                {
                    return true;
                }
            }

            return false;
        }

    }
}
