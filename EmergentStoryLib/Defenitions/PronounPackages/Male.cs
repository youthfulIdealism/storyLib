using System;
using System.Collections.Generic;
using System.Text;

namespace EmergentStoryLib.Defenitions.PronounPackages
{
    public class Male : PronounPackage
    {
        /**
         * he/she
         * */
        public override string subjective { get; protected set; } = "he";

        /**
         * him/her
         * */
        public override string objective { get; protected set; } = "him";

        /**
         *  his/hers
         * */
        public override string posessive { get; protected set; } = "his";

        /**
         *  his/her
         * */
        public override string posessive_odd { get; protected set; } = "his";

        /**
         *  himself/herself
         * */
        public override string reflexive { get; protected set; } = "himself";
    }
}
