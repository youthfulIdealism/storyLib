using EmergentStoryLib.Defenitions;
using EmergentStoryLib.Defenitions.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Active
{
    public class PlotPoint
    {
        public static event EventHandler<Command_Contnue_Story_Args> continuePlotArcEvent;
        public static event EventHandler<Command_Contnue_Story_Args> newPlotArcEvent;

        public string descriptor { get; set; }
        public Dictionary<string, string[]> characterFilters { get; set; }
        public List<Option> options { get; set; }
        public PlotContext context { get; set; }

        public PlotPoint(string descriptor, List<Option> options, PlotContext context)
        {
            this.descriptor = descriptor;
            this.options = options;
            this.context = context;
        }

        public void MakeChoice(int choice)
        {
            options[choice].outcome.run(context);
        }

        public static void onPlotArcContinued(Command sender, Command_Contnue_Story_Args e)
        {
            EventHandler<Command_Contnue_Story_Args> handler = PlotPoint.continuePlotArcEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        public static void onPlotArcChanged(Command sender, Command_Contnue_Story_Args e)
        {
            EventHandler<Command_Contnue_Story_Args> handler = PlotPoint.newPlotArcEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }

    public class Command_Contnue_Story_Args : EventArgs
    {
        public PlotPointFactory nextPlotPoint;
    }
}
