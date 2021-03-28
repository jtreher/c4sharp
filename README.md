# C4Sharp
___

C4Sharp (C4S) is a simple .NET superset of [C4-PlantUML](https://github.com/plantuml-stdlib/C4-PlantUML) to generate [C4 Model diagrams](https://c4model.com/) as code (C#). 
It's used for building Context, Container, Component and Deployment diagrams.

## Getting Started

### Install C4Sharp

install

You need these things to run PlantUML:
- [Java](https://www.java.com/en/download/)
- [Graphviz](https://plantuml.com/graphviz-dot) 

### Coding a C4 diagram

To build C4 Diagrams in C4S it's simple. Just use this following structure for all
diagrams:

```c#
var diagram = new <Diagram Type>
{
    Title = "...",
    Structures = new Structure[] { },
    Relationships = new Relationship[]{ }
};
```

For example, with C4S you can create a _C4 Context Diagram_, 
you could use this code:

```c#
var diagram = new ContextDiagram
{
    Title = "System Context diagram for Internet Banking System",
    Structures = new Structure[]
    {
        Customer,
        BankingSystem,
        Mainframe,
        MailSystem
    },
    Relationships = new []
    {
        Customer > BankingSystem,
        (Customer < MailSystem)["Sends e-mails to"],
        (BankingSystem > MailSystem)["Sends e-mails", "SMTP"][Neighbor],
        BankingSystem > Mainframe,
    }
};

PlantumlFile.Save(diagram);
PlantumlFile.ExportToPng(diagram);
```
> **ABOUT**  
> A System Context diagram is a good starting point for diagramming and documenting a software system, allowing you to step back and see the big picture. Draw a diagram showing your system as a box in the centre, surrounded by its users and the other systems that it interacts with.

This code results two files:

- ./c4/System_Context_diagram_for_Internet_Banking_System_C4_Context.puml
- ./c4/System_Context_diagram_for_Internet_Banking_System_C4_Context.png

The result will be:

<div style="text-align: center">

![context-example](docs/images/context-example.png)
</div>

See more samples [here](https://github.com/8T4/c4sharp/tree/main/tests/C4Sharp.Tests/C4Model/Samples)