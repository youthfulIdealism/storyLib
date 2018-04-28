using EmergentStoryLib.Defenitions.PronounPackages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Instance
{
    /**
     * Represents a person in the user's party.
     * */
    public class PartyMember
    {
        /**
         * Person's traits 
         * */
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
