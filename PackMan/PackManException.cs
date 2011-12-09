using System;
using System.Runtime.Serialization;

namespace PackMan
{
    [Serializable]
    public class PackManException : Exception
    {
        public PackManException()
        {
        }

        public PackManException(string message) : base(message)
        {
        }

        public PackManException(string message, Exception inner) : base(message, inner)
        {
        }

        protected PackManException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}