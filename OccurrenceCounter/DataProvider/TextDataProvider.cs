using System;
using System.Collections.Generic;
using System.Linq;
using OccurrenceCounter.SignsRemover;

namespace OccurrenceCounter.DataProvider
{
    /// <summary>
    /// Class which provides text data. They can be modified to remove not needed signs.
    /// </summary>
    public class TextDataProvider : IDataProvider<string>
    {
        #region Fields and Properties

        /// <summary>
        /// Keeps instance of class which removes not needed signs.
        /// </summary>
        private readonly ISignsRemover<string> signsRemover;

        private string text;

        /// <summary>
        /// Provided text.
        /// </summary>
        public string Text 
        { 
            get { return this.text; } 
            protected set { this.text = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Text data provider constructor.
        /// </summary>
        public TextDataProvider()
        {
            this.text = string.Empty;
        }

        /// <summary>
        /// Text data provider constructor.
        /// </summary>
        /// <param name="remover">Instance of <see cref="ISignsRemover<string>"/>, which will remove not needed signs.</param>
        public TextDataProvider(ISignsRemover<string> remover)
        {
            this.text = string.Empty;
            this.signsRemover = remover;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets text.
        /// </summary>
        /// <param name="data">Text to be prepared.</param>
        public virtual void SetData(string data)
        {
            this.text = string.IsNullOrEmpty(data) ? string.Empty : data;
        }

        /// <summary>
        /// Provides collection of words which were included in set text.
        /// </summary>
        /// <returns>Collection of words.</returns>
        public IEnumerable<string> GetData()
        {
            string data = this.PrepareData();

            char[] spliters = { ' ', '\t', '\n', '\r' };
            IEnumerable<string> words = data.Split(spliters, StringSplitOptions.RemoveEmptyEntries);

            return words.ToList();
        }

        /// <summary>
        /// If signs remover is present prepares text by removing not needed signs. 
        /// </summary>
        /// <returns>If signs remover was present returns data without not needed signs, 
        /// otherwise returns previously set data.</returns>
        protected string PrepareData()
        {
            string data = this.text;
            if (this.signsRemover != null)
            {
                data = this.signsRemover.Remove(this.text);
            }
            return data;
        }
        #endregion
    }
}
