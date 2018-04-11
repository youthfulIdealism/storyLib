using Newtonsoft.Json.Linq;
using StoryLib.Defenitions;
using StoryLib.Defenitions.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser
{
    public class OptionParser
    {
        public static OptionFactory parse(JToken token)
        {
            string descriptor = token.Value<string>("descriptor");
            JArray scriptToken = token["outcome"].Value<JArray>();

            OptionFactory option = new OptionFactory(descriptor, null);
            return option;
        }
    }
}
