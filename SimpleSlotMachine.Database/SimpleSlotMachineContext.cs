using Microsoft.EntityFrameworkCore;
using SimpleSlotMachine.Common.Enums;
using SimpleSlotMachine.Models;

namespace SimpleSlotMachine.Database
{
    public class SimpleSlotMachineContext : DbContext
    {
        protected SimpleSlotMachineContext(DbContextOptions options) 
            : base(options) { }

        public SimpleSlotMachineContext(DbContextOptions<SimpleSlotMachineContext> options) 
            : this((DbContextOptions)options)
        {
        }

        protected SimpleSlotMachineContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SymbolModel>().HasData(
                new SymbolModel { Id = Symbol.Apple, Icon = "A", Coefficient = 0.4m, Probability = 45 },
                new SymbolModel { Id = Symbol.Banana, Icon = "B", Coefficient = 0.6m, Probability = 35 },
                new SymbolModel { Id = Symbol.Pineapple, Icon = "P", Coefficient = 0.8m, Probability = 15 },
                new SymbolModel { Id = Symbol.WildCard, Icon = "*", Coefficient = 0m, Probability = 5 }
            );
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<UserModel>? Users { get; set; }
        public virtual DbSet<GameModel>? Games { get; set; }
        public virtual DbSet<SymbolModel>? Symbols { get; set; }
    }
}