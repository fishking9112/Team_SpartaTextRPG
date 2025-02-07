using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class ShopScene : Helper.Singleton<ShopScene>
    {
        public List<Item> itemList = new List<Item>();
        public List<Equip_Item> Inven_Equip_Item;

        public ShopScene ()
        {
            this.Inven_Equip_Item = Inven_Equip_Item;

            itemList = new List<Item>
            {
                new Item("검", "일반 검", 1500)
            };
        }
        public void ShowShop ()
        {
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 돌아가기");
            SceneManager.instance.Menu(ShowShop, TownScene.instance.Game_Main, ShowShopItem);
        }

        public void ShowShopItem ()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.instance.player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < itemList.Count; i++)
            {
                Console.WriteLine($"{i + 1}.   {itemList[i].Name}   |   {itemList[i].Description}   |   {itemList[i].Price}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            

            SceneManager.instance.Menu(ShowShopItem, ShowShop, TownScene.instance.Game_Main);
        }
        public void BuyItem (int input)
        {
            switch (input)
            {
                case 0:
                    ShowShop();
                    break;
                default:
                    Item select = itemList[input - 1];

                    break;
            }
        }

        public void Buy (Item item)
        {
            if (GameManager.instance.player.Gold > item.Price)
            {
                GameManager.instance.player.Gold -= item.Price;
                

            }
        }
    }
}
