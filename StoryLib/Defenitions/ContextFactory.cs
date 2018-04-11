using StoryLib.Active;
using StoryLib.Defenitions.Filters;
using System;
using System.Collections.Generic;

namespace StoryLib.Defenitions
{
    public class ContextBuilder
    {
        public Dictionary<string, Filter[]> characterFilters { get; set; }
        public List<PlotContext> contexts;
        public ContextBuilder(Dictionary<string, Filter[]> characterFilters)
        {
            this.characterFilters = characterFilters;
            contexts = new List<PlotContext>();
        }

        public PlotContext buildContext(Party party)
        {
            WorkingContext context = new WorkingContext(party.members);
            List<WorkingContext> possibilities = context.generateIterations(this);
            Random rand = new Random();
            return possibilities[rand.Next(possibilities.Count)];

        }

        protected class WorkingContext : PlotContext
        {
            HashSet<PartyMember> unusedCharacters;
            public WorkingContext(HashSet<PartyMember> unusedCharacters)
            {
                this.unusedCharacters = unusedCharacters;
            }

            public List<WorkingContext> generateIterations(ContextBuilder builder)
            {
                List<String> unfilled = new List<string>();
                foreach (string key in builder.characterFilters.Keys)
                {
                    if(!partyMemberDefenitions.ContainsKey(key))
                    {
                        unfilled.Add(key);
                    }
                }
                List<WorkingContext> iterations = new List<WorkingContext>();
                if (unfilled.Count == 0)
                {
                    iterations.Add(this);
                    return iterations;
                }


                

                foreach (string role in unfilled)
                {
                    foreach (PartyMember member in unusedCharacters)
                    {
                        bool memberValidForPosition = true;
                        foreach(Filter condition in builder.characterFilters[role])
                        {
                            memberValidForPosition = memberValidForPosition && condition.valid(member);
                        }



                        if(memberValidForPosition)
                        {
                            HashSet<PartyMember> nextSet = new HashSet<PartyMember>();
                            foreach (PartyMember m in unusedCharacters)
                            {
                                if (m != member)
                                {
                                    nextSet.Add(m);
                                }
                            }
                            WorkingContext next = new WorkingContext(nextSet);
                            next.partyMemberDefenitions.Add(role, member);
                            iterations.AddRange(next.generateIterations(builder));
                        }
                        


                    }
                }

                return iterations;
            }

        }
    }
}
