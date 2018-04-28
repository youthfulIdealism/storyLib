using EmergentStoryLib.Active;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions.Scripting
{
    /**
     * Takes string arguments passed to command and then converts
     * them to the correct types for the command to function.
     * */
    public abstract class ArgumentFinder
    {
        static ArgumentFinder()
        {
            wordReplacer = new WordReplacer();
        }

        /**
         * Word replacer (to replace any variables encountered).
         * */
        public static WordReplacer wordReplacer { get; set; }

        public abstract object[] findArguments(string[] args, PlotContext context);

        /**
         * Types for typechecking
         * */
        public Type[] types { get; protected set; }

        /**
         * Checks to ensure that returned objects are the correct type
         * */
        public virtual bool typeCheck(object[] args)
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
