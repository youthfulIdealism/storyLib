using StoryLib.Defenitions;
using StoryLib.Defenitions.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Active
{
    public class PlotPoint
    {
        public static event EventHandler<Command_Contnue_Story_Args> continueStoryEvent;

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

        public static void onStoryContinued(Command sender, Command_Contnue_Story_Args e)
        {
            EventHandler<Command_Contnue_Story_Args> handler = PlotPoint.continueStoryEvent;
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
