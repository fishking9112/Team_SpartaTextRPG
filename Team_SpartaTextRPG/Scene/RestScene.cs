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
            Console.Clear();
            Utill.ColorWriteLine("휴식하기");
            Console.WriteLine($"500 G를 내면 체력을 회복할 수 있습니다. (보유골드 : {player.Gold} G)");
            Console.WriteLine();
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            //RestMenu로 가기
            SceneManager.instance.Menu(Show_Rest, TownScene.instance.Game_Main, RestMenu);
        }
        
        public void RestMenu()
        {
                if ( player.MaxHP== player.HP)             //플레이어 체력이 최대치일때
                {
                    Console.WriteLine("체력이 이미 최대치입니다.\n\n");
                }
                else if ( player.Gold < 500)               //플레이어 골드가 500미만일때
                {
                    Console.WriteLine("Gold가 부족합니다.\n\n");
                }
                else
                {
                    player.Gold -= 500;                    //플레이어 골드가 -500
                    player.HP = player.MaxHP;              //플레이어 체력이 최대치가됨
                    Console.WriteLine("휴식을 완료했습니다.\n\n");
                }
            Console.WriteLine("0.확인");
            Console.WriteLine();
            //RestMenu 에서 Show_Rest로 돌아가기
            SceneManager.instance.Menu(RestMenu, Show_Rest);

        }
    }
}
