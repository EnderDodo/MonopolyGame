using System;

namespace MonopolyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Player[] players = new Player[3];
            players[0] = new Player("Антон", 1000, 200);
            players[1] = new Player("Борис", 1000, 200);
            players[2] = new Player("Владд", 1000, 200);

            Cell[] field = new Cell[9];
            field[0] = new RentCell("ул. Хыхышная", 600, 200);
            field[1] = new TaxCell("Сретенский Бульвар", 100);
            field[2] = new RentCell("ул. Нижнепотамская", 700, 300);
            field[3] = new RentCell("ул. Православная", 800, 400);
            field[4] = new RentCell("ул. Квадратичная", 250, 90);
            field[5] = new RentCell("ул. Триднестровская", 600, 250);
            field[6] = new TaxCell("ул. Обсужная", 300);
            field[7] = new RentCell("ул. Валерия Меладзе", 10000, 2000);
            field[8] = new RentCell("ул. Маршала Жукова", 300, 150);

            GameManager heehee = new GameManager(field, players);
            
        }
    }
}
