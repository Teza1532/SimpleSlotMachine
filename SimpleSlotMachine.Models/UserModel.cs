using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSlotMachine.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }   
        public decimal Funds { get; set; }

        public virtual ICollection<GameModel>? Games { get; set; }
    }
}
