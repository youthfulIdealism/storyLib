﻿using System;
using System.Collections.Generic;
using System.Text;
using StoryLib.Active;

namespace StoryLib.Defenitions.Scripting.DefaultLanguage.ArgumentFinders
{
    public class Find_Int : ArgumentFinder
    {
        public static Find_Int instance;
        static Find_Int()
        {
            instance = new Find_Int();
        }

        private Find_Int()
        {
            types = new Type[] { typeof(int) };
        }

        public override object[] findArguments(string[] args, PlotContext context, Party party)
        {
            return new object[] {int.Parse(args[0]) };
        }
    }
}
