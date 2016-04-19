using System.Text;
using System.Text.RegularExpressions;

namespace OccurrenceCounter.SignsRemover
{
    /// <summary>
    /// Class which removes punctuation from string.
    /// </summary>
    public class PunctuationRemover : ISignsRemover<string>
    {
        /// <summary>
        /// Removes punctuation from string.
        /// </summary>
        /// <param name="data">String from which punctuation should be removed.</param>
        /// <returns>String without punctuation.</returns>
        public string Remove(string data)
        {
            if (data != null)
            {
                Regex rgx = new Regex("[^a-zA-Z0-9 ]");
                data = rgx.Replace(data, " ");
                data = data.Replace("  ", " ");
                data = data.Trim();            
            }
            
            return data;
        }
    }
}
