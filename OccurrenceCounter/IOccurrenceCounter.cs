using System.Collections.Generic;
using OccurrenceCounter.DataProvider;

namespace OccurrenceCounter
{
    /// <summary>
    /// Interface which expose counting elements occurrence of specified type in collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOccurrenceCounter<T>
    {
        /// <summary>
        /// Counts the occurrance of elements provided by Data Provider.
        /// </summary>
        /// <returns>Collection of <see cref="OccurrenceInfo"/>.</returns>
        IEnumerable<OccurrenceInfo<T>> Count(IEnumerable<T> sourceData);
    }
}
