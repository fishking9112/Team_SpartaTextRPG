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
        public void Game_Stats()
        {
            Console.Clear();
            Utill.ColorWriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            Console.WriteLine($"공격력 : {player.AttDamage}");
            Console.WriteLine($"방어력 : {player.Defense}");
            Console.WriteLine($"체력 : {player.HP}");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            SceneManager.instance.Menu(Game_Stats, TownScene.instance.Game_Main);
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
