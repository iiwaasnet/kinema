using kinema.Core;

namespace kinema.Client
{
    public class CallbackHandlerKey
    {
        public IMessageIdentifier MessageIdentifier { get; set; }

        public long CallbackKey { get; set; }
    }
}