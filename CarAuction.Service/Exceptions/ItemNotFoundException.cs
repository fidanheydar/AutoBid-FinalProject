using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        private readonly static string _message = "Entity is not found";
        public ItemNotFoundException():base(_message)
        {
        }

        public ItemNotFoundException(string msg) : base(msg)
        {

        }

        public ItemNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
