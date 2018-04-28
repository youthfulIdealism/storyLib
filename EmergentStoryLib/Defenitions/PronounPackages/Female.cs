using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions.PronounPackages
{
    /**
     * Default pronoun package for a female party member
     * */
    public class Female : PronounPackage
    {
        /**
         * he/she
         * */
        public override string subjective { get; protected set; } = "she";

        /**
         * him/her
         * */
        public override string objective { get; protected set; } = "her";

        /**
         *  his/hers
         * */
        public override string posessive { get; protected set; } = "hers";

        /**
         *  his/her
         * */
        public override string posessive_odd { get; protected set; } = "her";

        /**
         *  himself/herself
         * */
        public override string reflexive { get; protected set; } = "herself";
    }
}
