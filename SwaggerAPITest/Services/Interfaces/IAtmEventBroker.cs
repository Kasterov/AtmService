﻿using SwaggerAPITest.Models.Events;

namespace SwaggerAPITest.Services.Interfaces
{
    public interface IAtmEventBroker
    {
        void StartStream(string key, AtmEvent @event);

        void AppendEvent(string key, AtmEvent @event);

        AtmEvent? FindEvent<T>(string key) where T : AtmEvent;

        AtmEvent GetLastEvent(string key);
    }
}