using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Diagrams.Core;
using Xunit;

namespace C4Sharp.Tests.Models.Diagrams
{
    public class DiagramTests
    {
        /// <summary>
        /// Should slug handle cases where title is not set since it is not required?
        /// </summary>
        [Fact]
        public void Slug_WillGenerate_WithoutTitle()
        {
            Diagram diagram = new ContainerDiagram();

            var actual = diagram.Slug();

            Assert.Equal("-c4container", actual);
        }

        [Fact]
        public void Slug_WillGenerate_WithTitle()
        {
            Diagram diagram = new ContainerDiagram();

            diagram.Title = "Nice Diagram";

            var actual = diagram.Slug();

            Assert.Equal("nice-diagram-c4container", actual);
        }
    }
}