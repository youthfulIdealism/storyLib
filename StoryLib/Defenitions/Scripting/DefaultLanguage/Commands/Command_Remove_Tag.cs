using StoryLib.Active;
using StoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions.Scripting.DefaultLanguage.Commands
{
    class Command_Remove_Tag : Command
    {
        public override ArgumentFinder argumentFinder { get; protected set; }

        public Command_Remove_Tag()
        {
            argumentFinder = Find_Person_And_String.instance;
        }


        public override void execute(object[] args, PlotContext context)
        {
            PartyMember member = (PartyMember)args[0];
            string tag = (string)args[1];
            member.tags.Remove(tag);
        }
    }
}
