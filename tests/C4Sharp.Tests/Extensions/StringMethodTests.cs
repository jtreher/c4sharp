using C4Sharp.Extensions;
using Xunit;

namespace C4Sharp.Tests.Extensions
{
    public class StringMethodTests
    {
        [Theory]
        [InlineData("same", "same")]
        [InlineData("LOWERCASE", "lowercase")]
        [InlineData("à", "a")]
        [InlineData("no-àccents", "no-accents")]
        [InlineData("space to hyphen", "space-to-hyphen")]
        [InlineData("non!alpha!", "nonalpha")]
        [InlineData("many     spaces    spaces", "many-spaces-spaces")]
        [InlineData("Some N0mb34s", "some-n0mb34s")]
        [InlineData("uniѬcode", "unicode")]
        [InlineData("All!     The-thingsѬ", "all-the-things")]
        public void GenerateSlug_WillFormatsHappyPaths_Correctly(string input, string expected)
        {
            var actual = input.GenerateSlug();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("ѬѬѬѬ", "")]
        [InlineData("ѬѬ  ѬѬ", "")]
        [InlineData("!ѬѬѬѬ!", "")]
        [InlineData("               ", "")]
        [InlineData("", "")]
        public void GenerateSlug_WillFormatsEdgeCase_WithEmptyStrings(string input, string expected)
        {
            var actual = input.GenerateSlug();

            Assert.Equal(expected, actual);
        }
    }
}