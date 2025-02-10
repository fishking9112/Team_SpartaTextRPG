using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
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


        // 층에 나오기 전에 나오는 몬스터 설정
        public DungeonScene()
        {
            // 몬스터 정보
            monsters[0] = new Monster("슬라임", 1, 1, 15, 1, 1, Utill.SLIME_PATH);
            monsters[1] = new Monster("해골", 3, 1, 22, 3, 5, Utill.SKELETON_PATH);
            monsters[2] = new Monster("오크", 5, 1, 26, 8, 4, Utill.SLIME_PATH);
            monsters[3] = new Monster("맼닠젘", 10, 1, 50, 10, 25, Utill.SLIME_PATH);
        }

        // 던전 화면
        public void Dungeon_Title()
        {
            TitleManager.instance.WriteTitle("던전");

            ScreenManager.instance.AsyncVideo("./resources/dungeon.gif",_frame:100);

            InputKeyManager.instance.ArtMenu(
                ($"Stage 입장", $"던전으로 입장합니다.", () => Select_Stage(1)), 
                ($"돌아가기", "마을로 나갑니다.", () => {TownScene.instance.Game_Main(); }));
        }

        public void Select_Stage(int _index)
        {
            if (_index == 1)
            {
                // 던전 진입시 몬스터 소환
                DrawMonster_Info();
            }

        }


        // 현재 몬스터를 Select_Stage로 출력
        public void DrawMonster_Info()
        {
            TitleManager.instance.WriteTitle("던전 - 전투");

            StringBuilder sb = new();

            List<(string _menuName, string? _explanation, Action? _action)> tempActions = new List<(string _menuName, string? _explanation, Action? _action)>();

            int startMonsterX = 24;
            int startMonsterY = 1;
            int monsterSizeX = 12;
            int monsterSizeY = 15;
            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i] != null)
                {
                    int temp = i;
                    tempActions.Add(($"{monsters[temp].Name}", $"{monsters[temp].Name}를 공격합니다.", () => Player_Att(temp + 1)));
                    sb.AppendLine($"{i + 1}.   이름 : {monsters[i].Name}   |   레벨: {monsters[i].Level}   |  HP : {monsters[i].HP} / {monsters[i].MaxHP}");
                    ScreenManager.instance.AsyncUnitVideo(monsters[temp].FilePath.idle, startX: startMonsterX+i*24, startY: startMonsterY, videoSizeX: monsterSizeX, videoSizeY: monsterSizeY, _isContinue: true, _isReversal:true, _frame:100);
                    ScreenManager.instance.AsyncText($"Lv.{monsters[i].Level} {monsters[i].Name} ({monsters[i].HP} / {monsters[i].MaxHP})", _startX: startMonsterX+i*24, _startY: monsterSizeY+2);
                }
            }

            tempActions.Add(($"돌아가기", "마을로 나갑니다.", () => {DungeonScene.instance.Dungeon_Title(); }));

            InputKeyManager.instance.ArtMenu(tempActions.ToArray());
        }

        // 플레이어가 몬스터를 공격
        public void Player_Att(int input)
        {
            monsters[input - 1].HP = (int)(monsters[input - 1].HP - player.AttDamage);

            Utill.ColorWriteLine($"{player.Name} 공격", ConsoleColor.Blue);
            Utill.ColorWriteLine($"{monsters[input - 1].Name}는(은) {player.AttDamage}의 데미지를 받았다.");
            Thread.Sleep(1000);

            //몬스터 체력 검사
            if (monsters[input - 1].IsDead != false)
            {
                //죽은 몬스터 배열에서 빼주기
                monsters[input - 1] = null;
                Monster_Att();

                SceneManager.instance.GoMenu(DrawMonster_Info);
            }

            //피 없으면 죽이고 
            else if (monsters[input] == null)
            {
                SceneManager.instance.GoMenu(Stage_Clear);
            }

            // monsters[0] = null

            //배열에 몬스터가 없으면 null

            //모든 몬스터가 죽으면 == monsters[i] == null

            //SceneManager.instance.GoMenu(DungeonScene.instance.다른 곳으로);

        }

        public void Monster_Att()
        {
            for (int i = 0; i < monsters.Length; i++)
            {

                if (monsters[i] != null)
                {
                    // 플레이어 체력 깎아주기
                    player.HP = (int)(player.HP - monsters[i].AttDamage);

                    Utill.ColorWriteLine($"{monsters[i].Name} 공격", ConsoleColor.Red);
                    Utill.ColorWriteLine($"{player.Name}는(은) {monsters[i].AttDamage}의 데미지를 받았다.");

                    Thread.Sleep(1000);
                }
            }

            SceneManager.instance.GoMenu(DrawMonster_Info);

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

        public void Stage_Clear()
        {
            Console.WriteLine("TEST");

        }
    }
}
