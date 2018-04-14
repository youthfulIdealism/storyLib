using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser.Lexer.TokenTypes
{
    public class Token_Whitespace : TokenType
    {
        public Token_Whitespace(string contents) : base(contents)
        {
            type = TokenTypes.WHITESPACE;
        }


        public override string ToString()
        {
            return "Token_Whitespace";
        }
    }
}
