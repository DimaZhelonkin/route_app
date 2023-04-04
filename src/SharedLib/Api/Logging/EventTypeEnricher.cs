﻿using System.Text;
using Murmur;
using Serilog.Core;
using Serilog.Events;

namespace Ark.SharedLib.Api.Logging;

internal class EventTypeEnricher : ILogEventEnricher
{
    #region ILogEventEnricher Members

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent is null)
            throw new ArgumentNullException(nameof(logEvent));

        if (propertyFactory is null)
            throw new ArgumentNullException(nameof(propertyFactory));

        var murmur = MurmurHash.Create32();
        var bytes = Encoding.UTF8.GetBytes(logEvent.MessageTemplate.Text);
        var hash = murmur.ComputeHash(bytes);
        var hexadecimalHash = BitConverter.ToString(hash).Replace("-", "");
        var eventId = propertyFactory.CreateProperty("EventType", hexadecimalHash);
        logEvent.AddPropertyIfAbsent(eventId);
    }

    #endregion
}