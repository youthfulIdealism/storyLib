using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser.Lexer
{
    public static class EscapeChars
    {
        public const char esc = '\\';
        public const char section = '#';
        public const char newline_n = '\n';
        public const char newline_r = '\r';
        public const char space_space = ' ';
        public const char space_tab = '\t';

        public const string type_person = "person";
        public const string type_text = "text";
        public const string type_option = "option";
        public const string type_tag = "tag";
        public const string type_custom = "custom";
        public static HashSet<string> validHeaders;
        static EscapeChars()
        {
            validHeaders = new HashSet<string>();
            validHeaders.Add(type_person);
            validHeaders.Add(type_text);
            validHeaders.Add(type_option);
            validHeaders.Add(type_tag);
            validHeaders.Add(type_custom);
        }

    }
}
