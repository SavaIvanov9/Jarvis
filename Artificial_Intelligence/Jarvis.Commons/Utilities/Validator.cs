namespace Jarvis.Commons.Utilities
{
    using System;

    public sealed class Validator
    {
        private static readonly Lazy<Validator> Lazy =
            new Lazy<Validator>(() => new Validator());

        private Validator()
        {
        }

        public static Validator Instance => Lazy.Value;

        public void ValidateIsAboveOqEqualMinimum(int lenght, int minimun, string errorMessage)
        {
            if (lenght < minimun)
            {
                throw new Exception(errorMessage);
            }
        }

        public void ValidateIsUnderOrEqualMax(int lenght, int max, string errorMessage)
        {
            if (lenght > max)
            {
                throw new Exception(errorMessage);
            }
        }

        public void ValidateIsEqual(int lenght, int expected, string errorMessage)
        {
            if (lenght != expected)
            {
                throw new Exception(errorMessage);
            }
        }

        public void ValidateNotNull(string input, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception(errorMessage);
            }
        }
    }
}
