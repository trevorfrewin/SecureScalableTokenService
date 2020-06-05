using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
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
            var databaseConnection = new DatabaseConnection { ConnectionString = "ConnectionString", DatabaseName = "DatabaseName", CollectionName = "CollectionName" };
            var databaseConnectionSets = new List<DatabaseConnectionSet> { new DatabaseConnectionSet { Name = "ConfigurationManagement", Connection = databaseConnection } };
            var databaseReaderMock = new Mock<IDatabaseReader>();

            dynamic baseConfiguration = new ExpandoObject();
            baseConfiguration.name = "SSTS.Base";
            baseConfiguration.configuration = new ExpandoObject();
            baseConfiguration.configuration.maximumConfigurationAgeInMilliseconds = 500;
            databaseReaderMock.Setup(dr => dr.Read(databaseConnection, new Dictionary<string, object> { { "name", "SSTS.Base" } })).Returns(baseConfiguration);
            databaseReaderMock.Setup(dr => dr.Read(databaseConnection, new Dictionary<string, object> { { "name", "SSTS.ItemUnderTest" } })).Returns(new { name = "SSTS.Base", configuration = new { somethingElseEntirely = "not relevant for test" } });
            var sut = new ConfigurationManagementSource(databaseConnectionSets, databaseReaderMock.Object);

            // Act
            sut.Load("SSTS.ItemUnderTest");
            sut.Load("SSTS.ItemUnderTest");
            sut.Load("SSTS.ItemUnderTest");
            sut.Load("SSTS.ItemUnderTest");
            Thread.Sleep(maximumAgeForConfigurationManagementInUnitTest + 50);
            sut.Load("SSTS.ItemUnderTest");

            // Assert
            databaseReaderMock.Verify(dr => dr.Read(databaseConnection, new Dictionary<string, object> { { "name", "SSTS.Base" } }), Times.Exactly(1));
            databaseReaderMock.Verify(dr => dr.Read(databaseConnection, new Dictionary<string, object> { { "name", "SSTS.ItemUnderTest" } }), Times.Exactly(2));
        }
    }
}