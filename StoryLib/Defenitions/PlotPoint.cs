using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class PlotPoint
    {
        public string id { get; private set; }
        public string descriptor { get; set; }
        public List<Option> options { get; set; }


        public PlotPoint(string id, string descriptor, List<Option> options)
        {
            this.id = id;
            this.descriptor = descriptor;
            this.options = options;
        }

        public string generatePlotText(Thesaurus thesaurus)
        {
            return new WordReplacer().replace(descriptor, thesaurus);
        }
    }
}
