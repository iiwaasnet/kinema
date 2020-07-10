using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using kinema.Core;

namespace kinema.Actors
{
    public abstract class Actor : IActor
    {
        protected Actor()
            => Identifier = ReceiverIdentifier.Create(ReceiverKind.Actor);

        public virtual IEnumerable<MessageHandlerDefinition> GetInterfaceDefinition()
            => GetActorRegistrationsByAttributes();

        private IEnumerable<MessageHandlerDefinition> GetActorRegistrationsByAttributes()
        {
            var methods = GetType()
                         .FindMembers(MemberTypes.Method,
                                      BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                                      InterfaceMethodFilter,
                                      typeof(MessageHandlerDefinitionAttribute))
                         .Cast<MethodInfo>();

            return methods.Select(Selector);
        }

        private MessageHandlerDefinition Selector(MethodInfo method)
        {
            var @delegate = (MessageHandler) Delegate.CreateDelegate(typeof(MessageHandler), this, method);
            var attr = method.GetCustomAttribute<MessageHandlerDefinitionAttribute>();

            return new MessageHandlerDefinition
                   {
                       Message = attr.MessageIdentifier,
                       Handler = @delegate,
                       KeepRegistrationLocal = attr.KeepRegistrationLocal
                   };
        }

        private static bool InterfaceMethodFilter(MemberInfo memberInfo, object filterCriteria)
            => memberInfo.GetCustomAttributes((Type) filterCriteria).Any();

        public ReceiverIdentifier Identifier { get; }
    }
}