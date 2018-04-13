using StoryLib.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public static class PlotPointRegistrar
    {
        public static string basePath;
        private static string extension = ".json";
        private static Dictionary<string, PlotPointFactory> plotPointMap;

        static PlotPointRegistrar()
        {
            plotPointMap = new Dictionary<string, PlotPointFactory>();
        }

        public static PlotPointFactory GetPlotPointFactory(string key)
        {
            if(!plotPointMap.ContainsKey(key))
            {
                plotPointMap.Add(key, PlotParser.parse(System.IO.File.ReadAllText(basePath + key + extension)));
            }
            return plotPointMap[key];
        }
    }
}
