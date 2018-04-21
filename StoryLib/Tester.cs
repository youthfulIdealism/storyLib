using StoryLib.Active;
using StoryLib.Defenitions;
using StoryLib.Defenitions.Filters;
using StoryLib.Defenitions.PronounPackages;
using StoryLib.Defenitions.Scripting;
using StoryLib.Defenitions.Scripting.DefaultLanguage.Commands;
using StoryLib.Parser;
using StoryLib.Parser.Lexer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StoryLib
{
    public class Tester
    {
        public static Tester instance;

        PlotPoint plot;
        Thesaurus thesaurus;
        Party party;
        public void test()
        {
            ScriptRegistrar.buildDefaultLanguage();


            instance = this;
            thesaurus = new Thesaurus();
            PlotPointRegistrar.basePath = "../StoryLib/Test/";
           


            PlotPoint.continueStoryEvent += continueStoryEvent;

            foreach (string file in Directory.EnumerateFiles("../StoryLib/Test/WordExtensions/"))
            {
                thesaurus.addWord(new WordExtensionParser().parse(new Lexer().lex(System.IO.File.ReadAllText(file))));
            }

            PlotPointFactory plotPoint = PlotPointRegistrar.GetPlotPointFactory("test_fire/start");
            



            party = new Party();
            PartyMember steve = new PartyMember("Steve", new Male());
            PartyMember bill = new PartyMember("Bill", new Male());
            bill.tags.Add("impulsive");
            PartyMember wanda = new PartyMember("Wanda", new Female());
            wanda.tags.Add("impulsive");
            party.members.Add(steve);
            party.members.Add(bill);
            party.members.Add(wanda);

            plot = plotPoint.generatePlotPoint(thesaurus, party);

            driver();
            Console.ReadLine();
        }

        public void driver()
        {
            

            Console.WriteLine(plot.descriptor);
            for (int i = 0; i < plot.options.Count; i++)
            {
                Console.WriteLine("\t" + i + "] " + plot.options[i].descriptor);
            }

            string choiceStr = Console.ReadLine();
            int choice = -1;
            while (!int.TryParse(choiceStr, out choice))
            {
                choiceStr = Console.ReadLine();
            }
            plot.MakeChoice(choice);
            driver();
        }
           
        static void continueStoryEvent(object sender, Command_Contnue_Story_Args e)
        {
            instance.plot = e.nextPlotPoint.generatePlotPoint(instance.plot.context, instance.thesaurus, instance.party);
        }
    }
}
