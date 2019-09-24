using Microsoft.Extensions.Logging;
using Moq;

namespace Sapient.MedicineTracking.Tests
{
    public abstract class TestBase
    {
        public ILoggerFactory GetLogger()
        {
            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(new Mock<ILogger>().Object);

            return mockLoggerFactory.Object;
        }
    }
}
