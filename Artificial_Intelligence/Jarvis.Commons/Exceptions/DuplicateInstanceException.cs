namespace Jarvis.Commons.Exceptions
{
    using System;
    
    public class DuplicateInstanceException : Exception
    {
        public DuplicateInstanceException()
        {
        }

        public DuplicateInstanceException(string message)
            : base(message)
        {
        }
    }
}
