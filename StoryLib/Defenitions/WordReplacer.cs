﻿using StoryLib.Active;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class WordReplacer
    {
        public static HashSet<Char> escapeChars;
        public static Random rand;
        public const char esc_var = '$';
        public const char esc_word = '<';
        public const char esc_word_end = '>';
        public const char esc_esc = '\\';
        public const string tense_flag_past = "-past";

        int ix;
        string input;
        StringBuilder builder;
        Thesaurus thesaurus;
        PlotContext context;


        static WordReplacer()
        {
            escapeChars = new HashSet<char>();
            escapeChars.Add(esc_var);
            escapeChars.Add(esc_word);
            escapeChars.Add(esc_esc);
            rand = new Random();
        }

        public string replace(string input, Thesaurus thesaurus, PlotContext context)
        {
            ix = 0;
            this.input = input;
            this.thesaurus = thesaurus;
            this.context = context;
            builder = new StringBuilder();
            


            while (ix < input.Length)
            {
                char considered = input[ix];
                if (!escapeChars.Contains(considered))
                {
                    builder.Append(considered);
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
                            builder.Append(input[ix]);
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
                        return context.partyMemberDefenitions[chunks[0]].name;
                    case "SEX":
                        return context.partyMemberDefenitions[chunks[0]].pronounPackage.variableAssociations[chunks[2]];
                    case "ID":
                        return context.partyMemberDefenitions[chunks[0]].name;

                }
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

            string[] args = preWord.ToString().Split(' ');
            if(args.Length == 1)
            {
                return args[0];
            }
            else
            {
                Tense currentTense = Tense.IMPERATIVE;
                List<WordExtension> possibleAlternatives = new List<WordExtension>();
                possibleAlternatives.Add(thesaurus[args[0]]["generic"][0]);
                for (int i = 1; i < args.Length; i++)
                {
                    var arg = args[i];

                    if (arg == tense_flag_past)
                    {
                        currentTense = Tense.PAST;
                    }
                    else
                    {
                        var currentAlternatives = thesaurus[args[0]][arg];

                        for (int p = 0; p < currentAlternatives.Count; p++)
                        {

                            possibleAlternatives.Add(currentAlternatives[p]);
                        }
                    }



                   

                }

                switch(currentTense)
                {
                    case Tense.IMPERATIVE:
                        return possibleAlternatives[rand.Next(possibleAlternatives.Count)].word;
                    case Tense.PAST:
                        return possibleAlternatives[rand.Next(possibleAlternatives.Count)].word_past;

                }
            }
            return "";
        }
    }
}
