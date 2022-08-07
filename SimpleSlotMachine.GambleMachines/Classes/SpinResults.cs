using SimpleSlotMachine.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSlotMachine.GambleMachines.Classes
{
    public class SpinResults
    {
        public SpinResults()
        {
            Symbols = new List<Symbol>();
        }

        public IList<Symbol> Symbols { get; set; }

        public decimal Winnings { get; set; }

        public decimal SumOfCoefficients { get; set; }
    }
}
