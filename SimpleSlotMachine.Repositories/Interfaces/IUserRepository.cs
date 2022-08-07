using SimpleSlotMachine.Models;
using SimpleSlotMachine.Repositories.Classes;

namespace SimpleSlotMachine.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public UserModel GetUserByGuid(Guid UserGuid);

        public void UpdateUser(UserModel User);

        public void Commit();
    }
}
