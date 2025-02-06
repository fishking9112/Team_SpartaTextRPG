using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class DungeonScene : Helper.Singleton<DungeonScene>
    {
        public void Dungeon_Title()
        {
            Console.WriteLine("1. [ Stage 1 ]");
            Console.WriteLine("2. [ Stage 2 ]");
            Console.WriteLine("3. [ Stage 3 ]");
            Console.WriteLine("4. [ 돌아가기 ]");

            SceneManager.instance.Menu(Dungeon_Title, null, ()=>Select_Stage(1), ()=>Select_Stage(2), ()=>Select_Stage(3), ()=>Select_Stage(4));
        }

        public void Select_Stage(int _index)
        {
            if(_index == 1)
            {
                // 스테이지 1 진입
            }
            else if (_index == 2)
            {
                // 스테이지 2
            }
            else if(_index == 3)
            {
                 // 스테이지 3 진입
            }
            else if(_index == 4)
            {
                // TownScene으로 돌아가기
                SceneManager.instance.GoMenu(TownScene.instance.Game_Main);
            }
        }
    }
}
