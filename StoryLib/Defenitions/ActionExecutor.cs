using System;
using System.Collections.Generic;
using System.Text;

namespace StoryLib.Defenitions
{
    public class ActionExecutor
    {
        public Action action;
        public object arg;

        public ActionExecutor(Action action, object arg)
        {
            this.action = action;
            this.arg = arg;
        }
    }
    
}
