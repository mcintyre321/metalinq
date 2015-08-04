using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MetaLinq.Extensions;

namespace MetaLinq.Initializers
{
    [DataContract]
    [KnownType(typeof(EditableMemberAssignment))]
    [KnownType(typeof(EditableMemberListBinding))]
    [KnownType(typeof(EditableMemberMemberBinding))]
    [XmlInclude(typeof(EditableMemberAssignment))]
    [XmlInclude(typeof(EditableMemberListBinding))]
    [XmlInclude(typeof(EditableMemberMemberBinding))]
    public abstract class EditableMemberBinding
    {       
        // Properties
        public abstract MemberBindingType BindingType
        {
            get;
            set;
        }

        [XmlIgnore]
        public MemberInfo Member
        {
            get;
            set;
        }

        [DataMember]
        public string MemberName
        {
            get { return Member.ToSerializableForm(); }
            set { Member = Member.FromSerializableForm(value); }
        }

        // Ctors
        protected EditableMemberBinding()
        {
        }

        protected EditableMemberBinding(MemberBindingType type, MemberInfo member)
        {
            BindingType = type;
            Member = member;
        }

        // Methods
        public abstract MemberBinding ToMemberBinding();

        public static EditableMemberBinding CreateEditableMemberBinding(MemberBinding member)
        {
            if (member is MemberAssignment) return new EditableMemberAssignment(member as MemberAssignment);
            else if (member is MemberListBinding) return new EditableMemberListBinding(member as MemberListBinding);
            else if (member is MemberMemberBinding) return new EditableMemberMemberBinding(member as MemberMemberBinding);
            else return null;

        }
    }
}
