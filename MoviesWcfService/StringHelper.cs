using System;

namespace MoviesWcfService
{
    public static class StringHelper
    {
        public static bool ContainsAnyCase(this string source, string toCheck, StringComparison comp = StringComparison.CurrentCultureIgnoreCase)
        {
            if (string.IsNullOrEmpty(toCheck) || string.IsNullOrEmpty(source))
                return true;

            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}