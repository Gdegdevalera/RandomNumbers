using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace WebApplication1.Extensions
{
    public static class DisplayExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetAttribute<DisplayAttribute>()?.Name;
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
                where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()
                            ?.GetCustomAttribute<TAttribute>();
        }

    }
}
