using EmergentStoryLib.Active;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders
{


    public class Find_Person_And_String : ArgumentFinder
    {
        public static Find_Person_And_String instance;
        static Find_Person_And_String()
        {
            instance = new Find_Person_And_String();
        }

        private Find_Person_And_String()
        {
            types = new Type[] { typeof(PartyMember), typeof(string) };
        }

        public override object[] findArguments(string[] args, PlotContext context)
        {
            return new object[] { context.partyMemberDefenitions[wordReplacer.replace(args[0], context)], wordReplacer.replace(args[1], context) };
        }
    }
}
