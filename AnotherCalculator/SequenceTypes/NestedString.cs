using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AnotherCalculator.SequenceTypes
{
    public class NestedStringElement
    {
        public string NestedString { get; set; }
        public int NestedLevel { get; set; } = 0;
        public List<NestedStringElement> Children { get; set; }

        public void AddChild(NestedStringElement childElement)
        {
            if (this.Children is null)
            {
                this.Children = new List<NestedStringElement>();
                childElement.NestedLevel = this.NestedLevel + 1;
                this.Children.Add(childElement);
            }
            childElement.NestedLevel = this.NestedLevel + 1;
            this.Children.Add(childElement);
        }

        public int? GetMaxNestedLevel()
        {
            if (this.Children == null)
            {
                return 0;
            }

            for (int bufferLevel = 0; IsThisLevelExist(bufferLevel); bufferLevel++)
            {
                if (IsThisLevelExist(bufferLevel + 1) == false)
                {
                    return bufferLevel;
                }
            }

            return null;
        }

        public List<NestedStringElement> GetChildrenFromLevel(int nestedLevel)
        {
            List<NestedStringElement> returnList = new List<NestedStringElement>();
            returnList.Add(this);
            List<NestedStringElement> bufferList = new List<NestedStringElement>();
            // если такого уровня вложенности нет, возвращаем нихуя
            if (IsThisLevelExist(nestedLevel) == false)
            {
                return null;
            }
            // хорошо бы сделать один алгоритм для любого уровня вложенности,
            // но пока пойдет так
            switch (nestedLevel)
            {
                case 0:
                {
                    return returnList;
                }
                case 1:
                {
                    returnList = Children;
                    return returnList;
                }
            }

            if (nestedLevel > 1)
            {
                bufferList = Children;
                while (nestedLevel != bufferList[0].NestedLevel)
                {
                    returnList = null;
                    // не прыгнул ли я тут на уровень глубже, чем надо?
                    foreach (var child in bufferList)
                    {
                        foreach (var nestedChild in child.Children)
                        {
                            returnList.Add(nestedChild);
                        }
                    }

                    bufferList = returnList;
                }

                return bufferList;
            }
            return null;
        }

        private bool IsThisLevelExist(int nestedLevel)
        {
            // нулевой уровень есть всегда, если есть экземпляр
            // а раз мы можем вызвать метод, экземпляр точно есть
            switch (nestedLevel)
            {
                case 0:
                    return true;
                case 1:
                {
                    if (this.Children is null)
                    {
                        return false;
                    }

                    return true;
                }
            }
            List<NestedStringElement> returnList = new List<NestedStringElement>();
            returnList.Add(this);
            List<NestedStringElement> bufferList = new List<NestedStringElement>();

            if (nestedLevel > 1)
            {
                bufferList = Children;
                try
                {
                    while (nestedLevel != bufferList[0].NestedLevel)
                    {
                        returnList = null;
                        foreach (var child in bufferList)
                        {
                            foreach (var nestedChild in child.Children)
                            {
                                returnList.Add(nestedChild);
                            }
                        }

                        bufferList = returnList;
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }

            return false;
        }
    }
}
