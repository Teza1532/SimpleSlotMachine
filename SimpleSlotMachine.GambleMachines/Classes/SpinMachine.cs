using SimpleSlotMachine.Common.Enums;
using SimpleSlotMachine.Common.Exceptions;
using SimpleSlotMachine.GambleMachines.Interfaces;
using SimpleSlotMachine.Models;

namespace SimpleSlotMachine.GambleMachines.Classes
{
    public class SpinMachine : ISlots
    {
        private SpinResults? results;

        public SpinResults CalculateWinnings(decimal stake)
        {
            if (results == null || results.Symbols.Count < 3)
            {
                throw new NoBetMadeToWorkWinningsException();
            }

            if (results.Symbols.Where(s => s != Symbol.WildCard).Distinct().Count() == 1)
            {
                results.Winnings = Math.Round(stake * results.SumOfCoefficients, 2);
            }

            return results;
        }

        public void Spin(IList<SymbolModel> symbolModel)
        {
            results = new SpinResults();

            Dictionary<Symbol, decimal> SymbolProbability = symbolModel.ToDictionary(sm => sm.Id, sm => sm.Probability);

            //Get universal probability
            decimal universalProb = SymbolProbability.Sum(sp => sp.Value);

            for (int spinNumber = 0; spinNumber < 3; spinNumber++)
            {
                decimal randomNumber = (decimal)new Random().NextDouble() * universalProb;

                decimal sum = 0;

                foreach (var symbol in SymbolProbability)
                {
                    // loop until the random number is less than our cumulative probability
                    if (randomNumber <= (sum = sum + symbol.Value))
                    {
                        results.Symbols.Add(symbol.Key);
                        results.SumOfCoefficients += symbolModel.Where(sm => sm.Id == symbol.Key).Select(sm => sm.Coefficient).FirstOrDefault();
                        break;
                    }
                }
            }
        }
    }
}
