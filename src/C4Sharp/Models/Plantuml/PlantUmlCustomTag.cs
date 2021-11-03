using C4Sharp.Models.Diagrams;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C4Sharp.Models.Plantuml
{
    internal static class PlantUmlCustomTag
    {
        public static IReadOnlyCollection<Tag> GatherTags(this Diagram diagram)
            => diagram.Structures.SelectMany(s => s.GatherTags()).Distinct().ToList();

        private static IEnumerable<Tag> GatherTags(this Structure structure)
        {
            return structure switch
            {
                Person person => person.CustomTags,
                SoftwareSystem system => system.CustomTags,
                SoftwareSystemBoundary softwareSystemBoundary => softwareSystemBoundary.GatherTags(),
                DeploymentNode deploymentNode => deploymentNode.GatherTags(),
                Component component => component.CustomTags,
                Container container => container.CustomTags,
                ContainerBoundary containerBoundary => containerBoundary.GatherTags(),
                _ => Enumerable.Empty<Tag>()
            };
        }

        private static IEnumerable<Tag> GatherTags(this SoftwareSystemBoundary boundary)
            => boundary.CustomTags.Concat(boundary.Containers.SelectMany(c => c.CustomTags));

        private static IEnumerable<Tag> GatherTags(this ContainerBoundary boundary)
            => boundary.Components.SelectMany(c => c.CustomTags);

        private static IEnumerable<Tag> GatherTags(this DeploymentNode deployment)
            => deployment.CustomTags.Concat(deployment.Nodes.SelectMany(n => n.CustomTags).Concat(deployment.Container.CustomTags));

        /// <summary>
        ///
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        /// <remarks>Example output:
        ///          AddElementTag("enlightened", $fontColor="#fdae61", $borderColor="#fdae61")
        ///         AddElementTag("deprecated", $bgColor = "#444444")</remarks>
        public static string ToPumlString(this IEnumerable<Tag> tags)
        {
            var outerSb = new StringBuilder();

            /// Todo: this method is not getting called with any tags. Why aren't they set?
            foreach (var tag in tags.Where(t => t.IsStyled))
            {
                var sb = new StringBuilder();

                if (tag.BorderColor.HasValue)
                {
                    sb.Append($", $borderColor=\"{tag.BorderColor.Value.ToHex()}\"");
                }

                if (tag.BackgroundColor.HasValue)
                {
                    sb.Append($", $bgColor=\"{tag.BackgroundColor.Value.ToHex()}\"");
                }

                if (tag.TextColor.HasValue)
                {
                    sb.Append($", $fontColor=\"{tag.TextColor.Value.ToHex()}\"");
                }

                outerSb.AppendLine($"AddElementTag(\"{tag.Value}\"{sb})");
            }

            return outerSb.ToString();
        }
    }
}