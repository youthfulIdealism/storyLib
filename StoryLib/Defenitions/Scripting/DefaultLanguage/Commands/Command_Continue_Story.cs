using StoryLib.Active;
using StoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions.Scripting.DefaultLanguage.Commands
{
    public class Command_Continue_Story : Command
    {
        public override ArgumentFinder argumentFinder { get; protected set; }

        public Command_Continue_Story()
        {
            argumentFinder = Find_String.instance;
        }


        public override void execute(object[] args, PlotContext context, Party party)
        {
            Console.WriteLine("called");
            Command_Contnue_Story_Args eventARgs = new Command_Contnue_Story_Args();
            onStoryContinued(eventARgs);
        }

        protected virtual void onStoryContinued(Command_Contnue_Story_Args e)
        {
            EventHandler<Command_Contnue_Story_Args> handler = continueStoryEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public static event EventHandler<Command_Contnue_Story_Args> continueStoryEvent;
    }
    public class Command_Contnue_Story_Args : EventArgs
    {
        public PlotPoint nextPlotPoint;
    }
}
