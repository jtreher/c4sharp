using C4Sharp.Models.Relationships;
using System.Collections.Generic;

namespace C4Sharp.Models
{
    /// <summary>
    /// Container Boundary
    /// </summary>
    public sealed class ContainerBoundary : Structure
    {
        public IEnumerable<Component> Components { get; set; }
        public IEnumerable<Relationship> Relationships { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should  be unique</param>
        /// <param name="label"></param>
        /// <param name="tags">Optional</param>
        public ContainerBoundary(string alias, string label, params Tag[] tags) : base(alias, label)
        {
            Relationships = new Relationship[] { };
            AddTag(tags);
        }
    }
}