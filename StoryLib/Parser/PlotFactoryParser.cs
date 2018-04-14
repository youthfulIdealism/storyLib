using StoryLib.Defenitions;
using StoryLib.Defenitions.Filters;
using StoryLib.Defenitions.Scripting;
using StoryLib.Parser.Lexer;
using StoryLib.Parser.Lexer.TokenTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser
{
    public class PlotFactoryParser
    {
        private LinkedList<TokenType> tokens;
        private List<OptionFactory> options;
        private Dictionary<string, Filter[]> characterFilters;

        public PlotPointFactory parse(LinkedList<TokenType> tokens)
        {
            this.tokens = tokens;

            options = new List<OptionFactory>();
            characterFilters = new Dictionary<string, Filter[]>();
            StringBuilder descriptor = new StringBuilder();

            

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
            

            return new PlotPointFactory(descriptor.ToString(), options, characterFilters);
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
                    break;
                case EscapeChars.type_option:
                    tokens.RemoveFirst();
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
                current = tokens.First.Value;

                List<Filter> filters = new List<Filter>();

                while (current.type != TokenTypes.SECTION)
                {
                    consumeWhitespaceAndNewlines();
                    filters.Add(parseFilter());
                    consumeWhitespaceAndNewlines();
                    current = tokens.First.Value;
                }

                characterFilters.Add(descriptor, filters.ToArray());
            }
            else
            {
                throw new Exception("Expected handle for person. Instead encountered " + current);
            }
        }



        private Filter parseFilter()
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
            current = tokens.First.Value;

            while (current.type != TokenTypes.NEWLINE && tokens.Count > 0)
            {
                
                if (current.type != TokenTypes.TEXT)
                {
                    throw new Exception("Expected text in filter, got " + current.type);
                }

                arguments.Add(current);
                tokens.RemoveFirst();
                consumeWhitespace();
                current = tokens.First.Value;
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











        /*public static HashSet<Char> escapeChars;
        public const char esc_esc = '\\';
        public const char esc_section = '#';
        public const string esc_newline = "\n\r";
        public const string esc_space = " \t";

        public const string type_person = "person";
        public const string type_text = "text";
        public const string type_option = "option";
        public const string type_tag = "tag";
        public const string type_custom = "custom";

        int ix;
        string input;

        static PlotFactoryParser()
        {
            escapeChars = new HashSet<char>();
            escapeChars.Add(esc_esc);
            escapeChars.Add(esc_section);
        }

        public PlotPointFactory parse(string input)
        {
            ix = 0;
            this.input = input;

            StringBuilder descriptor = new StringBuilder();
            List<OptionFactory> options = new List<OptionFactory>();
            Dictionary<string, Filter[]> characterFilters = new Dictionary<string, Filter[]>();

            while (ix < input.Length)
            {
                char considered = input[ix];
                if (!escapeChars.Contains(considered))
                {
                    descriptor.Append(considered);
                    ix++;
                }
                else
                {
                    switch (considered)
                    {
                        case esc_section:
                            ix++;
                            object sectionContents = getSectionContents();
                            if (sectionContents is List<OptionFactory>)
                            {
                                options.AddRange((List<OptionFactory>)sectionContents);
                            }
                            else if (sectionContents is Tuple<string, Filter[]>)
                            {
                                Tuple<string, Filter[]> contents = (Tuple<string, Filter[]>)sectionContents;
                                characterFilters.Add(contents.Item1, contents.Item2);
                            }else if(sectionContents is string)
                            {
                                descriptor.Append((string)sectionContents);
                            }
                            break;
                        case esc_esc:
                            ix++;
                            descriptor.Append(input[ix]);
                            break;
                    }
                }



                
            }


            return new PlotPointFactory(descriptor.ToString(), options, characterFilters);
        }

        public Object getSectionContents()
        {
            Console.WriteLine("in getSectionContents");
            StringBuilder sectionTypeBuilder = new StringBuilder();
            while (ix < input.Length)
            {
                char considered = input[ix];
                
                if (!esc_space.Contains("" + considered) && !esc_newline.Contains("" + considered))
                {
                    sectionTypeBuilder.Append(considered);
                }
                else
                {
                    string type = sectionTypeBuilder.ToString();
                    if (type == type_person)
                    {
                        return parsePerson();
                    }else if(type == type_text)
                    {
                        return parseText();
                    }
                    else if (type == type_option)
                    {
                        return parseOption();
                    }
                    else if (type == type_custom)
                    {
                        //TODO: implement
                    }
                }



                ix++;
            }
            return null;
        }

        public string parseText()
        {
            Console.WriteLine("in parseText");
            StringBuilder textBuilder = new StringBuilder();

            bool keepParsing = true;
            while (ix < input.Length && keepParsing)
            {
                char considered = input[ix];
                if (!escapeChars.Contains(considered))
                {
                    textBuilder.Append(considered);
                }
                else
                {
                    switch (considered)
                    {
                        case esc_section:
                            keepParsing = false;
                            break;
                        case esc_esc:
                            ix++;
                            textBuilder.Append(input[ix]);
                            break;
                    }
                }



                ix++;
            }

            return textBuilder.ToString().Trim();
        }

        public Tuple<string, Filter[]> parsePerson()
        {
            Console.WriteLine("in parsePerson");
            List<Filter> filters = new List<Filter>();
            skipWhitespace();
            
            string handle = parseHandle();
            skipWhitespace();
            skipNewlines();

            while (input[ix] != esc_section)
            {
                skipWhitespace();
                filters.Add(parseFilter());
                skipWhitespace();
                skipNewlines();
            }


            return new Tuple<string, Filter[]>(handle, filters.ToArray());
        }

        public string parseHandle()
        {
            Console.WriteLine("in parseHandle");
            StringBuilder handleBuilder = new StringBuilder();
            char considered = input[ix];
            while (ix < input.Length && !esc_space.Contains("" + considered) && !esc_newline.Contains("" + considered))
            {
                handleBuilder.Append(considered);
                ix++;
                considered = input[ix];
            }
            return handleBuilder.ToString();
        }

        public Filter parseFilter()
        {
            Console.WriteLine("in parseFilter");
            StringBuilder builder = new StringBuilder();
            char considered = input[ix];
            while (ix < input.Length && !esc_newline.Contains("" + considered))
            {
                builder.Append(considered);
                bool wasWhitespace = esc_space.Contains("" + considered);
                ix++;
                if (ix < input.Length)
                {
                    considered = input[ix];
                    if (wasWhitespace) { skipWhitespace(); }
                }
            }

            string[] subComponents = builder.ToString().Split(' ');
            switch (subComponents[0])
            {
                case "tag":
                    return new TagFilter(subComponents[1]);
            }
            return null;
        }

        public OptionFactory parseOption()
        {
            Console.WriteLine("in parseOption");
            StringBuilder descriptorBuilder = new StringBuilder();
            char considered = input[ix];
            while (ix < input.Length && !esc_newline.Contains("" + considered))
            {
                descriptorBuilder.Append(considered);
                ix++;
                considered = input[ix];
            }
            skipNewlines();

            List<CommandExecutor> lines = new List<CommandExecutor>();

            while (ix < input.Length && input[ix] != esc_section)
            {
                skipWhitespace();
                lines.Add(parseScriptLine());
                skipWhitespace();
                skipNewlines();
            }

            return new OptionFactory(descriptorBuilder.ToString(), new Script(lines));
        }


        public CommandExecutor parseScriptLine()
        {
            Console.WriteLine("in parseScriptLine");
            StringBuilder builder = new StringBuilder();
            char considered = input[ix];
            while (ix < input.Length && !esc_newline.Contains("" + considered))
            {
                builder.Append(considered);
                bool wasWhitespace = esc_space.Contains("" + considered);
                ix++;
                if(ix < input.Length)
                {
                    considered = input[ix];
                    if (wasWhitespace) { skipWhitespace(); }
                }
            }

            string[] subComponents = builder.ToString().Split(' ');
            Command command = ScriptRegistrar.getCommand(subComponents[0]);
            string[] args = new string[subComponents.Length - 1];
            for (int i = 1; i < subComponents.Length; i++)
            {
                args[i - 1] = subComponents[i];
            }
            return new CommandExecutor(command, args);
        }

        public void skipWhitespace()
        {
            while (ix < input.Length && esc_space.Contains("" + input[ix]))
            {
                ix++;
            }
        }
        public void skipNewlines()
        {
            while (ix < input.Length && esc_newline.Contains("" + input[ix]))
            {
                ix++;
            }
        }*/
    }
}
