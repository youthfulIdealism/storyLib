using System;
using System.Collections.Generic;
using System.Text;
using EmergentStoryLib.Instance;

namespace EmergentStoryLib.Defenitions.Filters.PartyMemberFilters
{
    public class IsPersonNotDeclaredFilter : Filter<PlotContext>
    {
        public IsPersonNotDeclaredFilter(string[] args) : base(args)
        {
            
        }

        public override bool valid(PlotContext context)
        {
            return !context.partyMemberDefenitions.ContainsKey(args[0]);
        }
    }
}
