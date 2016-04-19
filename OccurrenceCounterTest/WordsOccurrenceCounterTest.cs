using Moq;
using NUnit.Framework;
using OccurrenceCounter;
using System.Collections.Generic;
using System.Linq;

namespace OccurrenceCounterTest
{   
    [TestFixture]
    public class WordsOccurrenceCounterTest
    {
        [Test]
        public void CountOccurance_Empty()
        {
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();

            IEnumerable<OccurrenceInfo<string>> result = counter.Count(new List<string>());

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void CountOccurance_NullData()
        {
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();

            IEnumerable<OccurrenceInfo<string>> result = counter.Count(null);

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void CountOccurance_NullProvider()
        {
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();

            IEnumerable<OccurrenceInfo<string>> result = counter.Count(null);

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void CountOccurance_NoRepetition()
        {
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();

            IEnumerable<OccurrenceInfo<string>> result = counter.Count(new List<string> { "test1", "test2", "test3" });

            foreach (var element in result)
            {
                Assert.AreEqual(1, element.NumberOfOccurrences);
            }
        }

        [Test]
        public void CountOccurance_Repetition()
        {
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();

            IEnumerable<OccurrenceInfo<string>> result = counter.Count(new List<string> { "test1", "test2", "test2", "test3", "test1", "test4", "test2" }).ToList();

            Assert.AreEqual(2, result.FirstOrDefault(x => x.Element == "test1").NumberOfOccurrences);
            Assert.AreEqual(3, result.FirstOrDefault(x => x.Element == "test2").NumberOfOccurrences);
            Assert.AreEqual(1, result.FirstOrDefault(x => x.Element == "test3").NumberOfOccurrences);
            Assert.AreEqual(1, result.FirstOrDefault(x => x.Element == "test4").NumberOfOccurrences);
        }

        [Test]
        public void CountOccurance_NotCaseSensitive()
        {
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();

            IEnumerable<OccurrenceInfo<string>> result = counter.Count(new List<string> { "test1", "test2", "test3", "Test2", "tESt3" }).ToList();

            Assert.AreEqual(1, result.FirstOrDefault(x => x.Element == "test1").NumberOfOccurrences);
            Assert.AreEqual(2, result.FirstOrDefault(x => x.Element == "test2").NumberOfOccurrences);
            Assert.AreEqual(2, result.FirstOrDefault(x => x.Element == "test3").NumberOfOccurrences);
        }

        [Test]
        public void CountOccurance_SortedByOccurance()
        {
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();

            IEnumerable<OccurrenceInfo<string>> result = counter.Count(new List<string> { "test1", "test2", "test3", "Test2", "tESt3", "tesT3" }).ToList();

            Assert.AreEqual(1, result.FirstOrDefault(x => x.Element == "test1").NumberOfOccurrences);
            Assert.AreEqual(2, result.FirstOrDefault(x => x.Element == "test2").NumberOfOccurrences);
            Assert.AreEqual(3, result.FirstOrDefault(x => x.Element == "test3").NumberOfOccurrences);
            Assert.AreEqual("test3", result.ElementAt(0).Element);
            Assert.AreEqual("test2", result.ElementAt(1).Element);
            Assert.AreEqual("test1", result.ElementAt(2).Element);
        }
    }
}
