using System;
using System.Runtime.Serialization;

namespace Exmo
{
    [Serializable]
    public class ExmoApiException : Exception
    {
        public ExmoApiException()
        {
        }

        public ExmoApiException(string message) : base(message)
        {
        }

        public ExmoApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExmoApiException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
        }
    }
}
