using System.Collections.Generic;

namespace OccurrenceCounter.DataProvider
{
    /// <summary>
    /// Interface which exposes data provider functionality.
    /// </summary>
    /// <typeparam name="T">Type of data.</typeparam>
    public interface IDataProvider<T>
    {
        /// <summary>
        /// Gets data from provider.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetData();

        void SetData(T data);
    }
}
