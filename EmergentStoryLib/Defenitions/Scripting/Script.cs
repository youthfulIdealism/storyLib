using EmergentStoryLib.Instance;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions.Scripting
{
    public class Script
    {
        public List<CommandExecutor> lines;

        public Script(List<CommandExecutor> lines)
        {
            this.lines = lines;
        }

        public void run(PlotContext context)
        {
            foreach(CommandExecutor executor in lines)
            {
                executor.execute(context);
            }
        }
    }
}
