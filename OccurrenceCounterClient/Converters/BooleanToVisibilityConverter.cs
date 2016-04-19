using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace OccurrenceCounterClient.Converters
{
    /// <summary>
    /// Converter for transforming boolean value to visibility.
    /// True will be converted to visible and false will change to hidden.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        #region Fields and Properties
        /// <summary>
        /// Gets and sets visibility for true.
        /// </summary>
        public Visibility ValueForTrue { get; set; }

        /// <summary>
        /// Gets and sets visibility for false.
        /// </summary>
        public Visibility ValueForFalse { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Bool to visibility converter constructor.
        /// </summary>
        public BooleanToVisibilityConverter()
        {
            this.ValueForTrue = Visibility.Visible;
            this.ValueForFalse = Visibility.Hidden;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts bool to visibility.
        /// </summary>
        /// <param name="value">Boolean value.</param>
        /// <param name="targetType">Target type on which convert will be performed.</param>
        /// <param name="parameter">Parameter which can be used during conversion.</param>
        /// <param name="culture">Information about culture.</param>
        /// <returns>Visible when value true and Hidden when value false.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                return null;
            }
            return (bool)value ? this.ValueForTrue : this.ValueForFalse;
        }

        /// <summary>
        /// Converts from visibility to boolean.
        /// </summary>
        /// <param name="value">Visibility value.</param>
        /// <param name="targetType">Target type on which convert will be performed.</param>
        /// <param name="parameter">Parameter which can be used during conversion.</param>
        /// <param name="culture">Information about culture.</param>
        /// <returns>True when value Visible and false when value Hidden.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, this.ValueForTrue))
            {
                return true;
            }                
            if (Equals(value, this.ValueForFalse))
            {
                return false;
            }
            return null;
        }

        #endregion
    }
}
