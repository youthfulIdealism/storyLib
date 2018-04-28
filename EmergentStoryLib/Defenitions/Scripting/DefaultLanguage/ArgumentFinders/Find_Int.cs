using System;
using System.Collections.Generic;
using System.Text;
using EmergentStoryLib.Instance;

namespace EmergentStoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders
{
    public class Find_Int : ArgumentFinder
    {
        public static Find_Int instance;
        static Find_Int()
        {
            instance = new Find_Int();
        }

        private Find_Int()
        {
            types = new Type[] { typeof(int) };
        }

        public override object[] findArguments(string[] args, PlotContext context)
        {
            return new object[] {int.Parse(wordReplacer.replace(args[0], context)) };
        }
    }
}
