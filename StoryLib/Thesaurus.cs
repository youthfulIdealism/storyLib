using StoryLib.Defenitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib
{
    public class Thesaurus : Dictionary<String, Dictionary<String, List<WordExtension>>>
    {
        public Thesaurus()
        {

        }

        public void addWord(WordExtension extension)
        {
            foreach (string str in extension.tags)
            {

                if (!ContainsKey(extension.parent))
                {
                    Add(extension.parent, new Dictionary<String, List<WordExtension>>());
                }

                foreach (String val in extension.tags)
                {
                    if (!this[extension.parent].ContainsKey(val))
                    {
                        this[extension.parent].Add(val, new List<WordExtension>());
                    }
                    this[extension.parent][val].Add(extension);
                }
            }
        }
    }
}
