using StoryLib.Active;
using StoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions.Scripting.DefaultLanguage.Commands
{
    public class Command_Push_Story : Command
    {
        public override ArgumentFinder argumentFinder { get; protected set; }

        public Command_Push_Story()
        {
            argumentFinder = Find_String.instance;
        }


        public override void execute(object[] args, PlotContext context, Party party)
        {
            
        }
    }
}
