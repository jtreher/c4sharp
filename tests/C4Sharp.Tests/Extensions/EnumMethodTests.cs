using C4Sharp.Extensions;
using System.ComponentModel;
using Xunit;

namespace C4Sharp.Tests
{
    public class EnumMethodTests
    {
        public enum Test
        {
            [Description("A description")]
            OneThing,

            AnotherThing,

            [Description]
            LastThing
        }

        [Fact]
        public void GetDescription_WithAttribute_Matches()
        {
            // Arrange
            var sut = Test.OneThing;

            // Act
            var actual = sut.GetDescription();

            // Assert
            Assert.Equal("A description", actual);
        }

        [Fact]
        public void GetDescription_WithoutAttribute_ReturnsItself()
        {
            // Arrange
            var sut = Test.AnotherThing;

            // Act
            var actual = sut.GetDescription();

            // Assert
            Assert.Equal(nameof(Test.AnotherThing), actual);
        }

        [Fact]
        public void GetDescription_WithEmptyAttribute_ReturnsItself()
        {
            // Arrange
            var sut = Test.AnotherThing;

            // Act
            var actual = sut.GetDescription();

            // Assert
            Assert.Equal(nameof(Test.AnotherThing), actual);
        }
    }
}