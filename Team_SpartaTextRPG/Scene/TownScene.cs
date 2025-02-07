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



            SceneManager.instance.Menu(Game_Main, null,
                ()=>Select_Menu(1),
                () => Select_Menu(2),
                () => Select_Menu(3),
                () => Select_Menu(4),
                () => Select_Menu(5),
                () => Select_Menu(6),
                () => Select_Menu(7) );

        }
        public void Select_Menu(int _index)
        {
            switch(_index)
            {

                case 1: // 상태보기
                    SceneManager.instance.GoMenu(StatusScene.instance.Game_Stats);
                    break;
                case 2: // 인벤토리
                    GameManager.instance.isPlaying = false; // 임시로 채워놓은 게임 종료 입니다.
                    break;
                case 3: // 상점
                    SceneManager.instance.GoMenu(ShopScene.instance.ShowShop);
                    break;
                case 4: // 여관
                    SceneManager.instance.GoMenu(RestScene.instance.Show_Rest);
                    break;
                case 5: // 던전 입장
                    SceneManager.instance.GoMenu(DungeonScene.instance.Dungeon_Title);
                    break;
                case 6: // 저장
                    GameManager.instance.isPlaying = false; // 임시로 채워놓은 게임 종료 입니다.
                    break;
                case 7: // 종료
                    GameManager.instance.isPlaying = false;
                    break;

            }
        }
    }
}
