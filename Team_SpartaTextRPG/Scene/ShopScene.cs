using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class ShopScene : Helper.Singleton<ShopScene>
    {
        public List<Item> itemList = new List<Item>();
        public List<Equip_Item> equip_ItemsList = new List<Equip_Item>();
        public List<Equip_Item> equip_Items = GameManager.instance.player.Inven_Equip_Item;
        Player player = GameManager.instance.player;
        public List<Usable_Item> usable_item = GameManager.instance.player.Inven_Usable_Item;
        public List<Usable_Item> usable_ItemsList = new List<Usable_Item>();

        public ShopScene()
        {
            equip_ItemsList = new List<Equip_Item>
            {
                new Equip_Item("숏 소드", "일반 짧은 검", 100, Item_Slot_Type.WEAPON, Item_Job_Type.WARRIOR, 10.0f, 5f),
                new Equip_Item("롱 소드", "일반 긴 검", 100, Item_Slot_Type.WEAPON, Item_Job_Type.WARRIOR, 15.0f, 0f),
                new Equip_Item("자이언트 소드", "....", 300, Item_Slot_Type.WEAPON, Item_Job_Type.WARRIOR, 20.0f, 10f),
                new Equip_Item("세이버 소드", "....", 500, Item_Slot_Type.WEAPON, Item_Job_Type.WARRIOR, 25.0f, 0f),
                new Equip_Item("일반 활", "......", 100, Item_Slot_Type.WEAPON, Item_Job_Type.ARCHER, 10.0f, 5f),
                new Equip_Item("철 활", "......", 100, Item_Slot_Type.WEAPON, Item_Job_Type.ARCHER, 15.0f, 0f),
                new Equip_Item("자이언트 활", "....", 300, Item_Slot_Type.WEAPON, Item_Job_Type.ARCHER, 20.0f, 10f),
                new Equip_Item("세이버 활", "....", 500, Item_Slot_Type.WEAPON, Item_Job_Type.ARCHER, 25.0f, 0f),
            };
            usable_ItemsList = new List<Usable_Item>
            {
                new Usable_Item("체력 포션", "체력 50 증가", 100, 0f, 0f, 50f, 0f),
                new Usable_Item("마나 포션", "마나 50 증가", 100, 0f, 0f, 0f, 50f),
                new Usable_Item("반반 포션", "체력과 마나 50씩 증가", 200, 0f, 0f, 50f, 50f),
                new Usable_Item("공격력 포션", "공격력 10 증가", 100, 10f, 0f, 0f, 0f),
            };
        }
        public void ShowMenu()
        {
            Console.WriteLine("1. 장비 아이템 구매");
            Console.WriteLine("2. 소비 아이템 구매");
            Console.WriteLine("3. 아이템 판매");
            Console.WriteLine("0. 돌아가기");
            SceneManager.instance.Menu(ShowMenu, TownScene.instance.Game_Main, ShowShop, ShowUsableItems, ShowPurchaseItem);
        }

        public void ShowShop()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            List<Equip_Item> filteredItems = equip_ItemsList.Where(item => item.item_Job_Type == Item_Job_Type.NONE ||
            (player.Job == PLAYER_JOB.WARRIOR && item.item_Job_Type == Item_Job_Type.WARRIOR) ||
            (player.Job == PLAYER_JOB.ARCHER && item.item_Job_Type == Item_Job_Type.ARCHER) ||
            (player.Job == PLAYER_JOB.WIZARD && item.item_Job_Type == Item_Job_Type.WIZARD)).ToList();

            ShowShopItems(filteredItems);
        }

        public void ShowUsableItems()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            List<Action> tempActions = new List<Action>();
            tempActions.Add(ShopScene.instance.ShowMenu);
            for (int i = 0; i < usable_ItemsList.Count; i++)
            {
                int temp = i;
                tempActions.Add(() => BuyItem(usable_ItemsList[temp]));
                Console.WriteLine($"{i + 1}. {usable_ItemsList[i].Name}   |   {usable_ItemsList[i].Description}   |   {usable_ItemsList[i].HporMp()}   |   가격: {usable_ItemsList[i].Price}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            SceneManager.instance.Menu(ShowUsableItems, tempActions.ToArray());
        }

        public void ShowPurchaseItem()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요없는 아이템을 판매할 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[보유 장비 아이템 목록]");
            List<Action> tempActions = new List<Action>();
            tempActions.Add(ShopScene.instance.ShowMenu);
            int index = 1;
            for (int i = 0; i < player.Inven_Equip_Item.Count; i++, index++)
            {
                int temp = i;
                tempActions.Add(() => SellItem(player.Inven_Equip_Item[temp]));
                Console.WriteLine($"{index}. {player.Inven_Equip_Item[i].Name}   |   {player.Inven_Equip_Item[i].Description}   |   {player.Inven_Equip_Item[i].AtkorDef()}   |   판매가격: {player.Inven_Equip_Item[i].Price * 0.8}");
            }
            Console.WriteLine();
            Console.WriteLine("[보유 소비 아이템 목록]");
            for (int i = 0; i < player.Inven_Usable_Item.Count; i++, index++)
            {
                int temp = i;
                tempActions.Add(() => SellItem(player.Inven_Usable_Item[temp]));
                Console.WriteLine($"{index}. {player.Inven_Usable_Item[i].Name}   |   {player.Inven_Usable_Item[i].Description}   |   {player.Inven_Usable_Item[i].HporMp()}   |   판매가격: {player.Inven_Usable_Item[i].Price * 0.8}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            SceneManager.instance.Menu(ShowPurchaseItem, tempActions.ToArray());

        }

        public void ShowShopItems(List<Equip_Item> filteredItems)
        {
            List<Action> tempActions = new List<Action>();
            tempActions.Add(ShopScene.instance.ShowMenu);
            for (int i = 0; i < filteredItems.Count; i++)
            {
                int temp = i;
                tempActions.Add(() => BuyItem(filteredItems[temp]));
                Console.WriteLine($"{i + 1}.   {filteredItems[i].Name}   |   설명: {filteredItems[i].Description}   |   {filteredItems[i].AtkorDef()}   |   가격: {filteredItems[i].Price}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            SceneManager.instance.Menu(ShowShop, tempActions.ToArray());
        }

        public void BuyItem(Item item)
        {
            if (item is Equip_Item equip_Item)
            {
                Buy(equip_Item);
            }
            else if (item is Usable_Item usable_Item)
            {
                Buy(usable_Item);
            }
            
        }

        public void SellItem(Item item)
        {
            if (item is Equip_Item equip_Item)
            {
                Sell(equip_Item);
            }
            else if (item is Usable_Item usable_Item)
            {
                Sell(usable_Item);
            }
        }

        public void Sell(Item item)
        {
            int sellPrice = (int)(item.Price * 0.8f);
            if (item is Equip_Item equip_Item)
            {
                player.Inven_Equip_Item.Remove(equip_Item);
                player.Gold += sellPrice;
            }
            else if (item is Usable_Item usable_Item)
            {
                player.Inven_Usable_Item.Remove(usable_Item); 
                player.Gold += sellPrice;
            }
            SceneManager.instance.GoMenu(ShowPurchaseItem);
        }
        

        public void Buy(Item item)
        {
            //만약 플레이어 골드가 아이템 가격보다 많은 경우
            if (player.Gold >= item.Price)
            {
                player.Gold -= item.Price;
                if (item is Equip_Item equip_Item)
                {
                    // 만약 아이템이 Equip Item이면 인벤에 넣기
                    player.Inven_Equip_Item.Add(equip_Item);
                    SceneManager.instance.GoMenu(ShowShop);
                }
                else if (item is Usable_Item usable_Item)
                {
                    player.Inven_Usable_Item.Add(usable_Item);
                    SceneManager.instance.GoMenu(ShowUsableItems);
                }
                
            }
        }
    }
}
