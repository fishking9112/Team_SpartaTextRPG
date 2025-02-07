using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    enum PLAYER_JOB { WARRIOR , END }

    internal class Player : ICharacter
    {
        public Player() { }
        public Player(string _name , int _level , int _hp , float _attDamage , int _def) { }

        //Player 만 가지는 필드
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int MaxExp {  get; set; }
        public PLAYER_JOB Job { get; set; }
        
        //인벤토리 - 아이템 종류에 따라 리스트를 나눠 갖고있는다.

        //장착 아이템
        public List<Equip_Item> Inven_Equip_Item = new List<Equip_Item>();
        //소비 아이템
        public List<Usable_Item> Inven_Usable_Item = new List<Usable_Item>();
        // 미구현

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
