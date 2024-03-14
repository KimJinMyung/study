using MazeTRPG.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MazeTRPG.Battle
{
    internal class Battle
    {
        Player Player;
        Monsters monsters;
        Random random;

        bool PlayerTurn = false;

        public Battle(Player player, Monsters monsters)
        {
            this.Player = player;
            this.monsters = monsters;
        }

        public bool BattlePlayertoMonster() 
        { 
            random = new Random();
            while (true)
            {
                Console.WriteLine("=======================");
                Console.WriteLine($"| {this.Player.GetName} HP : {this.Player.GetHP}  ");
                Console.WriteLine($"| {this.monsters.GetName} HP : {this.monsters.GetCurrentHP}  ");
                Console.WriteLine("======================= \n");

                //플레이어의 턴에 플레이어가 이겼다.
                bool LoopEnd = InputKey();
                if (LoopEnd) 
                {
                    Console.WriteLine();    
                    int PickUpItemIndex = random.Next(3);
                    switch (PickUpItemIndex)
                    {
                        case 0:
                            itemClass item = new HealingPotion();
                            Player.PickUpItem(item);
                            break;
                        case 1:
                            itemClass weapons = new Weapons();
                            Player.PickUpItem(weapons);
                            break;
                        case 2:
                            itemClass armor = new Armor();
                            Player.PickUpItem(armor);
                            break;
                    }
                    Thread.Sleep(1000);
                    return true; 
                }

                //몬스터의 턴에 몬스터가 이겼다.
                bool playerDead = monsters.Attack(Player);
                if (playerDead) return false;
            }
        }

        #region 선공권비교
        //public bool SelectPriority()
        //{
        //    int randomNumber = random.Next(2);

        //    if (randomNumber == 0)
        //    {
        //        Console.WriteLine($"{player.GetName}의 우선권!!");
        //        PlayerTurn = true;
        //        return true;
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{monsters.GetName}의 우선권!!");
        //        PlayerTurn = true;
        //        return true;
        //    }
        //}
        #endregion

        public bool PlayerActionAttack(Monsters monsters)
        {
            bool battleEnd = this.Player.Attack(monsters);
            return battleEnd;
        }

        public bool PlayerActionRun()
        {
            bool battleEnd = false;
            int runSeuccess = random.Next(100);
            if (runSeuccess >= 40)
            {
                Console.WriteLine("도망치지 못했습니다.");
                return battleEnd;
            }
            Console.WriteLine("무사히 도망치는 것에 성공했습니다.");
            battleEnd = true;
            return battleEnd;
        }

        public bool InputKey()
        {
            bool battleEnd;
            while (true)
            {
                battleEnd = false;
                bool turnEnd;
                Console.WriteLine("A : 공격");
                Console.WriteLine("S : 아이템 사용");
                Console.WriteLine("D : 도망치기");
                Console.Write($"행동을 선택해주세요. : ");
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();
                PlayerAction action = this.Player.GetActionKey(key.Key);
                Console.WriteLine();
                switch (action)
                {
                    case PlayerAction.Attack:
                        battleEnd = PlayerActionAttack(monsters);
                        turnEnd = true;
                        break;
                    case PlayerAction.UseItem:
                        this.Player.PrintPlayerInfo();
                        Console.WriteLine();
                        this.Player.PrintEquipedItem();
                        Console.WriteLine();
                        this.Player.PrintPlayerInventroy();                        
                        turnEnd = this.Player.UseItem();                       
                        battleEnd = false;
                        break;
                    case PlayerAction.Run:
                        battleEnd = PlayerActionRun();
                        turnEnd = true;
                        break;
                    default:
                        return default;
                }

                if (turnEnd) break;
            }

            return battleEnd;
        }
    }
}
