using System.Collections.Generic;
using System.Linq.Expressions;
using MetaLinq.Expressions;

namespace MetaLinq.Collections
{
    public class EditableExpressionCollection : List<EditableExpression>
    {
        public EditableExpressionCollection() : base() { }
        public EditableExpressionCollection(IEnumerable<EditableExpression> source) : base(source) { }
        public EditableExpressionCollection(IEnumerable<Expression> source) 
        {
            foreach (Expression ex in source)
                this.Add(EditableExpression.CreateEditableExpression(ex));
        }
        
        public IEnumerable<Expression> GetExpressions()
        {
            foreach (EditableExpression editEx in this)
                yield return editEx.ToExpression();
        }

        public IEnumerable<ParameterExpression> GetParameterExpressions()
        {
            foreach (EditableExpression editEx in this)
                if (editEx is EditableParameterExpression)
                {
                    EditableParameterExpression parmEx = editEx as EditableParameterExpression;
                    yield return new EditableParameterExpression(parmEx.Type, parmEx.Name).ToExpression() as ParameterExpression;
                }
        }
    }
}
