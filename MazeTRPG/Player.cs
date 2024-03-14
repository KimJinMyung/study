using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeTRPG.Item;
using MazeTRPG.Maze;

enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right
}

namespace MazeTRPG
{
    enum Player_Job
    {
        None,
        Warrior,
        Archer,
        Mage
    }

    enum PlayerAction
    {
        Attack,
        UseItem,
        Run
    }

    internal partial class Player
    {
        protected string name = "";
        protected int currentHP;
        protected int MaxHP;
        protected int ATK;
        protected int ATKPower;
        protected int defence;
        protected bool IsDead = false;
        protected int Level;
        protected int totalExp;
        protected int MaxExp;
        protected Inventory<itemClass> Inventory;
        protected EquipedItem<itemClass> EquipedItem;

        private int PosX;
        private int PosY;
        Random Random = new Random();

        public Player(Tile_Type[,] tiles, int mapSize)
        {
            while (true)
            {
                PosX = Random.Next(0, mapSize);
                PosY = Random.Next(0, mapSize);

                if (tiles[PosX, PosY] == Tile_Type.Wall) continue;
                if (tiles[PosX, PosY] == Tile_Type.Exit) continue;
                if (tiles[PosX, PosY] == Tile_Type.Monster) continue;

                tiles[PosX, PosY] = Tile_Type.Player;
                break;
            }
            Inventory = new Inventory<itemClass>();
            EquipedItem = new EquipedItem<itemClass>();
        }

        #region Battle

        public void Info(string name, int MaxHP, int ATK, int defence)
        {
            this.name = name;
            this.MaxHP = MaxHP;
            this.ATK = ATK;
            this.defence = defence;

            InitStatus(this.MaxHP, this.ATK);
            
        }

        public string GetName {  get { return name; } }
        public int GetHP { get {  return currentHP; } }

        public void InitStatus(int HP, int Atk)
        {
            this.currentHP = HP;
            this.ATKPower = new Random().Next(ATK - 5, ATK + 5);
            this.Level = 1;
            this.totalExp = 0;
            UpdateMaxExp();
        }

        public void UpdateLevel(int totalExp)
        {
            if (totalExp >= this.MaxExp)
            {
                Level++;
                totalExp -= this.MaxExp;
            }
        }

        public void UpdateMaxExp()
        {
            switch (this.Level)
            {
                case 1:
                    this.MaxExp = 15;
                    break;
                case 2:
                    this.MaxExp = 30;
                    break;
                case 3:
                    this.MaxExp = 80;
                    break;
                case 4:
                    this.MaxExp = 100;
                    break;
                case 5:
                    Console.WriteLine("최대 레벨에 도달하였습니다.");
                    this.MaxExp = 10000;
                    break;
                default:
                    return;

            }
        }

        public void AddExp(int addexp)
        {
            this.totalExp += addexp;
        }

        public bool Hurt(int damage)
        {
            this.currentHP -= damage;
            if (currentHP <= 0)
            {
                Console.WriteLine($"{this.name}이(가) 힘을 다하여 쓰러졌습니다.");
                IsDead = true;
                return IsDead;
            }
            else
            {
                Console.WriteLine($"{name}이(가) {damage}만큼 피해를 입었습니다.");
                IsDead = false;
                return IsDead;
            }
        }
        public bool Attack(Monsters monster)
        {
            bool IsDeath = monster.Hurt(this.ATKPower);
            if (IsDeath)
            {
                this.totalExp += monster.GetGiveEXP;
                UpdateLevel(this.totalExp);
                UpdateMaxExp();
                Console.WriteLine("현재 가지고 있는 경험치 : {0}", totalExp);
                Thread.Sleep(1000);
            }
            return IsDeath;
        }

        public void Healing(int HealValue)
        {
            if (this.currentHP >= this.MaxHP)
            {
                Console.WriteLine("최대 체력입니다.");
                this.currentHP = MaxHP;
                return;
            }
            else
            {
                int CurrentHealth = this.currentHP;
                this.currentHP += HealValue;
                if (this.currentHP >= this.MaxHP) this.currentHP = MaxHP;
                Console.WriteLine($"체력이 {currentHP - CurrentHealth}만큼 회복되었습니다.");
            }
        }

        public void PowerUP(int value)
        {
            this.ATK += value;
            Console.WriteLine("무기를 장착하였다.");
        }

        public void DefenceUP(int value)
        {
            this.defence += value;
            Console.WriteLine("방어구를 장착하였다.");
        }

        public bool UseItem()
        {
            bool LoopEnd = false;
            if (Inventory.GetLength <= 0) return LoopEnd;

                Console.Write("사용할 아이템 입력 : ");
            string itemName = Console.ReadLine();
            Console.WriteLine();
            if (itemName == "end") LoopEnd = true; else LoopEnd = false;

            for (int i = 0; i < Inventory.GetLength; i++)
            {
                if (Inventory.GetItemNameList(i).GetName == itemName)
                {
                    itemClass item;
                    item = Inventory.GetItemNameList(i);
                    switch (item.Get_Type)
                    {
                        case Item_Type.HealingPotion:
                            Healing(item.GetEffect);
                            Inventory.Use(item);
                            break;
                        case Item_Type.Weapons:
                            if (EquipedItem.EquipWeapons(item))
                            {
                                PowerUP(item.GetEffect);
                                Inventory.Use(item);
                            }
                            break;
                        case Item_Type.Armors:
                            if (EquipedItem.EquipAromor(item))
                            {
                                DefenceUP(item.GetEffect);
                                Inventory.Use(item);
                            }
                            break;
                        default:
                            Console.WriteLine("다른 아이템은 아직 준비중...");
                            break;
                    }
                }
            }
            LoopEnd = true ;
            return LoopEnd;
        }



        public void PickUpItem(itemClass item)
        {
            Inventory.Add(item);
            Console.WriteLine("{0}을 획득하였습니다.\n", item.GetName);
        }

        public void PrintPlayerInfo()
        {
            Console.WriteLine("{0}", this.name);
            Console.WriteLine("=============================");
            Console.WriteLine("체력 : {0}", this.currentHP);
            Console.WriteLine("공격력 : {0}", this.ATK);
            Console.WriteLine("방어력 : {0}", this.defence);
            Console.WriteLine("=============================");
        }

        public void PrintPlayerInventroy()
        {
            Console.WriteLine("{0}의 인벤토리 목록", this.name);
            Console.WriteLine("=============================");
            if (Inventory.GetLength <= 0)
            {
                Console.WriteLine("아무것도 가진 것이 없다.");
                Console.WriteLine("=============================\n");
                return;
            }
            for (int i = 0; i < Inventory.GetLength; i++)
            {
                Console.Write("{0}  : ", Inventory.GetItemNameList(i).GetName);
                Console.WriteLine("{0}", Inventory.GetItemCountList(i));
            }
            Console.WriteLine("=============================\n");
        }

        public void PrintEquipedItem()
        {
            Console.WriteLine("{0}이/가 장착한 아이템", this.name);
            Console.WriteLine("=============================");
            if (EquipedItem.GetWeapon != null) Console.WriteLine("무기 : {0}", EquipedItem.GetWeapon.GetName); else Console.WriteLine("무기 : {0}", EquipedItem.GetWeapon);
            if (EquipedItem.GetArmor != null) Console.WriteLine("방어구 : {0}", EquipedItem.GetArmor.GetName); else Console.WriteLine("방어구 : {0}", EquipedItem.GetArmor);
            Console.WriteLine("=============================");
        }

        public PlayerAction GetActionKey(ConsoleKey consoleKey)
        {
            switch (consoleKey)
            {
                case ConsoleKey.A:
                    return PlayerAction.Attack;
                case ConsoleKey.S:
                    return PlayerAction.UseItem;
                case ConsoleKey.D:
                    return PlayerAction.Run;
                default:
                    return default;
            }
        }


        

        #endregion

        public int GetPlayerPositionX() { return PosX; }
        public int GetPlayerPositionY() { return PosY; }

        public void SetPlayerPosition(int x, int y)
        {
            PosX = x;
            PosY = y;
        }






    }
}
