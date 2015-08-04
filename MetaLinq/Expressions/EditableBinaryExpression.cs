using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace ExpressionBuilder
{
    [DataContract]
    public class EditableBinaryExpression : EditableExpression
    {
        // Properties
        [DataMember]
        public EditableExpression Left
        {
            get;
            set;
        }
        [DataMember]
        public EditableExpression Right
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
        public EditableBinaryExpression()
        {
        }

        public EditableBinaryExpression(BinaryExpression binex) : base(binex.Type)
        {
            Left = EditableExpression.CreateEditableExpression(binex.Left);
            Right = EditableExpression.CreateEditableExpression(binex.Right);
            NodeType = binex.NodeType;
        }

        // Methods
        public override Expression ToExpression()
        {
            return Expression.MakeBinary(NodeType, Left.ToExpression(), Right.ToExpression());
        }        
    }
}
