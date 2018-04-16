using StoryLib.Parser;
using StoryLib.Parser.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public static class PlotPointRegistrar
    {
        public static string basePath;
        private static string extension = ".txt";
        private static Dictionary<string, PlotPointFactory> plotPointMap;

        static PlotPointRegistrar()
        {
            plotPointMap = new Dictionary<string, PlotPointFactory>();
        }

        public static PlotPointFactory GetPlotPointFactory(string key)
        {
            if(!plotPointMap.ContainsKey(key))
            {
                plotPointMap.Add(key, new PlotFactoryParser().parse(new Lexer().lex(System.IO.File.ReadAllText(basePath + key + extension))));
            }
            return plotPointMap[key];
        }
    }
}
