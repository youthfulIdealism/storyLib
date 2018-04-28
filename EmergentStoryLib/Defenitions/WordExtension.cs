using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions
{
    public class WordExtension
    {
        public string parent { get; set; }
        public string[] tags { get; set; }
        public string word { get; set; }
        public string word_past { get; set; }
        public string word_ing { get; set; }
        public string word_plural { get; set; }
        public string word_present { get; set; }
    }
}
