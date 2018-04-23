﻿using StoryLib.Active;
using StoryLib.Defenitions.Filters;
using System;
using System.Collections.Generic;

namespace StoryLib.Defenitions
{
    public class ContextBuilder//
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
            //build a set of possible characters for each handle
            List<Tuple<string, HashSet<PartyMember>>> characterPossibilities = new List<Tuple<string, HashSet<PartyMember>>>();

            foreach(string key in characterFilters.Keys)
            {
                HashSet<PartyMember> possibilities = new HashSet<PartyMember>();
                //iterate through party members to see if any of them can fit the handle.
                foreach(PartyMember possiblePerson in party.members)
                {
                    bool canFillSlot = true;
                    foreach(Filter<PartyMember> filter in characterFilters[key])
                    {
                        if(!filter.valid(possiblePerson))
                        {
                            canFillSlot = false;
                            break;
                        }
                    }

                    if(canFillSlot)
                    {
                        possibilities.Add(possiblePerson);
                    }
                }

                characterPossibilities.Add(new Tuple<string, HashSet<PartyMember>>(key, possibilities));
            }

            //sort the possibilities so that we process the smaller lists first.
            //This should help to ensure that any blocking constraints are found
            //and eliminated early.
            characterPossibilities.Sort(

                delegate (Tuple<string, HashSet<PartyMember>> x, Tuple<string, HashSet<PartyMember>> y)
                {
                    return x.Item2.Count.CompareTo(y.Item2.Count);
                }

                );

            List<WorkingContext> workingContexts = new List<WorkingContext>();
            List<WorkingContext> nextWorkingContexts = new List<WorkingContext>();

            workingContexts.Add(new WorkingContext(party, new HashSet<PartyMember>()));

            int workingIndex = 0;

            while(workingIndex < characterPossibilities.Count)
            {
                Tuple<string, HashSet<PartyMember>> currentSlot = characterPossibilities[workingIndex];

                foreach (WorkingContext context in workingContexts)
                {
                    foreach(PartyMember member in currentSlot.Item2)
                    {
                        if(context.canGenerateNextIterationFor(member))
                        {
                            nextWorkingContexts.Add(context.generateIteration(currentSlot.Item1, member));
                        }
                    }
                }




                workingContexts = nextWorkingContexts;
                if(workingIndex + 1 < characterPossibilities.Count)
                {
                    nextWorkingContexts = new List<WorkingContext>(workingContexts.Count * characterPossibilities[workingIndex + 1].Item2.Count);
                }
                
                workingIndex++;
            }


            Random rand = new Random();
            return nextWorkingContexts[rand.Next(nextWorkingContexts.Count)].toPlainPlotContext();

        }

        public PlotContext addToContext(PlotContext previousContext)
        {

            //build a set of possible characters for each handle
            List<Tuple<string, HashSet<PartyMember>>> characterPossibilities = new List<Tuple<string, HashSet<PartyMember>>>();

            HashSet<PartyMember> unusedChars = new HashSet<PartyMember>();
            foreach(PartyMember member in previousContext.party.members)
            {
                if(!previousContext.partyMemberDefenitions.ContainsValue(member))
                {
                    unusedChars.Add(member);
                }
            }

            foreach (string key in characterFilters.Keys)
            {
                HashSet<PartyMember> possibilities = new HashSet<PartyMember>();
                //iterate through party members to see if any of them can fit the handle.
                foreach (PartyMember possiblePerson in unusedChars)
                {
                    bool canFillSlot = true;
                    foreach (Filter<PartyMember> filter in characterFilters[key])
                    {
                        if (!filter.valid(possiblePerson))
                        {
                            canFillSlot = false;
                            break;
                        }
                    }

                    if (canFillSlot)
                    {
                        possibilities.Add(possiblePerson);
                    }
                }

                characterPossibilities.Add(new Tuple<string, HashSet<PartyMember>>(key, possibilities));
            }

            //sort the possibilities so that we process the smaller lists first.
            //This should help to ensure that any blocking constraints are found
            //and eliminated early.
            characterPossibilities.Sort(

                delegate (Tuple<string, HashSet<PartyMember>> x, Tuple<string, HashSet<PartyMember>> y)
                {
                    return x.Item2.Count.CompareTo(y.Item2.Count);
                }

                );

            List<WorkingContext> workingContexts = new List<WorkingContext>();
            List<WorkingContext> nextWorkingContexts = new List<WorkingContext>();

            //clone previous context into starting seed
            WorkingContext startingContext = new WorkingContext(previousContext.party, new HashSet<PartyMember>());
            foreach (string key in previousContext.partyMemberDefenitions.Keys)
            {
                startingContext.partyMemberDefenitions.Add(key, previousContext.partyMemberDefenitions[key]);
            }

            workingContexts.Add(startingContext);

            int workingIndex = 0;

            while (workingIndex < characterPossibilities.Count)
            {
                Tuple<string, HashSet<PartyMember>> currentSlot = characterPossibilities[workingIndex];

                foreach (WorkingContext context in workingContexts)
                {
                    foreach (PartyMember member in currentSlot.Item2)
                    {
                        if (context.canGenerateNextIterationFor(member))
                        {
                            nextWorkingContexts.Add(context.generateIteration(currentSlot.Item1, member));
                        }
                    }
                }




                workingContexts = nextWorkingContexts;
                if (workingIndex + 1 < characterPossibilities.Count)
                {
                    nextWorkingContexts = new List<WorkingContext>(workingContexts.Count * characterPossibilities[workingIndex + 1].Item2.Count);
                }

                workingIndex++;
            }


            Random rand = new Random();
            return nextWorkingContexts[rand.Next(nextWorkingContexts.Count)].toPlainPlotContext();

        }

        protected class WorkingContext : PlotContext
        {
            HashSet<PartyMember> usedChars;
            public WorkingContext(Party party, HashSet<PartyMember> usedChars) : base(party)
            {
                this.usedChars = usedChars;
            }


            public WorkingContext generateIteration(string slot, PartyMember member)
            {
                HashSet<PartyMember> nextUsedCharacters = new HashSet<PartyMember>();
                foreach(PartyMember clone in usedChars)
                {
                    nextUsedCharacters.Add(clone);
                }
                nextUsedCharacters.Add(member);
                WorkingContext next = new WorkingContext(party, nextUsedCharacters);
                foreach(string key in partyMemberDefenitions.Keys)
                {
                    next.partyMemberDefenitions.Add(key, partyMemberDefenitions[key]);
                }
                next.partyMemberDefenitions[slot] = member;
                return next;
            }

            public bool canGenerateNextIterationFor(PartyMember member)
            {
                return !usedChars.Contains(member);
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
