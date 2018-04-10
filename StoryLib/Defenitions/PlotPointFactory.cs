using StoryLib.Active;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class PlotPointFactory
    {
        public string id { get; private set; }
        public string descriptor { get; set; }
        public Dictionary<string, string[]> characterFilters;
        public List<OptionFactory> options { get; set; }



        public PlotPointFactory(string id, string descriptor, List<OptionFactory> options)
        {
            this.id = id;
            this.descriptor = descriptor;
            this.options = options;
        }

        public PlotPoint generatePlotPoint(Thesaurus thesaurus)
        {
            List<Option> generatedOption = new List<Option>();
            foreach(OptionFactory factory in options)
            {
                generatedOption.Add(factory.generateOption(thesaurus));
            }


            return new PlotPoint(id, new WordReplacer().replace(descriptor, thesaurus), generatedOption);
        }
    }
}
