using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace MetaLinq.Expressions
{
    [DataContract]
    public class EditableTypeBinaryExpression : EditableExpression
    {
        // Properties
        [DataMember]
        public EditableExpression Expression
        {
            get;
            set;
        }
        
        public override ExpressionType NodeType
        {
            get { return ExpressionType.TypeIs; }
            set { }
        }

        //Ctors
        public EditableTypeBinaryExpression()
        {
        }

        public EditableTypeBinaryExpression(TypeBinaryExpression typeBinEx) 
            : this(EditableExpression.CreateEditableExpression(typeBinEx.Expression), typeBinEx.TypeOperand)
        { }

        public EditableTypeBinaryExpression(EditableExpression expression, Type type)
            : base(type)
        {
            Expression = expression;
        }

        // Methods
        public override Expression ToExpression()
        {
            return System.Linq.Expressions.Expression.TypeIs(Expression.ToExpression(), Type);
        }        
    }
}
