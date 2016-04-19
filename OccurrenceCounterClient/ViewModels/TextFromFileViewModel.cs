using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.FolderBrowser;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using OccurrenceCounter;
using OccurrenceCounterClient.Events;
using OccurrenceCounterClient.Resources;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using OccurrenceCounter.DataProvider;

namespace OccurrenceCounterClient.ViewModels
{
    /// <summary>
    /// ViewModel for text from file view.
    /// </summary>
    public class TextFromFileViewModel : BindableBase
    {
        #region Fields and Properties

        private readonly IDataProvider<string> dataProvider;

        private readonly IOccurrenceCounter<string> counter;

        private readonly IEventAggregator eventAggregator;

        private readonly IDialogService fileDialogService;

        private string filePath;

        /// <summary>
        /// Gets and sets path to the file.
        /// </summary>
        public string FilePath
        {
            get { return this.filePath; }
            set { this.SetProperty(ref this.filePath, value); }
        }

        private string errorText;

        /// <summary>
        /// Gets and sets error message in case of error during reading from file.
        /// </summary>
        public string ErrorText
        {
            get { return this.errorText; }
            set { this.SetProperty(ref this.errorText, value); }
        }

        /// <summary>
        /// Gets and sets command for performing count.
        /// </summary>
        public ICommand CountOccuranceCommand { get; set; }

        /// <summary>
        /// Gets and sets command to open file explorer to browse for a file.
        /// </summary>
        public ICommand BrowseCommand { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// ViewModel constructor.
        /// </summary>
        /// <param name="aggregator">Event aggregator used for subscribe and publish events.</param>
        /// <param name="wordsCounter">Occurrence counter.</param>
        /// <param name="provider">Data provider.</param>
        public TextFromFileViewModel(IEventAggregator aggregator, IOccurrenceCounter<string> wordsCounter, IDataProvider<string> provider)
        {
            this.fileDialogService = new DialogService();
            this.eventAggregator = aggregator;
            this.counter = wordsCounter;
            this.dataProvider = provider;

            this.eventAggregator.GetEvent<ClearDataEvent>().Subscribe(Clear);
            this.CountOccuranceCommand = new DelegateCommand(this.ExecuteCountOccurance, this.CanExecuteCountOccurance).ObservesProperty(() => this.FilePath);
            this.BrowseCommand = new DelegateCommand(this.ExecuteBrowse);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method which cleans input text area.
        /// </summary>
        /// <param name="message">String message.</param>
        public void Clear(string message)
        {
            this.FilePath = string.Empty;
            this.ErrorText = string.Empty;
        }

        /// <summary>
        /// Shows file explorer dialog to select text file.
        /// </summary>
        private void ExecuteBrowse()
        {
            OpenFileDialogSettings settings = new OpenFileDialogSettings
            {
                Title = "Select text file to check",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Text Documents (*.txt)|*.txt"
            };

            bool? success = fileDialogService.ShowOpenFileDialog(this, settings);
            if (success == true)
            {
                this.FilePath = settings.FileName;
            }
        }

        /// <summary>
        /// Method which indicates if count of occurrence can be performed.
        /// </summary>
        /// <returns>True when count can be performed.</returns>
        public bool CanExecuteCountOccurance()
        {
            return !string.IsNullOrWhiteSpace(this.FilePath);
        }

        /// <summary>
        /// Asynchronous method which performs words' occurrence count.
        /// </summary>
        private async void ExecuteCountOccurance()
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
            this.ErrorText = string.Empty;
            IEnumerable<OccurrenceInfo<string>> result = new List<OccurrenceInfo<string>>();
            try
            {
                this.dataProvider.SetData(this.FilePath);

                result = this.counter.Count(this.dataProvider.GetData());              
            }
            catch (Exception ex)
            {
                this.ErrorText = ConstantsDictionary.ProblemFile;
            }

            eventAggregator.GetEvent<ResultReadyEvent>().Publish(result);
            eventAggregator.GetEvent<BusyEvent>().Publish(false);
        }

        #endregion
    }
}
