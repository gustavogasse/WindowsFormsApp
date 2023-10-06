using System;
using System.Linq;

namespace WindowsFormsAppWithFirebird.Domain.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return String.IsNullOrWhiteSpace(str);
        }
        public static int ConvertToInt(this string value)
        {
            if (value != null)
            {
                value = value.RemoverCaracteres();
                var result = string.Empty;
                foreach (var item in value.ToArray())
                {
                    if (Int32.TryParse(item.ToString(), out int valueInt))
                    {
                        result += item;
                    }
                }
                return int.Parse(result);
            }

            return 0;
        }
    }
}
