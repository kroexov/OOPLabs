using System;
namespace IsuExtra.Tools
{
    public class IsuExtraException : Exception
    {
        public IsuExtraException()
        {
        }

        public IsuExtraException(string message)
            : base(message)
        {
        }

        public IsuExtraException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}