using NUnit.Framework;
using OccurrenceCounter;
using System.Collections.Generic;
using System.Linq;
using OccurrenceCounter.DataProvider;
using OccurrenceCounter.SignsRemover;

namespace OccurrenceCounterTest
{
    [TestFixture]
    public class TextDataProviderTest
    {
        [Test]
        public void GetData_EmptyText()
        {
            IDataProvider<string> provider = new TextDataProvider();
            provider.SetData(string.Empty);

            IEnumerable<string> result = provider.GetData();
            
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void GetData_NullInsteadOfText()
        {
            IDataProvider<string> provider = new TextDataProvider();
            provider.SetData(null);

            IEnumerable<string> result = provider.GetData();

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void GetData_NotSettingData()
        {
            IDataProvider<string> provider = new TextDataProvider();

            IEnumerable<string> result = provider.GetData();

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void GetData_StandardNoRepetitionText()
        {
            IDataProvider<string> provider = new TextDataProvider();
            provider.SetData("test1 test2 test3");

            IEnumerable<string> result = provider.GetData();

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Any(x => x == "test1"));
            Assert.IsTrue(result.Any(x => x == "test2"));
            Assert.IsTrue(result.Any(x => x == "test3"));
        }

        [Test]
        public void GetData_NewLine()
        {
            IDataProvider<string> provider = new TextDataProvider();
            provider.SetData(@"test1 test2 
                test3");

            IEnumerable<string> result = provider.GetData();

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Any(x => x == "test1"));
            Assert.IsTrue(result.Any(x => x == "test2"));
            Assert.IsTrue(result.Any(x => x == "test3"));
        }

        [Test]
        public void GetData_StandardTextWithRepetition()
        {
            IDataProvider<string> provider = new TextDataProvider();
            provider.SetData("test1 test2 test2 test3 test1 test4 test2");

            IEnumerable<string> result = provider.GetData();
            
            Assert.AreEqual(7, result.Count());
        }

        [Test]
        public void GetData_TextWithPunctuation()
        {
            ISignsRemover<string> punctuationRemover = new PunctuationRemover();
            IDataProvider<string> provider = new TextDataProvider(punctuationRemover);
            provider.SetData("test1, test2 !test3, .??");

            IEnumerable<string> result = provider.GetData();

            Assert.AreEqual(3, result.Count());
            Assert.IsFalse(result.Any(x => x == ".??"));
            Assert.IsFalse(result.Any(x => x == "!test3"));
            Assert.IsFalse(result.Any(x => x == "test1,"));
            Assert.IsTrue(result.Any(x => x == "test1"));
            Assert.IsTrue(result.Any(x => x == "test2"));
            Assert.IsTrue(result.Any(x => x == "test3"));
        }

        [Test]
        public void GetData_TextWithPunctuation_PunctuationNotRemoved()
        {
            IDataProvider<string> provider = new TextDataProvider();
            provider.SetData("test1, test2 !test3, .??");

            IEnumerable<string> result = provider.GetData();

            Assert.AreEqual(4, result.Count());
            Assert.IsTrue(result.Any(x => x == ".??"));
            Assert.IsTrue(result.Any(x => x == "!test3,"));
            Assert.IsTrue(result.Any(x => x == "test1,"));
            Assert.IsFalse(result.Any(x => x == "test1"));
            Assert.IsTrue(result.Any(x => x == "test2"));
            Assert.IsFalse(result.Any(x => x == "test3"));
        }

        [Test]
        public void GetData_TextWithPunctuationNoSpaceBetween()
        {
            ISignsRemover<string> punctuationRemover = new PunctuationRemover();
            IDataProvider<string> provider = new TextDataProvider(punctuationRemover);
            provider.SetData("test1,test2!test3,.??");

            IEnumerable<string> result = provider.GetData();

            Assert.AreEqual(3, result.Count());
            Assert.IsFalse(result.Any(x => x == ".??"));
            Assert.IsFalse(result.Any(x => x == "!test3"));
            Assert.IsFalse(result.Any(x => x == "test1,test2"));
            Assert.IsTrue(result.Any(x => x == "test1"));
            Assert.IsTrue(result.Any(x => x == "test2"));
            Assert.IsTrue(result.Any(x => x == "test3"));
        }

        [Test]
        public void GetData_NoTextOnlyPunctuation()
        {
            ISignsRemover<string> punctuationRemover = new PunctuationRemover();
            IDataProvider<string> provider = new TextDataProvider(punctuationRemover);
            provider.SetData(", !? : ---");

            IEnumerable<string> result = provider.GetData();

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void SetData_EmptyText()
        {
            TextDataProvider provider = new TextDataProvider();

            provider.SetData(string.Empty);

            Assert.AreEqual(string.Empty, provider.Text);
        }

        [Test]
        public void SetData_SomeText()
        {
            TextDataProvider provider = new TextDataProvider();

            provider.SetData("someString");

            Assert.AreEqual("someString", provider.Text);
        }

        [Test]
        public void SetData_nullPath()
        {
            TextDataProvider provider = new TextDataProvider();

            provider.SetData(null);
            
            Assert.AreEqual(string.Empty, provider.Text);
        }
    }
}
