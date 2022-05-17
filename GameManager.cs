using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame
{
    class GameManager
    {
        private Cell[] field;
        private LinkedList<Player> players;
        public GameManager(Cell[] field, Player[] players)
        {
            this.field = field;
            this.players = new LinkedList<Player>(players);
            TheGame();
        }

        private void TryToUpgrade(Player player)
        {
            for (LinkedListNode<RentCell> currentPropertyNode = player.Property.First; currentPropertyNode != null; currentPropertyNode = currentPropertyNode.Next)
            {
                if (currentPropertyNode.Value.Price * 2 <= player.Money && currentPropertyNode.Value.Upgrade(player) != null)
                {
                    player.Money -= currentPropertyNode.Value.Price;
                    field[player.Position] = currentPropertyNode.Value.Upgrade(player);
                    currentPropertyNode.Value = currentPropertyNode.Value.Upgrade(player);
                    
                    if (currentPropertyNode.Value.Upgrade(player) != null)
                        Console.WriteLine($"Игрок {player.Name} построил новый дом в {currentPropertyNode.Value.Name}! Рента увеличилась.");
                    else Console.WriteLine($"Игрок {player.Name} построил небоскрёб в {currentPropertyNode.Value.Name}!! Рента значительно увеличилась!");
                    Console.WriteLine($"У игрока {player.Name} осталось {player.Money} денег.");
                }
            }
        }
        private void Turn(Player player)
        {
            int dice = new Random().Next(2, 12);
            player.Position += dice;
            if (player.Position >= field.Length)
            {
                player.Position %= field.Length;
                player.Money += player.LoopPayment;
            }    

            Console.WriteLine("Игрок {0} теперь на клетке {1}", player.Name, field[player.Position].Name);

            field[player.Position].Action(player);

            Console.WriteLine($"У игрока {player.Name} осталось {player.Money} денег.");

            if (player.Property.Count > 0)
                TryToUpgrade(player);

            if (player.Money  < 0 || player.Money > 100000)
                player.IsLoser = true;
        }
        private void TheGame()
        {
            while (players.Count != 1)
            {
                for (LinkedListNode<Player> item = players.First; item != null; item = item.Next)
                {
                    Turn(item.Value);
                    
                    if (item.Value.IsLoser)
                    {
                        Console.WriteLine($"Игрок {item.Value.Name} обанкротился!!");
                        players.Remove(item);
                        Console.ReadKey();
                    }
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine();
                }
            }
            Console.WriteLine($"Игрок {players.First.Value.Name} победил!!!");
        }

    }
}