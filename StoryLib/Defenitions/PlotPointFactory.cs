using StoryLib.Active;
using StoryLib.Defenitions.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class PlotPointFactory
    {
        public string descriptor { get; set; }
        public Dictionary<string, Filter[]> characterFilters;
        public List<OptionFactory> options { get; set; }



        public PlotPointFactory(string descriptor, List<OptionFactory> options, Dictionary<string, Filter[]> characterFilters)
        {
            this.descriptor = descriptor;
            this.options = options;
            this.characterFilters = characterFilters;
        }

        public PlotPoint generatePlotPoint(Thesaurus thesaurus, Party party)
        {
            PlotContext context = new ContextBuilder(characterFilters).buildContext(party);

            return generatePlotPoint(context, thesaurus, party);
        }

        public PlotPoint generatePlotPoint(PlotContext context, Thesaurus thesaurus, Party party)
        {
            List<Option> generatedOption = new List<Option>();
            foreach (OptionFactory factory in options)
            {
                generatedOption.Add(factory.generateOption(thesaurus, context));
            }


            return new PlotPoint(new WordReplacer().replace(descriptor, thesaurus, context), generatedOption, context, party);
        }
    }
}
