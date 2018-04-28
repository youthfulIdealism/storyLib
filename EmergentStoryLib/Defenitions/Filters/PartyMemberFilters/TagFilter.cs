using System;
using System.Collections.Generic;
using System.Text;
using EmergentStoryLib.Active;

namespace EmergentStoryLib.Defenitions.Filters
{
    public class TagFilter : Filter<PartyMember>
    {
        public TagFilter(string[] args) : base(args)
        {
            
        }

        public override bool valid(PartyMember member)
        {
            return member.tags.Contains(args[0]);
        }
    }
}
