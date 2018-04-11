using StoryLib.Active;
using StoryLib.Defenitions.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class PlotPointFactory
    {
        public string id { get; private set; }
        public string descriptor { get; set; }
        public Dictionary<string, Filter[]> characterFilters;
        public List<OptionFactory> options { get; set; }



        public PlotPointFactory(string id, string descriptor, List<OptionFactory> options, Dictionary<string, Filter[]> characterFilters)
        {
            this.id = id;
            this.descriptor = descriptor;
            this.options = options;
            this.characterFilters = characterFilters;
        }

        public PlotPoint generatePlotPoint(Thesaurus thesaurus, Party party)
        {
            PlotContext context = new ContextBuilder(characterFilters).buildContext(party);

            List<Option> generatedOption = new List<Option>();
            foreach(OptionFactory factory in options)
            {
                generatedOption.Add(factory.generateOption(thesaurus, context));
            }


            return new PlotPoint(id, new WordReplacer().replace(descriptor, thesaurus, context), generatedOption);
        }
    }
}
