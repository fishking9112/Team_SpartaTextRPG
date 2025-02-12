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
                new Equip_Item("C# 코드 작성", "C# 기본 문법과 자료구조 , 알고리즘", 1500, Item_Slot_Type.WEAPON, Item_Job_Type.Programmer, 15.0f, 0f),
                new Equip_Item("2D 유니티 엔진", "유니티 엔진의 사용법과 2D 게임 구현", 5000, Item_Slot_Type.WEAPON, Item_Job_Type.Programmer, 40.0f, 0f),
                new Equip_Item("3D 유니티 엔진", "유니티 엔진의 심화 , 3D 게임 구현 능력", 50000, Item_Slot_Type.WEAPON, Item_Job_Type.Programmer, 480.0f, 0f),
                new Equip_Item("개노잼카피게임 기획", "양산형 게임을 기획", 1500, Item_Slot_Type.WEAPON, Item_Job_Type.Planner, 15.0f, 0f),
                new Equip_Item("무난무난게임 기획", "할만한 게임을 기획", 5000, Item_Slot_Type.WEAPON, Item_Job_Type.Planner, 40.0f, 0f),
                new Equip_Item("개꿀잼게임 기획", "AAA 급 게임을 기획", 50000, Item_Slot_Type.WEAPON, Item_Job_Type.Planner, 480.0f, 0f),
                //머리
                new Equip_Item("CRT 모니터", "화면은 나옵니다.", 1000, Item_Slot_Type.ARMOR_H, Item_Job_Type.NONE, 0f, 10.0f),
                new Equip_Item("싱글 모니터", "작은 화면으로 고통받으며 코딩", 5000, Item_Slot_Type.ARMOR_H, Item_Job_Type.NONE, 0f, 15.0f),
                new Equip_Item("더블 모니터", "두개의 화면으로 코딩", 10000, Item_Slot_Type.ARMOR_H, Item_Job_Type.NONE, 0f, 20.0f),
                new Equip_Item("트리플 모니터", "쾌적한 화면으로 행복코딩", 50000, Item_Slot_Type.ARMOR_H, Item_Job_Type.NONE, 0f, 80.0f),
                //옷
                new Equip_Item("싸구려 키보드", "가끔 키가 안먹습니다.", 1000, Item_Slot_Type.ARMOR_C, Item_Job_Type.NONE, 0f, 10.0f),
                new Equip_Item("피시방 키보드", "타닥타닥 시끄러워 주변의 눈치를 봐야합니다.", 5000, Item_Slot_Type.ARMOR_C, Item_Job_Type.NONE, 0f, 15.0f),
                new Equip_Item("무소음 키보드", "조용하게 코딩할 수 있습니다.", 10000, Item_Slot_Type.ARMOR_C, Item_Job_Type.NONE, 0f, 20.0f),
                new Equip_Item("커스텀 키보드", "최고의 편안함을 제공합니다.", 50000, Item_Slot_Type.ARMOR_C, Item_Job_Type.NONE, 0f, 80.0f),
                //장갑
                new Equip_Item("볼 마우스", "가끔 마우스 안에 볼을 빼고 싶은 생각이 듭니다.", 1000, Item_Slot_Type.ARMOR_G, Item_Job_Type.NONE, 0f, 10.0f),
                new Equip_Item("무선 마우스", "화장실에서도 코딩이 가능합니다.", 5000, Item_Slot_Type.ARMOR_G, Item_Job_Type.NONE, 0f, 15.0f),
                new Equip_Item("버티컬 마우스", "내 손목을 위한 응급처치용 마우스", 10000, Item_Slot_Type.ARMOR_G, Item_Job_Type.NONE, 0f, 20.0f),
                ///신발
                new Equip_Item("구형 컴퓨터", "플로피 디스크를 사용할 수 있다.", 1000, Item_Slot_Type.ARMOR_S, Item_Job_Type.NONE, 0f, 10.0f),
                new Equip_Item("사무용 컴퓨터", "거북이와 속도 대결이 가능하다.", 5000, Item_Slot_Type.ARMOR_S, Item_Job_Type.NONE, 0f, 15.0f),
                new Equip_Item("최신식 컴퓨터", "작업 속도는 동일하나, 파일은 빛의 속도로 열립니다.", 10000, Item_Slot_Type.ARMOR_S, Item_Job_Type.NONE, 0f, 20.0f)
            };
            usable_ItemsList = new List<Usable_Item>
            {
                //즉시발동 물약
                new Usable_Item("스누피커피우유", "카페인이 느껴집니다.", 60, 0f, 0f, 0f, 10f),
                new Usable_Item("커피", "잠이 안 오기 시작합니다.", 180, 0f, 0f, 0f, 30f),
                new Usable_Item("레드불", "뭐든 할 수 있을 것 같습니다.", 300, 0f, 0f, 0f, 50f),
                new Usable_Item("삼각김밥", "뭔가 허전합니다.", 120, 0f, 0f, 20f, 0f),
                new Usable_Item("라면", "세상에서 젤 기쁜 건", 180, 0f, 0f, 30f, 0f),
                new Usable_Item("햄버거", "살이 찔 것 같습니다.", 240, 0f, 0f, 40f, 0f),
                new Usable_Item("편의점 도시락", "배가 든든합니다.", 300, 0f, 0f, 50f, 0f),

                //지속효과 물약
                new Usable_Item("전자 담배", "머리가 안 아픕니다.", 150, 0f, 5f, 0f, 0f , 5),
                new Usable_Item("담배", "안 아프기 시작합니다.", 300, 0f, 10f, 0f, 0f , 5),
                new Usable_Item("맥주", "기분이 좋아집니다.", 150, 5f, 0f, 0f, 0f , 5),
                new Usable_Item("소주", "날뛸 수 있습니다.", 300, 10f, 0f, 0f, 0f , 5),
            };
        }
        public void ShowMenu()
        {
            TitleManager.instance.WriteTitle("상점");

            ScreenManager.instance.AsyncImage("./resources/shop.png");

            InputKeyManager.instance.ArtMenu(
                ($"장비 아이템 구매", "아이템을 구매합니다.", () => ShowShop()), 
                ($"소비 아이템 구매", "아이템을 구매합니다.", () => ShowUsableItems()), 
                ($"아이템 판매", "아이템을 판매합니다.", () => ShowPurchaseItem()), 
                ($"돌아가기", "마을로 돌아갑니다.", () => TownScene.instance.Game_Main()));
        }

        public void ShowShop()
        {
            TitleManager.instance.WriteTitle("상점");

            StringBuilder sb = new();
            // sb.AppendLine("상점");
            // sb.AppendLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            // sb.AppendLine();
            sb.AppendLine($"[보유 골드 : {GameManager.instance.player.Gold} G]");
            sb.AppendLine();
            sb.AppendLine("[아이템 목록]");
            
            ScreenManager.instance.AsyncText(sb);

            List<Equip_Item> filteredItems = equip_ItemsList.Where(item => item.item_Job_Type == Item_Job_Type.NONE ||
            (player.Job == PLAYER_JOB.Programmer && item.item_Job_Type == Item_Job_Type.Programmer) ||
            (player.Job == PLAYER_JOB.Planner && item.item_Job_Type == Item_Job_Type.Planner)).ToList();

            ShowShopItems(filteredItems, sb);
        }

        public void ShowUsableItems()
        {
            TitleManager.instance.WriteTitle("상점");

            StringBuilder sb = new();
            sb.AppendLine($"[보유 골드 : {player.Gold} G]");
            sb.AppendLine();
            sb.AppendLine("[소비 아이템 목록]");

            List<Action> tempActions = new List<Action>();
            tempActions.Add(ShopScene.instance.ShowMenu);
            
            ScreenManager.instance.AsyncText(sb);
            
            StringBuilder sb_name = new();
            StringBuilder sb_description = new();
            StringBuilder sb_price = new();

            sb_name.AppendLine($"[이름]");
            sb_description.AppendLine($"[설명]");
            sb_price.AppendLine($"[가격]");
            for (int i = 0; i < usable_ItemsList.Count; i++)
            {
                int temp = i;
                tempActions.Add(() => BuyItem(usable_ItemsList[temp]));
                sb_name.AppendLine($"{i + 1}. {usable_ItemsList[i].Name}");
                sb_description.AppendLine($"{usable_ItemsList[i].Description}");
                sb_price.AppendLine($"{usable_ItemsList[i].Price} G");
            }
            // 시간 없어서 수동으로 조절
            ScreenManager.instance.AsyncText(sb_name, 1, 5);
            ScreenManager.instance.AsyncText(sb_description, 21, 5);
            ScreenManager.instance.AsyncText(sb_price, 60, 5);

            ScreenManager.instance.AsyncText("0. 나가기" , 1, usable_ItemsList.Count+7);

            InputKeyManager.instance.InputMenu(ShowUsableItems,"살 소모품을 선택해주세요 >> ",tempActions.ToArray());
        }

        public void ShowPurchaseItem()
        {
            TitleManager.instance.WriteTitle("상점");
            
            StringBuilder sb = new();
            sb.AppendLine($"[보유 골드 : {player.Gold} G]");
            sb.AppendLine("");
            sb.AppendLine("[보유 장비 아이템 목록]");
            ScreenManager.instance.AsyncText(sb, 1, 1);
            
            List<Action> tempActions = new List<Action>();
            tempActions.Add(ShopScene.instance.ShowMenu);
            
            StringBuilder sb_name = new();
            StringBuilder sb_description = new();
            StringBuilder sb_atkorDef = new();
            StringBuilder sb_price = new();

            sb_name.AppendLine($"[이름]");
            sb_description.AppendLine($"[설명]");
            sb_atkorDef.AppendLine($"[효과]");
            sb_price.AppendLine($"[판매 가격]");
            
            int index = 1;
            for (int i = 0; i < player.Inven_Equip_Item.Count; i++)
            {
                int temp = i;
                tempActions.Add(() => SellItem(player.Inven_Equip_Item[temp]));
                sb_name.AppendLine($"{index++}. {player.Inven_Equip_Item[i].Name}");
                sb_description.AppendLine($"{player.Inven_Equip_Item[i].Description}");
                sb_atkorDef.AppendLine($"{player.Inven_Equip_Item[i].AtkorDef()}");
                sb_price.AppendLine($"{player.Inven_Equip_Item[i].Price * 0.8} G");
            }
            if(player.Inven_Equip_Item.Count == 0){
                ScreenManager.instance.AsyncText("보유한 장비 아이템이 없습니다.", 2, 5);
            } else {
                // 시간 없어서 수동으로 조절
                ScreenManager.instance.AsyncText(sb_name, 1, 5);
                ScreenManager.instance.AsyncText(sb_description, 24, 5);
                ScreenManager.instance.AsyncText(sb_atkorDef, 77, 5);
                ScreenManager.instance.AsyncText(sb_price, 103, 5);
            }

            sb.Clear();
            sb.AppendLine("[보유 소비 아이템 목록]");

            ScreenManager.instance.AsyncText(sb, 1, player.Inven_Equip_Item.Count+7);

            sb_name.Clear();
            sb_description.Clear();
            sb_price.Clear();

            sb_name.AppendLine($"[이름]");
            sb_description.AppendLine($"[설명]");
            sb_price.AppendLine($"[판매 가격]");
            for (int i = 0; i < player.Inven_Usable_Item.Count; i++)
            {
                int temp = i;
                tempActions.Add(() => SellItem(player.Inven_Usable_Item[temp]));
                sb_name.AppendLine($"{index++}. {player.Inven_Usable_Item[i].Name}");
                sb_description.AppendLine($"{player.Inven_Usable_Item[i].Description}");
                sb_price.AppendLine($"{player.Inven_Usable_Item[i].Price} G");
            }
            
            if(player.Inven_Usable_Item.Count == 0){
                ScreenManager.instance.AsyncText("보유한 소비 아이템이 없습니다.", 1, player.Inven_Equip_Item.Count+9);
            } else {
                // 시간 없어서 수동으로 조절
                ScreenManager.instance.AsyncText(sb_name, 1, player.Inven_Equip_Item.Count+9);
                ScreenManager.instance.AsyncText(sb_description, 21, player.Inven_Equip_Item.Count+9);
                ScreenManager.instance.AsyncText(sb_price, 60, player.Inven_Equip_Item.Count+9);
            }

            sb.Clear();
            sb.AppendLine("0. 나가기");

            ScreenManager.instance.AsyncText(sb, 1, player.Inven_Equip_Item.Count+player.Inven_Usable_Item.Count+11);

            InputKeyManager.instance.InputMenu(ShowPurchaseItem,"팔 아이템을 선택해주세요 >> ",tempActions.ToArray());
        }

        public void BuyItem(Item item)
        {
            if (item is Equip_Item equip_Item)
            {
                if (!equip_Item.IsPurchased)
                {

                }
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
            TitleManager.instance.WriteTitle("상점");

            int sellPrice = (int)(item.Price * 0.8f);
            if (item is Equip_Item equip_Item)
            {
                //장착 해제 후 판매
                int slotIndex = (int)equip_Item.item_Slot_Type;

                if (player.EquipSlot[slotIndex] != null)
                {
                    equip_Item.IsEquip = false;
                    player.EquipSlot[slotIndex] = null;
                }
                player.Inven_Equip_Item.Remove(equip_Item);
                player.Gold += sellPrice;
                equip_Item.IsPurchased = false;
            }
            else if (item is Usable_Item usable_Item)
            {
                player.Inven_Usable_Item.Remove(usable_Item); 
                player.Gold += sellPrice;
            }
            // 사지면
            ScreenManager.instance.AsyncText($"{item.Name}을 팔았습니다.", _color: ConsoleColor.Green);
            ScreenManager.instance.AsyncText($"{player.Gold}(+{sellPrice}) G", _color: ConsoleColor.Yellow);

            InputKeyManager.instance.ArtMenu(
                ($"뒤로", "상점으로 돌아갑니다.", () => ShowPurchaseItem()));
        }
        

        public void Buy(Item item)
        {
            TitleManager.instance.WriteTitle("상점");
            
            if (item is Equip_Item equip_Item)
            {
                // 이미 구매한 장비라면 구매 불가
                if (equip_Item.IsPurchased)
                {
                    ScreenManager.instance.AsyncText($"{item.Name}는 이미 구매한 아이템입니다!", _color: ConsoleColor.Red);
                    SceneManager.instance.GoMenu(ShowShop);
                    return;
                }
            }

            //만약 플레이어 골드가 아이템 가격보다 많은 경우
            if (player.Gold >= item.Price)
            {
                player.Gold -= item.Price;
                // 사지면
                ScreenManager.instance.AsyncText($"{item.Name}을 구매 했습니다.", _color: ConsoleColor.Green);

                if (item is Equip_Item equip)
                {
                    // 만약 아이템이 Equip Item이면 인벤에 넣기
                    player.Inven_Equip_Item.Add(equip);
                    equip.IsPurchased = true;
                    InputKeyManager.instance.ArtMenu(
                        ($"뒤로", "상점으로 돌아갑니다.", () => ShowShop()));
                }
                else if (item is Usable_Item usable_Item)
                {
                    player.Inven_Usable_Item.Add(usable_Item);
                    InputKeyManager.instance.ArtMenu(
                        ($"뒤로", "상점으로 돌아갑니다.", () => ShowUsableItems()));
                }
            }
            else
            {
                // 골드 부족하면 
                ScreenManager.instance.AsyncText($"{item.Name}을 구입하기엔 Gold 가 부족합니다.", _color: ConsoleColor.Red);
            }

            
        }

        public void ShowShopItems(List<Equip_Item> filteredItems, StringBuilder sb)
        {
            StringBuilder sb_name = new();
            StringBuilder sb_description = new();
            StringBuilder sb_atkorDef = new();
            StringBuilder sb_price = new();

            List<Action> tempActions = new List<Action>();
            tempActions.Add(ShopScene.instance.ShowMenu);

            sb_name.AppendLine($"[이름]");
            sb_description.AppendLine($"[설명]");
            sb_atkorDef.AppendLine($"[효과]");
            sb_price.AppendLine($"[가격]");
            for (int i = 0; i < filteredItems.Count; i++)
            {
                int temp = i;
                tempActions.Add(() => BuyItem(filteredItems[temp]));
                sb_name.AppendLine($"{i + 1}. {filteredItems[i].Name}");
                sb_description.AppendLine($"{filteredItems[i].Description}");
                sb_atkorDef.AppendLine($"{filteredItems[i].AtkorDef()}");
                sb_price.AppendLine($"{filteredItems[i].Price} G");
            }
            // 시간 없어서 수동으로 조절
            ScreenManager.instance.AsyncText(sb_name, 1, 5);
            ScreenManager.instance.AsyncText(sb_description, 24, 5);
            ScreenManager.instance.AsyncText(sb_atkorDef, 77, 5);
            ScreenManager.instance.AsyncText(sb_price, 103, 5);

            ScreenManager.instance.AsyncText("0. 나가기" , 1, filteredItems.Count+7);

            InputKeyManager.instance.InputMenu(ShowShop,"살 아이템을 번호를 입력하세요 >> ", tempActions.ToArray());
                
        }
    }
}
