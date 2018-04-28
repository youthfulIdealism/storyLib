using System;
using System.Collections.Generic;
using System.Text;
using EmergentStoryLib.Instance;
using EmergentStoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders;

namespace EmergentStoryLib.Defenitions.Scripting.DefaultLanguage.Commands
{
    public class Command_Kill : Command
    {
        public override ArgumentFinder argumentFinder { get; protected set; }

        public Command_Kill()
        {
            argumentFinder = Find_Person.instance;
        }

        
        public override void execute(object[] args, PlotContext context)
        {
            PartyMember member = (PartyMember)args[0];
            member.tags.Add("DEAD");
        }
    }


}
