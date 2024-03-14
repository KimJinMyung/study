using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeTRPG
{
    internal class Inventory<T>
    {
        private List<T> itemlist;
        private List<int> itemCount;

        public Inventory()
        {
            itemlist = new List<T>();
            itemCount = new List<int>();
        }

        public void Add(T item)
        {
            bool IsHaveItem = IsCheckHaveItem(item);
            if (IsHaveItem)
            {
                int count = itemCount[itemlist.IndexOf(item)];
                count++;
                itemCount[itemlist.IndexOf(item)] = count;
            }
            else
            {
                itemlist.Add(item);
                itemCount.Add(1);
            }
        }

        public bool IsCheckHaveItem(T item)
        {
            int isHaveItem = itemlist.IndexOf(item);
            if (isHaveItem > -1) { return true; } else { return false; }
        }

        public void Use(T item)
        {
            int isHaveItem = itemlist.IndexOf(item);
            if (isHaveItem <= -1)
            {
                Console.WriteLine("해당 아이템을 가지고 있지 않다.");
            }
            else
            {
                itemCount[isHaveItem]--;
                if (itemCount[isHaveItem] <= 0)
                {
                    itemCount.RemoveAt(isHaveItem);
                    itemlist.Remove(item);
                }
                Console.WriteLine("아이템이 사용되었습니다.");
            }
        }

        public int GetLength { get { return itemlist.Count; } }

        public T GetItemNameList(int index)
        {
            return itemlist[index];
        }
        public int GetItemCountList(int index)
        {
            return itemCount[index];
        }
    }

    class EquipedItem<T>
    {
        private T armor;
        private T weapon;

        public bool EquipAromor(T item)
        {
            if (armor == null)
            {
                this.armor = item;
                return true;
            }
            else
            {
                Console.WriteLine("이미 착용중인 방어구가 존재합니다..");
                return false;
            }
        }
        public bool EquipWeapons(T item)
        {
            if (weapon == null)
            {
                this.weapon = item;
                return true;
            }
            else
            {
                Console.WriteLine("이미 착용중인 무기가 존재합니다..");
                return false;
            }
        }

        public T GetArmor { get { return armor; } }
        public T GetWeapon { get { return weapon; } }
    }
}
