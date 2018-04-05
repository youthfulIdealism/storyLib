using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class Script
    {
        public List<ActionExecutor> lines;

        public Script(List<ActionExecutor> lines)
        {
            this.lines = lines;
        }

    }
}
