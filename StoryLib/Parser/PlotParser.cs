using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoryLib.Defenitions;
using StoryLib.Defenitions.Filters;
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

            Dictionary<string, Filter[]> filters = new Dictionary<string, Filter[]>();
            if(stuff.people != null)
            {
                JArray partyFilters = stuff.people;
                foreach (JToken token in partyFilters)
                {
                    dynamic protoPerson = JsonConvert.DeserializeObject(token.ToString());
                    filters.Add(token.Value<string>("handle"), FilterParser.parse(protoPerson));
                } 
            }
            


            PlotPointFactory plotPoint = new PlotPointFactory(descriptor, options, filters);
            return plotPoint;
        }
    }
}
