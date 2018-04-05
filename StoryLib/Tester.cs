using StoryLib.Defenitions;
using StoryLib.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StoryLib
{
    public class Tester
    {
        public static void test()
        {
            PlotPoint plotPoint = PlotParser.parse(System.IO.File.ReadAllText("../StoryLib/Test/test_fire.json"));
            Thesaurus thesaurus = new Thesaurus();
            foreach(string file in Directory.EnumerateFiles("../StoryLib/Test/WordExtensions/"))
            {
                thesaurus.addWord(WordExtensionParser.parse(System.IO.File.ReadAllText(file)));
            }

            
            Console.WriteLine(plotPoint.generatePlotText(thesaurus));
        }
    }
}
