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
            [Fact]
            public void ToPumlString_CreatesTag_WhenStyled()
            {
                List<Tag> tags = new()
                {
                    new Tag("TagOne", textColor: Color.Red, backgroundColor: Color.Green, borderColor: Color.Blue)
                };

                var actual = tags.ToPumlString();

                Assert.Equal("AddElementTag(\"TagOne\", $borderColor=\"0000FF\", $bgColor=\"008000\", $fontColor=\"FF0000\")", actual);
            }

            [Fact]
            public void ToPumlString_IsEmpty_WhenNotStyled()
            {
            }
        }
    }
}