//using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ExpressionBuilder;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System;

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

    public struct PropHolder
    {
        public int NumberProperty { get; set; }
    }

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

    public class Foo
    {
        public bool Bar<T, U>()
        {
            return true;
        }

        public bool Bar2()
        {
            return true;
        }
    }
    class Program
    {
        public static string message = "hi";
        static void Main(string[] args)
        {
            Expression<Func<Foo, bool>> myEx2 = x => x.Bar2();
            CheckSerialization<Foo, bool>(myEx2, new Foo());

            Expression <Func<Foo,bool>>  myEx =  x => x.Bar<string, List<Foo>>();
            CheckSerialization<Foo, bool>(myEx, new Foo());


            //make a lambda            
            //create an editable version of the lambda
            //Expression<Func<int, int>> lambda = x => x + 1;
            //EditableExpression mutableLambda = EditableExpression.CreateEditableExpression(lambda);

            Test testObject = new Test() { num = 1, Number = 2 };
            Func<Test, Test> methodCall = delegate(Test obj) { obj.num = 3; return obj; };
            int numberValue = 3;

            Console.WriteLine("X is " + numberValue);

            
            Expression e = Expression.UnaryPlus(Expression.New(typeof(Test)));
            //Expression<Func<int, Test, int>> lambda1 = (x, y) => +testObject;
            Expression<Func<int, string>> lambda = (x) => x.ToString();            

            //EditableExpression
            //EditableLambdaExpression
            //EditableBinaryExpression
            //EditableParameterExpression
            //EditableConstantExpression
            //EditableExpressionCollection
            CheckSerialization<int, int>(x => x + x, numberValue);            

            //EditableConditionalExpression
            //EditableUnaryExpression
            CheckSerialization<int, bool>(x => (-x > 5) ? true : false, numberValue);
            CheckSerialization<Test, int>(x => +x, testObject);

            //EditableMethodCallExpression
            CheckSerialization<int, string>(x => x.ToString(), numberValue);
            CheckSerialization<int, string>(x => string.Concat("",""), numberValue);

            //EditableMemberExpression                    
            CheckSerialization<Test, int>((x) => x.num + x.Number, testObject);
            CheckSerialization<int, string>((x) => Program.message, numberValue);

            //EditableTypeBinaryExpression
            CheckSerialization<object, bool>((x) => (x is Test), testObject);
            CheckSerialization<object, bool>((x) => (x is Test), numberValue); 

            //EditableInvocationExpression
            CheckSerialization<Test, Func<Test, Test>, Test>((x, y) => y(x), testObject, methodCall);

            //EditableNewExpression            
            CheckSerialization<Test>(() => new Test());
            CheckSerialization<Test>(() => new Test { num = 0 });
            CheckSerialization<object>(() => new { a = 1, b = 2 }); // using members

            //EditableNewArrayExpression
            CheckSerialization<Test[]>(() => new Test[5]);
            CheckSerialization<int[]>(() => new int[5] { 5, 4, 3, 2, 1 });

            //EditableListInitExpression 
            CheckSerialization<Test, TestList>((x => new TestList() { x, x }), testObject);            

            //EditableMemberInitExpression - EditableMemberAssignment
            CheckSerialization<Test, Test>((x => new Test { Number = 4 }), testObject);
            
            //EditableMemberInitExpression - EditableMemberListBinding
            CheckSerialization<Test, Test>((x => new Test { numbers = {1,2} }), testObject);

            //EditableMemberInitExpression - EditableMemberMemberBinding            
            CheckSerialization<Test, Test>((x => new Test() { numberProp = { NumberProperty = 3 } }), testObject);           
        }

        private static void CheckSerialization<T>(Expression<Func<T>> lambda)
        {
            EditableExpression e = CheckSerializationInternal(EditableExpression.CreateEditableExpression(lambda));
            InvokeExpression(e);
        }

        private static void CheckSerialization<T1, T2>(Expression<Func<T1, T2>> lambda, T1 value)
        {
            EditableExpression e = CheckSerializationInternal(EditableExpression.CreateEditableExpression(lambda));
            InvokeExpression<T1>(e, value);
        }

        private static void CheckSerialization<T1, T2, T3>(Expression<Func<T1, T2, T3>> lambda, T1 value1, T2 value2)
        {
            EditableExpression e = CheckSerializationInternal(EditableExpression.CreateEditableExpression(lambda));
            InvokeExpression<T1, T2>(e, value1, value2);
        }

        private static EditableExpression CheckSerializationInternal(EditableExpression mutableLambda)
        {            
            DataContractSerializer dcs = new DataContractSerializer(mutableLambda.GetType());
            MemoryStream ms = new MemoryStream();
            XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateTextWriter(ms, Encoding.UTF8, true);
            //dcs.WriteObject(xdw, mutableLambda);
            
            XmlSerializer xs = new XmlSerializer(mutableLambda.GetType());
            xs.Serialize(ms, mutableLambda);

            xdw.Flush();
            ms.Flush();
            string str = Encoding.UTF8.GetString(ms.ToArray());

            MemoryStream ms2 = new MemoryStream(Encoding.UTF8.GetBytes(str));
            //Object o = dcs.ReadObject(ms2);
            object o = xs.Deserialize(ms2);
            if (o is EditableExpression)
            {
                return o as EditableExpression;
            }
            else
            {
                return null;
            }
        }

        private static void InvokeExpression(EditableExpression mutableLambda)
        {
            LambdaExpression e = mutableLambda.ToExpression() as LambdaExpression;
            Console.WriteLine(e.ToString() + " = " + e.Compile().DynamicInvoke());
        }

        private static void InvokeExpression<T>(EditableExpression mutableLambda, T value)
        {
            LambdaExpression e = mutableLambda.ToExpression() as LambdaExpression;
            Console.WriteLine(e.ToString() + " = " + e.Compile().DynamicInvoke(value));
        }
        private static void InvokeExpression<T1, T2>(EditableExpression mutableLambda, T1 value1, T2 value2)
        {
            LambdaExpression e = mutableLambda.ToExpression() as LambdaExpression;
            Console.WriteLine(e.ToString() + " = " + e.Compile().DynamicInvoke(value1, value2));
        }
    }
}
