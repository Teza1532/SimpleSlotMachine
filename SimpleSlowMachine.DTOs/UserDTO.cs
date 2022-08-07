using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSlowMachine.DTOs
{
    public class UserDTO
    {
        public UserDTO(decimal funds)
        {
            GUID = new Guid();
            Funds = funds;
            ReservedFunds = 0;
        }

        public Guid GUID { get; set; }

        public decimal Funds { get; set; }

        public decimal ReservedFunds { get; set; }

        public decimal CurrentWinnings { get; set; }
    }
}
