using Moq;
using NUnit.Framework;
using OccurrenceCounter;
using OccurrenceCounterClient.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OccurrenceCounter.DataProvider;
using OccurrenceCounter.SignsRemover;

namespace OccurrenceCounterClientTest
{
    [TestFixture]
    public class FreeTextViewModelTest
    {
        [Test]
        public void Clear_InputText()
        {
            IEventAggregator aggregator = new EventAggregator();
            ISignsRemover<string> remover = new PunctuationRemover();
            TextDataProvider provider = new TextDataProvider(remover);
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();
            FreeTextViewModel viewModel = new FreeTextViewModel(aggregator, counter, provider);

            viewModel.Clear("someMessage");

            Assert.AreEqual(string.Empty, viewModel.InputText);
        }

        [Test]
        public void CanExecuteCountOccurance_InputTextEmpty()
        {
            IEventAggregator aggregator = new EventAggregator();
            ISignsRemover<string> remover = new PunctuationRemover();
            TextDataProvider provider = new TextDataProvider(remover);
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();
            FreeTextViewModel viewModel = new FreeTextViewModel(aggregator, counter, provider)
            {
                InputText = string.Empty
            };

            bool result = viewModel.CanExecuteCountOccurence();

            Assert.AreEqual(false, result);
        }

        [Test]
        public void CanExecuteCountOccurance_InputTextNull()
        {
            IEventAggregator aggregator = new EventAggregator();
            ISignsRemover<string> remover = new PunctuationRemover();
            TextDataProvider provider = new TextDataProvider(remover);
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();
            FreeTextViewModel viewModel = new FreeTextViewModel(aggregator, counter, provider)
            {
                InputText = null
            };

            bool result = viewModel.CanExecuteCountOccurence();

            Assert.AreEqual(false, result);
        }

        [Test]
        public void CanExecuteCountOccurance_InputTextNotEmpty()
        {
            IEventAggregator aggregator = new EventAggregator();
            ISignsRemover<string> remover = new PunctuationRemover();
            TextDataProvider provider = new TextDataProvider(remover);
            IOccurrenceCounter<string> counter = new WordsOccurrenceCounter();
            FreeTextViewModel viewModel = new FreeTextViewModel(aggregator, counter, provider)
            {
                InputText = "some text"
            };

            bool result = viewModel.CanExecuteCountOccurence();

            Assert.AreEqual(true, result);
        }
    }
}
