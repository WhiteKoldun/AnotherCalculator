using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace AnotherCalculator
{
    public class TreeProcessing
    {


        private string RemoveExpression(string nestedString)
            // вызывается только после проверки на наличие скобок
        {
            int openCoord = 0;
            int closeCoord = 0;
            int iterationCoord = 0;
            int parenthesisCount = 0;
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
            // цикл обрезки одной скобки (и всего, что в нее вложено)
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

            nestedString = nestedString.Remove(openCoord, closeCoord - openCoord);
            return nestedString;
        }

        private string GetExpression(string nestedString)
        {
                int openCoord = 0;
                int closeCoord = 0;
                int iterationCoord = 0;
                int parenthesisCount = 0;
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
                // цикл обрезки одной скобки (и всего, что в нее вложено)
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
                        closeCoord = iterationCoord+1;
                    }
                    processInput = processInput.Remove(iterationCoord, 1);
                    processInput = processInput.Insert(iterationCoord, " ");

                iterationCoord = FindParenthesis(processInput);
                } while (parenthesisCount > 0);

                nestedString = nestedString.Remove(closeCoord, nestedString.Length - closeCoord);
                nestedString = nestedString.Remove(0, openCoord);
                return nestedString;
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
