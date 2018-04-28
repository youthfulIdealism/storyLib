using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Parser.Lexer
{
    /**
     * container for the various constant symbols used by the lexer and parser.
     * */
    public static class SpecialSymbols
    {
        public const char esc = '\\';
        public const char section = '#';
        public const char newline_n = '\n';
        public const char newline_r = '\r';
        public const char space_space = ' ';
        public const char space_tab = '\t';

        public const string header_person = "person";
        public const string header_text = "text";
        public const string header_option = "option";
        public const string header_filter = "filter";
        public const string header_end = "end";
        public const string header_custom = "custom";
        public const string header_script = "script";

        public const string header_parent = "parent";
        public const string header_tags = "tags";
        public const string header_word = "word";
        public const string header_word_past = "word_past";
        public const string header_word_ing = "word_ing";
        public const string header_word_present = "word_present";
        public const string header_word_plural = "word_plural";


        public static HashSet<string> validHeaders;
        static SpecialSymbols()
        {
            validHeaders = new HashSet<string>();
            validHeaders.Add(header_person);
            validHeaders.Add(header_text);
            validHeaders.Add(header_option);
            validHeaders.Add(header_filter);
            validHeaders.Add(header_end);
            validHeaders.Add(header_custom);
            validHeaders.Add(header_script);

            validHeaders.Add(header_parent);
            validHeaders.Add(header_tags);
            validHeaders.Add(header_word);
            validHeaders.Add(header_word_past);
            validHeaders.Add(header_word_ing);
            validHeaders.Add(header_word_present);
            validHeaders.Add(header_word_plural);
        }

    }
}
