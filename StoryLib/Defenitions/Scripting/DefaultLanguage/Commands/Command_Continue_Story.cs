﻿using StoryLib.Active;
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
            Command_Contnue_Story_Args eventArgs = new Command_Contnue_Story_Args();
            eventArgs.nextPlotPoint = (PlotPointFactory)PlotPointRegistrar.GetPlotPointFactory((string)args[0]);
            PlotPoint.onStoryContinued(this, eventArgs);
        }




    }

}