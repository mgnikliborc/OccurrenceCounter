using NUnit.Framework;
using OccurrenceCounter;
using OccurrenceCounterClient.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OccurrenceCounterClientTest
{
    [TestFixture]
    public class ResultViewModelTest
    {
        [Test]
        public void UpdateResult_Text()
        {
            IEventAggregator aggregator = new EventAggregator();
            ResultViewModel viewModel = new ResultViewModel(aggregator);
            var output = new List<OccurrenceInfo<string>>();
            output.Add(new OccurrenceInfo<string> { Element = "text", NumberOfOccurrences=1 });
            viewModel.UpdateResult(output);

            Assert.AreEqual(1, viewModel.Result.Count);
            Assert.AreEqual("text", viewModel.Result[0].Element);
            Assert.AreEqual(1, viewModel.Result[0].NumberOfOccurrences);
        }

        [Test]
        public void UpdateResult_Null()
        {
            IEventAggregator aggregator = new EventAggregator();
            ResultViewModel viewModel = new ResultViewModel(aggregator);

            viewModel.UpdateResult(null);

            Assert.AreEqual(0, viewModel.Result.Count);
        }

        [Test]
        public void UpdateResult_EmptyList()
        {
            IEventAggregator aggregator = new EventAggregator();
            ResultViewModel viewModel = new ResultViewModel(aggregator);

            viewModel.UpdateResult(new List<OccurrenceInfo<string>>());

            Assert.AreEqual(0, viewModel.Result.Count);
        }
    }
}
