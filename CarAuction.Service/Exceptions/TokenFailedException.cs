using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Exceptions
{
    internal class TokenFailedException : Exception
    {
        private readonly static string _message = "User didn't found with this Token or Token expired";
        public TokenFailedException() : base(_message)
        {
        }

        public TokenFailedException(string? message) : base(message)
        {
        }

        public TokenFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TokenFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
