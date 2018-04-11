using Newtonsoft.Json.Linq;
using StoryLib.Defenitions.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser
{
    public class FilterParser
    {
        public static Filter[] parse(dynamic input)
        {
            List<Filter> filters = new List<Filter>();
            JArray optionTokens = input.filters;
            foreach (JToken token in optionTokens)
            {
                string[] subComponents = token.Value<string>().Split(' ');
                switch(subComponents[0])
                {
                    case "tag":
                        filters.Add(new TagFilter(subComponents[1]));
                        break;
                }
            }

            return filters.ToArray();
        }
    }
}
