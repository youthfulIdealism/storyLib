using Newtonsoft.Json.Linq;
using StoryLib.Defenitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser
{
    public class OptionParser
    {
        public static Option parse(JToken token)
        {
            string descriptor = token.Value<string>("descriptor");
            JArray scriptToken = token["outcome"].Value<JArray>();



            Option option = new Option(descriptor, null);
            return option;
        }
    }
}
