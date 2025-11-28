using System.Reflection;

namespace HUP.Common.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValueEmpty(object? value)
        {
            // 1. Check for Null
            if (value == null) return true;

            // 2. Check for Strings (Empty or Whitespace)
            if (value is string str) return string.IsNullOrWhiteSpace(str);

            // 3. Check for DateTime (Default is 0001-01-01)
            if (value is DateTime date) return date == default;

            return false;
        }
    }
}