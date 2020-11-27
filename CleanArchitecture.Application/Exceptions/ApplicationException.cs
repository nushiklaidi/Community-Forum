using System;
using System.Runtime.Serialization;

namespace CleanArchitecture.Application.Exceptions
{
    [Serializable]
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message)
        {

        }

        protected ApplicationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
