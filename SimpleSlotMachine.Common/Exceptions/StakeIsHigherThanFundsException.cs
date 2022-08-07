using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSlotMachine.Common.Exceptions
{
    public class StakeIsHigherThanFundsException : Exception
    {
        public decimal Funds { get; }
        public decimal Stake { get; }

        public override string Message { get; }

        public StakeIsHigherThanFundsException(decimal Funds, decimal Stake)
        {
            this.Funds = Funds;
            this.Stake = Stake;
            this.Message = $"Stake {Stake} is higher than Funds {Funds} and therefore a bet cannot be made";
        }

        public StakeIsHigherThanFundsException(string message, decimal Funds, decimal Stake) : base(message)
        {
            this.Funds = Funds;
            this.Stake = Stake;
            Message = message;
        }
    }
}
