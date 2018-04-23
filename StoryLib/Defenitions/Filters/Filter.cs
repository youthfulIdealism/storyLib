using StoryLib.Active;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions.Filters
{
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
