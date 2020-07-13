namespace kinema.Messaging.Messages
{
    public class ExceptionMessage
    {
        public string StackTrace { get; set; }

        public string Message { get; set; }

        public string ExceptionType { get; set; }
    }
}