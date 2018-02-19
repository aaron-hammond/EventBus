﻿using EventsSample.AspNetCore.Events;
using JKang.Events;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsSample.AspNetCore.EventHandlers
{
    public class MessageSentEventHandler : IEventHandler<MessageSent>
    {
        private readonly IMemoryCache _cache;

        public MessageSentEventHandler(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task HandleEventAsync(MessageSent @event)
        {
            if (_cache.TryGetValue("messages", out List<string> messages))
            {
                messages.Add(@event.Message);
            }
            else
            {
                messages = new List<string>();
            }
            _cache.Set("messages", messages);

            return Task.CompletedTask;
        }
    }
}
