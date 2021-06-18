using System;
using System.Collections.Generic;
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

        }
        
    }
}
