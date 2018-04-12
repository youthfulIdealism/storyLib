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
        public Dictionary<string, string[]> characterFilters;
        public List<Option> options { get; set; }
        public PlotContext context { get; set; }
        public Party party { get; set; }

        public PlotPoint(string id, string descriptor, List<Option> options, PlotContext context, Party party)
        {
            this.id = id;
            this.descriptor = descriptor;
            this.options = options;
            this.context = context;
            this.party = party;
        }

        public void MakeChoice(int choice)
        {
            options[choice].outcome.run(context, party);
        }
    }
}
