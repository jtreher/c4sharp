using C4Sharp.Models;
using C4Sharp.Models.Diagrams.Core;
using C4Sharp.Models.Plantuml;
using FluentAssertions;
using Xunit;

namespace C4Sharp.Tests.Models
{
    public class PlantUmlCustomTagTests
    {
        [Fact]
        public void GatherTags_Diagram_ReturnsUniqueTags()
        {
            ContainerDiagram diagram = new();

            diagram.Structures = new[]
            {
                new Container(alias: "Test", ContainerType.None, description:"", technology: "", new Tag("TagOne")),
                new Container(alias: "Another Test", ContainerType.None, description:"Contains a matching tag", technology:"", Tag.Deprecated, new Tag("TagOne")),
            };

            var actual = diagram.GatherTags();

            actual.Should().BeEquivalentTo(new[] { new Tag("TagOne"), Tag.Deprecated });
        }
    }
}