using StoryLib.Defenitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Active
{
    public class PlotPoint
    {
        public string id { get; private set; }
        public string descriptor { get; set; }

        public PlotPoint(string id, string descriptor, List<Option> options)
        {
            this.id = id;
            this.descriptor = descriptor;
            this.options = options;
        }

        public Dictionary<string, string[]> characterFilters;
        public List<Option> options { get; set; }
    }
}
