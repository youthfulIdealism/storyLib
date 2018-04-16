using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser.Lexer
{
    public class Lexer
    {
        private int ix;
        private string input;

        public LinkedList<TokenType> tokens;
        public LinkedList<TokenType> lex(string input)
        {
            ix = 0;
            this.input = input;
            tokens = new LinkedList<TokenType>();

            StringBuilder current = new StringBuilder();

            while (ix < input.Length)
            {
                char considered = input[ix];
                switch(considered)
                {
                    case EscapeChars.section:
                        ix++;
                        parseSection();
                        break;
                    case EscapeChars.space_space:
                    case EscapeChars.space_tab:
                    case EscapeChars.newline_n:
                    case EscapeChars.newline_r:
                        if (current.Length > 0)
                        {
                            tokens.AddLast(new TokenType(current.ToString(), TokenTypes.TEXT));
                            current.Clear();
                        }

                        if(considered == EscapeChars.newline_n || considered == EscapeChars.newline_r)
                        {
                            tokens.AddLast(new TokenType("" + considered, TokenTypes.NEWLINE));
                        }
                        else if (considered == EscapeChars.space_space || considered == EscapeChars.space_tab)
                        {
                            tokens.AddLast(new TokenType("" + considered, TokenTypes.WHITESPACE));
                        }

                        ix++;
                        break;
                    case EscapeChars.esc:
                        ix++;
                        current.Append(considered);
                        ix++;
                        break;
                    default:
                        current.Append(considered);
                        ix++;
                        break;

                }
            }

            if(current.Length > 0)
            {
                tokens.AddLast(new TokenType(current.ToString(), TokenTypes.TEXT));
            }

            return tokens;
        }

        private void parseSection()
        {
            StringBuilder current = new StringBuilder();
            

            while (ix < input.Length)
            {
                char considered = input[ix];
                switch (considered)
                {
                    case EscapeChars.section:
                        throw new Exception("Invalid syntax. Unexpected character " + EscapeChars.section + " in section designation.");
                    case EscapeChars.space_space:
                    case EscapeChars.space_tab:
                    case EscapeChars.newline_n:
                    case EscapeChars.newline_r:
                        if (current.Length > 0)
                        {
                            string contents = current.ToString();
                            if(!EscapeChars.validHeaders.Contains(contents))
                            {
                                throw new Exception("Invalid section header " + contents + ".");
                            }
                            tokens.AddLast(new TokenType(current.ToString(), TokenTypes.SECTION));
                        }
                        return;
                    case EscapeChars.esc:
                        ix++;
                        current.Append(considered);
                        ix++;
                        break;
                    default:
                        current.Append(considered);
                        ix++;
                        break;

                }
            }
        }

        

    }
}
