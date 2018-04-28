using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Instance
{
    /**
     * Represents a collection of characters and the resources available to them.
     * */
    public class Party
    {
        public Dictionary<String, int> resources { get; set; }
        public HashSet<PartyMember> members { get; set; }

        public Party()
        {
            resources = new Dictionary<string, int>();
            members = new HashSet<PartyMember>();
        }
    }
}
