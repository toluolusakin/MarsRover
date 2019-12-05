using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MarsRover.Api.Exceptions
{
    public class MarsRoverException : Exception
    {
        public MarsRoverException()
        {
        }

        public MarsRoverException(string message) : base(message)
        {
        }

        public MarsRoverException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MarsRoverException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
