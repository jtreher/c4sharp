using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace C4Sharp.Models
{
    /// <summary>
    /// Tags are optional on all structures.
    /// </summary>
    /// <remarks>
    /// Tags are ouput in PUML on items and can be used for styling as well as additional
    /// data that can appear on structures on the diagram. See the section on Custom Tags here:
    /// https://github.com/plantuml-stdlib/C4-PlantUML
    ///
    /// Tags can have formatting applied in PUMLs like so:
    /// AddElementTag("deprecated", $bgColor="#444444")
    ///
    /// And their use would be in one of the structure methods such as container:
    /// Container(spa, "SPA", "angular", "The main interface that the customer interacts with via v1.0", $tags="deprecated")
    /// </remarks>
    public sealed class Tag : IEquatable<Tag>
    {
        public static readonly Tag Deprecated = new("deprecated", backgroundColor: Color.Red);

        public static readonly Tag Planned = new("planned", backgroundColor: Color.Beige);

        public static readonly Tag InProgress = new("in-progress", backgroundColor: Color.LightSeaGreen);

        public string Value { get; }

        public Color? BackgroundColor { get; }

        public Color? BorderColor { get; }

        public Color? TextColor { get; }

        public bool IsStyled => BackgroundColor.HasValue || BorderColor.HasValue || TextColor.HasValue;

        /// <summary>
        /// Creates an unstyled tag
        /// </summary>
        /// <param name="value">Tested PlantUML and unicode chars are ok</param>
        public Tag(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Contains(',') || value.Length > 100)
            {
                throw new ArgumentException($"nameof(value) must be less than 100 and cannot contain commas. Value: {value}");
            }

            Value = value;
        }

        /// <summary>
        /// Creates a styled tag
        /// </summary>
        /// <param name="value">Tested PlantUML and unicode chars are ok</param>
        /// <param name="textColor"></param>
        /// <param name="backgroundColor"></param>
        /// <param name="borderColor"></param>
        public Tag(string value, Color? textColor = null, Color? backgroundColor = null, Color? borderColor = null) : this(value)
        {
            TextColor = textColor;
            BackgroundColor = backgroundColor;
            BorderColor = borderColor;
        }

        public bool Equals(Tag tag) =>
            Value == tag.Value &&
                EqualityComparer<Color?>.Default.Equals(BackgroundColor, tag.BackgroundColor) &&
                EqualityComparer<Color?>.Default.Equals(BorderColor, tag.BorderColor) &&
                EqualityComparer<Color?>.Default.Equals(TextColor, tag.TextColor);

        public override bool Equals(object obj)
        {
            if (obj is Tag tag)
            {
                return Equals(tag);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, BackgroundColor, BorderColor, TextColor);
        }

        public override string ToString() => Value;
    }

    /// TODO: Extract to file
    public static class ColorExtensions
    {
        public static string ToHex(this Color color) => color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
    }

    /// TODO: Extract to file
    public static class TagExtensions
    {
        public static List<string> ToStringList(this IReadOnlyCollection<Tag> tags) => tags.Select(t => t.Value).ToList();
    }
}