using StoryLib.Active;
using StoryLib.Defenitions.Filters;
using System;
using System.Collections.Generic;

namespace StoryLib.Defenitions
{
    public class ContextBuilder
    {
        public Dictionary<string, Filter<PartyMember>[]> characterFilters { get; set; }
        public List<PlotContext> contexts;
        public ContextBuilder(Dictionary<string, Filter<PartyMember>[]> characterFilters)
        {
            this.characterFilters = characterFilters;
            contexts = new List<PlotContext>();
        }

        public PlotContext buildContext(Party party)
        {
            WorkingContext context = new WorkingContext(party.members, party);
            List<WorkingContext> possibilities = context.generateIterations(this);
            Random rand = new Random();
            return possibilities[rand.Next(possibilities.Count)].toPlainPlotContext();

        }

        protected class WorkingContext : PlotContext
        {
            HashSet<PartyMember> unusedCharacters;
            public WorkingContext(HashSet<PartyMember> unusedCharacters, Party party) : base(party)
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
                        foreach(Filter<PartyMember> condition in builder.characterFilters[role])
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
                            WorkingContext next = new WorkingContext(nextSet, party);
                            next.partyMemberDefenitions.Add(role, member);
                            iterations.AddRange(next.generateIterations(builder));
                        }
                        


                    }
                }

                return iterations;
            }

            public PlotContext toPlainPlotContext()
            {
                PlotContext context = new PlotContext(party);
                context.partyMemberDefenitions = partyMemberDefenitions;
                return context;
            }

        }
    }
}
