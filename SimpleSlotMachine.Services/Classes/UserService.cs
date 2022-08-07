using SimpleSlotMachine.Models;
using SimpleSlotMachine.Repositories.Interfaces;
using SimpleSlotMachine.Services.Interfaces;

namespace SimpleSlotMachine.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public Guid AddUser(decimal Funds)
        {
            if (Funds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Funds));
            }

            UserModel user = new UserModel
            {
                Funds = Funds
            };

            _unitOfWork.UserRepository.Insert(user);

            _unitOfWork.Save();

            return user.Id;
        }

        public decimal GetFunds(Guid UserGuid)
        {
            var user = _unitOfWork.UserRepository.GetByID(UserGuid);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Funds;
        }

        public decimal GetWinnings(Guid UserGuid)
        {
            var games = _unitOfWork.GameRepository.Get(filter: g => g.User.Id == UserGuid);
            
            if (games == null)
            {
                return 0;
            }

            return games.Sum(g => g.Winnings);
        }
    }
}
