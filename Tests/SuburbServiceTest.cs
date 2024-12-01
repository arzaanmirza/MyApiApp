namespace MyProject.Tests;
using Xunit;
using Moq;
public class SuburbServiceTest
{
    [Fact]
    public void getCorrectPropertyInfo()
    {
        var mockLoadingData = new Mock<LoadingData>();
        var suburbsData = new Dictionary<int, SuburbRecord>
        {
            { 1, new SuburbRecord { Id = 1, Suburb = "Millers Point", Value = 2250, Date = "04/03/2023", NumberOfBedrooms = 4, Type = "house" } }
        };
        mockLoadingData.Setup(loadingData => loadingData.loadSuburbData(It.IsAny<string>())).Returns(suburbsData);

        var suburbService = new SuburbService(mockLoadingData.Object);
        var response = suburbService.getPropertyId("1");

        Assert.NotNull(response);
        Assert.Equal(1, response.Id);
        Assert.Equal("Millers Point", response.Suburb);
        Assert.Equal(2250, response.Value);
        Assert.Equal("04/03/2023", response.Date);
        Assert.Equal(4, response.NumberOfBedrooms);
        Assert.Equal("house", response.Type);
    }

}
