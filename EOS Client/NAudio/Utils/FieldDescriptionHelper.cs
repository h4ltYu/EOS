using System;
using System.Reflection;

namespace NAudio.Utils
{
    public static class FieldDescriptionHelper
    {
        public static string Describe(Type t, Guid guid)
        {
            foreach (FieldInfo fieldInfo in t.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (fieldInfo.IsPublic && fieldInfo.IsStatic && fieldInfo.FieldType == typeof(Guid) && (Guid)fieldInfo.GetValue(null) == guid)
                {
                    foreach (object obj in fieldInfo.GetCustomAttributes(false))
                    {
                        FieldDescriptionAttribute fieldDescriptionAttribute = obj as FieldDescriptionAttribute;
                        if (fieldDescriptionAttribute != null)
                        {
                            return fieldDescriptionAttribute.Description;
                        }
                    }
                    return fieldInfo.Name;
                }
            }
            return guid.ToString();
        }
    }
}
