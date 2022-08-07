using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSlotMachine.Models
{
    public class GameModel
    {
        [Key]
        public int GamesID { get; set; }
        public decimal Stake { get; set; }  
        public decimal Winnings { get; set; }
        public DateTime? TimeStarted { get; set; }
        public DateTime? TimeFinished { get; set; }

        public virtual UserModel? User { get; set; }
    }
}
