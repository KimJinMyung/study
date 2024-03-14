using MazeTRPG.Battle;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MazeTRPG.Maze
{
    enum Tile_Type
    {
        Road,
        Wall,
        Player,
        Monster,
        Exit
    }

    internal class Map
    {
        const char Check = 'ㅁ';
        protected int size;
        protected Tile_Type[,] tile;
        protected Random random;
        protected Exit exit;
        protected Player player;
        protected List<Monsters> monsters;

        protected Direction Direction;

        public Map(int inputSize) 
        {
            if (inputSize %2 ==0)
            {
                Console.WriteLine("맵의 크기는 홀수로 입력해야 한다.");
                return;
            }
            this.size = inputSize;
            random = new Random();
            tile = new Tile_Type[size, size];     
            monsters = new List<Monsters>();
        }

        public void InitMaze()
        {
            for (int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if (i % 2 == 0 || j % 2 == 0)
                    {
                        tile[i, j] = Tile_Type.Wall;
                    }
                    else
                        tile[i, j] = Tile_Type.Road;
                }
            }

           for(int i =0; i< size; i++)
            {
                int count = 1;

                for(int j = 0; j<size; j++)
                {
                    if (i % 2 == 0 || j % 2 == 0) continue;
                    if (i == size - 2 && j == size - 2) continue;

                    if (random.Next(2) == 0)
                    {
                        if( j == size - 2)
                        {
                            tile[i+1,j] = Tile_Type.Road;
                            continue;
                        }

                        tile[i, j + 1] = Tile_Type.Road;
                        count++;
                    }
                    else
                    {
                        if ( i==size - 2)
                        {
                            tile[i,j+1] = Tile_Type.Road;
                            continue;
                        }

                        int index = random.Next(0, count);

                        tile[i + 1, j - (index * 2)] = Tile_Type.Road;
                        count = 1;
                    }
                }
            }

            exit = new Exit(tile, size);
            player = new Player(tile, size);
            player.Info("야만 전사", 500, 30, 5);
            

            for (int i = 0; i < random.Next(3,5); i++)
            {
                Monsters monster = new Monsters();
                monster.SpawnPosition(tile,size);
                monster.Info("슬라임", 100,10,5);
                monsters.Add(monster);
            }           
            

        }
        
        public void Rander()
        {
            ConsoleColor MyColor = Console.ForegroundColor;
            for(int i = 0;i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = GetTileColor(tile[i,j]);
                    Console.Write(Check);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor= MyColor;
        }

        ConsoleColor GetTileColor(Tile_Type tile)
        {
            switch (tile)
            {
                case Tile_Type.Road:
                    return ConsoleColor.Black;
                case Tile_Type.Wall:
                    return ConsoleColor.White;
                case Tile_Type.Player:
                    return ConsoleColor.Blue;
                case Tile_Type.Exit:
                    return ConsoleColor.Green;
                case Tile_Type.Monster:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.White;
            }
        }

        //public bool MeetMonster()
        //{           
        //    int x = Monster.GetPositionX();
        //    int y = Monster.GetPositionY();
        //    if (x==player.GetPlayerPositionX() && y == player.GetPlayerPositionY())
        //    {
        //        return true;
        //    }else return false;
        //}

        public bool MazeExit()
        {
            int x = player.GetPlayerPositionX();
            int y = player.GetPlayerPositionY();

            if (x == exit.GetPositionX && y == exit.GetPositionY) return true;
            else return false;
        }

        public bool MovePlayer(Direction direction)
        {
            int x = player.GetPlayerPositionX();
            int y = player.GetPlayerPositionY();

            switch (direction)
            {
                case Direction.Left:
                    y--;
                    break;
                case Direction.Right:
                    y++;
                    break;
                case Direction.Up:
                    x--;
                    break;
                case Direction.Down:
                    x++;
                    break;
            }            

            bool insideMap = x >= 0 && x < size && y >= 0 && y < size;
            bool notWall = tile[x, y] != Tile_Type.Wall;

            //몬스터 만남
            if (tile[x, y] == Tile_Type.Monster)
            {
                Console.WriteLine("몬스터 만남");
                Thread.Sleep(2000);
                Console.Clear();
                int monsterIndex = random.Next(monsters.Count);
                Battle.Battle battle = new Battle.Battle(player, monsters[monsterIndex]);
                bool Win = battle.BattlePlayertoMonster();
                if (Win)
                {
                    tile[player.GetPlayerPositionX(), player.GetPlayerPositionY()] = Tile_Type.Road;
                    tile[x, y] = Tile_Type.Player;
                    player.SetPlayerPosition(x, y);
                    monsters.RemoveAt(monsterIndex);
                    return true;
                }else return false;
            }

            //미로 안에 있거나 지정한 좌표가 벽이 아니면 이동
            if (insideMap && notWall)
            {
                tile[player.GetPlayerPositionX(), player.GetPlayerPositionY()] = Tile_Type.Road;
                tile[x, y] = Tile_Type.Player;
                player.SetPlayerPosition(x, y);
                return true;
            }
            return false;
        }

        public bool InputKey(ConsoleKeyInfo inputkey)
        {
            this.Direction = GetDirection(inputkey.Key);

            if (Direction != Direction.None )
            {
                bool move = MovePlayer(Direction);
                if (!move) Console.WriteLine("이동 불가...");                               
            }
            return false;
        }

        

        static Direction GetDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.RightArrow:
                    return Direction.Right;
                case ConsoleKey.LeftArrow:
                    return Direction.Left;
                case ConsoleKey.UpArrow:
                    return Direction.Up;
                case ConsoleKey.DownArrow:
                    return Direction.Down;
                default:
                    return Direction.None;
            }
        }
    }
}
