using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Exceptions
{
    public class LoginFailedException : Exception
    {
        private readonly static string _message = "Username or Password is invalid";
        public LoginFailedException():base(_message)
        {
        }

        protected LoginFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public LoginFailedException(string? message) : base(message)
        {
        }

        public LoginFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
