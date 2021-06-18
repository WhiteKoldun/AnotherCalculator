using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AnotherCalculator.SequenceTypes;

namespace AnotherCalculator
{
    public class TreeProcessing
    {
        public NestedStringElement GenerateTree(string rawInput)
        {
            NestedStringElement nestedTree = new NestedStringElement();
            int nodeCount = rawInput.Count(f => f == '(');
            if (nodeCount == 0)
            {
                nestedTree.NestedString = rawInput;
                nestedTree.NestedLevel = 0;
                return nestedTree;
            }

            var bufferString = rawInput;
            int nestedLevel = 0;
            while (bufferString.Length > 0)
            {
                // удаляем вложенные выражения со скобками

                if (ContainsParenthesis(bufferString))
                {
                    var currentInput = RemoveParenthesis(bufferString);
                    NestedStringElement childInput = GetAllParenthesis(bufferString);
                    nestedTree.NestedString = currentInput;
                    nestedTree.NestedLevel = nestedLevel;
                }
                else
                {

                }

            }

            return nestedTree;
        }

        public List<NestedStringElement> ChildrenCreator(NestedStringElement nestedBufferElement)
        {
            List<NestedStringElement> childrenList = new List<NestedStringElement>();
            string nestedString = nestedBufferElement.NestedString;
            while (IsContainsParenthesis(nestedString))
            {
                NestedStringElement childElement = new NestedStringElement();
                childElement.NestedString = GetExpression(nestedString);
                nestedString = RemoveExpression(nestedString);
                childrenList.Add(childElement);
            }

            return childrenList;
        }

        private string RemoveExpression(string nestedString)
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

        private string GetExpression(string nestedString)
        {
            throw new NotImplementedException();
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

    }
}
