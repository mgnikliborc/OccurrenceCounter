using OccurrenceCounter;
using Prism.Events;
using System.Collections.Generic;

namespace OccurrenceCounterClient.Events
{
    /// <summary>
    /// Event which is published when results are ready to present them.
    /// </summary>
    internal class ResultReadyEvent : PubSubEvent<IEnumerable<OccurrenceInfo<string>>>
    {
    }
}
