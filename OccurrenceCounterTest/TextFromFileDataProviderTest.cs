using Moq;
using Moq.Protected;
using NUnit.Framework;
using OccurrenceCounter.DataProvider;

namespace OccurrenceCounterTest
{
    [TestFixture]
    public class TextFromFileDataProviderTest
    {
        [Test]
        public void SetData_EmptyText()
        {
            Mock<TextFromFileDataProvider> mockProvider = new Mock<TextFromFileDataProvider> { CallBase = true };
            mockProvider.Protected().Setup<string>("ReadFromFile", string.Empty).Returns("test data");
            TextFromFileDataProvider provider = mockProvider.Object;

            provider.SetData(string.Empty);

            mockProvider.Protected().Verify("ReadFromFile", Times.Never(), string.Empty);
            Assert.AreEqual(string.Empty, provider.Text);
        }

        [Test]
        public void SetData_SomePath()
        {
            Mock<TextFromFileDataProvider> mockProvider = new Mock<TextFromFileDataProvider> { CallBase = true };
            mockProvider.Protected().Setup<string>("ReadFromFile", "somePath").Returns("test data");
            TextFromFileDataProvider provider = mockProvider.Object;

            provider.SetData("somePath");

            mockProvider.Protected().Verify("ReadFromFile", Times.Once(), "somePath");
            Assert.AreEqual("test data", provider.Text);
        }

        [Test]
        public void SetData_nullPath()
        {
            Mock<TextFromFileDataProvider> mockProvider = new Mock<TextFromFileDataProvider> { CallBase = true };
            mockProvider.Protected().Setup<string>("ReadFromFile", ItExpr.IsNull<string>()).Returns("test data");
            TextFromFileDataProvider provider = mockProvider.Object;

            provider.SetData(null);

            mockProvider.Protected().Verify("ReadFromFile", Times.Never(), ItExpr.IsNull<string>());
            Assert.AreEqual(string.Empty, provider.Text);
        }
    }
}
