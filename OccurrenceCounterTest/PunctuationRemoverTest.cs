using NUnit.Framework;
using OccurrenceCounter.SignsRemover;

namespace OccurrenceCounterTest
{
    [TestFixture]
    public class PunctuationRemoverTest
    {
        [Test]
        public void Remove_EmptyText()
        {
            PunctuationRemover remover = new PunctuationRemover();

            string result = remover.Remove(string.Empty);
            
            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void Remove_TextWithoutPunctuation()
        {
            PunctuationRemover remover = new PunctuationRemover();

            string result = remover.Remove("ola ma kota");

            Assert.AreEqual("ola ma kota", result);
        }

        [Test]
        public void Remove_TextWithPunctuation()
        {
            PunctuationRemover remover = new PunctuationRemover();

            string result = remover.Remove("ola ma kota, a nawet dwa");

            Assert.AreEqual("ola ma kota a nawet dwa", result);
        }

        [Test]
        public void Remove_nullPath()
        {
            PunctuationRemover remover = new PunctuationRemover();

            string result = remover.Remove(null);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void Remove_TextWithPunctuationAtTheBeginning()
        {
            PunctuationRemover remover = new PunctuationRemover();

            string result = remover.Remove("!ola ma kota, a nawet dwa");

            Assert.AreEqual("ola ma kota a nawet dwa", result);
        }

        [Test]
        public void Remove_TextWithPunctuationAtTheEnd()
        {
            PunctuationRemover remover = new PunctuationRemover();

            string result = remover.Remove("ola ma kota, a nawet dwa!");

            Assert.AreEqual("ola ma kota a nawet dwa", result);
        }
    }
}
