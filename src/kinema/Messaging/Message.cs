using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using kinema.Core;
using kinema.Core.Framework;

namespace kinema.Messaging
{
    internal class Message : MessageIdentifier, IMessage
    {
        private byte[] body;
        private readonly IMessageSerializer? serializer;
        private object? deserializedPayload;
        private readonly object? payload;
        private List<MessageIdentifier> callbackPoint;
        private static readonly byte[] EmptyCorrelationId = Guid.Empty.ToString().GetBytes();

        private Message(object payload,
                        IMessageIdentifier messageIdentifier,
                        IMessageSerializer? serializer = default!)
            : base(messageIdentifier.Identity, messageIdentifier.Version, messageIdentifier.Partition)
        {
            this.payload = payload;
            this.serializer = serializer;
            Domain = string.Empty;
            callbackPoint = new List<MessageIdentifier>();
            Signature = IdentityExtensions.Empty;
            ReceiverNodeIdentity = IdentityExtensions.Empty;
            ReceiverIdentity = IdentityExtensions.Empty;
        }

        public static IMessage CreateFlowStartMessage(object payload,
                                                      IMessageIdentifier messageIdentifier,
                                                      byte[]? correlationId = default,
                                                      IMessageSerializer? serializer = default!)
            => Create(payload, messageIdentifier, correlationId, DistributionPattern.Unicast, serializer);

        public static IMessage Create(object payload,
                                      IMessageIdentifier messageIdentifier,
                                      byte[]? correlationId = default,
                                      DistributionPattern distributionPattern = DistributionPattern.Unicast,
                                      IMessageSerializer? serializer = default!)
            => new Message(payload, messageIdentifier, serializer)
               {
                   Distribution = distributionPattern,
                   CorrelationId = correlationId ?? EmptyCorrelationId
               };

        private static byte[] GenerateCorrelationId()
            => Guid.NewGuid().ToString().GetBytes();

        [return: MaybeNull]
        public T GetPayload<T>([AllowNull] IMessageSerializer serializer = default!)
            where T : new()
            => (T) (payload
                 ?? (deserializedPayload ??= (serializer ?? this.serializer)!.Deserialize<T>(body)));

        public void SetReceiverNode(ReceiverIdentifier receiverNode)
        {
            throw new NotImplementedException();
        }

        public void SetReceiverActor(ReceiverIdentifier receiverNode, ReceiverIdentifier receiverActor)
        {
            throw new NotImplementedException();
        }

        public void EncryptPayload()
        {
            throw new NotImplementedException();
        }

        public void DecryptPayload()
        {
            throw new NotImplementedException();
        }

        internal void SetBody(byte[] body)
        {
        }

        public DistributionPattern Distribution { get; private set; }

        public byte[] CorrelationId { get; private set; }

        //public byte[] Body { get; }

        public string Domain { get; }

        public byte[] ReceiverIdentity { get; }

        public byte[] ReceiverNodeIdentity { get; }

        public byte[] Signature { get; }

        public IEnumerable<MessageIdentifier> CallbackPoint
        {
            get => callbackPoint;
            private set => callbackPoint = value.ToList();
        }

        public long CallbackKey { get; private set; }

        public byte[] CallbackReceiverIdentity { get; private set; }

        public byte[] CallbackReceiverNodeIdentity { get; private set; }
    }
}