using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions.PronounPackages
{
    public abstract class PronounPackage
    {
        public Dictionary<string, string> variableAssociations;

        public PronounPackage()
        {
            variableAssociations = new Dictionary<string, string>();
            variableAssociations.Add("SUBJECTIVE", subjective);
            variableAssociations.Add("OBJECTIVE", objective);
            variableAssociations.Add("POSESSIVE", posessive);
            variableAssociations.Add("POSESSIVE_ODD", posessive_odd);
            variableAssociations.Add("REFLEXIVE", reflexive);
            variableAssociations.Add("HESHE", subjective);
            variableAssociations.Add("HIMHER", objective);
            variableAssociations.Add("HISHERS", posessive);
            variableAssociations.Add("HISHER", posessive_odd);
            variableAssociations.Add("HIMSELFHERSELF", reflexive);
        }

        /**
         * he/she
         * */
        public abstract string subjective { get; protected set; }

        /**
         * him/her
         * */
        public abstract string objective { get; protected set; }

        /**
         *  his/hers
         * */
        public abstract string posessive { get; protected set; }


        /**
         *  his/her
         * */
        public abstract string posessive_odd { get; protected set; }

        /**
         *  himself/herself
         * */
        public abstract string reflexive { get; protected set; }

        
    }
}
