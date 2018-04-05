using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class ScriptHelper
    {
        public static Dictionary<String, Action> actionMap;
        public static Dictionary<Action, String> reverseActionMap;
        private static Dictionary<Action, Type> validArgs;

        static ScriptHelper()
        {
            actionMap = new Dictionary<string, Action>();
            reverseActionMap = new Dictionary<Action, string>();






        }

        private static void addAction(Action action, String str)
        {
            actionMap.Add(str, action);
            reverseActionMap.Add(action, str);
        }

        private static void addValidArg(Action action, Type args)
        {
            validArgs.Add(action, args);
        }

        
    }
}
