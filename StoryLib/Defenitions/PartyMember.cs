using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class PartyMember
    {
        public HashSet<string> tags { get; set; }
        public string name { get; set; }

        public PartyMember()
        {
            tags = new HashSet<string>();
        }




    }
}
