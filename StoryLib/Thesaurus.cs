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
            foreach (string tag in extension.tags)
            {

                if (!ContainsKey(extension.parent))
                {
                    Add(extension.parent, new Dictionary<String, List<WordExtension>>());
                }

                if (!this[extension.parent].ContainsKey(tag))
                {
                    this[extension.parent].Add(tag, new List<WordExtension>());
                }
                this[extension.parent][tag].Add(extension);
            }
        }
    }
}
