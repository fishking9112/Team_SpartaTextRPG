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
        Monster[] monster = new Monster[3];

        public void Dungeon_Title()
        {
            Console.WriteLine("1. [ Stage 1 ]");
            //Console.WriteLine("2. [ Stage 2 ]");
            //Console.WriteLine("3. [ Stage 3 ]");
            Console.WriteLine("0. [ 돌아가기 ]");
            initStage();

            SceneManager.instance.Menu(Dungeon_Title, TownScene.instance.Game_Main, () => Select_Stage(1));
        }

        public void Select_Stage(int _index)
        {
            if (_index == 1)
            {
                // 던전 진입시 몬스터 소환
                DrawMonster_Info();
            }

        }
        


        // 층에 나오기 전에 나오는 몬스터 설정
        public void initStage()
        {
            // 몬스터 정보
            monster[0] = new Monster("슬라임", 1, 5, 5);
            monster[1] = new Monster("고블린", 3, 10, 10);
            monster[2] = new Monster("오크", 5, 20, 20);
        }

        // 현재 몬스터를 Select_Stage로 출력
        public void DrawMonster_Info()
        {
            monster[0].Monster_Info();
            monster[1].Monster_Info();
            monster[2].Monster_Info();
        }
    }
}
