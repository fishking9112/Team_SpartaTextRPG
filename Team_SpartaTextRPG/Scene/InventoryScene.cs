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

        public void ShowInventory ()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[장비 아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Inven_Equip_Item.Count; i++)
            {
                Console.WriteLine($"{i+1}. {Inven_Equip_Item[i].Name}   |   {Inven_Equip_Item[i].Description}   |   {Inven_Equip_Item[i].AtkorDef()}");
            }

            Console.WriteLine();
            Console.WriteLine("[소비 아이템 목록]");
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            SceneManager.instance.Menu(ShowInventory, TownScene.instance.Game_Main);

        }

        public void ShowInventoryItem ()
        {
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("[장비 아이템 목록]");
            Console.WriteLine();
            for (int i = 0; i < Inven_Equip_Item.Count; i++)
            {

                Console.WriteLine($"{i + 1}. {Inven_Equip_Item[i].Name}   |   {Inven_Equip_Item[i].Description}   |   {Inven_Equip_Item[i].AtkorDef()}");
            }

            Console.WriteLine();
            Console.WriteLine("[소비 아이템 목록]");
            Console.WriteLine();

            for (int i = 0; i < Inven_Usable_Item.Count; i++)
            {
                Console.WriteLine($"{i + 1}.   {Inven_Usable_Item[i].Name}   |   {Inven_Usable_Item[i].Description}   |   {Inven_Usable_Item[i].HporMp()}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            SceneManager.instance.Menu(ShowInventoryItem, ShowInventory);
        }
    }
}
