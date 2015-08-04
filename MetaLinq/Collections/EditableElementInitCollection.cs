using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using MetaLinq.Initializers;

namespace MetaLinq.Collections
{
    [DataContract]
    public class EditableElementInitCollection : List<EditableElementInit>
    {
        public EditableElementInitCollection() : base() { }
        public EditableElementInitCollection(IEnumerable<EditableElementInit> source) : base(source) { }
        public EditableElementInitCollection(IEnumerable<ElementInit> source) 
        {
            foreach (ElementInit ex in source)
                this.Add(new EditableElementInit(ex));
        }

        public IEnumerable<ElementInit> GetElementsInit()
        {
            foreach (EditableElementInit editEx in this)
                yield return editEx.ToElementInit();
        }       
    }
}
