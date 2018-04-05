using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoryLib.Defenitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryLib.Parser
{
    public class PlotParser
    {
        public static PlotPoint parse(string input)
        {
            dynamic stuff = JsonConvert.DeserializeObject(input);
            string id = stuff.id;
            string descriptor = stuff.descriptor;
            List<Option> options = new List<Option>();
            JArray optionTokens = stuff.options;
            foreach(JToken token in optionTokens)
            {
                options.Add(OptionParser.parse(token));
            }


            PlotPoint plotPoint = new PlotPoint(id, descriptor, options);
            return plotPoint;
        }
    }
}
