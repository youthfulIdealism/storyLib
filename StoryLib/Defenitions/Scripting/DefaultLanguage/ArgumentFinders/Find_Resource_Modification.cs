using System;
using System.Collections.Generic;
using System.Text;
using StoryLib.Active;

namespace StoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders
{
    public class Find_Resource_Modification : ArgumentFinder
    {
        public static Find_Resource_Modification instance;
        static Find_Resource_Modification()
        {
            instance = new Find_Resource_Modification();
        }

        private Find_Resource_Modification()
        {
            types = new Type[] { typeof(string), typeof(int) };
        }

        public override object[] findArguments(string[] args, PlotContext context)
        {
            return new object[] { wordReplacer.replace(args[0], context), int.Parse(wordReplacer.replace(args[1], context)) };
        }
    }
}
