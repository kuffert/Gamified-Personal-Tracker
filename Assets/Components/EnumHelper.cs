using System;
using System.Reflection;
using System.ComponentModel;

// Author: Yvette Kim
// Date: 11/16/2015
public static class EnumHelper
{
	// In combination with the different Enums, use to get the descriptions, which are display-ready.
	public static string GetDescription(this Enum value)
	{
		FieldInfo field = value.GetType().GetField(value.ToString());
		object[] attribs = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
		if(attribs.Length > 0)
		{
			return ((DescriptionAttribute)attribs[0]).Description;
		}
		return string.Empty;
	}
}