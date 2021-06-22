using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AnotherCalculator.SequenceTypes;
using Microsoft.VisualBasic;

namespace AnotherCalculator
{
    public class TreeProcessing
    {
        public NestedStringElement GenerateTree(string rawInput)
        {
            NestedStringElement nestedTree = new NestedStringElement();
            int nodeCount = rawInput.Count(f => f == '(');
            nestedTree.NestedString = rawInput;
            nestedTree.NestedLevel = 0;
            nestedTree.MotherId = null;
            nestedTree.ThisId = Guid.NewGuid();
            if (nodeCount == 0)
            {
                return nestedTree;
            }
            var bufferElement = nestedTree;
            List<NestedStringElement> bufferList = new List<NestedStringElement>();
            List<NestedStringElement> iterationList = new List<NestedStringElement>();
            var bufferString = rawInput;
            int nestedLevel = 0;
            if (nodeCount == 1)
            {
               nestedTree.AddChild(ChildrenCreator(nestedTree).Children);
            }

            if (nodeCount > 1)
            {
                nestedTree.AddChild(ChildrenCreator(nestedTree).Children);
                bool containParenthesis;
                do
                {
                    containParenthesis = false;
                    foreach (var element in bufferList)
                    {
                        iterationList.AddRange(ChildrenCreator(element).Children);
                    }

                    bufferList = iterationList;

                    foreach (var element in bufferList)
                    {
                        if (IsContainsParenthesis(element.NestedString))
                        {
                            containParenthesis = true;
                        }
                    }
                } while (containParenthesis);

            }

            return nestedTree;
        }

        public NestedStringElement ChildrenCreator(NestedStringElement nestedBufferElement)
        {
            List<NestedStringElement> childrenList = new List<NestedStringElement>();
            NestedStringElement bufferElement = new NestedStringElement();
            string nestedString = nestedBufferElement.NestedString;
            int nestedLevel = nestedBufferElement.NestedLevel + 1;
            if (IsContainsParenthesis(nestedString) == false)
            {
                return null;
            }

            while (IsContainsParenthesis(nestedString))
            {
                NestedStringElement childElement = new NestedStringElement();
                childElement.NestedString = GetExpression(nestedString);
                childElement.NestedLevel = nestedLevel;
                childElement.MotherId = nestedBufferElement.ThisId;
                childElement.ThisId = Guid.NewGuid();
                nestedString = RemoveExpression(nestedString);
                childrenList.Add(childElement);
            }

            bufferElement.NestedString = nestedString;
            bufferElement.Children = childrenList;
            return bufferElement;
        }

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
                parenthesisCount = 1;
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
                processInput.Remove(iterationCoord, 1);
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
                    closeCoord = iterationCoord;
                }

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
                    parenthesisCount = 1;
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
                    processInput.Remove(iterationCoord, 1);
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
                        closeCoord = iterationCoord;
                    }

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
            if (positionOpenParenthesis > positionCloseParenthesis)
            {
                return positionCloseParenthesis;
            }

            return positionOpenParenthesis;
        }

        private bool IsOpenedParenthesis(string input)
        {
            int position = FindParenthesis(input);
            if (input.Substring(position, 1) == "(")
            {
                return true;
            }

            return false;
        }

    }
}
