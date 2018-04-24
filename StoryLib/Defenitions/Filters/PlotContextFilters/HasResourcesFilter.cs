using System;
using System.Collections.Generic;
using System.Text;
using StoryLib.Active;

namespace StoryLib.Defenitions.Filters.PartyMemberFilters
{
    public class HasResourcesFilter : Filter<PlotContext>
    {
        public HasResourcesFilter(string[] args) : base(args)
        {
            
        }

        public override bool valid(PlotContext context)
        {
            if (context.party.resources.ContainsKey(args[0]))
            {
                return context.party.resources[args[0]] >= int.Parse(args[1]);
            }

            return false;
        }
    }
}
