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

        public override object[] findArguments(string[] args, PlotContext context, Party party)
        {
            return new object[] { context.partyMemberDefenitions[args[0].Replace("$", "")] };
        }
    }
}
