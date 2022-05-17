using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame
{
    
    class Player
    {
        

        public string Name { get; set; }
        public int Money { get; set; }
        public int Position { get; set; }
        public bool IsInJail { get; set; }
        public bool IsWinner { get; set; }
        public int LoopPayment { get; set; }
        public bool IsLoser { get; set; }
        public LinkedList<RentCell> Property { get; set; }
        
        public Player(string name, int money, int payment)
        {
            Money = money;
            Name = name;
            LoopPayment = payment;
            Property = new LinkedList<RentCell>();
        }
        
    }
}
