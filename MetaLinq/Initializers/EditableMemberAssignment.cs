using System.Linq.Expressions;
using System.Runtime.Serialization;
using MetaLinq.Expressions;

namespace MetaLinq.Initializers
{
    [DataContract]
    public class EditableMemberAssignment : EditableMemberBinding
    {
        // Properties
        [DataMember]
        public EditableExpression Expression 
        { 
            get;
            set;
        }

        public override MemberBindingType BindingType
        {
            get { return MemberBindingType.Assignment; }
            set { }
        }

        // Ctors
        public EditableMemberAssignment()
        {
        }

        public EditableMemberAssignment(MemberAssignment member) 
            : base(member.BindingType, member.Member)
        {
            Expression = EditableExpression.CreateEditableExpression(member.Expression);
        }

        // Methods
        public override MemberBinding ToMemberBinding()
        {
            return System.Linq.Expressions.Expression.Bind(Member, Expression.ToExpression());
        }

    }
}
