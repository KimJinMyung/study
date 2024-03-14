using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeTRPG.Item
{
    enum Item_Type
    {
        None,
        HealingPotion,
        Weapons,
        Armors
    }
    internal class itemClass
    {
        protected string ItemName;
        protected Item_Type Type;
        protected int Effect;


        public Item_Type Get_Type { get { return Type; } }
        public int GetEffect { get { return Effect; } }
        public string GetName { get { return ItemName; } }
    }

    class HealingPotion : itemClass
    {
        public HealingPotion()
        {
            ItemName = "붉은 포션";
            Type = Item_Type.HealingPotion;
            Effect = 50;
        }
    }

    class Weapons : itemClass
    {
        public Weapons()
        {
            ItemName = "철검";
            Type = Item_Type.Weapons;
            Effect = 10;
        }
    }

    class Armor : itemClass
    {
        public Armor()
        {
            ItemName = "가죽 갑옷";
            Type = Item_Type.Armors;
            Effect = 5;
        }
    }
}
