using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Team_SpartaTextRPG
{
    internal class DungeonScene : Helper.Singleton<DungeonScene>
    {
        Player player = GameManager.instance.player;
        Monster[] monsters = new Monster[3];

        public void Dungeon_Title()
        {
            Console.WriteLine("1. [ Stage 입장 ]");
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
            monsters[0] = new Monster("슬라임", 1, 15, 15);
            // monsters[1] = new Monster("고블린", 3, 22, 22);
            // monsters[2] = new Monster("오크", 5, 26, 26);
        }

        // 현재 몬스터를 Select_Stage로 출력
        public void DrawMonster_Info()
        {
            monsters[0].Monster_Info();
            // monsters[1].Monster_Info();
            // monsters[2].Monster_Info();
            Console.WriteLine("==================\n");

            SceneManager.instance.Menu(DrawMonster_Info, DungeonScene.instance.Dungeon_Title, () => Monster_Att(1) /*() => Monster_Att(2), () => Monster_Att(3)*/);
        }

        // 플레이어가 몬스터를 공격
        public void Monster_Att(int input)
        {
            switch (input)
            {
                case 1:
                    monsters[0].HP = (int)(monsters[0].MaxHP - player.AttDamage);
                    Console.WriteLine($"{player}가 {monsters[0].Name}에게 {player.AttDamage}만큼 데미지를 주었습니다.");
                    break;

        public void Monster_Dead()
        {
            if (monsters[0].HP > 0)
            {
                Console.WriteLine($"{monsters[0].Name}이(가) 죽었습니다.");
            }
            else
            {
                Console.WriteLine($"{monsters[0].Name}이(가) {player.AttDamage}를 받았습니다.");
            }

            SceneManager.instance.Menu(DrawMonster_Info);

        }
    }
}
