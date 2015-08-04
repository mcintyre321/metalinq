using System.Linq.Expressions;
using System.Runtime.Serialization;
using MetaLinq.Collections;

namespace MetaLinq.Initializers
{
    [DataContract]
    public class EditableMemberMemberBinding : EditableMemberBinding
    {
        // Properties
        [DataMember]
        public EditableMemberBindingCollection Bindings 
        { 
            get; 
            set; 
        }

        public override MemberBindingType BindingType
        {
            get { return MemberBindingType.MemberBinding; }
            set { }
        }

        // Ctors
        public EditableMemberMemberBinding()
        {
        }

        public EditableMemberMemberBinding(MemberMemberBinding member) : base(member.BindingType, member.Member)
        {
            Bindings = new EditableMemberBindingCollection(member.Bindings);
        }

        // Methods
        public override MemberBinding ToMemberBinding()
        {
            return Expression.MemberBind(Member, Bindings.GetMemberBindings());
        }


    }
}
