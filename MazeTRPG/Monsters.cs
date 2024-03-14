using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeTRPG.Maze;

namespace MazeTRPG
{
    internal class Monsters
    {
        protected string name = "";
        protected int currentHP;
        protected int MaxHP;
        protected int ATK;
        protected int ATKPower;
        protected bool IsDead = false;
        protected int GiveEXP;
        public string GetName { get { return name; } }
        public int GetCurrentHP { get { return currentHP; } }
        public int GetGiveEXP { get { return GiveEXP; } }

        public void Info(string name, int MaxHP, int ATK, int EXP)
        {
            this.name = name;
            this.MaxHP = MaxHP;
            this.ATK = ATK;
            this.GiveEXP = EXP;

            InitStatus(this.MaxHP, this.ATK);

            Console.WriteLine($"{this.name}의 등장!!!\n");
        }

        public void InitStatus(int MaxHP, int ATK)
        {
            this.currentHP = MaxHP;
            this.ATKPower = new Random().Next(this.ATK - 2, this.ATK + 2);
        }

        public bool Hurt(int damage)
        {
            this.currentHP -= damage;
            if (currentHP <= 0)
            {
                Console.WriteLine($"{name}이 처치되었습니다.\n");
                Console.WriteLine($"경험치 {GiveEXP}을 획득하였습니다.");
                this.IsDead = true;
                return this.IsDead;
            }
            else
            {
                Console.WriteLine($"{name}이(가) {damage}만큼의 피해를 입었습니다.");
                this.IsDead = false;
                return this.IsDead;
            }
        }

        public bool Attack(Player player)
        {
            bool IsDead = player.Hurt(this.ATKPower);
            return IsDead;
        }

        private int PosX;
        private int PosY;
        Random Random = new Random();

        #region Maze
        public void SpawnPosition(Tile_Type[,] tiles, int mapSize)
        {
            while (true)
            {
                PosX = Random.Next(0, mapSize);
                PosY = Random.Next(0, mapSize);

                if (tiles[PosX, PosY] == Tile_Type.Wall) continue;
                if (tiles[PosX, PosY] == Tile_Type.Exit) continue;
                if (tiles[PosX, PosY] == Tile_Type.Player) continue;

                tiles[PosX, PosY] = Tile_Type.Monster;
                break;
            }
        }

        public int GetPositionX() { return PosX; }
        public int GetPositionY() { return PosY; }

        public void SetPosition(int x, int y)
        {
            PosX = x;
            PosY = y;
        }
        #endregion


    }
}
