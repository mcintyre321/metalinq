using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace MetaLinq.Expressions
{
    [DataContract]
    public class EditableParameterExpression : EditableExpression
    {
        // Members
        private static Dictionary<string, ParameterExpression> _usableParameters = new Dictionary<string, ParameterExpression>();

        // Properties
        [DataMember]
        public string Name 
        { 
            get; 
            set; 
        }

        public override ExpressionType NodeType
        {
            get { return ExpressionType.Parameter; }
            set { }
        }

        // Ctors
        public EditableParameterExpression()
        {
        }

        public EditableParameterExpression(ParameterExpression parmEx)
            : this(parmEx.Type, parmEx.Name)
        { }

        public EditableParameterExpression(Type type, string name)
            : base(type)
        {
            Name = name;
        }

        // Methods
        static public ParameterExpression CreateParameter(Type type, string name)
        {
            ParameterExpression parameter = null;
            string key = type.AssemblyQualifiedName + Environment.NewLine + name;
            if (_usableParameters.ContainsKey(key))
            {
                parameter = _usableParameters[key] as ParameterExpression;
            }
            else
            {
                parameter = Expression.Parameter(type, name);
                _usableParameters.Add(key, parameter);
            }
            return parameter;
        }

        public override Expression ToExpression()
        {
            return CreateParameter(Type, Name);
        }
    }
}
