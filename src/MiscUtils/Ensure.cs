using System;

namespace MiscUtils
{
    /// <summary>
    ///   Ensure input parameters
    /// </summary>
    public static class Ensure
    {
        public static void ArgumentNotNull([ValidatedNotNull] object value, string name)
        {
            if (value != null) return;

            throw new ArgumentNullException(name);
        }

        public static void ArgumentNonNegative(int value, string name)
        {
            if (value > -1) return;

            throw new ArgumentException("The argument must be non-negative", name);
        }

        public static void ArgumentNotNullOrEmptyString([ValidatedNotNull] string value, string name)
        {
            ArgumentNotNull(value, name);
            if (!string.IsNullOrWhiteSpace(value)) return;

            throw new ArgumentException("String cannot be empty", name);
        }
    }

    // Tells CodeAnalysis that this argument is being validated 
    // to be not null. Great for guard methods.
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class ValidatedNotNullAttribute : Attribute
    {
        // Internal so it doesn't conflict with the potential billion 
        // other implementations of this attribute.
    }
}