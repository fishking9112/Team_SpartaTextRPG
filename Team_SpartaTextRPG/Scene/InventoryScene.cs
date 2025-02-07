using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class InventoryScene : Helper.Singleton<InventoryScene>
    {
        List<Equip_Item> Inven_Equip_Item = GameManager.instance.player.Inven_Equip_Item;
        List<Usable_Item> Inven_Usable_Item = GameManager.instance.player.Inven_Usable_Item;
        Player player = GameManager.instance.player;

        public void ShowInventory ()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[장비 아이템 목록]");
            Console.WriteLine();
            if (Inven_Equip_Item.Count <= 0)
            {
                Console.WriteLine("보유 중인 장비 아이템이 없습니다.");
            }
            for (int i = 0; i < Inven_Equip_Item.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Inven_Equip_Item[i].Name}   |   {Inven_Equip_Item[i].Description}   |   {Inven_Equip_Item[i].AtkorDef()}   |   {Inven_Equip_Item[i].ShowEquip()}");
            }

            Console.WriteLine();
            Console.WriteLine("[소비 아이템 목록]");
            Console.WriteLine();
            if (player.Inven_Usable_Item.Count <= 0)
            {
                Console.WriteLine("보유 중인 소비 아이템이 없습니다.");
            }
            for (int i = 0; i < Inven_Usable_Item.Count; i++)
            {
                Console.WriteLine($"{i + 1}.   {Inven_Usable_Item[i].Name}   |   {Inven_Usable_Item[i].Description}   |   {Inven_Usable_Item[i].HporMp()}");
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            SceneManager.instance.Menu(ShowInventory, TownScene.instance.Game_Main, ShowInventoryItem);
        }


        public void ShowInventoryItem ()
        {
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[장비 아이템 목록]");
            Console.WriteLine();
            List<Action> tempActions = new List<Action>();
            tempActions.Add(TownScene.instance.Game_Main);
            if (Inven_Equip_Item.Count <= 0)
            {
                Console.WriteLine("보유 중인 장비 아이템이 없습니다.");
            }
            for (int i = 0; i < Inven_Equip_Item.Count; i++)
            {
                int temp = i;
                tempActions.Add(() => BuyItem(temp + 1));
                Console.WriteLine($"{i + 1}. {Inven_Equip_Item[i].Name}   |   {Inven_Equip_Item[i].Description}   |   {Inven_Equip_Item[i].AtkorDef()}   |   {Inven_Equip_Item[i].ShowEquip()}");
            }

            Console.WriteLine();
            Console.WriteLine("[소비 아이템 목록]");
            Console.WriteLine();
            if (player.Inven_Usable_Item.Count <= 0)
            {
                Console.WriteLine("보유 중인 소비 아이템이 없습니다.");
            }
            for (int i = 0; i < Inven_Usable_Item.Count; i++)
            {
                Console.WriteLine($"{i + 1}.   {Inven_Usable_Item[i].Name}   |   {Inven_Usable_Item[i].Description}   |   {Inven_Usable_Item[i].HporMp()}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            SceneManager.instance.Menu(ShowInventoryItem, tempActions.ToArray());
        }

        public void BuyItem(int input)
        {

            Equip_Item item = Inven_Equip_Item[input - 1];
            Equip(item);
        }
        public void Equip (Equip_Item item)
        {
            int slotIndex = (int)item.item_Slot_Type;

            if (player.EquipSlot[slotIndex] != null)
            {
                Equip_Item currentItem = player.EquipSlot[slotIndex];
                Console.WriteLine($"기존 {currentItem.Name}이(가) 장착되어 있습니다.");
                Console.WriteLine($"새로운 아이템 {item.Name}을(를) 장착합니다.");

                player.EquipSlot[slotIndex] = item;
                SceneManager.instance.GoMenu(ShowInventoryItem);
            }
            else
            {
                Console.WriteLine($"{item.Name}을(를) 장착합니다.");
                player.EquipSlot[slotIndex] = item;
                SceneManager.instance.GoMenu(ShowInventoryItem);
            }

            item.IsEquip = true;
        }
    }
}
