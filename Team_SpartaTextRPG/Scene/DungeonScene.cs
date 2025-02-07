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
        Monster[] monsters = new Monster[4];

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
            monsters[1] = new Monster("고블린", 3, 22, 22);
            monsters[2] = new Monster("오크", 5, 26, 26);
            monsters[3] = new Monster("맼닠젘", 10, 50, 50);
        }

        // 현재 몬스터를 Select_Stage로 출력
        public void DrawMonster_Info()
        {
            //monsters[0].Monster_Info();
            // monsters[1].Monster_Info();
            // monsters[2].Monster_Info();

            List<Action> tempActions = new List<Action>();
            tempActions.Add(TownScene.instance.Game_Main);
            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i] != null)
                {
                    int temp = i;
                    tempActions.Add(() => Player_Att(temp + 1));
                    Console.WriteLine($"{i + 1}.   이름 : {monsters[i].Name}   |   레벨: {monsters[i].Level}   |  HP : {monsters[i].HP} / {monsters[i].MaxHP}");
                }
            }
            Console.WriteLine("==================\n");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            SceneManager.instance.Menu(DrawMonster_Info, tempActions.ToArray());

            //SceneManager.instance.Menu(DrawMonster_Info, DungeonScene.instance.Dungeon_Title, () => Monster_Att(1) /*() => Monster_Att(2), () => Monster_Att(3)*/);
        }

        // 플레이어가 몬스터를 공격
        public void Player_Att(int input)
        {
            monsters[input - 1].HP = (int)(monsters[input - 1].HP - player.AttDamage);

            Utill.ColorWriteLine($"{monsters[input - 1].Name} 은 어쩌고 저쩌고");
            Thread.Sleep(1000);

            //몬스터 체력 검사

            //피 없으면 죽이고 
            //죽은 몬스터 배열에서 빼주기

            // monsters[0] = null

            //배열에 몬스터가 없으면 null

            //모든 몬스터가 죽으면 == monsters[i] == null


                 //SceneManager.instance.GoMenu(DungeonScene.instance.다른 곳으로);



            SceneManager.instance.GoMenu(DungeonScene.instance.Monster_Att);
        }

        public void Monster_Att()
        {
            for (int i = 0; i < monsters.Length; i++)
            {
                if(monsters[i] != null)
                {
                    Utill.ColorWriteLine($"monsters[i] 공격", ConsoleColor.Red);
                    Utill.ColorWriteLine($"player 는 monsters[i].att 의 데미지를 받았다.");

                    // 플레이어 체력 깎아주기


                    Thread.Sleep(1000);
                }
            }

            SceneManager.instance.Menu(Monster_Att, null , DungeonScene.instance.DrawMonster_Info);

        }
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
