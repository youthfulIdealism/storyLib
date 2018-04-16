using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser.Lexer
{
    public class TokenType
    {
        public string contents { get; set; }
        public TokenTypes type { get; protected set; }
        public TokenType(string contents, TokenTypes type)
        {
            this.contents = contents;
            this.type = type;
        }

        public override string ToString()
        {
            return this.GetType() + " \"" + contents + "\"";
        }
    }
}
