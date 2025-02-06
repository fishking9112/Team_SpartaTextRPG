using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class Monster : ICharacter
    {
        public Monster() { }
        public Monster(string _name, int _level, int _hp, float _attDamage, int _def) { }

        //Monster만 가지는 필드

        //뭐가 있을까 .. ?
        

        //인터페이스 _ 프로퍼티
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public float AttDamage { get; set; }
        public int Defense { get; set; }
        public bool IsDead => HP <= 0;
    }
}
