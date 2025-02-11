using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class Buff
    {
        public Buff(float _Bonus_Att , float _Bonus_Def, float _Bonus_HP, float _Bonus_MP ,int _Bonus_Turn)
        {
            Bonus_Att = _Bonus_Att;
            Bonus_Def = _Bonus_Def;
            Bonus_HP = _Bonus_HP;
            Bonus_MP = _Bonus_MP;
            Bonus_Turn = _Bonus_Turn;
        }
        public float Bonus_Att { get; set; }
        public float Bonus_Def { get; set; }
        public float Bonus_HP { get; set; }
        public float Bonus_MP { get; set; }
        public int Bonus_Turn {  get; set; }

        public bool Check_BuffTurn()
        {
            if (Bonus_Turn == 0)
                return false;

            return true;
        }

    }
}
