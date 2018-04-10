﻿using StoryLib.Active;
using StoryLib.Defenitions;
using StoryLib.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StoryLib
{
    public class Tester
    {
        public static void test()
        {
            PlotPointFactory plotPoint = PlotParser.parse(System.IO.File.ReadAllText("../StoryLib/Test/test_fire.json"));
            Thesaurus thesaurus = new Thesaurus();
            foreach(string file in Directory.EnumerateFiles("../StoryLib/Test/WordExtensions/"))
            {
                thesaurus.addWord(WordExtensionParser.parse(System.IO.File.ReadAllText(file)));
            }

            Party party = new Party();
            PartyMember steve = new PartyMember("steve");
            PartyMember bill = new PartyMember("bill");
            bill.tags.Add("impulsive");
            party.Add(steve);
            party.Add(bill);

            PlotPoint plot = plotPoint.generatePlotPoint(thesaurus, party);

            Console.WriteLine(plot.descriptor);
            for (int i = 0; i < plot.options.Count; i++)
            {
                Console.WriteLine("\t" + i + "] " + plot.options[i].descriptor);
            }
        }
    }
}
