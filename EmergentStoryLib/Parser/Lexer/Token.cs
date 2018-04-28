using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Parser.Lexer
{
    /**
     * Discrete segment of information produced by the lexer.
     * */
    public class Token
    {
        /**
         * The thing contained in the token. For example, a whitespace token may contain
         * a space or \t. A newline token may contain \n or \r. A text token may contain
         * the string "fungible". A section token must contain a valid section heading
         * (see SpecialSymbols class)
         * */
        public string contents { get; set; }

        public TokenTypes type { get; protected set; }

        public Token(string contents, TokenTypes type)
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
