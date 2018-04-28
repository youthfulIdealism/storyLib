using EmergentStoryLib.Defenitions;
using EmergentStoryLib.Defenitions.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Instance
{
    /**
     * Represents a segment of a story arc.
     * */
    public class PlotPoint
    {
        /**
         * Event for continuing a given plot arc with roles maintained
         * between current plot point and last plot point.
         * */
        public static event EventHandler<Command_Contnue_Story_Args> continuePlotArcEvent;

        /**
         * Event for continuing a given story, but with rolls filled from
         * scratch to maintain variety.
         * */
        public static event EventHandler<Command_Contnue_Story_Args> newPlotArcEvent;

        /**
         * Story text.
         * */
        public string text { get; set; }

        /**
         * Options available to player
         * */
        public List<Option> options { get; set; }

        public PlotContext context { get; set; }

        public PlotPoint(string descriptor, List<Option> options, PlotContext context)
        {
            this.text = descriptor;
            this.options = options;
            this.context = context;
        }

        /**
         * Runs script attached to choice at given index
         * */
        public void MakeChoice(int index)
        {
            options[index].outcome.run(context);
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
