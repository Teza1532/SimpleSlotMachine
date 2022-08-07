using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleSlotMachine.Database;
using SimpleSlotMachine.GambleMachines.Classes;
using SimpleSlotMachine.GambleMachines.Interfaces;
using SimpleSlotMachine.Repositories;
using SimpleSlotMachine.Repositories.Interfaces;
using SimpleSlotMachine.Services.Classes;
using SimpleSlotMachine.Services.Interfaces;
using SimpleSlotMachine;
using Microsoft.Extensions.Logging;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
        .AddTransient<IGambleService, GambleService>()
        .AddTransient<IUserService, UserService>()
        .AddTransient<ISlots, SpinMachine>()
        .AddTransient<IUnitOfWork, UnitOfWork>()
        .AddHostedService<SlotWorker>()
        .AddDbContext<SimpleSlotMachineContext>(options =>
            options.UseInMemoryDatabase(databaseName: "SlotsDB")
        ))
        .ConfigureLogging((hostingContext, logging) => logging.SetMinimumLevel(LogLevel.Error))
        .Build();

host.Run();