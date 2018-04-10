using StoryLib.Active;
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

        public Option generateOption(Thesaurus thesaurus)
        {
            return new Option(new WordReplacer().replace(descriptor, thesaurus), null);
        }
    }
}
