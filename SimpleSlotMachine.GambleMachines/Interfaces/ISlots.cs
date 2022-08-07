using SimpleSlotMachine.Common.Enums;
using SimpleSlotMachine.GambleMachines.Classes;
using SimpleSlotMachine.Models;

namespace SimpleSlotMachine.GambleMachines.Interfaces
{
    public interface ISlots
    {
        public SpinResults CalculateWinnings(decimal stake);

        public void Spin(IList<SymbolModel> symbolModel);
    }
}
