using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser.Lexer.TokenTypes
{
    public abstract class TokenType
    {
        public string contents { get; set; }
        public TokenTypes type { get; protected set; }
        public TokenType(string contents)
        {
            this.contents = contents;
        }

        public override string ToString()
        {
            return this.GetType() + " \"" + contents + "\"";
        }
    }
}
