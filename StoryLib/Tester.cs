using StoryLib.Active;
using StoryLib.Defenitions;
using StoryLib.Defenitions.Filters;
using StoryLib.Defenitions.Scripting;
using StoryLib.Defenitions.Scripting.DefaultLanguage.Commands;
using StoryLib.Parser;
using StoryLib.Parser.Lexer;
using StoryLib.Parser.Lexer.TokenTypes;
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
            //PlotPointFactory factory = new PlotFactoryParser().parse(new Lexer().lex(System.IO.File.ReadAllText("../StoryLib/Test/test_fire/start.txt")));
            //Console.WriteLine(factory.descriptor);
            //Console.WriteLine

            /*PlotPointFactory factory = new PlotFactoryParser().parse(new Lexer().lex(System.IO.File.ReadAllText("../StoryLib/Test/test_fire/start.txt")));
            foreach (string key in factory.characterFilters.Keys)
            {
                Console.Write(key + " :");
                foreach(Filter filter in factory.characterFilters[key])
                {
                    Console.Write(filter);
                }
                Console.WriteLine();
            }
            Console.WriteLine(factory.descriptor);*/


            instance = this;
            thesaurus = new Thesaurus();
            PlotPointRegistrar.basePath = "../StoryLib/Test/";
           


            PlotPoint.continueStoryEvent += continueStoryEvent;

            foreach (string file in Directory.EnumerateFiles("../StoryLib/Test/WordExtensions/"))
            {
                Console.WriteLine(file);
                //thesaurus.addWord(WordExtensionParser.parse(System.IO.File.ReadAllText(file)));
                thesaurus.addWord(new WordExtensionParser().parse(new Lexer().lex(System.IO.File.ReadAllText(file))));
            }

            PlotPointFactory plotPoint = PlotPointRegistrar.GetPlotPointFactory("test_fire/start");
            



            party = new Party();
            PartyMember steve = new PartyMember("steve", "him");
            PartyMember bill = new PartyMember("bill", "him");
            bill.tags.Add("impulsive");
            PartyMember wanda = new PartyMember("wanda", "her");
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
