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
                    case SpecialSymbols.section:
                        ix++;
                        parseSection();
                        break;
                    case SpecialSymbols.space_space:
                    case SpecialSymbols.space_tab:
                    case SpecialSymbols.newline_n:
                    case SpecialSymbols.newline_r:
                        if (current.Length > 0)
                        {
                            tokens.AddLast(new TokenType(current.ToString(), TokenTypes.TEXT));
                            current.Clear();
                        }

                        if(considered == SpecialSymbols.newline_n || considered == SpecialSymbols.newline_r)
                        {
                            tokens.AddLast(new TokenType("" + considered, TokenTypes.NEWLINE));
                        }
                        else if (considered == SpecialSymbols.space_space || considered == SpecialSymbols.space_tab)
                        {
                            tokens.AddLast(new TokenType("" + considered, TokenTypes.WHITESPACE));
                        }

                        ix++;
                        break;
                    case SpecialSymbols.esc:
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
                    case SpecialSymbols.section:
                        throw new Exception("Invalid syntax. Unexpected character " + SpecialSymbols.section + " in section designation.");
                    case SpecialSymbols.space_space:
                    case SpecialSymbols.space_tab:
                    case SpecialSymbols.newline_n:
                    case SpecialSymbols.newline_r:
                        if (current.Length > 0)
                        {
                            string contents = current.ToString();
                            if(!SpecialSymbols.validHeaders.Contains(contents))
                            {
                                throw new Exception("Invalid section header " + contents + ".");
                            }
                            tokens.AddLast(new TokenType(current.ToString(), TokenTypes.SECTION));
                        }
                        return;
                    case SpecialSymbols.esc:
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
