using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class StatusScene : Helper.Singleton<StatusScene>
    {
        Player player = GameManager.instance.player;
        Equip_Item[] EquipSlot = GameManager.instance.player.EquipSlot;
        public void Player_Stats()
        {
            Console.Clear();
            Utill.ColorWriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            Console.WriteLine($"공격력 : {player.FinalDamage()} ({player.AttDamage}+{player.Equip_Damage()})"); //최종공격력 (기본공격력+아이템공격력)
            Console.WriteLine($"방어력 : {player.FinalDefense()} ({player.Defense}+{player.Equip_Defense()})"); //최종방어력 (기본방어력+아이템방어력)
            Console.WriteLine($"체력 : {player.HP}");
            Console.WriteLine($"마나 : {player.MaxMP}");
            Console.WriteLine($"Gold : {player.Gold} G");
            
           
            for (int i = 0; i < EquipSlot.Length; i++)
            {
                if (EquipSlot[i] != null)
                {
                    switch (i)
                    { 
                      case 0:
                      Console.WriteLine($"무기아이템 : {EquipSlot[i].Name}");
                      break;
                      case 1:
                      Console.WriteLine($"머리아이템 : {EquipSlot[i].Name}");
                      break;
                      case 2:
                      Console.WriteLine($"갑옷아이템 : {EquipSlot[i].Name}");
                      break;
                      case 3:
                      Console.WriteLine($"장갑아이템 : {EquipSlot[i].Name}");
                      break;
                      case 4:
                      Console.WriteLine($"신발아이템 : {EquipSlot[i].Name}");
                      break;
                    }
                }
                else
                {
                    Console.WriteLine("장착아이템: 장착중인 아이템이 없습니다.");
                }
            }
            

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            SceneManager.instance.Menu(Player_Stats, TownScene.instance.Game_Main);
        }

        public void selectMenu(int _input)
        {
            if (_input == 0)
            {
                //Game_Main으로 돌아가기
                SceneManager.instance.GoMenu(TownScene.instance.Game_Main);
            }
        }
    }

}
