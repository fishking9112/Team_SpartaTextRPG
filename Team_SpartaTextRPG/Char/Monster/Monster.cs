﻿using System;
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
        public Monster(string _name, int _level, int _hp, float _attDamage, int _def)
        {
            Name = _name;
            Level = _level;
            HP = _hp;
            AttDamage = _attDamage;
            Defense = _def;
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
        public void Status_Monster(Monster monster)
        {
            // AttDamge와 Defense 제외한 다른 것을 보여줘야함
            Console.WriteLine($"{Name} Lv. {Level} {HP}/{MaxHP}");
        }
    }
}
