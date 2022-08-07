using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleSlotMachine.Common.Exceptions;
using SimpleSlotMachine.Services.Interfaces;

namespace SimpleSlotMachine
{
    internal class SlotWorker : IHostedService
    {
        private readonly IGambleService _gambleService;
        private readonly IUserService _userService;
        private readonly ILogger<SlotWorker> _logger;

        public SlotWorker(IGambleService GambleService, IUserService UserService, ILogger<SlotWorker> Logger)
        {
            _gambleService = GambleService;
            _logger = Logger;
            _userService = UserService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            DoTask();

            return Task.CompletedTask;
        }

        private void DoTask()
        {
            bool gotFundsLeft = true;

            _logger.LogInformation("Slot worker started");

            Console.WriteLine("Welcome to the Terrys Simple Slot Game");
            Console.WriteLine("Please enter the amount you would like to play. \nThe Max amount that can be added is £1000000");

            Guid userGuid = AddUserFunds();

            while (gotFundsLeft)
            {
                try
                {
                    Console.WriteLine("Please enter a stake for a bet");
                    var stakeAmount = Console.ReadLine();

                    _logger.LogInformation($"User has entered a stake of £{stakeAmount:0.00}");

                    if (!decimal.TryParse(stakeAmount, out decimal stake))
                    {
                        throw new InvalidDataException();
                    }

                    var gamble = _gambleService.Spin(userGuid, stake);

                    foreach (var symbol in gamble.Symbols)
                    {
                        Console.Write(symbol.Icon);
                    }

                    Console.WriteLine();

                    if (gamble.Winnings > 0)
                    {
                        _logger.LogInformation($"The customer won: £{gamble.Winnings:0.00}");
                        Console.WriteLine($"Congratulations you have won £{gamble.Winnings:0.00}");
                    }
                    else
                    {
                        _logger.LogInformation($"The customer didn't win this time");
                        Console.WriteLine($"I'm afraid you haven't won this time better look next time");
                    }

                    var userFunds = _userService.GetFunds(userGuid);

                    Console.WriteLine($"Your overall winnings are £{_userService.GetWinnings(userGuid):0.00}");
                    Console.WriteLine($"Your funds are now £{userFunds:0.00}");

                    if (userFunds <= 0)
                    {
                        Console.WriteLine("I'm afraid you have no funds left, thanks for playing");
                        gotFundsLeft = false;
                    }
                }
                catch (InvalidDataException)
                {
                    _logger.LogWarning("Invalid stake was entered by user");
                    Console.WriteLine("The Stake you entered is not a valid amount");
                }
                catch (ArgumentOutOfRangeException )
                {
                    _logger.LogWarning("OutOfRange amount entered in the stake");
                    Console.WriteLine("Invalid stake entered try again");
                }
                catch (StakeIsHigherThanFundsException sihtfe)
                {
                    _logger.LogWarning($"User entered stake £{sihtfe.Stake} but only had £{sihtfe.Funds} funds left");
                    Console.WriteLine("The Stake you entered is more than your remaining funds");
                }
                catch (MissingMemberException mme)
                {
                    _logger.LogCritical($"No {mme.Message} where found for the game");
                    Console.WriteLine("Something went wrong with the game please contact support");

                    throw;
                }
                catch (NoBetMadeToWorkWinningsException)
                {
                    _logger.LogCritical($"The game tried to work out the winnings before the game had been played");
                    Console.WriteLine("Something went wrong with the game please contact support");

                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong with the game please contact support");
                    _logger.LogCritical($"Something unexplainable went wrong: \n {ex}");

                    throw;
                }
            }
        }

        private Guid AddUserFunds()
        {
            while (true)
            {
                _logger.LogDebug("Getting user to enter funds");

                var fundsRead = Console.ReadLine();

                _logger.LogInformation($"User entered funds: £{fundsRead:0.00}");

                if (decimal.TryParse(fundsRead, out decimal funds))
                {
                    try
                    {
                        if (funds > 1000000)
                        {
                            throw new ArgumentOutOfRangeException();
                        }

                        var userGuid = _userService.AddUser(funds);

                        Console.WriteLine($"Thankyou, £{funds:0.00} have now been added to your account.");

                        return userGuid;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        //Swallow out of range and ask for new amount
                    }
                }

                _logger.LogWarning("User entered an invalid funds amount");

                Console.WriteLine($"I'm sorry but £{fundsRead:0.00} is not a valid amount please enter a valid amount between 0 and 1000000");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
