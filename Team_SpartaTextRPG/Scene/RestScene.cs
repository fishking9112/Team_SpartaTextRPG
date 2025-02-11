using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class RestScene : Helper.Singleton<RestScene>
    {
        Player player = GameManager.instance.player;
        public void Show_Rest()
        {
            TitleManager.instance.WriteTitle("여관", ConsoleColor.Yellow);

            StringBuilder sb = new();
            sb.AppendLine($"500 G를 내면 체력을 회복할 수 있습니다. (보유골드 : {player.Gold} G)");

            ScreenManager.instance.AsyncText(sb);

            InputKeyManager.instance.ArtMenu(
                ($"휴식하기", $"500 G를 내면 체력을 회복할 수 있습니다. (보유골드 : {player.Gold} G)", () => RestMenu()), 
                ($"나가기", "마을로 나갑니다.", () => {TownScene.instance.Game_Main(); }));
        }
        
        public void RestMenu()
        {
            TitleManager.instance.WriteTitle("휴식하기", ConsoleColor.Yellow);

            
            StringBuilder sb = new();
            if ( player.MaxHP== player.HP)             //플레이어 체력이 최대치일때
            {
                sb.AppendLine("체력이 이미 최대치입니다.\n\n");
            }
            else if ( player.Gold < 500)               //플레이어 골드가 500미만일때
            {
                sb.AppendLine("Gold가 부족합니다.\n\n");
            }
            else
            {
                int fail = new Random().Next(1, 100);
                if (fail <= 50)
                {
                    player.Gold -= 500;                    //플레이어 골드가 -500
                    player.HP -= 50;              //플레이어 체력이 50깎임
                    sb.AppendLine("휴식을 실패했습니다.\n체력이 50 줄어들었습니다.\n");
                }
                else
                {
                    player.Gold -= 500;                    //플레이어 골드가 -500
                    player.HP = player.MaxHP;              //플레이어 체력이 최대치가됨
                    sb.AppendLine("휴식을 완료했습니다.\n");
                }
            }

            ScreenManager.instance.AsyncText(sb);

            InputKeyManager.instance.ArtMenu(
                ($"확인", "휴식하기로 돌아갑니다.", () => Show_Rest()));
        }
    }
}
