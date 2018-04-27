using StoryLib.Active;
using StoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions.Scripting.DefaultLanguage.Commands
{
    public class Command_Pick_Random_Segment : Command
    {
        public override ArgumentFinder argumentFinder { get; protected set; }

        public Dictionary<string, List<string>> areas;
        public Random random;

        public Command_Pick_Random_Segment()
        {
            argumentFinder = Find_String.instance;

            areas = new Dictionary<string, List<string>>();
            areas.Add("wastes", new List<string>());
            areas["wastes"].Add("random_event_wastes/doompack_start");
            areas["wastes"].Add("random_event_wastes/hunt_A_start");

            areas.Add("canyons", new List<string>());
            areas["canyons"].Add("random_event_canyons/cave_start");
            areas["canyons"].Add("random_event_canyons/flash_flood_start");

            random = new Random();
        }


        public override void execute(object[] args, PlotContext context)
        {
            Command_Contnue_Story_Args eventArgs = new Command_Contnue_Story_Args();
            string source = (string)args[0];
            eventArgs.nextPlotPoint = (PlotPointFactory)PlotPointRegistrar.GetPlotPointFactory(areas[source][random.Next(areas[source].Count)]);
            PlotPoint.onPlotArcChanged(this, eventArgs);
        }




    }

}
