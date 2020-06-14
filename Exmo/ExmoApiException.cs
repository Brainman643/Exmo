using System;
using System.Runtime.Serialization;

namespace Exmo
{
    [Serializable]
    public class ExmoApiException : Exception
    {
        public int? Code { get; set; }

        public ExmoApiException()
        {
        }

        public ExmoApiException(string message)
            : base(message)
        {
        }

        public ExmoApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ExmoApiException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            Code = (int?)serializationInfo.GetValue(nameof(Code), typeof(int?));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Code), Code);
        }
    }
}
