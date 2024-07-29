using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Exceptions
{
    public class InvalidBidException : Exception
    {
        private readonly static string _message = "You must bid more than last one";
        public InvalidBidException():base(_message)
        {
        }

        protected InvalidBidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidBidException(string? message) : base(message)
        {
        }

        public InvalidBidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
