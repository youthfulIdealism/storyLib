using System;
using System.Collections.Generic;
using System.Text;
using StoryLib.Active;

namespace StoryLib.Defenitions.Filters.PartyMemberFilters
{
    public class IsPersonDeclaredFilter : Filter<PlotContext>
    {
        public string handle;
        public IsPersonDeclaredFilter(string handle)
        {
            this.handle = handle;
        }

        public bool valid(PlotContext context)
        {
            return context.partyMemberDefenitions.ContainsKey(handle);
        }
    }
}
