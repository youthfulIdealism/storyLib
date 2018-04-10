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
        public static PlotPointFactory parse(string input)
        {
            dynamic stuff = JsonConvert.DeserializeObject(input);
            string id = stuff.id;
            string descriptor = stuff.descriptor;
            List<OptionFactory> options = new List<OptionFactory>();
            JArray optionTokens = stuff.options;
            foreach(JToken token in optionTokens)
            {
                options.Add(OptionParser.parse(token));
            }


            PlotPointFactory plotPoint = new PlotPointFactory(id, descriptor, options);
            return plotPoint;
        }
    }
}
