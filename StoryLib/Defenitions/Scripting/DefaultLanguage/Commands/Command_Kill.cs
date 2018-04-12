using System;
using System.Collections.Generic;
using System.Text;
using StoryLib.Active;
using StoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders;

namespace StoryLib.Defenitions.Scripting.DefaultLanguage.Commands
{
    public class Command_Kill : Command
    {
        public override ArgumentFinder argumentFinder { get; protected set; }

        public Command_Kill()
        {
            argumentFinder = Find_Person.instance;
        }


        public override void execute(object[] args, PlotContext context, Party party)
        {
            PartyMember member = (PartyMember)args[0];
            party.members.Remove(member);
        }
    }


}
