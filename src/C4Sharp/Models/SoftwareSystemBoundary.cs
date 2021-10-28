using System.Collections.Generic;

namespace C4Sharp.Models
{
    /// <summary>
    /// Software System Boundary
    /// </summary>
    public sealed class SoftwareSystemBoundary : Structure
    {
        public IEnumerable<Container> Containers { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="tags">Optional</param>
        public SoftwareSystemBoundary(string alias, string label, params Tag[] tags)
            : base(alias, label)
        {
            AddTag(tags);
        }
    }
}