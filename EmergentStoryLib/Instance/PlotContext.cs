using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Instance
{
    /**
     * Represents the context in which a plot takes place, including
     * but not limited to character roles.
     * */
    public class PlotContext
    {
        /**
         * Role->Character map
         * */
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
