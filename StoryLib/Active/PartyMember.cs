using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Active
{
    public class PartyMember
    {
        public HashSet<string> tags { get; set; }
        public string name { get; set; }
        public string sex { get; set; }

        public PartyMember(string name, string sex)
        {
            this.name = name;
            this.sex = sex;
            tags = new HashSet<string>();
        }




    }
}
