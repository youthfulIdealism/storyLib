using StoryLib.Active;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions.Scripting
{
    public class CommandExecutor
    {
        public Command command;
        public string[] args;

        public CommandExecutor(Command command, string[] args)
        {
            this.command = command;
            this.args = args;
        }

        public void execute(PlotContext context)
        {
            object[] generatedArgs = command.argumentFinder.findArguments(args, context);
            if(!command.argumentFinder.typeCheck(generatedArgs))
            {
                throw new Exception("Wrong argument type.");
            }
            command.execute(generatedArgs, context);
        }

    }
}
