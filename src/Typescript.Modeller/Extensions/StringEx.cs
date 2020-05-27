namespace TypeScript.Modeller.Extensions
{
    public static class StringEx
    {
        public static string ToCamelCase(this string value)
        {
            var output = string.Empty;

            if (value?.Length > 0)
            {
                output +=
                    value
                        .Substring(0, 1)
                        .ToLower();
            }

            if (value?.Length > 1)
            {
                output +=
                    value
                        .Substring(1, value.Length - 1);
            }

            return output;
        }
    }
}
