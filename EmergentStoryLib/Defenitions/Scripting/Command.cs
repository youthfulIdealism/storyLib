using EmergentStoryLib.Active;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions.Scripting
{
    public abstract class Command
    {
        public abstract ArgumentFinder argumentFinder { get; protected set; }

        public abstract void execute(object[] args, PlotContext context);
    }
}
