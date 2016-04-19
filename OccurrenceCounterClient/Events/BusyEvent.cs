using Prism.Events;

namespace OccurrenceCounterClient.Events
{
    /// <summary>
    /// Event which is published busy indicator should be shown.
    /// </summary>
    internal class BusyEvent : PubSubEvent<bool>
    {
    }
}
