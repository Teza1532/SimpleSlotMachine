namespace SimpleSlowMachine.DTOs
{
    public class GambleDTO
    {
        public Guid UserGuid { get; set; }

        public IList<SymbolDTO> Symbols { get; set; }

        public decimal Winnings { get; set; }

        public decimal Stake { get; set; }

        public GambleDTO(Guid UserGuid, List<SymbolDTO> Symbols, decimal Winnings, decimal Stake)
        {
            this.UserGuid = UserGuid;
            this.Symbols = Symbols;
            this.Winnings = Winnings;
            this.Stake = Stake;
        }
    }
}
