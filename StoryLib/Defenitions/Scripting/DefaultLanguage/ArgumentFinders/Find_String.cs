using System;
using System.Collections.Generic;
using System.Text;
using StoryLib.Active;

namespace StoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders
{
    public class Find_String : ArgumentFinder
    {
        public static Find_String instance;
        static Find_String()
        {
            instance = new Find_String();
        }

        private Find_String()
        {
            types = new Type[] { typeof(string) };
        }

        public override object[] findArguments(string[] args, PlotContext context)
        {
            return new object[] { wordReplacer.replace(args[0], context) };
        }
    }
}
