﻿using EmergentStoryLib.Instance;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions
{
    /**
     * Responsible for replacing requested substrings.
     * 
     * Can currently replace using word extensions (ex:
     * user has defined word run and extensions sprint
     * and jog. String is "<run> and fetched the ball"
     * can produce "sprint and fetched the ball" or 
     * "jog and fetch the ball".)
     * 
     * Can currently replace variables. (ex: user has
     * defined party member GUY who has the name "steve".
     * "$GUY.NAME$ ran away" will produce "steve ran
     * away").
     * 
     * Responsible for handling special symbols. \n, \r,
     * \t will all be handled elegantly by the replacer.
     * \^ will capitolize the next character in the
     * string.
     * 
     * */
    public class WordReplacer
    {
        public static HashSet<Char> escapeChars;
        public static Random rand;
        public const char esc_var = '$';
        public const char esc_word = '<';
        public const char esc_word_end = '>';
        public const char esc_esc = '\\';
        public const string modifier_flag_past = "-past";
        public const string modifier_flag_ing = "-ing";
        public const string modifier_flag_plural = "-plural";
        public const string modifier_flag_present = "-present";

        private int ix;
        private string input;
        private StringBuilder builder;
        private Thesaurus thesaurus;
        private PlotContext context;
        private bool nextCharUppercase;


        static WordReplacer()
        {
            escapeChars = new HashSet<char>();
            escapeChars.Add(esc_var);
            escapeChars.Add(esc_word);
            escapeChars.Add(esc_esc);
            rand = new Random(4);
        }

        public string replace(string input, PlotContext context)
        {
            nextCharUppercase = false;
            ix = 0;
            this.input = input;
            this.thesaurus = context.thesaurus;
            this.context = context;
            builder = new StringBuilder();
            


            while (ix < input.Length)
            {
                char considered = input[ix];
                if (!escapeChars.Contains(considered))
                {
                    if(nextCharUppercase)
                    {
                        builder.Append(("" + considered).ToUpper());
                    }
                    else
                    {
                        builder.Append(considered);
                    }
                    
                }
                else
                {
                    ix++;
                    switch (considered)
                    {
                        case esc_var:
                            builder.Append(fillVar());
                            break;
                        case esc_word:
                            builder.Append(genWord());
                            break;
                        case esc_esc:
                            switch (input[ix])
                            {
                                case '\\':
                                    builder.Append('\\');
                                    break;
                                case 'n':
                                    builder.Append('\n');
                                    break;
                                case 'r':
                                    builder.Append('\r');
                                    break;
                                case '^':
                                    nextCharUppercase = true;
                                    break;



                            }

                            break;
                    }
                }



                ix++;
            }


            return builder.ToString();
        }

        private string fillVar()
        {
            StringBuilder preWord = new StringBuilder();
            char wordChar = input[ix];
            while (ix < input.Length && wordChar != esc_var)
            {
                preWord.Append(wordChar);
                ix++;
                if(ix < input.Length)
                {
                    wordChar = input[ix];
                }
            }

            string[] chunks = preWord.ToString().Split('.');

            if(context.partyMemberDefenitions.ContainsKey(chunks[0]))
            {
                switch (chunks[1])
                {
                    case "NAME":
                        return handleUppercase(context.partyMemberDefenitions[chunks[0]].name);
                    case "SEX":
                        return handleUppercase(context.partyMemberDefenitions[chunks[0]].pronounPackage.variableAssociations[chunks[2]]);
                }
            }else if(chunks[0] == "RESOURCE")
            {
                return handleUppercase(context.party.resources[chunks[1]] + "");
            }

            return "ERROR";
        }

        private string genWord()
        {
            StringBuilder preWord = new StringBuilder();
            char wordChar = input[ix];
            while (ix < input.Length && wordChar != esc_word_end)
            {
                preWord.Append(wordChar);
                ix++;
                wordChar = input[ix];
            }

            Tense currentTense = Tense.IMPERATIVE;
            List<WordExtension> possibleAlternatives = new List<WordExtension>();

            string[] args = preWord.ToString().Split(' ');

            List<string> flags = new List<string>();
            List<string> tags = new List<string>();

            for (int i = 1; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg.StartsWith("-"))
                {
                    flags.Add(arg);
                }
                else
                {
                    tags.Add(arg);
                }
            }

            if (tags.Count == 0)
            {
                tags.Add("generic");
            }
            
            foreach (string tag in tags)
            {
                possibleAlternatives.AddRange(thesaurus[args[0]][tag]);
            }

            foreach (string flag in flags)
            {
                switch (flag)
                {
                    case modifier_flag_past:
                        currentTense = Tense.PAST;
                        break;
                    case modifier_flag_ing:
                        currentTense = Tense.ING;
                        break;
                    case modifier_flag_plural:
                        currentTense = Tense.PLURAL;
                        break;
                    case modifier_flag_present:
                        currentTense = Tense.PRESENT;
                        break;
                    default:
                        throw new Exception("flag " + flag + " not recognized while replacing word " + args[0] + ".");
                }
            }

            switch (currentTense)
            {
                case Tense.IMPERATIVE:
                    return handleUppercase(possibleAlternatives[rand.Next(possibleAlternatives.Count)].word);
                case Tense.PAST:
                    return handleUppercase(possibleAlternatives[rand.Next(possibleAlternatives.Count)].word_past);
                case Tense.ING:
                    return handleUppercase(possibleAlternatives[rand.Next(possibleAlternatives.Count)].word_ing);
                case Tense.PLURAL:
                    return handleUppercase(possibleAlternatives[rand.Next(possibleAlternatives.Count)].word_plural);
                case Tense.PRESENT:
                    return handleUppercase(possibleAlternatives[rand.Next(possibleAlternatives.Count)].word_present);

            }
            return "ERROR";
        }

        private string handleUppercase(string input)
        {
            if(nextCharUppercase)
            {
                nextCharUppercase = false;
                return ("" + input[0]).ToUpper() + input.Substring(1);
            }
            return input;
        }
    }
}
