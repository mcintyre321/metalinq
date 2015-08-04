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
    public class EditableListInitExpression : EditableExpression
    {
        // Properties
        [DataMember]
        public EditableExpression NewExpression
        {
            get;
            set;
        }

        [DataMember]
        public EditableElementInitCollection Initializers
        {
            get;
            set;
        }
        
        public override ExpressionType NodeType
        {
            get { return ExpressionType.ListInit; }
            set { }
        }

        // Ctors
        public EditableListInitExpression()
        {
            Initializers = new EditableElementInitCollection();
        }

        public EditableListInitExpression(ListInitExpression listInit)
            : this()
        {
            NewExpression = EditableExpression.CreateEditableExpression(listInit.NewExpression);
            foreach (ElementInit e in listInit.Initializers)
            {
                Initializers.Add(new EditableElementInit(e));
            }
        }

        public EditableListInitExpression(EditableExpression newEx, IEnumerable<EditableElementInit> initializers)
        {
            Initializers = new EditableElementInitCollection(initializers);
            NewExpression = newEx;
        }

        // Methods
        public override Expression ToExpression()
        {
            return Expression.ListInit(NewExpression.ToExpression() as NewExpression, Initializers.GetElementsInit().ToArray<ElementInit>());
        }
    }
}
