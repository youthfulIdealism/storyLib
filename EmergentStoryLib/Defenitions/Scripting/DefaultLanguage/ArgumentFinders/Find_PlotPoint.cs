using System;
using System.Collections.Generic;
using System.Text;
using EmergentStoryLib.Active;

namespace EmergentStoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders
{
    public class Find_PlotPoint : ArgumentFinder
    {
        public static Find_PlotPoint instance;
        static Find_PlotPoint()
        {
            instance = new Find_PlotPoint();
        }

        private Find_PlotPoint()
        {
            types = new Type[] { typeof(PlotPointFactory) };
        }

        public override object[] findArguments(string[] args, PlotContext context)
        {
            return new object[] { PlotPointRegistrar.GetPlotPointFactory(wordReplacer.replace(args[0], context)) };
        }
    }
}
