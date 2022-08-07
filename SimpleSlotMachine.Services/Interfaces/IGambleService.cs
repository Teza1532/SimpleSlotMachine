using SimpleSlotMachine.Common;
using SimpleSlowMachine.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSlotMachine.Services.Interfaces
{
    public interface IGambleService
    {
        public GambleDTO Spin(Guid UserGuid, decimal Stake);
    }
}
