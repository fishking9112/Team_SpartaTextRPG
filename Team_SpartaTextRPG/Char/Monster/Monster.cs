using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class Monster : ICharacter
    {
        // 기본 생성자
        public Monster() { }

        // 사용자 지정 생성자
        public Monster(string _name, int _level, int _hp, int maxhp, int _attDamage, int _def)
        {
            Name = _name;
            Level = _level;
            MaxHP = maxhp;
            HP = _hp;

            AttDamage = _attDamage;
            Defense = _def;

            bool IsDead = false;
        }

        //Monster만 가지는 필드

        //뭐가 있을까 .. ?


        //인터페이스 _ 프로퍼티
        public string Name { get; set; }
        public int Level { get; set; }
        private int hp;
        public int HP
        {
            get { return hp; }
            set
            {
                hp = value;

                // HP 값이 MaxHP를 넘지 않도록 제한
                if (hp > MaxHP) hp = MaxHP;
                else if (hp < 0) hp = 0;
            }
        }
        public int MaxHP { get; set; }
        public float AttDamage { get; set; }
        public int Defense { get; set; }
        public bool IsDead => HP <= 0;
    }
}
