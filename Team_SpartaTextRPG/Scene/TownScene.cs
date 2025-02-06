using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class TownScene : Helper.Singleton<TownScene>
    {
        public void Game_Main()
        {
            Console.WriteLine("1. [ 상태보기 ]");
            Console.WriteLine("2. [ 인벤토리 ]");
            Console.WriteLine("3. [  상  점  ]");
            Console.WriteLine("4. [  여  관  ]");
            Console.WriteLine("5. [ 던전입장 ]");
            Console.WriteLine("6. [  저  장  ]");
            Console.WriteLine("7. [  종  료  ]");

            SceneManager.instance.Menu(Game_Main, null, () => Select_Numbers(1), () => Select_Numbers(2), () => Select_Numbers(3), () => Select_Numbers(4), () => Select_Numbers(5), () => Select_Numbers(6),() => Select_Numbers(7));
        }

        public void Select_Numbers(int _index)
        {
            if (_index == 5)
            {
                // 던전입장 선택지
                SceneManager.instance.GoMenu(DungeonScene.instance.Dungeon_Title);

            }
            else
            {
                // 나머지 선택지
            }
        }
    }
}
