using StoryLib.Active;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions.Filters
{
    public interface Filter
    {
        bool valid(PartyMember member);
    }
}
