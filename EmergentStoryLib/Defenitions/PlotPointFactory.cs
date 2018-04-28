using EmergentStoryLib.Instance;
using EmergentStoryLib.Defenitions.Filters;
using EmergentStoryLib.Defenitions.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions
{
    /**
     * Main point of interaction for generating plots.
     * */
    public class PlotPointFactory
    {
        /**
         * Pre-word-replacement text.
         * */
        public string text { get; set; }

        /**
         * Map of roles and conditions required to fulfill those roles.
         * */
        public Dictionary<string, Filter<PartyMember>[]> characterFilters { get; set; }

        /**
         * Options
         * */
        public List<OptionFactory> options { get; set; }

        /**
         * Setup script to be run as soon as the plot is generated
         * */
        public Script setupScript { get; set; }

        /**
         * Plot points contained in filters, to be generated if filter conditions are met.
         * */
        public List<Tuple<Filter<PlotContext>[], PlotPointFactory>> nestedPlotPoints { get; set; }


        public PlotPointFactory(string descriptor, List<OptionFactory> options, Dictionary<string, Filter<PartyMember>[]> characterFilters, List<Tuple<Filter<PlotContext>[], PlotPointFactory>> nestedPlotPoints, Script setupScript = null)
        {
            this.text = descriptor;
            this.options = options;
            this.characterFilters = characterFilters;
            this.setupScript = setupScript;
            this.nestedPlotPoints = nestedPlotPoints;
        }

        /**
         * Generates a new plot from scratch.
         * */
        public PlotPoint generatePlotPoint(Thesaurus thesaurus, Party party)
        {
            PlotContext context = new ContextBuilder(characterFilters).buildContext(party, thesaurus);

            return generatePlotPoint(context, thesaurus);
        }

        /**
         * Generates a new plot point, but with roles maintained from the lost plot point.
         * */
        public PlotPoint generatePlotPoint(PlotContext context, Thesaurus thesaurus)
        {
            

            PlotPoint plotPoint = new PlotPoint("", new List<Option>(), context);
            if(setupScript != null)
            {
                setupScript.run(context);
            }

            //delay generation of options and descriptor until after the setup script has run.
            plotPoint.text = new WordReplacer().replace(text, plotPoint.context);

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

        /**
         * Aggregate this plot point into the argument plot point.
         * */
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
            
            plotPoint.text += " " + new WordReplacer().replace(text, plotPoint.context);


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
