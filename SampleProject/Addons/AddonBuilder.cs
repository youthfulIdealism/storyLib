using StoryLib.Defenitions.Scripting;
using StoryLib.Defenitions.Scripting.DefaultLanguage.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Addons
{
    public class AddonBuilder
    {
        public AddonBuilder()
        {
            
        }

        public void buildAddons()
        {
            ScriptRegistrar.addCommand(new Command_Pick_Random_Segment(), "go_to_random_story");
        }
    }
}
