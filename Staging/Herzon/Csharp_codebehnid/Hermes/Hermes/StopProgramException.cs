using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes
{
    class StopProgramException : Exception
    {
        public override String ToString()
        {
            // TODO:  Flesh out
            return "Shutdown exception";
        }
    }
}
