using C4Sharp.Models.Relationships;

namespace C4Sharp.Models
{
    /// <summary>
    /// A person represents one of the human users of your software system (e.g. actors, roles, personas, etc)
    /// <see href="https://c4model.com/"/>
    /// </summary>
    public sealed class Person : Structure
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="tags">Optional</param>
        public Person(string alias, string label, string description, params Tag[] tags)
            : base(alias, label, description)
        {
            AddTag(tags);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="link"></param>
        /// <param name="tags">Optional</param>
        public Person(string alias, string label, string description, string link, params Tag[] tags)
            : base(alias, label, description, link)
        {
            AddTag(tags);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="boundary"></param>
        /// <param name="tags">Optional</param>
        public Person(string alias, string label, string description, Boundary boundary, params Tag[] tags)
            : base(alias, label, description, boundary)
        {
            AddTag(tags);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="link"></param>
        /// <param name="boundary"></param>
        /// <param name="tags">Optional</param>
        public Person(string alias, string label, string description, string link, Boundary boundary, params Tag[] tags)
            : base(alias, label, description, link, boundary)
        {
            AddTag(tags);
        }
    }
}