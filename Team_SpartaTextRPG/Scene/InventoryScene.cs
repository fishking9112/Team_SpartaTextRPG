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
            TitleManager.instance.WriteTitle("인벤토리");

            StringBuilder sb = new();
            sb.AppendLine("[장비 아이템 목록]");
            sb.AppendLine();
            int index = 1;
            if (Inven_Equip_Item.Count <= 0)
            {
                sb.AppendLine("보유 중인 장비 아이템이 없습니다.");
            }
            for (int i = 0; i < Inven_Equip_Item.Count; i++, index++)
            {
                sb.AppendLine($"{index}. {Inven_Equip_Item[i].Name}   |   {Inven_Equip_Item[i].Description}   |   {Inven_Equip_Item[i].AtkorDef()}   |   {Inven_Equip_Item[i].ShowEquip()}");
            }

            sb.AppendLine();
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

            ScreenManager.instance.AsyncText(sb);

            InputKeyManager.instance.ArtMenu(
                ($"아이템 관리", "아이템을 관리합니다.", () => ShowInventoryItem()), 
                ($"돌아가기", "마을로 돌아갑니다.", () => TownScene.instance.Game_Main()));
        }


        public void ShowInventoryItem ()
        {
            TitleManager.instance.WriteTitle("인벤토리 - 장착관리");

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
            for (int i = 0; i < Inven_Equip_Item.Count; i++, index++)
            {
                int temp = i;
                tempActions.Add(() => EquipItem(player.Inven_Equip_Item[temp]));
                sb.AppendLine($"{index}. {Inven_Equip_Item[i].Name}   |   {Inven_Equip_Item[i].Description}   |   {Inven_Equip_Item[i].AtkorDef()}   |   {Inven_Equip_Item[i].ShowEquip()}");
            }

            sb.AppendLine();
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

            ScreenManager.instance.AsyncText(sb);

            InputKeyManager.instance.InputMenu(ShowInventoryItem, "장착할 아이템을 선택해주세요 >> ", tempActions.ToArray());
        }


        public void EquipItem(Item item)
        {
            if (item is Equip_Item equip_Item)
            {
                Equip(equip_Item);
            }
            else if (item is Usable_Item usable_Item)
            {
                Use(usable_Item);
            }
        }
        
        public void Use(Usable_Item item)
        {
            if (item.Bonus_HP > 0 && item.Bonus_MP <= 0)
            {
                player.HP += (int)item.Bonus_HP;
            }
            else if (item.Bonus_MP > 0 && player.HP <= 0)
            {
                player.MP += (int)item.Bonus_MP;
            }
            else if (item.Bonus_HP > 0 && item.Bonus_MP > 0)
            {
                player.HP += (int)item.Bonus_HP;
                player.MP += (int)item.Bonus_MP;
            }
            else if (item.Bonus_Att > 0)
            {
                player.AttDamage += (int)item.Bonus_Att;
            }
            else
            {
                player.Defense += (int)item.Bonus_Def;
            }
            player.Inven_Usable_Item.Remove(item);
            
            InputKeyManager.instance.GoMenu(ShowInventoryItem);
        }

        public void Equip(Equip_Item item)
        {
            int slotIndex = (int)item.item_Slot_Type;

            if (player.EquipSlot[slotIndex] != null)
            {
                Equip_Item currentItem = player.EquipSlot[slotIndex];
                currentItem.IsEquip = false;
                player.EquipSlot[slotIndex] = item;
                item.IsEquip = true;
            }
            else
            {
                player.EquipSlot[slotIndex] = item;
                item.IsEquip = true;
            }
            InputKeyManager.instance.GoMenu(ShowInventoryItem);
        }
    }
}
