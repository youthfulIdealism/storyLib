using StoryLib.Active;
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
            PartyMember steve = new PartyMember("steve", "him");
            PartyMember bill = new PartyMember("bill", "him");
            bill.tags.Add("impulsive");
            PartyMember wanda = new PartyMember("wanda", "her");
            wanda.tags.Add("impulsive");
            party.members.Add(steve);
            party.members.Add(bill);
            party.members.Add(wanda);

            PlotPoint plot = plotPoint.generatePlotPoint(thesaurus, party);

            Console.WriteLine(plot.descriptor);
            for (int i = 0; i < plot.options.Count; i++)
            {
                Console.WriteLine("\t" + i + "] " + plot.options[i].descriptor);
            }
        }
    }
}
