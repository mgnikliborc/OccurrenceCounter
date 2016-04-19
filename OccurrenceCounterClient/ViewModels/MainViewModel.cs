using Microsoft.Practices.Prism.Commands;
using OccurrenceCounterClient.Events;
using OccurrenceCounterClient.Resources;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace OccurrenceCounterClient.ViewModels
{
    /// <summary>
    /// ViewModel for main view.
    /// </summary>
    public class MainViewModel : BindableBase
    {
        #region Fields and Properties

        private readonly IRegionManager regionManager;

        private readonly IEventAggregator eventAggregator;

        private bool isModeNotDisplayed;

        /// <summary>
        /// Indicates if mode of checking occurrence was already selected.
        /// </summary>
        public bool IsModeNotDisplayed
        {
            get { return isModeNotDisplayed; }
            set { this.SetProperty(ref this.isModeNotDisplayed, value); }
        }

        private bool isBusy;

        /// <summary>
        /// Indicates if application is busy and perform some task in additional thread.
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set { this.SetProperty(ref this.isBusy, value); }
        }

        /// <summary>
        /// Gets and sets command for navigating between views.
        /// </summary>
        public DelegateCommand<string> NavigateCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// ViewModel's constructor.
        /// </summary>
        /// <param name="manager">Region manager which is used to navigate between views.</param>
        /// <param name="aggregator">Event aggregator used for subscribe and publish events.</param>
        public MainViewModel(IRegionManager manager, IEventAggregator aggregator)
        {
            this.eventAggregator = aggregator;
            this.regionManager = manager;
            this.IsModeNotDisplayed = true;
            this.eventAggregator.GetEvent<BusyEvent>().Subscribe(this.UpdateBusy);
            this.NavigateCommand = new DelegateCommand<string>(this.Navigate);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates is busy indicator.
        /// </summary>
        /// <param name="isBusy">Boolean value specifying if application is busy.</param>
        public void UpdateBusy(bool isBusy)
        {
            this.IsBusy = isBusy;
        }

        /// <summary>
        /// Navigates to specified view and publish event for clear data.
        /// </summary>
        /// <param name="uri">URI representing view to which navigation will be performed.</param>
        public void Navigate(string uri)
        {
            this.regionManager.RequestNavigate(ConstantsDictionary.InputDataRegion, uri);
            if (!this.IsModeNotDisplayed)
            {
                eventAggregator.GetEvent<ClearDataEvent>().Publish(string.Empty);                
            }
            this.IsModeNotDisplayed = false;
        }

        #endregion
    }
}
