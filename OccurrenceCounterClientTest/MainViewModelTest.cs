using NUnit.Framework;
using OccurrenceCounterClient.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace OccurrenceCounterClientTest
{
    [TestFixture]
    public class MainViewModelTest
    {
        [Test]
        public void UpdateBusy_Busy()
        {
            IEventAggregator aggregator = new EventAggregator();
            IRegionManager manager = new RegionManager();
            MainViewModel viewModel = new MainViewModel(manager, aggregator);

            viewModel.UpdateBusy(true);

            Assert.AreEqual(true, viewModel.IsBusy);
        }

        [Test]
        public void UpdateBusy_NotBusy()
        {
            IEventAggregator aggregator = new EventAggregator();
            IRegionManager manager = new RegionManager();
            MainViewModel viewModel = new MainViewModel(manager, aggregator);

            viewModel.UpdateBusy(false);

            Assert.AreEqual(false, viewModel.IsBusy);
        }

        [Test]
        public void Navigate_ModeSelected()
        {
            IEventAggregator aggregator = new EventAggregator();
            IRegionManager manager = new RegionManager();
            MainViewModel viewModel = new MainViewModel(manager, aggregator);

            viewModel.Navigate("uri");

            Assert.AreEqual(false, viewModel.IsModeNotDisplayed);
        }
    }
}
