using kinema.Core;

namespace kinema.Messaging.Messages
{
    public static class MessageIdentifiers
    {
        public static readonly IMessageIdentifier Exception = MessageIdentifier.CreateForFramework("EXCEPTION", 1);
        public static readonly IMessageIdentifier ReceiptConfirmation = MessageIdentifier.CreateForFramework("RCPTCONFIRM", 1);
    }
}