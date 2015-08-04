using System;
using System.Collections.Generic;

namespace ExpressionBuilderDemo
{
    public class TestList : List<Test>
    {
        public override string ToString()
        {
            string str = "";
            foreach (Test t in this)
            {
                str += Environment.NewLine+ t.ToString();
            }
            return str;
        }
    }
}