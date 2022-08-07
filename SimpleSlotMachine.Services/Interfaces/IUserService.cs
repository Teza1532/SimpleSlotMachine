using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSlotMachine.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Adds a User to the datastore
        /// </summary>
        /// <param name="Funds">The funds the user initially deposits</param>
        /// <returns>The Id generated to identify the user</returns>
        public Guid AddUser(decimal Funds);

        ///// <summary>
        ///// Gets the amount of funds that the user has left
        ///// </summary>
        ///// <param name="UserGuid">The user to find out their winnings</param>
        ///// <returns>The amount of funds a user has left</returns>
        public decimal GetFunds(Guid UserGuid);

        ///// <summary>
        ///// Gets a users Current winnings
        ///// </summary>
        ///// <param name="UserGuid">The users guid that you want to get the winnings for</param>
        ///// <returns>The users current winnings</returns>
        public decimal GetWinnings(Guid UserGuid);
    }
}
