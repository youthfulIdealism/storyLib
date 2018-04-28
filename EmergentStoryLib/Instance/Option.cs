using EmergentStoryLib.Defenitions;
using EmergentStoryLib.Defenitions.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Instance
{
    /**
     * Represents an option available to the player,
     * with the consequences of selecting the option
     * executable via a script.
     * */
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
