using C4Sharp.Models.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C4Sharp.Models
{
    /// <summary>
    /// In order to create these maps of your code, we first need a common set of abstractions to create a ubiquitous
    /// language that we can use to describe the static structure of a software system. The C4 model considers the
    /// static structures of a software system in terms of containers, components and code. And people use the software
    /// systems that we build.
    /// <see href="https://c4model.com/"/>
    /// </summary>
    public abstract class Structure
    {
        public string Alias { get; }
        public string Label { get; }
        public string Description { get; } = string.Empty;

        [Obsolete("Prefer CustomTags")]
        public string[] Tags => CustomTags.Select(t => t.Value).ToArray();

        private readonly List<Tag> _customTags = new();
        public IReadOnlyCollection<Tag> CustomTags => _customTags;
        public string Link { get; } = string.Empty;
        public Boundary Boundary { get; } = Boundary.Internal;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        protected Structure(string alias, string label) =>
            (Alias, Label, Description) = (alias, label, string.Empty);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        protected Structure(string alias, string label, string description) =>
            (Alias, Label, Description) = (alias, label, description);

        //////ADD builders to concrete classes??? Stuck right now trying to keep these one liners around.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="link"></param>
        protected Structure(string alias, string label, string description, string link) =>
            (Alias, Label, Description, Link) = (alias, label, description, link);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="boundary"></param>
        protected Structure(string alias, string label, string description, Boundary boundary) =>
            (Alias, Label, Description, Boundary) = (alias, label, description, boundary);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="link"></param>
        /// <param name="boundary"></param>
        protected Structure(string alias, string label, string description, string link, Boundary boundary) =>
            (Alias, Label, Description, Link, Boundary) = (alias, label, description, link, boundary);

        /// <summary>
        /// Add Tag
        /// </summary>
        /// <param name="tags"></param>
        ///
        public void AddTag(params Tag[] tags) => _customTags.AddRange(tags);

        /// <summary>
        /// Add Tag
        /// </summary>
        /// <param name="tags"></param>
        [Obsolete("Use AddTag(params Tag[] tags")]
        public void AddTag(params string[] tags) => AddTag(tags.Select(t => new Tag(t)).ToArray());

        /// <summary>
        /// Forward relationship
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Relationship operator >(Structure a, Structure b) =>
            new Relationship(a, b, "uses");

        /// <summary>
        /// Bidirectional relationship
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Relationship operator >=(Structure a, Structure b) =>
            new Relationship(a, Direction.Bidirectional, b, "uses");

        /// <summary>
        /// Bidirectional relationship
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Relationship operator <=(Structure a, Structure b) =>
            new Relationship(a, Direction.Bidirectional, b, "uses");

        /// <summary>
        /// Back relationship
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Relationship operator <(Structure a, Structure b) =>
            new Relationship(a, Direction.Back, b, "uses");
    }
}