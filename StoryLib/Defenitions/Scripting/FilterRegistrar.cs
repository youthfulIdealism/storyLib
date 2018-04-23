﻿using StoryLib.Active;
using StoryLib.Defenitions.Filters;
using StoryLib.Defenitions.Filters.PartyMemberFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions.Scripting
{
    public class FilterRegistrar
    {
        private static Dictionary<String, Type> partyFilterMap;
        private static Dictionary<Type, String> reversePartyFilterMap;

        private static Dictionary<String, Type> contextFilterMap;
        private static Dictionary<Type, String> reverseContextFilterMap;

        static FilterRegistrar()
        {
            partyFilterMap = new Dictionary<string, Type>();
            reversePartyFilterMap = new Dictionary<Type, string>();

            contextFilterMap = new Dictionary<string, Type>();
            reverseContextFilterMap = new Dictionary<Type, string>();
        }

        public static void buildDefaultLanguage()
        {
            addPartyFilter(typeof(TagFilter), "tag");

            addContextFilter(typeof(IsPersonDeclaredFilter), "person_is_declared");
        }

        private static void addPartyFilter(Type filter, String str)
        {
            if(!typeof(Filter<PartyMember>).IsAssignableFrom(filter))
            {
                throw new Exception("Incorrect type in addPartyFilter for " + str);
            }

            partyFilterMap.Add(str, filter);
            reversePartyFilterMap.Add(filter, str);
        }

        private static void addContextFilter(Type filter, String str)
        {
            if (!typeof(Filter<PlotContext>).IsAssignableFrom(filter))
            {
                throw new Exception("Incorrect type in addContextFilter for " + str);
            }

            contextFilterMap.Add(str, filter);
            reverseContextFilterMap.Add(filter, str);
        }

        public static Filter<PlotContext> getContextFilter(string key, string[] args)
        {
            if(!contextFilterMap.ContainsKey(key))
            {
                throw new Exception("Filter type " + key + " not found.");
            }
            return (Filter<PlotContext>)Activator.CreateInstance(contextFilterMap[key], new object[] { args });
        }

        public static Filter<PartyMember> getPartyFilter(string key, string[] args)
        {
            if (!partyFilterMap.ContainsKey(key))
            {
                throw new Exception("Filter type " + key + " not found.");
            }
            return (Filter<PartyMember>)Activator.CreateInstance(partyFilterMap[key], new object[] { args });
        }
    }
}