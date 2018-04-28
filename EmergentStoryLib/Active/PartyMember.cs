using EmergentStoryLib.Defenitions.PronounPackages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Active
{
    public class PartyMember
    {
        public HashSet<string> tags { get; set; }
        public string name { get; set; }
        public PronounPackage pronounPackage { get; set; }

        public PartyMember(string name, PronounPackage pronounPackage)
        {
            this.name = name;
            this.pronounPackage = pronounPackage;
            tags = new HashSet<string>();
        }




    }
}
