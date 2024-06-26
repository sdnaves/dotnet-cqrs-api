﻿namespace Sample.Infra.CrossCutting.Mediator.Models
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public string? AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
