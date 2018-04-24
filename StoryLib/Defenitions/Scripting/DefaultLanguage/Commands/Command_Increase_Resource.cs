using StoryLib.Active;
using StoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions.Scripting.DefaultLanguage.Commands
{
    class Command_Increase_Resource : Command
    {
        public override ArgumentFinder argumentFinder { get; protected set; }

        public Command_Increase_Resource()
        {
            argumentFinder = Find_Resource_Modification.instance;
        }


        public override void execute(object[] args, PlotContext context)
        {
            string resource = (string)args[0];
            int amount = (int)args[1];
            if(context.party.resources.ContainsKey(resource))
            {
                context.party.resources[resource] = context.party.resources[resource] + amount;
            }else
            {
                context.party.resources.Add(resource, amount);
            }
        }
    }
}
