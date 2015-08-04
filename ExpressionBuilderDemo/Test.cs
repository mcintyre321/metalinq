using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ExpressionBuilderDemo
{
    [DataContract]
    public class Test
    {
        static string value;

        public Test()
        {

        }
        public Test(int a, int b)
        {
            num = a;
            Number = b;
        }
        public Test(int a)
        {
            num = a;
        }
        [DataMember]
        public int num;
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public List<int> numbers = new List<int>();
        [DataMember]
        public PropHolder numberProp = new PropHolder();

        public override string ToString()
        {
            return string.Format("num: {0}, Number: {1}, numberProp: {3}, Numbers: {2}", num, Number,
                string.Join(",", Array.ConvertAll<int, string>(numbers.ToArray(), Convert.ToString)), numberProp.NumberProperty);
        }
        public static int operator +(Test a)
        {
            return a.num+1;
        }     
    }
}