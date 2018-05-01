using System;
using System.Collections.Generic;
using System.Text;
using EmergentStoryLib.Instance;

namespace EmergentStoryLib.Defenitions.Filters.PartyMemberFilters
{
    public class DoesNotHaveResourcesFilter : Filter<PlotContext>
    {
        public DoesNotHaveResourcesFilter(string[] args) : base(args)
        {
            
        }

        public override bool valid(PlotContext context)
        {
            if (context.party.resources.ContainsKey(args[0]))
            {
                return context.party.resources[args[0]] <= int.Parse(args[1]);
            }

            return false;
        }
    }
}
