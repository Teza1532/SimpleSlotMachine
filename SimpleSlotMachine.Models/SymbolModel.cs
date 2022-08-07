using SimpleSlotMachine.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSlotMachine.Models
{
    public class SymbolModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Symbol Id { get; set; }
        public string Icon { get; set; }
        public decimal Coefficient { get; set; }
        public decimal Probability { get; set; }
    }
}
