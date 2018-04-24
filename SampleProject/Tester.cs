using StoryLib;
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

namespace SampleProject
{
    public class Tester
    {
        public static Tester instance;

        PlotPoint plot;
        Thesaurus thesaurus;
        Party party;


        string[] maleNames =
        {
            "Jon", "Christian", "Waylon", "Collin", "Royce", "Diego", "Nehemiah", "Finn", "Cale", "Cassius", "Salvatore", "Desmond", "Ibrahim", "Rex", "Jadon", "Kamren", "Keshawn", "Gavyn", "Franklin", "Leandro", "Nathanial", "Curtis", "Lukas", "Rafael", "Antoine", "Conor", "Bronson", "Jean", "Jimmy", "Uriel", "Jonah", "Raiden", "Uriah", "Isai", "Abdullah", "Oswaldo", "Jordyn", "Triston", "Aditya", "Keaton", "Zachery", "Danny", "Ryker", "Marlon", "Hezekiah", "Nash", "Valentin", "Cohen", "Ethan", "Frankie"
        };

        string[] femaleNames =
        {
            "Alena", "Scarlet", "Johanna", "Stacy", "Lexie", "Sherlyn", "Gina", "Addyson", "Ann", "Shannon", "Kristen", "Allison", "Joyce", "Julianna", "Caylee", "Chasity", "Miah", "Jazmyn", "Taniyah", "Amara", "Stephanie", "Livia", "Tiara", "Mariyah", "Shiloh", "Leyla", "Lilianna", "Judith", "Ryan", "Yaretzi", "Brisa", "Harper", "Chloe", "Karly", "Maliyah", "Rylee", "Veronica", "Monique", "Meadow", "Kierra", "Gisselle", "Jazmin", "Lisa", "Anaya", "Isabel", "Abbie", "Akira", "Sanai", "Chaya", "Barbara"
        };

        string[,] personalityTraits = {
            { "adventurous", "cautious"},
            { "grumpy", "cheerful" },
            { "athletic", "sickly" },
            { "wise", "foolish" },
            { "attractive", "plain" },
            { "charming", "awkward" },
            { "unflappable", "nervous" },
            { "compassionate", "miserly" }
        };

        public void test()
        {
            ScriptRegistrar.buildDefaultLanguage();
            FilterRegistrar.buildDefaultLanguage();


            instance = this;
            thesaurus = new Thesaurus();
            PlotPointRegistrar.basePath = "../SampleProject/Data/Story/";
           


            PlotPoint.continueStoryEvent += continueStoryEvent;

            foreach (string file in Directory.EnumerateFiles("../SampleProject/Data/Thesaurus/"))
            {
                thesaurus.addWord(new WordExtensionParser().parse(new Lexer().lex(System.IO.File.ReadAllText(file))));
            }

            PlotPointFactory plotPoint = PlotPointRegistrar.GetPlotPointFactory("story_start/story_start");

            



            

            setUpInitialParty();

            plot = plotPoint.generatePlotPoint(thesaurus, party);

            driver();
            Console.ReadLine();
        }

        public void setUpInitialParty()
        {
            Random random = new Random();

            Console.WriteLine("Are you male, or female?\n\t0) male\n\t1) female");
            int choice = -1;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("That was not valid input. Try again?");
            }

            //Build the party
            //Build the player's spouse.
            party = new Party();
            PartyMember spouse = null;
            string spouseTag0 = personalityTraits[random.Next(personalityTraits.GetLength(0)), random.Next(2)];
            string spouseTag1 = personalityTraits[random.Next(personalityTraits.GetLength(0)), random.Next(2)];
            while (spouseTag0 == spouseTag1) { spouseTag1 = personalityTraits[random.Next(personalityTraits.GetLength(0)), random.Next(2)]; }

            if (choice == 0)
            {
                spouse = new PartyMember(femaleNames[random.Next(femaleNames.Length)], new Female());

            }
            else if (choice == 1)
            {
                spouse = new PartyMember(maleNames[random.Next(femaleNames.Length)], new Male());
            }

            spouse.tags.Add(spouseTag0);
            spouse.tags.Add(spouseTag1);
            spouse.tags.Add("player_spouse");
            party.members.Add(spouse);

            Console.WriteLine();
            Console.WriteLine("Thank you. How many children do you have?");
            choice = -1;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("That was not valid input. Try again?");
            }

            List<PartyMember> playerKids = new List<PartyMember>();
            List<String> playerKidDescriptions = new List<string>();

            for (int i = 0; i < choice; i++)
            {
                PartyMember kid = null;
                string kidTag0 = personalityTraits[random.Next(personalityTraits.GetLength(0)), random.Next(2)];
                string kidTag1 = personalityTraits[random.Next(personalityTraits.GetLength(0)), random.Next(2)];
                while(kidTag0 == kidTag1) { kidTag1 = personalityTraits[random.Next(personalityTraits.GetLength(0)), random.Next(2)]; }

                if (random.Next(2) == 0)
                {
                    kid = new PartyMember(femaleNames[random.Next(femaleNames.Length)], new Female());

                }
                else
                {
                    kid = new PartyMember(maleNames[random.Next(femaleNames.Length)], new Male());
                }

                kid.tags.Add(kidTag0);
                kid.tags.Add(kidTag1);
                playerKids.Add(kid);
                kid.tags.Add("player_kid");
                party.members.Add(kid);
                playerKidDescriptions.Add(kid.name + ", who has been called " + kidTag0 + " and " + kidTag1 + ".");
            }

            Console.WriteLine();
            Console.WriteLine(spouse.name + " is your spouse. " + spouse.pronounPackage.subjective + " is known to be " + spouseTag0 + " and " + spouseTag1 + ". Together you have " + choice + " grown children:");
            foreach (String str in playerKidDescriptions)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();

            
        }

        public void driver()
        {
            

            Console.WriteLine(plot.descriptor);
            for (int i = 0; i < plot.options.Count; i++)
            {
                Console.WriteLine("\t" + i + "] " + plot.options[i].descriptor);//
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
            instance.plot = e.nextPlotPoint.generatePlotPoint(instance.plot.context, instance.thesaurus);
        }
    }
}
