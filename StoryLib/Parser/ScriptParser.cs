using Newtonsoft.Json.Linq;
using StoryLib.Defenitions;
using StoryLib.Defenitions.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser
{
    public class ScriptParser
    {
        public static Script parse(JArray tokens)
        {
            List<CommandExecutor> lines = new List<CommandExecutor>();
            foreach(JToken token in tokens)
            {
                string preprocess = token.Value<string>();
                string[] pieces = preprocess.Split(' ');
                Command command = ScriptRegistrar.getCommand(pieces[0]);
                string[] args = new string[pieces.Length - 1];
                for(int i = 1; i < pieces.Length; i++)
                {
                    args[i - 1] = pieces[i];
                }
                lines.Add(new CommandExecutor(command, args));
            }

            return new Script(lines);
        }
    }
}
