using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Team_SpartaTextRPG
{
    internal class DungeonScene : Helper.Singleton<DungeonScene>
    {
        public void Dungeon_Title()
        {
            Console.WriteLine("1. [ Stage 1 ]");
            //Console.WriteLine("2. [ Stage 2 ]");
            //Console.WriteLine("3. [ Stage 3 ]");
            Console.WriteLine("0. [ 돌아가기 ]");

            SceneManager.instance.Menu(Dungeon_Title, TownScene.instance.Game_Main, () => Select_Stage(1));
        }

        public void Select_Stage(int _index)
        {
            if (_index == 1)
            {
                // 던전 진입시 몬스터 소환
                initStage();
            }

        }
        


        // 층에 나오기 전에 나오는 몬스터 설정
        public void initStage()
        {
            // 몬스터 정보
            Monster Monster_A = new Monster("슬라임", 1, 10, 1, 1);
            Monster Monster_B = new Monster("고블린", 3, 18, 3, 5);
            Monster Monster_C = new Monster("오크", 5, 25, 5, 7);
        }

        // 현재 몬스터를 Select_Stage로 출력
        public void DrawMonster_Info()
        {

        }
    }
}
