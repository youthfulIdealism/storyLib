using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Active
{
    public class PlotContext
    {
        public Dictionary<String, PartyMember> partyMemberDefenitions { get; set; }

        public PlotContext()
        {
            partyMemberDefenitions = new Dictionary<string, PartyMember>();
        }
    }
}
