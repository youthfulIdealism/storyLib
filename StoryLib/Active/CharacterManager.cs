using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Active
{
    public static class CharacterManager
    {
        private static long current = long.MinValue;
        private static Dictionary<long, PartyMember> characterList;
        static CharacterManager()
        {
            characterList = new Dictionary<long, PartyMember>();
        }
    }
}
