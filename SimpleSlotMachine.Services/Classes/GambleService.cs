using SimpleSlotMachine.Common.Exceptions;
using SimpleSlotMachine.GambleMachines.Interfaces;
using SimpleSlotMachine.Models;
using SimpleSlotMachine.Repositories.Interfaces;
using SimpleSlotMachine.Services.Interfaces;
using SimpleSlowMachine.DTOs;

namespace SimpleSlotMachine.Services.Classes
{
    public class GambleService : IGambleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISlots _slotsMachine;

        public GambleService(IUnitOfWork UnitOfWork, ISlots slotsMachine)
        {
            _unitOfWork = UnitOfWork;
            _slotsMachine = slotsMachine;
        }

        public GambleDTO Spin(Guid UserGuid, decimal Stake)
        {
            var user = _unitOfWork.UserRepository.GetByID(UserGuid);

            GuardChecks(user, Stake);

            GameModel game = StartGameForUser(user, Stake);

            //Get Symbols
            var symbols = _unitOfWork.SymbolRepository.Get().ToList();

            if (symbols is null || symbols.Count() < 1)
            {
                throw new MissingMemberException(nameof(symbols));
            }

            //Do spins
            _slotsMachine.Spin(symbols);

            //Work out winnings
            var results = _slotsMachine.CalculateWinnings(Stake);

            //Add winnings to User
            user.Funds += results.Winnings;
            _unitOfWork.UserRepository.Update(user);

            //Endgame
            game.Winnings = results.Winnings;
            game.TimeFinished = DateTime.UtcNow;
            _unitOfWork.GameRepository.Update(game);

            _unitOfWork.Save();

            //Make and return GambleDTO
            List<SymbolDTO> symbolDtos = new List<SymbolDTO>();
            foreach (var symbol in results.Symbols)
            {
                symbolDtos.Add(new SymbolDTO
                {
                    Symbol = symbol,
                    Icon = symbols.Where(s => s.Id == symbol).Select(s => s.Icon).First()
                });
            }

            var gambleInfo = new GambleDTO
            (
                Stake: Stake,
                UserGuid: UserGuid,
                Winnings: game.Winnings,
                Symbols: symbolDtos
            );

            return gambleInfo;
        }

        private void GuardChecks(UserModel user, decimal Stake)
        {
            //Check Stake isn't less than 0
            if (Stake < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Stake));
            }

            //Check stake isn't more than funds
            if (user.Funds < Stake)
            {
                throw new StakeIsHigherThanFundsException(user.Funds, Stake);
            }
        }

        /// <summary>
        /// Creates a new game for a user with their stake
        /// </summary>
        /// <param name="User">The user the game is for</param>
        /// <param name="Stake">The amount to stake on the game</param>
        /// <returns>The new Game</returns>
        private GameModel StartGameForUser(UserModel User, decimal Stake)
        {
            //Create new game
            GameModel game = new GameModel
            {
                Stake = Stake,
                User = User,
                TimeStarted = DateTime.UtcNow
            };

            _unitOfWork.GameRepository.Insert(game);

            _unitOfWork.Save();

            User.Funds = User.Funds - Stake;

            _unitOfWork.UserRepository.Update(User);

            _unitOfWork.Save();

            return game;
        }
    }
}
