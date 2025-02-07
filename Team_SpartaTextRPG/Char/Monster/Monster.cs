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
        public Monster(string _name, int _level, int _hp, int maxhp /* _attDamage, int _def*/)
        {
            Name = _name;
            Level = _level;
            HP = _hp;
            MaxHP = maxhp;

            // 몬스터가 소환 시 AttDamge와 Defense는 안보여줌
            //AttDamage = _attDamage;
            //Defense = _def;
        }

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



        // 몬스터의 기본 정보
        public void Monster_Info()
        {
            Console.WriteLine($"{Name} Lv. {Level} {HP}/{MaxHP}");
        }
    }
}
