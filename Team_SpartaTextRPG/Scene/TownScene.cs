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

            SceneManager.instance.Menu(Game_Main, null, () => Select_Menu(1), () => Select_Menu(2), () => Select_Menu(3));
        }
        public void Select_Menu(int _index)
        {
            if (_index == 1)
            {
                SceneManager.instance.GoMenu(StatusScene.instance.Game_Stats);
            }
            else
            {
                // 궁수 선택
            }
        }
    }
}
