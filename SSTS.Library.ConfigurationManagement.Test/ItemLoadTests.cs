using System.Dynamic;
using Moq;
using NUnit.Framework;
using SSTS.Library.Common.Data;

// HINT: to enable debug attachments in VSCode - export VSTEST_HOST_DEBUG=1
namespace SSTS.Library.ConfigurationManagement.Test
{
    public class ItemLoadTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ItemsReloadedAfterCyclingOut()
        {
            // Arrange
            var maximumAgeForConfigurationManagementInUnitTest = 500; // milliseconds
            dynamic baseConfiguration = new ExpandoObject();
            baseConfiguration.name = "SSTS.Base";
            baseConfiguration.configuration = new ExpandoObject();
            baseConfiguration.configuration.maximumConfigurationAgeInMilliseconds = maximumAgeForConfigurationManagementInUnitTest;

            var databaseReaderMock = new Mock<IDatabaseReader>();
            databaseReaderMock.Setup(dr => dr.Read(new Dictionary<string, object> { { "name", "SSTS.Base" } })).Returns(baseConfiguration);
            databaseReaderMock.Setup(dr => dr.Read(new Dictionary<string, object> { { "name", "SSTS.ItemUnderTest" } })).Returns(new { name = "SSTS.Base", configuration = new { somethingElseEntirely = "not relevant for test" } });

            var databaseAccessFactoryMock = new Mock<IDatabaseAccessFactory>();
            databaseAccessFactoryMock.Setup(daf => daf.GetReader("ConfigurationManagement")).Returns(databaseReaderMock.Object);

            var sut = new ConfigurationManagementSource(databaseAccessFactoryMock.Object);

            // Act
            sut.Load("SSTS.ItemUnderTest");
            sut.Load("SSTS.ItemUnderTest");
            sut.Load("SSTS.ItemUnderTest");
            sut.Load("SSTS.ItemUnderTest");
            Thread.Sleep(maximumAgeForConfigurationManagementInUnitTest + 50);
            sut.Load("SSTS.ItemUnderTest");

            // Assert
            databaseReaderMock.Verify(dr => dr.Read(new Dictionary<string, object> { { "name", "SSTS.Base" } }), Times.Exactly(1));
            databaseReaderMock.Verify(dr => dr.Read(new Dictionary<string, object> { { "name", "SSTS.ItemUnderTest" } }), Times.Exactly(2));
        }
    }
}