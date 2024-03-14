using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeTRPG.Maze
{
    internal class Exit
    {
        protected int x;
        protected int y;

        public int GetPositionX { get { return x; } }
        public int GetPositionY { get { return y; } }

        Random Random = new Random();
        public Exit(Tile_Type[,] tiles, int mapSize)
        {
            while (true)
            {
                x = Random.Next(0, mapSize);
                y = Random.Next(0, mapSize);

                if (tiles[x, y] == Tile_Type.Wall) continue;
                if (tiles[x,y]==Tile_Type.Player) continue;
                if (tiles[x,y]==Tile_Type.Monster) continue;
                tiles[x,y]=Tile_Type.Exit;
                break;
            }           
        }
    }
}
