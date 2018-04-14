using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser.Lexer.TokenTypes
{
    public class Token_Text : TokenType
    {
        public Token_Text(string contents) : base(contents)
        {
            type = TokenTypes.TEXT;
        }


        public override string ToString()
        {
            return "Token_Text " + contents;
        }
    }
}
