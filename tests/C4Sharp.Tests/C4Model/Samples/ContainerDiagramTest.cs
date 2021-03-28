﻿using System.IO;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Relationships;
using Xunit;
using static C4Sharp.Tests.C4Model.Persons;
using static C4Sharp.Tests.C4Model.Systems;
using static C4Sharp.Tests.C4Model.Containers;


namespace C4Sharp.Tests.C4Model.Samples
{
    public class ContainerDiagramTest
    {
        [Fact]
        public void Its_C4_Model_Container_Diagram_Test()
        {
            var diagram = new ContainerDiagram
            {
                Title = "Container diagram for Internet Banking System",
                Structures = new Structure[]
                {
                    Customer,
                    new SoftwareSystemBoundary("c1", "Internet Banking", new[]
                    {
                        WebApp,
                        Spa,
                        MobileApp,
                        Database,
                        BackendApi
                    }),
                    BankingSystem,
                    MailSystem,
                },
                Relationships = new Relationship[]
                {
                    (Customer > WebApp)["Uses", "HTTPS"],
                    (Customer > Spa)["Uses", "HTTPS"],
                    (Customer > MobileApp)["Uses"],

                    (WebApp > Spa)["Delivers"][Position.Neighbor],
                    (Spa > BackendApi)["Uses", "async, JSON/HTTPS"],
                    (MobileApp > BackendApi)["Uses", "async, JSON/HTTPS"],
                    (Database < BackendApi)["Uses", "async, JSON/HTTPS"][Position.Neighbor],
                    
                    (Customer < MailSystem)["Sends e-mails to"],
                    (MailSystem < BackendApi)["Sends e-mails using", "sync, SMTP"],
                    (BackendApi > BankingSystem)["Uses", "sync/async, XML/HTTPS"][Position.Neighbor]
                }
            };

            PlantumlFile.Save(diagram);
            PlantumlFile.ExportToPng(diagram);

            Assert.True(File.Exists($"c4/{diagram.Slug()}.puml"));
        }
    }
}