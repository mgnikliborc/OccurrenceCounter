using OccurrenceCounter;
using OccurrenceCounter.DataProvider;
using OccurrenceCounterClient.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OccurrenceCounterClient.ViewModels
{
    /// <summary>
    /// ViewModel for free text View.
    /// </summary>
    public class FreeTextViewModel : BindableBase
    {
        #region Fields and Properties

        private readonly IDataProvider<string> dataProvider;
 
        private readonly IOccurrenceCounter<string> counter;

        private readonly IEventAggregator eventAggregator;

        private string inputText;

        /// <summary>
        /// Gets and sets text provided by user.
        /// </summary>
        public string InputText
        {
            get { return this.inputText; }
            set { this.SetProperty(ref this.inputText, value); }
        }

        /// <summary>
        /// Gets and sets command for performing count.
        /// </summary>
        public ICommand CountOccuranceCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// ViewModel constructor.
        /// </summary>
        /// <param name="aggregator">Event aggregator used for subscribe and publish events.</param>
        /// <param name="wordsCounter">Occurrence counter.</param>
        /// <param name="provider">Data provider.</param>
        public FreeTextViewModel(IEventAggregator aggregator, IOccurrenceCounter<string> wordsCounter, IDataProvider<string> provider)
        {
            this.eventAggregator = aggregator;
            this.counter = wordsCounter;
            this.dataProvider = provider;

            this.eventAggregator.GetEvent<ClearDataEvent>().Subscribe(Clear);
            this.CountOccuranceCommand = new DelegateCommand(this.ExecuteCountOccurence, this.CanExecuteCountOccurence).ObservesProperty(() => this.InputText);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method which cleans input text area.
        /// </summary>
        /// <param name="message">String message.</param>
        public void Clear(string message)
        {
            this.InputText = string.Empty;
        }

        /// <summary>
        /// Method which indicates if count of occurrence can be performed.
        /// </summary>
        /// <returns>True when count can be performed.</returns>
        public bool CanExecuteCountOccurence()
        {
            return !string.IsNullOrWhiteSpace(this.InputText);
        }

        /// <summary>
        /// Asynchronous method which performs words' occurrence count.
        /// </summary>
        private async void ExecuteCountOccurence()
        {
            await Count();
        }

        private Task Count()
        {
            return Task.Run(() => PerformCount());
        }

        private void PerformCount()
        {
            eventAggregator.GetEvent<BusyEvent>().Publish(true);
            this.dataProvider.SetData(this.InputText);
            var result = this.counter.Count(this.dataProvider.GetData()).ToList();

            eventAggregator.GetEvent<ResultReadyEvent>().Publish(result);
            eventAggregator.GetEvent<BusyEvent>().Publish(false);
        }

        #endregion
    }
}
