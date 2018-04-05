using Newtonsoft.Json.Linq;
using StoryLib.Defenitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser
{
    public class ScriptParser
    {
        public static Script parse(JArray tokens)
        {
            List<ActionExecutor> lines = new List<ActionExecutor>();



            return new Script(lines);
        }
    }
}
