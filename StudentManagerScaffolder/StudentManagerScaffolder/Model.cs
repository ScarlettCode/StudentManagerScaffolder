using System;
using Humanizer;

namespace StudentManagerScaffolder
{
    [Serializable]
    public class Model
    {
        public Model(string name)
        {
            Name = name;
        }

        public string SolutionName => "StudentManager";

        public string Name { get; set; }

        public string NamePlural => Name.Pluralize();
        public bool Scaffold { get; set; }
    }
}
