using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Active
{
    public class PlotContext
    {
        public Dictionary<String, PartyMember> partyMemberDefenitions { get; set; }
        public Party party { get; set; }

        public PlotContext(Party party)
        {
            this.party = party;
            partyMemberDefenitions = new Dictionary<string, PartyMember>();
        }
    }
}
