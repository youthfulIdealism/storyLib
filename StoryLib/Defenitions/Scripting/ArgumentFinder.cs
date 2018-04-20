using StoryLib.Active;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions.Scripting
{
    public abstract class ArgumentFinder
    {
        public abstract object[] findArguments(string[] args, PlotContext context);
        public Type[] types { get; protected set; }

        public bool typeCheck(object[] args)
        {
            if(types.Length != args.Length) { return false; }
            for(int i = 0; i < args.Length; i++)
            {
                if(types[i] != args[i].GetType())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
