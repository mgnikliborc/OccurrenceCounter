using NUnit.Framework;
using OccurrenceCounter;
using OccurrenceCounter.DataProvider;
using OccurrenceCounter.SignsRemover;
using OccurrenceCounterClient.ViewModels;
using Prism.Events;

namespace OccurrenceCounterClientTest
{
    [TestFixture]
    public class TextFromFileViewModelTest
    {
        [Test]
        public void Clear_FilePath()
        {
            IEventAggregator aggregator = new EventAggregator();
            ISignsRemover<string> remover = new PunctuationRemover();
            TextDataProvider provider = new TextDataProvider(remover);
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();
            TextFromFileViewModel viewModel = new TextFromFileViewModel(aggregator, counter, provider);
            
            viewModel.Clear("someMessage");

            Assert.AreEqual(string.Empty, viewModel.FilePath);
        }

        [Test]
        public void CanExecuteCountOccurance_FilePathEmpty()
        {
            IEventAggregator aggregator = new EventAggregator();
            ISignsRemover<string> remover = new PunctuationRemover();
            TextDataProvider provider = new TextDataProvider(remover);
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();
            TextFromFileViewModel viewModel = new TextFromFileViewModel(aggregator, counter, provider)
            {
                FilePath = string.Empty
            };

            bool result = viewModel.CanExecuteCountOccurance();

            Assert.AreEqual(false, result);
        }

        [Test]
        public void CanExecuteCountOccurance_FilePathNull()
        {
            IEventAggregator aggregator = new EventAggregator();
            ISignsRemover<string> remover = new PunctuationRemover();
            TextDataProvider provider = new TextDataProvider(remover);
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();
            TextFromFileViewModel viewModel = new TextFromFileViewModel(aggregator, counter, provider)
            {
                FilePath = null
            };

            bool result = viewModel.CanExecuteCountOccurance();

            Assert.AreEqual(false, result);
        }

        [Test]
        public void CanExecuteCountOccurance_FilePathtNotEmpty()
        {
            IEventAggregator aggregator = new EventAggregator();
            ISignsRemover<string> remover = new PunctuationRemover();
            TextDataProvider provider = new TextDataProvider(remover);
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();
            TextFromFileViewModel viewModel = new TextFromFileViewModel(aggregator, counter, provider)
            {
                FilePath = "some text"
            };

            bool result = viewModel.CanExecuteCountOccurance();

            Assert.AreEqual(true, result);
        }
    }
}
