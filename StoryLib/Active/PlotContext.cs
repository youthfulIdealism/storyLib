using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Active
{
    public class PlotContext
    {
        public Dictionary<String, PartyMember> partyMemberDefenitions { get; set; }
        public Party party { get; set; }
        public Thesaurus thesaurus { get; set; }

        public PlotContext(Party party, Thesaurus thesaurus)
        {
            this.party = party;
            this.thesaurus = thesaurus;
            partyMemberDefenitions = new Dictionary<string, PartyMember>();
        }
    }
}
