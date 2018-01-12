using System;

namespace Urbagestion.Model.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ManyToManyAttribute : Attribute
    {
        public Type Type { get; }
        public string PropertyName { get; }

        public ManyToManyAttribute(Type type, string propertyName)
        {
            Type = type;
            PropertyName = propertyName;
        }
    }
}