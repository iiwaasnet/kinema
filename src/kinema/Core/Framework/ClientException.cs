using System;

namespace kinema.Core.Framework
{
    public class ClientException : Exception
    {
        public ClientException(string message, string type, string stackTrace)
            : base(message)
        {
            StackTrace = stackTrace;
            Type = type;
        }

        public override string ToString()
            => $"[{Type}] {Message} {StackTrace}";

        public override string StackTrace { get; }

        public string Type { get; }
    }
}