using EmergentStoryLib.Instance;
using EmergentStoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions.Scripting.DefaultLanguage.Commands
{
    public class Command_Change_Plot_Arc : Command
    {
        public override ArgumentFinder argumentFinder { get; protected set; }

        public Command_Change_Plot_Arc()
        {
            argumentFinder = Find_String.instance;
        }


        public override void execute(object[] args, PlotContext context)
        {
            Command_Contnue_Story_Args eventArgs = new Command_Contnue_Story_Args();
            eventArgs.nextPlotPoint = (PlotPointFactory)PlotPointRegistrar.GetPlotPointFactory((string)args[0]);
            PlotPoint.onPlotArcChanged(this, eventArgs);
        }




    }

}
