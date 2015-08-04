using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using MetaLinq.Collections;

namespace MetaLinq.Expressions
{
    [DataContract]
    public class EditableMemberInitExpression : EditableExpression
    {
        // Properties
        [DataMember]
        public EditableNewExpression NewExpression
        {
            get;
            set;
        }

        [DataMember]
        public EditableMemberBindingCollection Bindings
        {
            get;
            set;
        }
        
        public override ExpressionType NodeType
        {
            get { return ExpressionType.MemberInit; }
            set { }
        }

        // Ctors
        public EditableMemberInitExpression()
        {
            Bindings = new EditableMemberBindingCollection();
        }

        public EditableMemberInitExpression(MemberInitExpression membInit)
            : this(EditableExpression.CreateEditableExpression(membInit.NewExpression) as EditableNewExpression, membInit.Bindings)
        {
        }

        public EditableMemberInitExpression(EditableNewExpression newEx, IEnumerable<MemberBinding> bindings)
        {
            Bindings = new EditableMemberBindingCollection(bindings);
            NewExpression = newEx;
        }

        public EditableMemberInitExpression(NewExpression newRawEx, IEnumerable<MemberBinding> bindings)
            : this(EditableExpression.CreateEditableExpression(newRawEx) as EditableNewExpression, bindings)
        {
        }

        // Methods
        public override Expression ToExpression()
        {
            return Expression.MemberInit(NewExpression.ToExpression() as NewExpression, Bindings.GetMemberBindings());
        }
    }
}
