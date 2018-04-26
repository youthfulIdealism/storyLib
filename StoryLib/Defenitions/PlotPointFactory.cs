using StoryLib.Active;
using StoryLib.Defenitions.Filters;
using StoryLib.Defenitions.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class PlotPointFactory//
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
            PlotContext context = new ContextBuilder(characterFilters).buildContext(party, thesaurus);

            return generatePlotPoint(context, thesaurus);
        }

        public PlotPoint generatePlotPoint(PlotContext context, Thesaurus thesaurus)
        {
            

            PlotPoint plotPoint = new PlotPoint("", new List<Option>(), context);
            if(setupScript != null)
            {
                setupScript.run(context);
            }

            //delay generation of options and descriptor until after the setup script has run.
            plotPoint.descriptor = new WordReplacer().replace(descriptor, plotPoint.context);

            foreach (OptionFactory factory in options)
            {
                plotPoint.options.Add(factory.generateOption(plotPoint.context));
            }


            foreach (Tuple<Filter<PlotContext>[], PlotPointFactory> addTo in nestedPlotPoints)
            {
                bool shouldAdd = true;
                foreach(Filter<PlotContext> filter in addTo.Item1)
                {
                    if(!filter.valid(plotPoint.context))
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
            ContextBuilder newContext = new ContextBuilder(characterFilters);

            if(!newContext.canAddToContext(plotPoint.context))
            {
                return;
            }

            plotPoint.context = newContext.addToContext(plotPoint.context);
            List<Option> generatedOption = new List<Option>();
            foreach (OptionFactory factory in options)
            {
                
                plotPoint.options.Add(factory.generateOption(plotPoint.context));
            }

            if(setupScript != null)
            {
                setupScript.run(plotPoint.context);
            }
            
            plotPoint.descriptor += " " + new WordReplacer().replace(descriptor, plotPoint.context);


            foreach (Tuple<Filter<PlotContext>[], PlotPointFactory> addTo in nestedPlotPoints)
            {
                bool shouldAdd = true;
                foreach (Filter<PlotContext> filter in addTo.Item1)
                {
                    if (!filter.valid(plotPoint.context))
                    {
                        shouldAdd = false;
                        break;
                    }

                }

                if (shouldAdd)
                {
                    addTo.Item2.buildInto(plotPoint, thesaurus);
                }
            }
        }
    }
}
