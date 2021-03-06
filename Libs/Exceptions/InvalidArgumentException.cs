using System;

namespace Libs.Exceptions
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException(string message) : base(message)
        {
        }

        public InvalidArgumentException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}