using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableInvocationExpression : EditableExpression
    {
        // Properties
        [DataMember]
        public EditableExpression Expression
        {
            get;
            set;
        }

        [DataMember]
        public EditableExpressionCollection Arguments
        {
            get;
            set;
        }
    
        public override ExpressionType NodeType
        {
            get { return ExpressionType.Invoke; }
            set { }
        }

        // Ctors
        public EditableInvocationExpression()
        {
            Arguments = new EditableExpressionCollection();
        }

        public EditableInvocationExpression(InvocationExpression invocEx)
            : this()
        {
            Expression = EditableExpression.CreateEditableExpression(invocEx.Expression);
            foreach (Expression ex in invocEx.Arguments)
                Arguments.Add(EditableExpression.CreateEditableExpression(ex));
        }

        // Methods
        public override Expression ToExpression()
        {
            return System.Linq.Expressions.Expression.Invoke(Expression.ToExpression(), Arguments.GetExpressions());
        }
    }
}
