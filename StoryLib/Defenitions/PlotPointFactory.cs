using StoryLib.Active;
using StoryLib.Defenitions.Filters;
using StoryLib.Defenitions.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class PlotPointFactory
    {
        public string descriptor { get; set; }
        public Dictionary<string, Filter<PartyMember>[]> characterFilters { get; set; }
        public List<OptionFactory> options { get; set; }
        public Script setupScript { get; set; }
        public List<Tuple<Filter<PlotContext>[], PlotPointFactory>> nestedPlotPoints { get; set; }


        public PlotPointFactory(string descriptor, List<OptionFactory> options, Dictionary<string, Filter<PartyMember>[]> characterFilters, List<Tuple<Filter<PlotContext>[], PlotPointFactory>> nestedPlotPoints, Script setupScript = null)
        {
            this.descriptor = descriptor;
            this.options = options;
            this.characterFilters = characterFilters;
            this.setupScript = setupScript;
            this.nestedPlotPoints = nestedPlotPoints;
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

            PlotPoint plotPoint = new PlotPoint(new WordReplacer().replace(descriptor, thesaurus, context), generatedOption, context);
            if(setupScript != null)
            {
                setupScript.run(context);
            }
            
            foreach(Tuple<Filter<PlotContext>[], PlotPointFactory> addTo in nestedPlotPoints)
            {
                bool shouldAdd = true;
                foreach(Filter<PlotContext> filter in addTo.Item1)
                {
                    if(!filter.valid(context))
                    {
                        shouldAdd = false;
                        break;
                    }

                }

                if(shouldAdd)
                {
                    addTo.Item2.buildInto(plotPoint, thesaurus);
                }
            }

            return plotPoint;
        }

        public void buildInto(PlotPoint plotPoint, Thesaurus thesaurus)
        {
            plotPoint.context = new ContextBuilder(characterFilters).addToContext(plotPoint.context);

            List<Option> generatedOption = new List<Option>();
            foreach (OptionFactory factory in options)
            {
                plotPoint.options.Add(factory.generateOption(thesaurus, plotPoint.context));
            }

            if(setupScript != null)
            {
                setupScript.run(plotPoint.context);
            }
            
            plotPoint.descriptor += new WordReplacer().replace(descriptor, thesaurus, plotPoint.context);
            
        }
    }
}
