using StoryLib.Defenitions.Scripting.DefaultLanguage.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions.Scripting
{
    public static class ScriptRegistrar
    {
        private static Dictionary<String, Command> commandMap;
        private static Dictionary<Command, String> reverseCommandMap;

        static ScriptRegistrar()
        {
            commandMap = new Dictionary<string, Command>();
            reverseCommandMap = new Dictionary<Command, string>();

        }

        public static void buildDefaultLanguage()
        {
            addCommand(new Command_Kill(), "kill");
            addCommand(new Command_Push_Story(), "push");
            addCommand(new Command_Relationship_Add(), "relationship_add");
        }

        private static void addCommand(Command command, String str)
        {
            commandMap.Add(str, command);
            reverseCommandMap.Add(command, str);
        }

        public static Command getCommand(string key)
        {
            return commandMap[key];
        }
    }
}
