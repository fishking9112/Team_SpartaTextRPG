using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class Usable_Item : Item
    {
        public float Bonus_Att {  get; set; }
        public float Bonus_Def {  get; set; }
        public float Bonus_HP {  get; set; }
        public float Bonus_MP {  get; set; }


        public Usable_Item(string _Name, string _Des, int _Price, float _Bonus_Att, float _Bonus_Def, float _Bonus_HP, float _Bonus_MP):base(_Name, _Des, _Price) 
        {
            Bonus_Att = _Bonus_Att;
            Bonus_Def = _Bonus_Def;
            Bonus_HP = _Bonus_HP;
            Bonus_MP = _Bonus_MP;
        }

        public void Use(Usable_Item item)
        {
            Player player = GameManager.instance.player;
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
            SceneManager.instance.GoMenu(InventoryScene.instance.ShowInventoryItem);
        }

        public string HporMp()
        {
            string str = "";
            if (Bonus_HP > 0 && Bonus_MP > 0)
            {
                str = $"HP +{Bonus_HP}   |   MP +{Bonus_MP}";
            }
            else if (Bonus_MP > 0 && Bonus_HP <= 0)
            {
                str = $"MP +{Bonus_MP}";
            }
            else if (Bonus_HP > 0 && Bonus_MP <= 0)
            {
                str = $"HP +{Bonus_HP}";
            }
            else if (Bonus_Att > 0)
            {
                str = $"Att +{Bonus_Att}";
            }
            else if (Bonus_Def > 0)
            {
                str = $"DEF +{Bonus_Def}";
            }
            return str;
        }
    }
}
