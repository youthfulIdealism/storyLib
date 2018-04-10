using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Active
{
    public class PartyMember
    {
        public HashSet<string> tags { get; set; }
        public string name { get; set; }

        public PartyMember(string name)
        {
            this.name = name;
            tags = new HashSet<string>();
        }




    }
}
