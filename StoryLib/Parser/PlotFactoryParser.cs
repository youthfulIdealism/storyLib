﻿using StoryLib.Active;
using StoryLib.Defenitions;
using StoryLib.Defenitions.Filters;
using StoryLib.Defenitions.Filters.PartyMemberFilters;
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
        private List<Tuple<Filter<PlotContext>[], PlotPointFactory>> nestedPlotPoints;
        private Script setupScript;

        public PlotPointFactory parse(LinkedList<TokenType> tokens)
        {
            this.tokens = tokens;

            options = new List<OptionFactory>();
            characterFilters = new Dictionary<string, Filter<PartyMember>[]>();
            descriptor = new StringBuilder();
            nestedPlotPoints = new List<Tuple<Filter<PlotContext>[], PlotPointFactory>>();

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
            

            return new PlotPointFactory(descriptor.ToString().Trim(), options, characterFilters, nestedPlotPoints, setupScript);
        }


        private void parseSection()
        {
            TokenType current = tokens.First.Value;
            switch (current.contents)
            {
                case SpecialSymbols.header_person:
                    tokens.RemoveFirst();
                    parsePerson();
                    break;
                case SpecialSymbols.header_text:
                    tokens.RemoveFirst();
                    parseText();
                    break;
                case SpecialSymbols.header_option:
                    tokens.RemoveFirst();
                    parseOption();
                    break;
                case SpecialSymbols.header_filter:
                    tokens.RemoveFirst();
                    parseNested();
                    break;
                case SpecialSymbols.header_script:
                    tokens.RemoveFirst();
                    parseScript();
                    break;
                    //TODO: Implement Custom section type.
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

                while (current.type != TokenTypes.SECTION && tokens.Count > 0)
                {
                    consumeWhitespaceAndNewlines();
                    filters.Add(parsePartyMemberFilter());
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

        private void parseNested()
        {
            LinkedList<TokenType> subList = new LinkedList<TokenType>();
            int nestedCount = 1;

            consumeWhitespaceAndNewlines();
            TokenType current = tokens.First.Value;

            List<Filter<PlotContext>> filters = new List<Filter<PlotContext>>();

            while (current.type != TokenTypes.SECTION && tokens.Count > 0)
            {
                consumeWhitespaceAndNewlines();
                filters.Add(parsePlotContextFilter());
                consumeWhitespaceAndNewlines();
                if (tokens.Count > 0)
                {
                    current = tokens.First.Value;
                }
            }

            while (nestedCount > 0 && tokens.Count > 0)
            {
                current = tokens.First.Value;
                if (current.type == TokenTypes.SECTION && current.contents == SpecialSymbols.header_end)
                {
                    nestedCount--;
                }else if (current.type == TokenTypes.SECTION && current.contents == SpecialSymbols.header_filter)
                {
                    nestedCount++;
                }
                else
                {
                    subList.AddLast(current);
                }
                tokens.RemoveFirst();
            }

            nestedPlotPoints.Add(new Tuple<Filter<PlotContext>[], PlotPointFactory>(filters.ToArray(), new PlotFactoryParser().parse(subList)));
        }

        private Filter<PlotContext> parsePlotContextFilter()
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
            foreach (TokenType token in arguments)
            {
                argumentStrings.Add(token.contents);
            }

            return FilterRegistrar.getContextFilter(filterType, argumentStrings.ToArray());
        }

        private Filter<PartyMember> parsePartyMemberFilter()
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

            return FilterRegistrar.getPartyFilter(filterType, argumentStrings.ToArray());
        }

        private void parseText()
        {
            consumeWhitespaceAndNewlines();
            TokenType current = tokens.First.Value;
            
            while (current.type != TokenTypes.SECTION && tokens.Count > 0)
            {
                descriptor.Append(current.contents);
                tokens.RemoveFirst();
                if (tokens.Count > 0)
                {
                    current = tokens.First.Value;
                }
            }
        }

        private void parseScript()
        {
            TokenType current = tokens.First.Value;

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

            if(setupScript == null)
            {
                setupScript = new Script(lines);
            }else
            {
                setupScript.lines.AddRange(lines);
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
                tokens.RemoveFirst();
                consumeWhitespace();

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
