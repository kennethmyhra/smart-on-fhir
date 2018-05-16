using System;
using System.Linq;
using System.Reflection;

namespace EHRApp
{
    internal class ReflectionUtility
    {
        public static object GetPropertyValue(object target, string name, object defaultValue = null)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            Type type = target.GetType();

            MemberInfo memberInfo = type.GetMember(name).FirstOrDefault();
            if (memberInfo == null) return defaultValue;
            if (memberInfo.MemberType != MemberTypes.Property) throw new ArgumentException($"'{name}' is not a property.", nameof(name));

            return type.InvokeMember(name, BindingFlags.GetProperty, null, target, null);
        }
    }
}
