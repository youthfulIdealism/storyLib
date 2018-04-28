using EmergentStoryLib.Parser;
using EmergentStoryLib.Parser.Lexer;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions
{
    /**
     * Loader and cache for plot point factories.
     * */
    public static class PlotPointRegistrar
    {
        /**
         * Path that the plot points should be loaded from.
         * */
        public static string basePath { get; set; }

        /**
         * File extension for plot points.
         * */
        private static string extension = ".txt";

        /**
         * Plot factory cache
         * */
        private static Dictionary<string, PlotPointFactory> plotPointMap;

        static PlotPointRegistrar()
        {
            plotPointMap = new Dictionary<string, PlotPointFactory>();
        }

        /**
         * Returns a plot point factory.
         * */
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
