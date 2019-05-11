namespace FastReader
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class TypeGeneratorException : Exception
    {
        public TypeGeneratorException(string message) : base(message)
        {
        }

        public TypeGeneratorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypeGeneratorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}