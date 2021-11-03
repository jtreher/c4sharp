using C4Sharp.Models;
using C4Sharp.Models.Diagrams.Core;
using C4Sharp.Models.Plantuml;
using FluentAssertions;
using System.Collections.Generic;
using System.Drawing;
using Xunit;

namespace C4Sharp.Tests.Models
{
    public class PlantUmlCustomTagTests
    {
        public class GatherTags
        {
            [Fact]
            public void GatherTags_Diagram_ReturnsUniqueTags()
            {
                ContainerDiagram diagram = new();

                diagram.Structures = new Structure[]
                {
                new Container(alias: "Test", ContainerType.None, description:"", technology: "", new Tag("TagOne")),
                new Container(alias: "Another Test", ContainerType.None, description:"Contains a matching tag", technology:"", Tag.Deprecated, new Tag("TagOne")),
                new Person("Person","","",Tag.Deprecated),
                new SoftwareSystemBoundary("SoftwareSystemBoundary", "", new Tag("TagTwo"))
                {
                    Containers = new []
                    {
                        new Container(alias: "SSBChildContainer", ContainerType.None, description:"", technology: "", new Tag("TagThree")),
                        new Container(alias: "SSBChildContainer2", ContainerType.None, description:"", technology: "", new Tag("TagThree")),
                    }
                },
                new SoftwareSystem("SoftwareSystem","",new Tag("TagFour")),
                new DeploymentNode("DeploymentNode","","",new Tag("TagFive")),
                new Component("Component","","","", new Tag("TagSix")),
                new Component("Component","","","", new Tag("TagSeven")),
                };

                var actual = diagram.GatherTags();

                actual.Should().BeEquivalentTo(new[] {
                new Tag("TagOne"), Tag.Deprecated, new Tag("TagThree"),
                new Tag("TagTwo"), new Tag("TagFour"), new Tag("TagFive"),
                new Tag("TagSix"), new Tag("TagSeven") });
            }
        }

        public class ToPumlString
        {
            [Theory]
            [MemberData(nameof(StyledTagsTheories))]
            public void ToPumlString_CreatesTag_WhenStyled(string caseName, Tag arrangement, string expected)
            {
                _ = caseName;

                List<Tag> tags = new()
                {
                    arrangement
                };

                var actual = tags.ToPumlString();

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void ToPumlString_IsEmpty_WhenNotStyled()
            {
                List<Tag> tags = new()
                {
                    new Tag("TagOne")
                };

                var actual = tags.ToPumlString();

                Assert.Equal(string.Empty, actual);
            }

            public static IEnumerable<object[]> StyledTagsTheories()
            {
                yield return new object[]
                {
                    "All Styles",
                    new Tag("TagOne", textColor: Color.Red, backgroundColor: Color.Green, borderColor: Color.Blue),
                    "AddElementTag(\"TagOne\", $borderColor=\"0000FF\", $bgColor=\"008000\", $fontColor=\"FF0000\")"
                };

                yield return new object[]
                {
                    "Text Only",
                    new Tag("TagOne", textColor: Color.Red),
                    "AddElementTag(\"TagOne\", $fontColor=\"FF0000\")"
                };
                yield return new object[]
                {
                    "Background Only",
                    new Tag("TagOne", backgroundColor: Color.Green),
                    "AddElementTag(\"TagOne\", $bgColor=\"008000\")"
                };

                yield return new object[]
                {
                    "Border Only",
                    new Tag("TagOne", borderColor: Color.Blue),
                    "AddElementTag(\"TagOne\", $borderColor=\"0000FF\")"
                };
            }
        }
    }
}