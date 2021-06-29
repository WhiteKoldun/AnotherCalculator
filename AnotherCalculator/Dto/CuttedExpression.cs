using System;
using System.Collections.Generic;
using System.Text;

namespace AnotherCalculator.Dto
{
    public class CuttedExpression
    {
        public string Expression { get; set; }
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
    }
}
