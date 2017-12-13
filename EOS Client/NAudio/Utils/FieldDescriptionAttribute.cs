using System;

namespace NAudio.Utils
{
    [AttributeUsage(AttributeTargets.Field)]
    public class FieldDescriptionAttribute : Attribute
    {
        public string Description { get; private set; }

        public FieldDescriptionAttribute(string description)
        {
            this.Description = description;
        }

        public override string ToString()
        {
            return this.Description;
        }
    }
}
