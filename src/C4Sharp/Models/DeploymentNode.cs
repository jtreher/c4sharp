using System.Collections.Generic;

namespace C4Sharp.Models
{
    /// <summary>
    /// A deployment node is something like physical infrastructure (e.g. a physical server or device),
    /// virtualised infrastructure (e.g. IaaS, PaaS, a virtual machine), containerised infrastructure
    /// (e.g. a Docker container), an execution environment (e.g. a database server, Java EE web/application
    /// server, Microsoft IIS), etc. Deployment nodes can be nested.
    /// <see href="https://c4model.com/#DeploymentDiagram"/>
    /// </summary>
    public sealed class DeploymentNode : Structure
    {
        public IEnumerable<DeploymentNode> Nodes { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public Container Container { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        /// <param name="description"></param>
        /// <param name="tags">Optional</param>
        public DeploymentNode(string alias, string label, string description, params Tag[] tags)
            : base(alias, label, description)
        {
            Nodes = default;
            Container = default;
            Properties = default;
            AddTag(tags);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="container"></param>
        /// <param name="tags">Optional</param>
        public DeploymentNode(string alias, Container container, params Tag[] tags)
            : base(alias, container?.Label, container?.Description)
        {
            Nodes = default;
            Container = container;
            Properties = default;
            AddTag(tags);
        }
    }
}