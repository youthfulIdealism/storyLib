using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class Party : HashSet<PartyMember>
    {
        public Dictionary<String, int> resources { get; set; }

        public Party()
        {
            resources = new Dictionary<string, int>();
        }
    }
}
