using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Exceptions
{
    public class ItemAlreadyExistsException : Exception
    {
        private readonly static string _message = "Entity already exists.Please add another one";
        public ItemAlreadyExistsException():base(_message)
        {
        }

        public ItemAlreadyExistsException(string msg) : base(msg)
        {

        }

        public ItemAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ItemAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
