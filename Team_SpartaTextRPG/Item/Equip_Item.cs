using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    // 장비 착용 부위
    enum Item_Slot_Type { WEAPON, ARMOR, SLOT_MAX }
    // 장비 착용 시 필요한 직업
    enum Item_Job_Type { NONE, WARRIOR, ARCHER, WIZARD }

    internal class Equip_Item : Item
    {
        // 장비 착용 부위
        public Item_Slot_Type item_Slot_Type;
        // 장비 착용 시 필요한 직업
        public Item_Job_Type item_Job_Type;
        // 장착 여부
        public bool IsEquip { get; set; }
        public float Bonus_Att { get; set; }
        public float Bonus_Def { get; set; }


        public Equip_Item(string _Name, string _Des, int _Price, Item_Slot_Type _Slot, Item_Job_Type _Job, float _Bonus_Att, float _Bonus_Def) : base(_Name, _Des, _Price)
        {
            item_Slot_Type = _Slot;
            item_Job_Type = _Job;
            Bonus_Att = _Bonus_Att;
            Bonus_Def = _Bonus_Def;
            IsEquip = false;
        }
    }
}
