using SampleProject.Addons;
using EmergentStoryLib;
using EmergentStoryLib.Instance;
using EmergentStoryLib.Defenitions;
using EmergentStoryLib.Defenitions.Filters;
using EmergentStoryLib.Defenitions.PronounPackages;
using EmergentStoryLib.Defenitions.Scripting;
using EmergentStoryLib.Defenitions.Scripting.DefaultLanguage.Commands;
using EmergentStoryLib.Parser;
using EmergentStoryLib.Parser.Lexer;
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
            { "attrInstance", "plain" },
            { "charming", "awkward" },
            { "unflappable", "nervous" },
            { "compassionate", "miserly" }
        };

        string[,] occupations = {
            { "seer", "seer"},
            { "knight", "valkryie" }

        };

        public void test()
        {
            CommandRegistrar.buildDefaultLanguage();
            FilterRegistrar.buildDefaultLanguage();
            new AddonBuilder().buildAddons();


            instance = this;
            thesaurus = new Thesaurus();
            PlotPointRegistrar.basePath = "../Data/Story/";
           


            PlotPoint.continuePlotArcEvent += continueStoryEvent;
            PlotPoint.newPlotArcEvent += changeStoryEvent;

            foreach (string file in Directory.EnumerateFiles("../Data/Thesaurus/"))
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
                int occupationIX = 0;
                PartyMember kid = null;
                string kidTag0 = personalityTraits[random.Next(personalityTraits.GetLength(0)), random.Next(2)];
                string kidTag1 = personalityTraits[random.Next(personalityTraits.GetLength(0)), random.Next(2)];
                while(kidTag0 == kidTag1) { kidTag1 = personalityTraits[random.Next(personalityTraits.GetLength(0)), random.Next(2)]; }

                if (random.Next(2) == 0)
                {
                    kid = new PartyMember(femaleNames[random.Next(femaleNames.Length)], new Female());
                    occupationIX = 1;
                }
                else
                {
                    kid = new PartyMember(maleNames[random.Next(femaleNames.Length)], new Male());
                }

                kid.tags.Add(kidTag0);
                kid.tags.Add(kidTag1);

                bool role = random.Next(2) == 0;
                string kidOccupation = "";
                if(role)
                {
                    kidOccupation = occupations[random.Next(occupations.GetLength(0)), occupationIX];
                    kid.tags.Add(kidOccupation);
                }

                playerKids.Add(kid);
                kid.tags.Add("player_kid");
                party.members.Add(kid);

                string description = kid.name + ", who has been called " + kidTag0 + " and " + kidTag1 + ".";
                if(kidOccupation != "")
                {
                    description += " " + kid.pronounPackage.subjective + " has been trained as a " + kidOccupation + ".";
                }
                
                    playerKidDescriptions.Add(description);
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
            

            Console.WriteLine(plot.text);
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
           if(!checkForEndgame())
           {
                instance.plot = e.nextPlotPoint.generatePlotPoint(instance.plot.context, instance.thesaurus);
           }
            
        }

        static void changeStoryEvent(object sender, Command_Contnue_Story_Args e)
        {
            if(!checkForEndgame())
            {
                instance.plot = e.nextPlotPoint.generatePlotPoint(instance.thesaurus, instance.party);
            }
            
        }

        static bool checkForEndgame()
        {
            if (instance.party.resources["FOOD"] <= 0)
            {
                instance.plot = PlotPointRegistrar.GetPlotPointFactory("consequenceEvents/noFood").generatePlotPoint(instance.thesaurus, instance.party);
                return true;
            }
            else if (instance.party.resources["MORALE"] <= 0)
            {
                instance.plot = PlotPointRegistrar.GetPlotPointFactory("consequenceEvents/noMorale").generatePlotPoint(instance.thesaurus, instance.party);
                return true;
            }
            else if (instance.party.resources["WATER"] <= 0)
            {
                instance.plot = PlotPointRegistrar.GetPlotPointFactory("consequenceEvents/noWater").generatePlotPoint(instance.thesaurus, instance.party);
                return true;
            }else
            {
                foreach(PartyMember member in instance.party.members)
                {
                    return false;
                }
                instance.plot = PlotPointRegistrar.GetPlotPointFactory("consequenceEvents/noParty").generatePlotPoint(instance.thesaurus, instance.party);
                return true;
            }
            
        }
    }
}
