using SimpleSlotMachine.Models;
using SimpleSlotMachine.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSlotMachine.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public BaseRepository<UserModel> UserRepository { get; }
        public BaseRepository<GameModel> GameRepository { get; }
        public BaseRepository<SymbolModel> SymbolRepository { get; }

        public void Save();
    }
}
