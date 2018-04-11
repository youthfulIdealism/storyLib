using System;
using System.Collections.Generic;
using System.Text;
using StoryLib.Active;

namespace StoryLib.Defenitions.Filters
{
    public class TagFilter : Filter
    {
        public string tag;
        public TagFilter(string tag)
        {
            this.tag = tag;
        }

        public bool valid(PartyMember member)
        {
            return member.tags.Contains(tag);
        }
    }
}
