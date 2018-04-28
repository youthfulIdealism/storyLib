using EmergentStoryLib.Active;
using EmergentStoryLib.Defenitions.Filters;
using System;
using System.Collections.Generic;

namespace EmergentStoryLib.Defenitions
{
    /**
     * Builds context for plot, including party and roles.
     * */
    public class ContextBuilder
    {
        /**
         * Role->conditions-to-fill-role map
         * */
        public Dictionary<string, Filter<PartyMember>[]> characterFilters { get; set; }

        public ContextBuilder(Dictionary<string, Filter<PartyMember>[]> characterFilters)
        {
            this.characterFilters = characterFilters;
        }

        /**
         * Returns true if it's possible to fill all roles
         * */
        public bool canBuildContext(Party party, Thesaurus thesaurus)
        {
            //TODO: this is dumb. Find out a more efficient way.
            return buildContext(party, thesaurus) != null;
        }

        /**
         * Returns true if it's possible to fill all roles
         * */
        public bool canAddToContext(PlotContext previousContext)
        {
            //TODO: this is dumb. Find out a more efficient way.
            return addToContext(previousContext) != previousContext || characterFilters.Count == 0;
        }

        /**
         * Returns a context with roles filled
         * */
        public PlotContext buildContext(Party party, Thesaurus thesaurus)
        {
            if(characterFilters.Count == 0)
            {
                return new PlotContext(party, thesaurus);
            }

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

            workingContexts.Add(new WorkingContext(party, new HashSet<PartyMember>(), thesaurus));

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

        /**
         * Returns a context with roles filled
         * */
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
            WorkingContext startingContext = new WorkingContext(previousContext.party, new HashSet<PartyMember>(), previousContext.thesaurus);
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


            
            if(nextWorkingContexts.Count > 0)
            {
                Random rand = new Random();
                return nextWorkingContexts[rand.Next(nextWorkingContexts.Count)].toPlainPlotContext();
            }

            
            return previousContext;
        }

        protected class WorkingContext : PlotContext
        {
            HashSet<PartyMember> usedChars;
            public WorkingContext(Party party, HashSet<PartyMember> usedChars, Thesaurus thesaurus) : base(party, thesaurus)
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
                WorkingContext next = new WorkingContext(party, nextUsedCharacters, thesaurus);
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
                PlotContext context = new PlotContext(party, thesaurus);
                context.partyMemberDefenitions = partyMemberDefenitions;
                return context;
            }

        }
    }
}
