using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace DataAccessLayer.Enum
{
   public static class  EnumExtension
    {

        public static string GetDescription(this System.Enum value)
        {
            var type = value.GetType();

            var fi = type.GetField(value.ToString());

            var descriptions = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return descriptions.Length > 0 ? descriptions[0].Description : value.ToString();
        }

        public static SortedDictionary<string, T> GetBoundEnum<T>() where T : struct, IConvertible
        {
            // validate.
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an Enum type.");
            }

            var results = new SortedDictionary<string, T>();

            FieldInfo[] fieldInfos = typeof(T).GetFields();

            foreach (var fi in fieldInfos)
            {

                var value = (T)fi.GetValue(fi);
                var description = GetDescription((System.Enum)fi.GetValue(fi));

                if (!results.ContainsKey(description))
                {
                    results.Add(description, value);
                }
            }
            return results;
        }
    }
}
