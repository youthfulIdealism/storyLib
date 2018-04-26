using StoryLib.Active;
using StoryLib.Defenitions.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class OptionFactory
    {
        public String descriptor { get; set; }
        public Script outcome { get; set; }

        public OptionFactory(string descriptor, Script outcome)
        {
            this.descriptor = descriptor;
            this.outcome = outcome;
        }

        public Option generateOption(PlotContext context)
        {
            return new Option(new WordReplacer().replace(descriptor, context), outcome);
        }
    }
}
