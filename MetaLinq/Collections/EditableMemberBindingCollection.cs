using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using MetaLinq.Initializers;

namespace MetaLinq.Collections
{
    [DataContract]
    public class EditableMemberBindingCollection : List<EditableMemberBinding>
    {
        public EditableMemberBindingCollection() : base() { }
        public EditableMemberBindingCollection(IEnumerable<EditableMemberBinding> source) : base(source) { }
        public EditableMemberBindingCollection(IEnumerable<MemberBinding> source) 
        {
            foreach (MemberBinding ex in source)
                this.Add(EditableMemberBinding.CreateEditableMemberBinding(ex));
        }

        public IEnumerable<MemberBinding> GetMemberBindings()
        {
            foreach (EditableMemberBinding editEx in this)
                yield return editEx.ToMemberBinding();
        }       
    }
}
