using Xunit;

namespace C4Sharp.Tests.Extensions
{
    public class ResourceMethodTests
    {
        [Fact(Skip = "I am not sure how I want to hande static classes with volatile dependencies.")]
        public void GetPlantumlResource_ReturnsStream_WhenExists()
        {
        }
    }
}