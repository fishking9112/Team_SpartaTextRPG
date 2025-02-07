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
        List<Equip_Item> Inven_Equip_Item = GameManager.instance.player.Inven_Equip_Item;
        public void Game_Stats()
        {
            Console.Clear();
            Utill.ColorWriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ( {player.Job} )");
            Console.WriteLine($"공격력 : {player.AttDamage} ({player.AttDamage}-)");
            Console.WriteLine($"방어력 : {player.Defense} ({player.Defense}-");
            Console.WriteLine($"체력 : {player.HP}");
            Console.WriteLine($"마나 : {player.MaxMP}");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            //아이템에서 공방가져오는중

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
