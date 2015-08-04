using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace MetaLinq.Expressions
{
    [DataContract]
    public class EditableConstantExpression : EditableExpression
    {
        // Properties
        [DataMember]
        public object Value
        {
            get;
            set;
        }
        
        public override ExpressionType NodeType
        {
            get { return ExpressionType.Constant; }
            set { }
        }

        // Ctors
        public EditableConstantExpression()
        {
        }

        public EditableConstantExpression(object value)
        {
            Value = value;
        }

        public EditableConstantExpression(ConstantExpression startConstEx)
        {
            Value = startConstEx.Value;
        }

        // Methods
        public override Expression ToExpression()
        {
            return Expression.Constant(Value) as Expression;
        }
    }
}
