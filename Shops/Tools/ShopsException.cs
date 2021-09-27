using System;
namespace Shops.Tools
{
    public class ShopsException : Exception
    {
        public ShopsException()
        {
        }

        public ShopsException(string message)
            : base(message)
        {
        }

        public ShopsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}