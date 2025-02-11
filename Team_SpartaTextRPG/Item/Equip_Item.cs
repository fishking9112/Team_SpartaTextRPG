using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    // 장비 착용 부위
    enum Item_Slot_Type { WEAPON, ARMOR_H, ARMOR_C, ARMOR_G, ARMOR_S, SLOT_MAX }
    // 장비 착용 시 필요한 직업
    enum Item_Job_Type { NONE, Programmer, Planner }

    internal class Equip_Item : Item
    {
        // 장비 착용 부위
        public Item_Slot_Type item_Slot_Type;
        // 장비 착용 시 필요한 직업
        public Item_Job_Type item_Job_Type;
        // 장착 여부
        public bool IsEquip { get; set; }
        public bool IsPurchased { get; set; }
        public float Bonus_Att { get; set; }
        public float Bonus_Def { get; set; }


        public Equip_Item(string _Name, string _Des, int _Price, Item_Slot_Type _Slot, Item_Job_Type _Job, float _Bonus_Att, float _Bonus_Def) : base(_Name, _Des, _Price)
        {
            item_Slot_Type = _Slot;
            item_Job_Type = _Job;
            Bonus_Att = _Bonus_Att;
            Bonus_Def = _Bonus_Def;
            IsEquip = false;
            IsPurchased = false;
        }

        public string AtkorDef()
        {
            string str = "";
            if (Bonus_Att > 0 && Bonus_Def > 0)
            {
                str = $"공격력 +{Bonus_Att}   |   방어력 +{Bonus_Def}";
            }
            else if (Bonus_Def > 0 && Bonus_Att <= 0)
            {
                str = $"방어력 +{Bonus_Def}";
            }
            else if (Bonus_Att > 0 && Bonus_Def <= 0)
            {
               str = $"공격력 +{Bonus_Att}";
            }
            return str;
        }

        public void Equip(Equip_Item item)
        {
            Player player = GameManager.instance.player;

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
            SceneManager.instance.GoMenu(InventoryScene.instance.ShowInventoryItem);
        }

        public string ShowEquip()
        {
            return IsEquip ? "장착" : "장착 가능";
        }
        
        public string CheckPurchase()
        {
            
            return IsPurchased ? "이미 구매" : "구매 가능";
        }
    }
}
