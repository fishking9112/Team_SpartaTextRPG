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
        }
        public void ShowMenu()
        {
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 돌아가기");
            SceneManager.instance.Menu(ShowMenu, TownScene.instance.Game_Main, ShowShop);
        }

        public void ShowShop()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.instance.player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            List<Equip_Item> filteredItems = equip_ItemsList.Where(item => item.item_Job_Type == Item_Job_Type.NONE ||
            (player.Job == PLAYER_JOB.WARRIOR && item.item_Job_Type == Item_Job_Type.WARRIOR) ||
            (player.Job == PLAYER_JOB.ARCHER && item.item_Job_Type == Item_Job_Type.ARCHER) ||
            (player.Job == PLAYER_JOB.WIZARD && item.item_Job_Type == Item_Job_Type.WIZARD)).ToList();

            ShowShopItems(filteredItems);
        }
        public void BuyItem(int input)
        {
            Item select = equip_ItemsList[input - 1];
            Buy(select);
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
                }
                else if (item is Usable_Item usable_Item)
                {
                    player.Inven_Usable_Item.Add(usable_Item);
                }
                SceneManager.instance.GoMenu(ShowShop);
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
                SceneManager.instance.GoMenu(ShowShop);
            }
        }

        public void ShowShopItems(List<Equip_Item> filteredItems)
        {
            List<Action> tempActions = new List<Action>();
            tempActions.Add(TownScene.instance.Game_Main);
            for (int i = 0; i < filteredItems.Count; i++)
            {
                int temp = i;
                tempActions.Add(() => BuyItem(temp + 1));
                Console.WriteLine($"{i + 1}.   {filteredItems[i].Name}   |   설명: {filteredItems[i].Description}   |   {filteredItems[i].AtkorDef()}   |   가격: {filteredItems[i].Price}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            SceneManager.instance.Menu(ShowShop, tempActions.ToArray());
        }
    }
}
