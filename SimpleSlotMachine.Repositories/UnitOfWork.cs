using SimpleSlotMachine.Database;
using SimpleSlotMachine.Models;
using SimpleSlotMachine.Repositories.Classes;
using SimpleSlotMachine.Repositories.Interfaces;

namespace SimpleSlotMachine.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private SimpleSlotMachineContext context;

        private BaseRepository<UserModel>? _userRepository;

        private BaseRepository<GameModel>? _gameRepository;

        private BaseRepository<SymbolModel>? _symbolRepository;

        /// <summary>
        /// Constructor for the unit of work class
        /// </summary>
        /// <param name="Context">The Database Context</param>
        public UnitOfWork(SimpleSlotMachineContext Context)
        {
            context = Context;
            context.Database.EnsureCreated();
        }

        public BaseRepository<UserModel> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new BaseRepository<UserModel>(context);
                }
                return _userRepository;
            }
        }

        public BaseRepository<GameModel> GameRepository
        {
            get
            {
                if (this._gameRepository == null)
                {
                    this._gameRepository = new BaseRepository<GameModel>(context);
                }
                return _gameRepository;
            }
        }

        public BaseRepository<SymbolModel> SymbolRepository
        {
            get
            {
                if (this._symbolRepository == null)
                {
                    this._symbolRepository = new BaseRepository<SymbolModel>(context);
                }
                return _symbolRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
