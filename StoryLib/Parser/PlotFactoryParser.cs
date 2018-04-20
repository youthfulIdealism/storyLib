using StoryLib.Active;
using StoryLib.Defenitions;
using StoryLib.Defenitions.Filters;
using StoryLib.Defenitions.Scripting;
using StoryLib.Parser.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser
{
    public class PlotFactoryParser
    {
        private LinkedList<TokenType> tokens;
        private List<OptionFactory> options;
        private Dictionary<string, Filter<PartyMember>[]> characterFilters;
        private StringBuilder descriptor;

        public PlotPointFactory parse(LinkedList<TokenType> tokens)
        {
            this.tokens = tokens;

            options = new List<OptionFactory>();
            characterFilters = new Dictionary<string, Filter<PartyMember>[]>();
            descriptor = new StringBuilder();

            while (tokens.Count > 0)
            {
                TokenType current = tokens.First.Value;

                switch(current.type)
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



                }






            }
            

            return new PlotPointFactory(descriptor.ToString().Trim(), options, characterFilters);
        }


        private void parseSection()
        {
            TokenType current = tokens.First.Value;
            switch (current.contents)
            {
                case EscapeChars.type_person:
                    tokens.RemoveFirst();
                    parsePerson();
                    break;
                case EscapeChars.type_text:
                    tokens.RemoveFirst();
                    parseText();
                    break;
                case EscapeChars.type_option:
                    tokens.RemoveFirst();
                    parseOption();
                    break;
                    //TODO: Implement Custom section type. Implement conditional section type.
            }
        }

        private void parsePerson()
        {
            consumeWhitespace();
            TokenType current = tokens.First.Value;
            if (current.type == TokenTypes.TEXT)
            {
                string descriptor = current.contents;
                tokens.RemoveFirst();
                consumeWhitespaceAndNewlines();
                if (tokens.Count > 0)
                {
                    current = tokens.First.Value;
                }

                List<Filter<PartyMember>> filters = new List<Filter<PartyMember>>();

                while (current.type != TokenTypes.SECTION)
                {
                    consumeWhitespaceAndNewlines();
                    filters.Add(parseFilter());
                    consumeWhitespaceAndNewlines();
                    if (tokens.Count > 0)
                    {
                        current = tokens.First.Value;
                    }
                }

                characterFilters.Add(descriptor, filters.ToArray());
            }
            else
            {
                throw new Exception("Expected handle for person. Instead encountered " + current);
            }
        }

        private Filter<PartyMember> parseFilter()
        {
            TokenType current = tokens.First.Value;

            if (current.type != TokenTypes.TEXT)
            {
                throw new Exception("Expected text in filter, got " + current.type + " instead.");
            }

            string filterType = current.contents;
            List<TokenType> arguments = new List<TokenType>();
            tokens.RemoveFirst();
            consumeWhitespace();
            if (tokens.Count > 0)
            {
                current = tokens.First.Value;
            }

            while (current.type != TokenTypes.NEWLINE && tokens.Count > 0)
            {
                
                if (current.type != TokenTypes.TEXT)
                {
                    throw new Exception("Expected text in filter, got " + current.type);
                }

                arguments.Add(current);
                tokens.RemoveFirst();
                consumeWhitespace();
                if (tokens.Count > 0)
                {
                    current = tokens.First.Value;
                }
            }

            List<String> argumentStrings = new List<string>();
            foreach(TokenType token in arguments)
            {
                argumentStrings.Add(token.contents);
            }

            switch (filterType)
            {
                case EscapeChars.type_tag:
                    return new TagFilter(argumentStrings[0]);
            }
            throw new Exception("Filter type " + filterType + " not found.");
        }

        private void parseText()
        {
            consumeWhitespaceAndNewlines();
            TokenType current = tokens.First.Value;
            
            while (current.type != TokenTypes.SECTION)
            {
                descriptor.Append(current.contents);
                tokens.RemoveFirst();
                if (tokens.Count > 0)
                {
                    current = tokens.First.Value;
                }
            }
        }

        private void parseOption()
        {
            //TODO: add branching and naming of script lines
            consumeWhitespace();
            TokenType current = tokens.First.Value;
            StringBuilder optionText = new StringBuilder();
            while(current.type != TokenTypes.NEWLINE && current.type != TokenTypes.SECTION && tokens.Count > 0)
            {
                optionText.Append(current.contents);
                tokens.RemoveFirst();
                if (tokens.Count > 0)
                {
                    current = tokens.First.Value;
                }

            }

            consumeWhitespaceAndNewlines();
            if (tokens.Count > 0)
            {
                current = tokens.First.Value;
            }

            List<CommandExecutor> lines = new List<CommandExecutor>();
            while (current.type != TokenTypes.SECTION && tokens.Count > 0)
            {
                lines.Add(parseScriptLine());
                consumeWhitespaceAndNewlines();
                if (tokens.Count > 0)
                {
                    current = tokens.First.Value;
                }
            }
            options.Add(new OptionFactory(optionText.ToString(), new Script(lines)));
        }

        private CommandExecutor parseScriptLine()
        {
            StringBuilder builder = new StringBuilder();

            TokenType current = tokens.First.Value;
            if(current.type != TokenTypes.TEXT)
            {
                throw new Exception("Expected Text tokentype in script line, got " + current.type + " instead.");
            }
            string commandStr = current.contents;
            tokens.RemoveFirst();

            consumeWhitespace();
            if (tokens.Count > 0)
            {
                current = tokens.First.Value;
            }

            List<String> arguments = new List<string>();
            while (current.type != TokenTypes.NEWLINE && tokens.Count > 0)
            {
                if (current.type != TokenTypes.TEXT)
                {
                    throw new Exception("Expected Text tokentype in script line, got " + current.type + " instead.");
                }
                arguments.Add(current.contents);
                consumeWhitespace();
                tokens.RemoveFirst();

                if(tokens.Count > 0)
                {
                    current = tokens.First.Value;
                }
            }

            string[] args = arguments.ToArray();
            Command command = ScriptRegistrar.getCommand(commandStr);
            return new CommandExecutor(command, args);
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
