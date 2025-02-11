using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    enum PLAYER_JOB { Programmer, Planner}

    internal class Player : ICharacter
    {
        public Player(string _name, PLAYER_JOB _job, List<Skill_Key> _skillList)
        {
            // 이름, 직업 설정
            Name = _name;
            Job = _job;

            FilePath = Utill.WARRIOR_PATH;

            // 스킬 목록 추가
            SkillList = _skillList;

            // 레벨과 경험치 설정
            Level = 1;
            MaxExp = 100;
            Exp = 0;

            // HP, MP, 데미지, 방어력 설정
            MaxHP = 100;
            HP = 100;
            MaxMP = 100;
            MP = 100;
            AttDamage = 10;
            Defense = 5;

            // 소지금 설정
            Gold = 1500;
        }
        public Player(string _name, PLAYER_JOB _job, List<Skill_Key> _skillList, int _level, int _maxExp, int _exp, int _maxHp, int _hp, int _maxMp, int _mp, float _attDamage, int _def, int _gold)
        {
            // 이름, 직업 설정
            Name = _name;
            Job = _job;
            
            FilePath = Utill.WARRIOR_PATH;

            // 스킬 목록 추가
            SkillList = _skillList;

            // 레벨과 경험치 설정
            Level = _level;
            MaxExp = _maxExp;
            Exp = _exp;

            // HP, MP, 데미지, 방어력 설정
            MaxHP = _maxHp;
            HP = _hp;
            MaxMP = _maxMp;
            MP = _mp;
            AttDamage = _attDamage;
            Defense = _def;

            // 소지금 설정
            Gold = _gold;
        }


        //Player 만 가지는 필드
        public int Gold { get; set; }
        public int Exp {  get; set; }
        public int MaxExp { get; set; }
        public PLAYER_JOB Job { get; set; }

        // 플레이어가 배운 스킬 목록 (Enum 리스트)
        public List<Skill_Key> SkillList { get; set; } = new List<Skill_Key>();

        //인벤토리 - 아이템 종류에 따라 리스트를 나눠 갖고있는다.

        //장착 아이템
        public List<Equip_Item> Inven_Equip_Item = new List<Equip_Item>();
        //소비 아이템
        public List<Usable_Item> Inven_Usable_Item = new List<Usable_Item>();
        //장비 슬롯
        public Equip_Item[] EquipSlot = new Equip_Item[(int)Item_Slot_Type.SLOT_MAX];
        //버프 리스트
        public List<Buff> BuffList = new List<Buff>();

        // TODO : 미구현

        //인터페이스 _ 프로퍼티
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHP { get; set; }
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

        public int MaxMP { get; set; }
        private int mp;
        public int MP
        {
            get { return mp; }
            set
            {
                mp = value;
                if (mp > MaxMP) mp = MaxMP;
                else if (mp < 0) mp = 0;
            }
        }
        public float AttDamage { get; set; }
        public int Defense { get; set; }
        public bool IsDead => HP <= 0;

        public (string idle,string die, string end) FilePath { get; set; }

        // 최종 데미지
        public float FinalDamage()
        {
            float finalDamage = 0f;

            finalDamage += AttDamage;

            //아이템에 의한 데미지 증가
            foreach (var EquipItem in EquipSlot)
            {
                if(EquipItem != null)
                {
                    finalDamage += EquipItem.Bonus_Att;
                }
            }

            return finalDamage;
        }
        
        public float Equip_Damage()
        {
            float EquipDamage = 0f;
            //아이템에 의한 데미지 증가

            foreach (var EquipItem in EquipSlot)
            {
                if (EquipItem != null)
                {
                    EquipDamage += EquipItem.Bonus_Att;
                }
            }

            return EquipDamage;
        }

        // 최종 방어력
        public int FinalDefense()
        {
            int finalDefense = 0;

            finalDefense += Defense;

            foreach (var EquipItem in EquipSlot)
            {
                if (EquipItem != null)
                {
                    finalDefense += (int)EquipItem.Bonus_Def;
                }
            }

            return finalDefense;
        }
        public float Equip_Defense()
        {
            float EquipDefense = 0f;
            //아이템에 의한 데미지 증가

            foreach (var EquipItem in EquipSlot)
            {
                if (EquipItem != null)
                {
                    EquipDefense += EquipItem.Bonus_Def;
                }
            }

            return EquipDefense;
        }
        //치명타 기능
        public int CriticalAttack(float finalDamage, ref bool isCritical)
        {
            int critical = new Random().Next(1, 100);
            if (critical <= 15)
            {
                isCritical = true;
                float newCharacterCri = finalDamage * 1.6f;
                finalDamage = (int)Math.Round(newCharacterCri);
            }
            else
            {
                isCritical = false;
            }

            return (int)Math.Round(finalDamage);
        }

        //버프 한턴씩
        public void CountBuff()
        {
            for(int i = 0; i < BuffList.Count; i++)
            {
                BuffList[i].Bonus_Turn--;
                if(BuffList[i].Check_BuffTurn() == false)
                {
                    //능력치 해제
                    if(BuffList[i].Bonus_Att > 0)
                        AttDamage -= BuffList[i].Bonus_Att;
                    if (BuffList[i].Bonus_Def > 0)
                        Defense -= (int)BuffList[i].Bonus_Def;

                    //지속 회복


                    BuffList.Remove(BuffList[i]);
                }
            }
        }
        public void LevelUp()
        {
            if (Exp >= MaxExp)
            {
                do
                {
                    Exp -= MaxExp;
                    Level += 1;
                    MaxExp = Level * 100;
                    MaxHP += 100;
                    AttDamage += 0.5f;
                    Defense += 1;

                } while (Exp > MaxExp);

                HP = MaxHP;
            }
        }
    }
}
