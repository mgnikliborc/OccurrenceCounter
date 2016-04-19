namespace OccurrenceCounter
{
    /// <summary>
    /// Class which provides information about element and its number of occurrences. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OccurrenceInfo<T>
    {
        /// <summary>
        /// Element for which occurrence was counted.
        /// </summary>
        public T Element { get; set; }

        /// <summary>
        /// Number of element's occurrences.
        /// </summary>
        public int NumberOfOccurrences { get; set; }
    }
}
