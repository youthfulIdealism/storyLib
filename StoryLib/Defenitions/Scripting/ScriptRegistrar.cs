using StoryLib.Active;
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
            addCommand(new Command_Continue_Plot_Arc(), "continue_story");
            addCommand(new Command_Change_Plot_Arc(), "change_story");
            addCommand(new Command_Increase_Resource(), "resource_add");
            addCommand(new Command_Decrease_Resource(), "resource_subtract");
            addCommand(new Command_Set_Resource(), "resource_set");
            addCommand(new Command_Add_Tag(), "add_tag");
            addCommand(new Command_Remove_Tag(), "remove_tag");
        }

        public static void addCommand(Command command, String str)
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
