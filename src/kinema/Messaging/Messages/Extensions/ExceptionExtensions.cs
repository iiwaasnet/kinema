using System;
using System.Text;

namespace kinema.Messaging.Messages.Extensions
{
    public static class ExceptionExtensions
    {
        public static ExceptionMessage BuildExceptionMessage(this Exception err)
        {
            var message = new StringBuilder(err.Message);
            var type = new StringBuilder(err.GetType().ToString());
            var stackTrace = new StringBuilder(err.StackTrace);
            err = err.InnerException;
            while (err != null)
            {
                message.AppendFormat($" ==>> {err.Message}");
                type.AppendFormat($" ==>> {err.GetType()}");
                stackTrace.AppendFormat($" ==>> {err.StackTrace}");

                err = err.InnerException;
            }

            return new ExceptionMessage
                   {
                       Message = message.ToString(),
                       ExceptionType = type.ToString(),
                       StackTrace = stackTrace.ToString()
                   };
        }
    }
}