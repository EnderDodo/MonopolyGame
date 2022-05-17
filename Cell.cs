using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame
{
    abstract class Cell
    {
        public string Name { get; set; }


        public Cell(string name)
        {
            Name = name;
        }

        abstract public void Action(Player player);
    }
    class TaxCell : Cell
    {
        public int Tax { get; set; }
        public TaxCell(string name, int tax) : base(name)
        {
            Tax = tax;
        }
        public override void Action(Player player)
        {
            player.Money -= Tax;
            Console.WriteLine($"Игрок {player.Name} заплатил {Tax} денег налогами.");
        }
    }
    class RentCell : Cell
    {
        public int Price { get; set; }
        public virtual int Rent { get; private set; }
        
        /*public int Multiplier { get; set; }*/
        public Player Owner { get; set; }
        public RentCell(string name, int price, int rent) : base(name)
        {
            Price = price;
            Rent = rent;
        }
        public override void Action(Player player)
        {
            if (Owner == null)
            {
                if (Price * 3 <= player.Money)
                {
                    player.Money -= Price;
                    Owner = player;
                    player.Property.AddLast(this);
                    Console.WriteLine($"Игрок {player.Name} купил {Name} за {Price} денег.");
                }
            }

            else if (player != Owner)
            {
                player.Money -= Rent;
                Owner.Money += Rent;
                Console.WriteLine($"Игрок {player.Name} заплатил игроку {Owner.Name} {Rent} денег в качестве ренты.");
            }
        }
        virtual public RentCell Upgrade(Player player)
        {
            return new OneHouseCell(this);
        }

    }
    abstract class CellDecorator : RentCell
    {
        protected RentCell cell;
        public CellDecorator(RentCell cell, string name, int price, int rent) : base(name, price, rent)
        {
            this.cell = cell;
        }
    }
    class OneHouseCell : CellDecorator
    {
        public OneHouseCell(RentCell cell) : base(cell, cell.Name + " с 1 домом", cell.Price, cell.Rent)
        {
        }
        const int MULTIPLIER = 2;

        public override int Rent
        {
            get { return MULTIPLIER * base.Rent; }
        }
        public override RentCell Upgrade(Player player)
        {
            return new TwoHouseCell(cell);
        }
    }

    class TwoHouseCell : CellDecorator
    {
        public TwoHouseCell(RentCell cell) : base(cell, cell.Name + " с 2 домами", cell.Price, cell.Rent)
        {
        }
        const int MULTIPLIER = 3;

        public override int Rent
        {
            get { return MULTIPLIER * base.Rent; }
        }
        public override RentCell Upgrade(Player player)
        {
            return new ThreeHouseCell(cell);
        }
    }

    class ThreeHouseCell : CellDecorator
    {
        public ThreeHouseCell(RentCell cell) : base(cell, cell.Name + " с 3 домами", cell.Price, cell.Rent)
        {
            Price *= 3;
        }
        const int MULTIPLIER = 4;

        public override int Rent
        {
            get { return MULTIPLIER * base.Rent; }
        }
        public override RentCell Upgrade(Player player)
        {
            player.LoopPayment += this.Price / 6;
            return new SkyScraperCell(cell);
        }
    }
    class SkyScraperCell : CellDecorator
    {
        public SkyScraperCell(RentCell cell) : base(cell, cell.Name + " с небоскребом", cell.Price, cell.Rent)
        {
        }
        const int MULTIPLIER = 8;

        public override int Rent
        {
            get { return MULTIPLIER * base.Rent; }
        }
        public override RentCell Upgrade(Player player)
        {
            return null;
        }
    }
}
