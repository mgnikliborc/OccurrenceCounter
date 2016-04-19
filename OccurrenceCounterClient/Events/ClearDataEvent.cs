using Prism.Events;

namespace OccurrenceCounterClient.Events
{
    /// <summary>
    /// Event which is published when data should be clear out.
    /// </summary>
    internal class ClearDataEvent : PubSubEvent<string>
    {
    }
}
