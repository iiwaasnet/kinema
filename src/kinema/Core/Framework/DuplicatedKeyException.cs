using System;

namespace kinema.Core.Framework
{
    public class DuplicatedKeyException : Exception
    {
        public DuplicatedKeyException(string message)
            : base(message)
        {
        }
    }
}