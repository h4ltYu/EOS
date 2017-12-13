using System;
using System.Reflection;

namespace NAudio.Utils
{
	/// <summary>
	/// Helper to get descriptions
	/// </summary>
	// Token: 0x02000068 RID: 104
	public static class FieldDescriptionHelper
	{
		/// <summary>
		/// Describes the Guid  by looking for a FieldDescription attribute on the specified class
		/// </summary>
		// Token: 0x06000247 RID: 583 RVA: 0x00007668 File Offset: 0x00005868
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
