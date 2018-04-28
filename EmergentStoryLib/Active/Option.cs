using EmergentStoryLib.Defenitions;
using EmergentStoryLib.Defenitions.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Active
{
    public class Option
    {
        public String descriptor { get; set; }
        public Script outcome { get; set; }

        public Option(string descriptor, Script outcome)
        {
            this.descriptor = descriptor;
            this.outcome = outcome;
        }

    }
}
