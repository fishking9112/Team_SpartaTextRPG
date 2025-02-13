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
        public Monster(string _name, int _level, int _hp, int maxhp, int _attDamage, int _def, (string, string, string) _filePath)
        {
            Name = _name;
            Level = _level;
            MaxHP = maxhp;
            HP = _hp;

            AttDamage = _attDamage;
            Defense = _def;

            bool IsDead = false;

            FilePath = _filePath;
            
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
        public float AttDamage {  get; set; }
        public int Defense { get; set; }
        public bool IsDead => HP <= 0;

        public (string idle,string die, string end) FilePath { get; set; }


        // 몬스터 공격력 오차 범위
        public float Monster_AttDamage_Range()
        {
            // 랜덤을 통해 오차 10% 만들기
            Random random = new Random();
            float AttRange = random.Next(-10, 11);

            // 위 내용을 토대로 몬스터 공격력 범위로 수정한다.
            float ChangeMonsterDamage = AttDamage + (AttDamage / 100.0f * AttRange);

            return ChangeMonsterDamage;
        }

        public bool IsAvoid(float _avoid)
        {
            int avoid = new Random().Next(1, 100);
            if (avoid <= 10)
            {
                return true;
            }

            return false;
        }
    }
}
