using System;
using System.Collections.Generic;
using System.Text;
using StoryLib.Active;
using StoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders;

namespace StoryLib.Defenitions.Scripting.DefaultLanguage.Commands
{
    public class Command_Relationship_Add : Command
    {
        public override ArgumentFinder argumentFinder { get; protected set; }

        public Command_Relationship_Add()
        {
            argumentFinder = Find_Int.instance;
        }


        public override void execute(object[] args, PlotContext context, Party party)
        {
            
        }
    }
}
