using System;
using System.Collections.Generic;
using System.Text;
using StoryLib.Active;

namespace StoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders
{
    public class Find_Person : ArgumentFinder
    {
        public static Find_Person instance;
        static Find_Person()
        {
            instance = new Find_Person();
        }

        private Find_Person()
        {
            types = new Type[] { typeof(PartyMember) };
        }

        public override object[] findArguments(string[] args, PlotContext context)
        {
            return new object[] { context.partyMemberDefenitions[wordReplacer.replace(args[0], context)] };
        }
    }
}
