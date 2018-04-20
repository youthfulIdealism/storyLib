using StoryLib.Defenitions;
using StoryLib.Parser.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser
{
    class WordExtensionParser
    {
        private LinkedList<TokenType> tokens;
        List<string> tags;
        string word = "";
        string word_past = "";
        string word_ing = "";
        string parent = "";

        public WordExtension parse(LinkedList<TokenType> tokens)
        {
            this.tokens = tokens;
            tags = new List<string>();
            word = "";
            word_past = "";

            while (tokens.Count > 0)
            {
                TokenType current = tokens.First.Value;

                switch (current.type)
                {
                    case TokenTypes.NEWLINE:
                        tokens.RemoveFirst();
                        break;
                    case TokenTypes.TEXT:
                        tokens.RemoveFirst();
                        break;
                    case TokenTypes.WHITESPACE:
                        tokens.RemoveFirst();
                        break;
                    case TokenTypes.SECTION:
                        parseSection();
                        break;
                    default:
                        throw new Exception("Unexpected token type " + current.type);


                }
            }

            WordExtension extension = new WordExtension();
            extension.parent = parent;
            extension.word = word;
            extension.word_past = word_past;
            extension.word_ing = word_ing;
            extension.tags = tags.ToArray();

            return extension;
        }

        private void parseSection()
        {
            TokenType current = tokens.First.Value;
            switch (current.contents)
            {
                case SpecialSymbols.header_parent:
                    tokens.RemoveFirst();
                    parent = parseOneLine()[0];
                    break;
                case SpecialSymbols.header_tags:
                    tokens.RemoveFirst();
                    tags.AddRange(parseOneLine());
                    break;
                case SpecialSymbols.header_word:
                    tokens.RemoveFirst();
                    word = parseOneLine()[0];
                    break;
                case SpecialSymbols.header_word_past:
                    tokens.RemoveFirst();
                    word_past = parseOneLine()[0];
                    break;
                case SpecialSymbols.header_word_ing:
                    tokens.RemoveFirst();
                    word_ing = word = parseOneLine()[0];
                    break;
                    //TODO: Implement Custom section type.
            }
        }

        private string[] parseOneLine()
        {
            List<String> returnable = new List<string>();

            consumeWhitespace();
            TokenType current = tokens.First.Value;
            StringBuilder optionText = new StringBuilder();
            while (current.type != TokenTypes.NEWLINE && current.type != TokenTypes.SECTION && tokens.Count > 0)
            {
                returnable.Add(current.contents);
                tokens.RemoveFirst();
                consumeWhitespace();
                if (tokens.Count > 0)
                {
                    current = tokens.First.Value;
                }

            }

            return returnable.ToArray();
        }

        private void consumeWhitespace()
        {
            while (tokens.Count > 0 && tokens.First.Value.type == TokenTypes.WHITESPACE)
            {
                tokens.RemoveFirst();
            }
        }

        private void consumeNewlines()
        {
            while (tokens.Count > 0 && tokens.First.Value.type == TokenTypes.NEWLINE)
            {
                tokens.RemoveFirst();
            }
        }

        private void consumeWhitespaceAndNewlines()
        {
            while (tokens.Count > 0 && (tokens.First.Value.type == TokenTypes.NEWLINE || tokens.First.Value.type == TokenTypes.WHITESPACE))
            {
                tokens.RemoveFirst();
            }
        }
    }
}
