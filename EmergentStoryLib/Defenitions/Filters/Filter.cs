using EmergentStoryLib.Instance;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions.Filters
{
    /**
     * Checks if object of type T is valid. for scripting.
     * See PlotContextFilters and PartyMemberFilters for
     * examples.
     * */
    public abstract class Filter<T>
    {
        public string[] args { get; protected set; }

        public Filter(string[] args)
        {
            this.args = args;
        }

        public abstract bool valid(T filterBy);
    }
}
