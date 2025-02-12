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
            sb.AppendLine();

            ScreenManager.instance.AsyncUnitVideo(player.FilePath.idle, startX: 1, startY: 0, videoSizeX: 12, videoSizeY: 15, _isContinue: true, _isReversal:true, _frame:33);

            ScreenManager.instance.AsyncText(sb, _startX: 29);
           
            for (int i = 0; i < EquipSlot.Length; i++)
            {
                if (EquipSlot[i] != null)
                {
                    switch (i)
                    { 
                      case 0:
                        ScreenManager.instance.AsyncText($"무기아이템 : {EquipSlot[i].Name}", 29, _startY:11 + i, ConsoleColor.Cyan);
                      break;
                      case 1:
                        ScreenManager.instance.AsyncText($"장착모니터 : {EquipSlot[i].Name}", 29, _startY:11 + i, ConsoleColor.DarkRed);
                      break;
                      case 2:
                        ScreenManager.instance.AsyncText($"장착키보드 : {EquipSlot[i].Name}", 29, _startY:11 + i, ConsoleColor.DarkBlue);
                      break;
                      case 3:
                        ScreenManager.instance.AsyncText($"장착마우스 : {EquipSlot[i].Name}", 29, _startY:11 + i, ConsoleColor.DarkYellow);
                      break;
                      case 4:
                        ScreenManager.instance.AsyncText($"장착컴퓨터 : {EquipSlot[i].Name}", 29, _startY:11 + i, ConsoleColor.DarkCyan);
                      break;
                    }
                }
                else
                {
                    switch (i)
                    { 
                      case 0:
                        ScreenManager.instance.AsyncText($"무기아이템 : 장착중인 아이템이 없습니다.", 29, _startY:11 + i, ConsoleColor.Cyan);
                      break;
                      case 1:
                        ScreenManager.instance.AsyncText($"장착모니터 : 장착중인 아이템이 없습니다.", 29, _startY:11 + i, ConsoleColor.DarkRed);
                      break;
                      case 2:
                        ScreenManager.instance.AsyncText($"장착키보드 : 장착중인 아이템이 없습니다.", 29, _startY:11 + i, ConsoleColor.DarkBlue);
                      break;
                      case 3:
                        ScreenManager.instance.AsyncText($"장착마우스 : 장착중인 아이템이 없습니다.", 29, _startY:11 + i, ConsoleColor.DarkYellow);
                      break;
                      case 4:
                        ScreenManager.instance.AsyncText($"장착컴퓨터 : 장착중인 아이템이 없습니다.", 29, _startY:11 + i, ConsoleColor.DarkCyan);
                      break;
                    }
                }
            }
            
            InputKeyManager.instance.ArtMenu(($"나가기", "마을로 돌아갑니다.", () =>  {TownScene.instance.Game_Main();}));
        }
    }

}
