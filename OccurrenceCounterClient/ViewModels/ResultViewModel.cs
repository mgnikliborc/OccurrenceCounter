using OccurrenceCounterClient.Events;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OccurrenceCounter;
using System;

namespace OccurrenceCounterClient.ViewModels
{
    /// <summary>
    /// ViewModel for result view.
    /// </summary>
    public class ResultViewModel : BindableBase
    {
        #region Fields and Properties
               
        private ObservableCollection<OccurrenceInfo<string>> result;

        /// <summary>
        /// Gets and sets collection of results.
        /// </summary>
        public ObservableCollection<OccurrenceInfo<string>> Result
        {
            get { return this.result; }
            set { this.SetProperty(ref this.result, value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// ViewModel constructor.
        /// </summary>
        /// <param name="eventAggregator">Event aggregator used for subscribe and publish events.</param>
        public ResultViewModel(IEventAggregator eventAggregator)
        {            
            eventAggregator.GetEvent<ResultReadyEvent>().Subscribe(UpdateResult);
            eventAggregator.GetEvent<ClearDataEvent>().Subscribe(Clear);
        }
            
        #endregion

        #region Methods

        /// <summary>
        /// Updates collection of results.
        /// </summary>
        /// <param name="resultToUpdate">New collection of results.</param>
        public void UpdateResult(IEnumerable<OccurrenceInfo<string>> resultToUpdate)
        {
            if(resultToUpdate != null)
            {
                this.Result = new ObservableCollection<OccurrenceInfo<string>>(resultToUpdate);
            }
            else
            {
                this.Result = new ObservableCollection<OccurrenceInfo<string>>();
            }
        }

        /// <summary>
        /// Method which cleans input text area.
        /// </summary>
        /// <param name="message">String message.</param>
        public void Clear(string obj)
        {
            this.Result = new ObservableCollection<OccurrenceInfo<string>>();
        }

        #endregion
    }
}
