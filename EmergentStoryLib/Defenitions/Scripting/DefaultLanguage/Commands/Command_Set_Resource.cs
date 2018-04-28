using EmergentStoryLib.Instance;
using EmergentStoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions.Scripting.DefaultLanguage.Commands
{
    class Command_Set_Resource : Command
    {
        public override ArgumentFinder argumentFinder { get; protected set; }

        public Command_Set_Resource()
        {
            argumentFinder = Find_Resource_Modification.instance;
        }


        public override void execute(object[] args, PlotContext context)
        {
            string resource = (string)args[0];
            int amount = (int)args[1];
            context.party.resources[resource] = amount;
        }
    }
}
