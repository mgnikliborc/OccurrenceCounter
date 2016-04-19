using System;
using System.Collections.Generic;
using System.Linq;

namespace OccurrenceCounter
{
    /// <summary>
    /// Class which counts occurrence of words in words collection.
    /// </summary>
    public class WordsOccurrenceCounter : IOccurrenceCounter<string>
    {
        #region Fields and Properties
        
        /// <summary>
        /// Delegate for map method.
        /// </summary>
        private readonly Func<string, string> map;

        /// <summary>
        /// Delegate for reduce method.
        /// </summary>
        private readonly Func<IGrouping<string, string>, OccurrenceInfo<string>> reduce;

        /// <summary>
        /// Delegate for sorting method.
        /// </summary>
        private readonly Func<OccurrenceInfo<string>, int> order;

        #endregion

        #region Constructors
        
        public WordsOccurrenceCounter()
        {
            this.map = value => value.ToLower();
            this.reduce = grouping => new OccurrenceInfo<string>{Element = grouping.Key, NumberOfOccurrences = grouping.Count()};
            this.order = value => value.NumberOfOccurrences;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Counts words' occurrence in the given collection.
        /// </summary>
        /// <returns>Collection of <see cref="OccurrenceInfo"/>.</returns>
        public IEnumerable<OccurrenceInfo<string>> Count(IEnumerable<string> sourceData)
        {
            IEnumerable<OccurrenceInfo<string>> result = new List<OccurrenceInfo<string>>();
            
            if(sourceData != null)
            {
                result = sourceData.AsParallel()
                             .GroupBy(map)
                             .Select(reduce)
                             .OrderByDescending(order);
            }

            return result.ToList();
        }
        #endregion
    }
}
