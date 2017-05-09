using System.Collections.Generic;

public class Person
{
    public string id { get; set; }
    public string name { get; set; }
    public int age { get; set; }
    public List<Relation> relations { get; set; }
}

public class Relation
{
    public string name { get; set; }
    public int age { get; set; }
    public string relation { get; set; }
}