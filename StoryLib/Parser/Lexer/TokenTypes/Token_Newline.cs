using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser.Lexer.TokenTypes
{
    public class Token_Newline : TokenType
    {

        public Token_Newline(string contents) : base(contents)
        {
            type = TokenTypes.NEWLINE;
        }


        public override string ToString()
        {
            return "Token_Newline";
        }
    }
}
