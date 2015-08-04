using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace MetaLinq.Expressions
{
    [DataContract]
    public class EditableConditionalExpression : EditableExpression
    {
        // Properties 
        [DataMember]
        public EditableExpression Test
        {
            get;
            set;
        }
        [DataMember]
        public EditableExpression IfTrue
        {
            get;
            set;
        }
        [DataMember]
        public EditableExpression IfFalse
        {
            get;
            set;
        }
         
        public override ExpressionType NodeType
        {
            get;
            set;
        }

        // Ctors
        public EditableConditionalExpression()
        {
        }

        public EditableConditionalExpression(ConditionalExpression condEx)
        {
            NodeType = condEx.NodeType;
            Test = EditableExpression.CreateEditableExpression(condEx.Test);
            IfTrue = EditableExpression.CreateEditableExpression(condEx.IfTrue);
            IfFalse = EditableExpression.CreateEditableExpression(condEx.IfFalse);
        }

        public EditableConditionalExpression(ExpressionType nodeType, EditableExpression test, EditableExpression ifTrue, EditableExpression ifFalse)
        {
            NodeType = nodeType;
            Test = test;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        // Methods
        public override Expression ToExpression()
        {
            return Expression.Condition(Test.ToExpression(), IfTrue.ToExpression(), IfFalse.ToExpression());
        }
    }
}
