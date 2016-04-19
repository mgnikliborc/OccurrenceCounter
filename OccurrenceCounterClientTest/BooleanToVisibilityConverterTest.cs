using NUnit.Framework;
using OccurrenceCounterClient.Converters;
using System.Windows;

namespace OccurrenceCounterClientTest
{
    [TestFixture]
    public class BooleanToVisibilityConverterTest
    {
        [Test]
        public void Convert_True()
        {
            BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

            Visibility result = (Visibility)converter.Convert(true, null, null, null);

            Assert.AreEqual(Visibility.Visible, result);
        }

        [Test]
        public void ConvertBack_Visible()
        {
            BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

            bool result = (bool)converter.ConvertBack(Visibility.Visible, null, null, null);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Convert_False()
        {
            BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

            Visibility result = (Visibility)converter.Convert(false, null, null, null);

            Assert.AreEqual(Visibility.Hidden, result);
        }

        [Test]
        public void ConvertBack_Hidden()
        {
            BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

            bool result = (bool)converter.ConvertBack(Visibility.Hidden, null, null, null);

            Assert.AreEqual(false, result);
        }

        [Test]
        public void Convert_Null()
        {
            BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void ConvertBack_Null()
        {
            BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

            var result = converter.ConvertBack(null, null, null, null);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void Convert_WrongType()
        {
            BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

            var result = converter.Convert("wrong", null, null, null);

            Assert.AreEqual(null, result);
        }

        [Test]
        public void ConvertBack_WrongType()
        {
            BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

            var result = converter.ConvertBack("wrong", null, null, null);

            Assert.AreEqual(null, result);
        }
    }
}
