
namespace OccurrenceCounter.SignsRemover
{
    /// <summary>
    /// Interface which provides remove signs functionality.
    /// </summary>
    public interface ISignsRemover<T>
    {
        /// <summary>
        /// Removes signs from given data.
        /// </summary>
        /// <param name="data">Data from which signs will be removed.</param>
        /// <returns>Data without removed signs.</returns>
        T Remove(T data);
    }
}
