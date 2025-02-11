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
            TitleManager.instance.WriteTitle("상태 보기", ConsoleColor.Yellow);

            StringBuilder sb = new();

            sb.AppendLine("캐릭터의 정보가 표시됩니다.");
            sb.AppendLine();
            sb.AppendLine($"Lv. {player.Level}");
            sb.AppendLine($"{player.Name} ( {player.Job} )");
            sb.AppendLine($"공격력 : {player.FinalDamage()} ({player.AttDamage}+{player.Equip_Damage()})"); //최종공격력 (기본공격력+아이템공격력)
            sb.AppendLine($"방어력 : {player.FinalDefense()} ({player.Defense}+{player.Equip_Defense()})"); //최종방어력 (기본방어력+아이템방어력)
            sb.AppendLine($"체력 : {player.HP}");
            sb.AppendLine($"마나 : {player.MaxMP}");
            sb.AppendLine($"Gold : {player.Gold} G");

           
            for (int i = 0; i < EquipSlot.Length; i++)
            {
                if (EquipSlot[i] != null)
                {
                    switch (i)
                    { 
                      case 0:
                      sb.AppendLine($"무기아이템 : {EquipSlot[i].Name}");
                      break;
                      case 1:
                      sb.AppendLine($"머리아이템 : {EquipSlot[i].Name}");
                      break;
                      case 2:
                      sb.AppendLine($"갑옷아이템 : {EquipSlot[i].Name}");
                      break;
                      case 3:
                      sb.AppendLine($"장갑아이템 : {EquipSlot[i].Name}");
                      break;
                      case 4:
                      sb.AppendLine($"신발아이템 : {EquipSlot[i].Name}");
                      break;
                    }
                }
                else
                {
                    sb.AppendLine("장착아이템: 장착중인 아이템이 없습니다.");
                }
            }

            ScreenManager.instance.AsyncText(sb);
            
            InputKeyManager.instance.ArtMenu(($"나가기", "마을로 돌아갑니다.", () =>  {TownScene.instance.Game_Main();}));
        }
    }

}
