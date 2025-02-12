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

        public void ShowInventory()
        {
            TitleManager.instance.WriteTitle("인벤토리");

            ScreenManager.instance.AsyncImage("./resources/bag.png",_startX:80, _startY:2, imageSizeX:20, imageSizeY:20);

            StringBuilder sb = new();
            sb.AppendLine("[장비 아이템 목록]");
            sb.AppendLine();
            int index = 1;
            if (Inven_Equip_Item.Count <= 0)
            {
                sb.AppendLine("보유 중인 장비 아이템이 없습니다.");
            }
            
            ScreenManager.instance.AsyncText(sb);

            for (int i = 0; i < Inven_Equip_Item.Count; i++, index++)
            {
                
                ConsoleColor color = ConsoleColor.Cyan;
                if(Inven_Equip_Item[i].item_Slot_Type == Item_Slot_Type.WEAPON){
                    color = ConsoleColor.Cyan;
                } else if(Inven_Equip_Item[i].item_Slot_Type == Item_Slot_Type.ARMOR_H){
                    color = ConsoleColor.DarkRed;
                } else if(Inven_Equip_Item[i].item_Slot_Type == Item_Slot_Type.ARMOR_C){
                    color = ConsoleColor.DarkBlue;
                } else if(Inven_Equip_Item[i].item_Slot_Type == Item_Slot_Type.ARMOR_G){
                    color = ConsoleColor.DarkYellow;
                } else if(Inven_Equip_Item[i].item_Slot_Type == Item_Slot_Type.ARMOR_S){
                    color = ConsoleColor.DarkCyan;
                }

                sb.AppendLine($"{index}. {Inven_Equip_Item[i].Name}   |   {Inven_Equip_Item[i].Description}   |   {Inven_Equip_Item[i].AtkorDef()}   |   {Inven_Equip_Item[i].ShowEquip()}");
                ScreenManager.instance.AsyncText($"{index}. {Inven_Equip_Item[i].Name}   |   {Inven_Equip_Item[i].Description}   |   {Inven_Equip_Item[i].AtkorDef()}   |   {Inven_Equip_Item[i].ShowEquip()}", 1, 3 + i, color);
            }

            sb.Clear();
            sb.AppendLine("[소비 아이템 목록]");
            sb.AppendLine();
            if (Inven_Usable_Item.Count <= 0)
            {
                sb.AppendLine("보유 중인 소비 아이템이 없습니다.");
            }
            for (int i = 0; i < Inven_Usable_Item.Count; i++, index++)
            {
                sb.AppendLine($"{index}.   {Inven_Usable_Item[i].Name}   |   {Inven_Usable_Item[i].Description}   |   {Inven_Usable_Item[i].HporMp()}");
            }

            int itemYCount = Inven_Equip_Item.Count == 0 ? 1 : Inven_Equip_Item.Count;
            ScreenManager.instance.AsyncText(sb, 1, _startY:itemYCount + 4);

            InputKeyManager.instance.ArtMenu(
                ($"아이템 관리", "아이템을 관리합니다.", () => ShowInventoryItem()), 
                ($"돌아가기", "마을로 돌아갑니다.", () => TownScene.instance.Game_Main()));
        }


        public void ShowInventoryItem()
        {
            TitleManager.instance.WriteTitle("인벤토리 - 장착관리");

            ScreenManager.instance.AsyncImage("./resources/bag_open.png",_startX:80, _startY:2, imageSizeX:20, imageSizeY:20);

            StringBuilder sb = new();

            sb.AppendLine("[장비 아이템 목록]");
            sb.AppendLine();
            List<Action> tempActions = new List<Action>();
            tempActions.Add(ShowInventory);
            int index = 1;
            if (Inven_Equip_Item.Count <= 0)
            {
                sb.AppendLine("보유 중인 장비 아이템이 없습니다.");
            }
            
            ScreenManager.instance.AsyncText(sb);

            for (int i = 0; i < Inven_Equip_Item.Count; i++, index++)
            {
                int temp = i;
                ConsoleColor color = ConsoleColor.Cyan;
                if(Inven_Equip_Item[i].item_Slot_Type == Item_Slot_Type.WEAPON){
                    color = ConsoleColor.Cyan;
                } else if(Inven_Equip_Item[i].item_Slot_Type == Item_Slot_Type.ARMOR_H){
                    color = ConsoleColor.DarkRed;
                } else if(Inven_Equip_Item[i].item_Slot_Type == Item_Slot_Type.ARMOR_C){
                    color = ConsoleColor.DarkBlue;
                } else if(Inven_Equip_Item[i].item_Slot_Type == Item_Slot_Type.ARMOR_G){
                    color = ConsoleColor.DarkYellow;
                } else if(Inven_Equip_Item[i].item_Slot_Type == Item_Slot_Type.ARMOR_S){
                    color = ConsoleColor.DarkCyan;
                }

                tempActions.Add(() => EquipItem(player.Inven_Equip_Item[temp]));
                ScreenManager.instance.AsyncText($"{index}. {Inven_Equip_Item[i].Name}   |   {Inven_Equip_Item[i].Description}   |   {Inven_Equip_Item[i].AtkorDef()}   |   {Inven_Equip_Item[i].ShowEquip()}", 1, 3 + i, color);
            }

            sb.Clear();
            sb.AppendLine("[소비 아이템 목록]");
            sb.AppendLine();
            if (player.Inven_Usable_Item.Count <= 0)
            {
                sb.AppendLine("보유 중인 소비 아이템이 없습니다.");
            }
            for (int i = 0; i < Inven_Usable_Item.Count; i++, index++)
            {
                int temp = i;
                tempActions.Add(() => EquipItem(player.Inven_Usable_Item[temp]));
                sb.AppendLine($"{index}.   {Inven_Usable_Item[i].Name}   |   {Inven_Usable_Item[i].Description}   |   {Inven_Usable_Item[i].HporMp()}");
            }

            sb.AppendLine();
            sb.AppendLine("0. 나가기");
            int itemYCount = Inven_Equip_Item.Count == 0 ? 1 : Inven_Equip_Item.Count;
            ScreenManager.instance.AsyncText(sb, 1, _startY:itemYCount + 4);

            InputKeyManager.instance.InputMenu(ShowInventoryItem, "아이템을 선택해주세요 >> ", tempActions.ToArray());
        }


        public void EquipItem(Item item)
        {
            if (item is Equip_Item equip_Item)
            {
                equip_Item.Equip(equip_Item);
            }
            else if (item is Usable_Item usable_Item)
            {
                usable_Item.Use(usable_Item);
            }
            InputKeyManager.instance.GoMenu(ShowInventoryItem);
        }
    }
}
