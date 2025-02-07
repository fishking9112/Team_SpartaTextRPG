using System;
using System.Collections.Generic;
using System.Linq;
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
            return str;
        }
    }
}
