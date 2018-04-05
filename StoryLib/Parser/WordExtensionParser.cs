using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoryLib.Defenitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Parser
{
    class WordExtensionParser
    {
        public static WordExtension parse(string input)
        {
            dynamic stuff = JsonConvert.DeserializeObject(input);
            string parent = stuff.parent;
            List<string> tags = new List<string>();
            string word = stuff.word;
            string word_past = stuff.word_past;

            JArray optionTokens = stuff.tags;
            foreach (JToken token in optionTokens)
            {
                tags.Add(token.Value<String>());
            }

            WordExtension extension = new WordExtension();
            extension.parent = parent;
            extension.word = word;
            extension.word_past = word_past;
            extension.tags = tags.ToArray();

            return extension;
        }
    }
}
