using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser.Lexer.TokenTypes
{
    public class Token_Section : TokenType
    {
        public Token_Section(string contents) : base(contents)
        {
            type = TokenTypes.SECTION;
        }


        public override string ToString()
        {
           return "Token_Section " + contents;
        }
    }
}
