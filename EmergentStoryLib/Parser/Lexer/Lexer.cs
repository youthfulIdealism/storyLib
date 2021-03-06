﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Parser.Lexer
{
    public class Lexer
    {
        private int ix;
        private string input;

        public LinkedList<Token> tokens;
        public LinkedList<Token> lex(string input)
        {
            ix = 0;
            this.input = input;
            tokens = new LinkedList<Token>();

            StringBuilder current = new StringBuilder();

            while (ix < input.Length)
            {
                char considered = input[ix];
                switch(considered)
                {
                    case SpecialSymbols.section:
                        ix++;
                        lexSection();
                        break;
                    case SpecialSymbols.space_space:
                    case SpecialSymbols.space_tab:
                    case SpecialSymbols.newline_n:
                    case SpecialSymbols.newline_r:
                        if (current.Length > 0)
                        {
                            tokens.AddLast(new Token(current.ToString(), TokenTypes.TEXT));
                            current.Clear();
                        }

                        if(considered == SpecialSymbols.newline_n || considered == SpecialSymbols.newline_r)
                        {
                            tokens.AddLast(new Token("" + considered, TokenTypes.NEWLINE));
                        }
                        else if (considered == SpecialSymbols.space_space || considered == SpecialSymbols.space_tab)
                        {
                            tokens.AddLast(new Token("" + considered, TokenTypes.WHITESPACE));
                        }

                        ix++;
                        break;
                    case SpecialSymbols.esc:
                        ix++;
                        if (current.Length > 0)
                        {
                            tokens.AddLast(new Token(current.ToString(), TokenTypes.TEXT));
                            current.Clear();
                        }
                        switch(input[ix])
                        {
                            case '#':
                                tokens.AddLast(new Token("#", TokenTypes.TEXT));
                                break;
                            default:
                                tokens.AddLast(new Token("" + SpecialSymbols.esc + input[ix], TokenTypes.TEXT));
                                break;
                        }
                       
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
                tokens.AddLast(new Token(current.ToString(), TokenTypes.TEXT));
            }

            return tokens;
        }

        private void lexSection()
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
                            tokens.AddLast(new Token(current.ToString(), TokenTypes.SECTION));
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
