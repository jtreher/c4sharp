using C4Sharp.FileSystem;
using C4Sharp.Models.Diagrams;
using System.IO;
using System.Linq;
using System.Text;

namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// Parser Diagram to PlantUML
    /// </summary>
    public static class PlantumlDiagram
    {
        /// <summary>
        /// Create PUML content from Diagram
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="useStandardLibrary"></param>
        /// <returns></returns>
        public static string ToPumlString(this Diagram diagram, bool useStandardLibrary = false)
        {
            var path = GetPumlFilePath(diagram, useStandardLibrary);

            var tags = diagram.GatherTags();

            var stream = new StringBuilder();
            stream.AppendLine($"@startuml {diagram.Slug()}");
            stream.AppendLine($"!include {path}");
            stream.AppendLine();

            if (diagram.LayoutWithLegend && !diagram.ShowLegend)
            {
                stream.AppendLine("LAYOUT_WITH_LEGEND()");
            }

            if (diagram.LayoutAsSketch)
            {
                stream.AppendLine("LAYOUT_AS_SKETCH()");
            }

            stream.Append(tags.ToPumlString());

            stream.AppendLine(PlantUmlCustomTag.ToPumlString(diagram.Structures.SelectMany(s => s.CustomTags)));

            stream.AppendLine($"{(diagram.FlowVisualization == DiagramLayout.TopDown ? "LAYOUT_TOP_DOWN()" : "LAYOUT_LEFT_RIGHT()")}");
            stream.AppendLine();

            if (!string.IsNullOrWhiteSpace(diagram.Title))
            {
                stream.AppendLine($"title {diagram.Title}");
                stream.AppendLine();
            }

            foreach (var structure in diagram.Structures)
            {
                stream.AppendLine(structure.ToPumlString());
            }

            stream.AppendLine();

            foreach (var relationship in diagram.Relationships)
            {
                stream.AppendLine(relationship.ToPumlString());
            }

            if (diagram.ShowLegend)
            {
                stream.AppendLine();
                stream.AppendLine("SHOW_LEGEND()");
            }

            stream.AppendLine("@enduml");
            return stream.ToString();
        }

        private static string GetPumlFilePath(this Diagram diagram, bool useStandardLibrary)
        {
            return useStandardLibrary
                ? $"<C4/{diagram.Name}>"
                : Path.Join(C4Directory.ResourcesFolderName, $"{diagram.Name}.puml");
        }
    }
}