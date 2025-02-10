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
            int startMonsterY = 4;
            int monsterSizeX = 12;
            int monsterSizeY = 15;
            
            ScreenManager.instance.AsyncUnitVideo(player.FilePath.idle, startX: 0, startY: startMonsterY, videoSizeX: monsterSizeX, videoSizeY: monsterSizeY, _isContinue: true, _isReversal:true, _frame:33);
            for (int i = 0; i < monsters.Length; i++)
            {
                int temp = i;
                if (monsters[i].IsDead == false)
                {
                    tempActions.Add(($"{monsters[temp].Name}", $"{monsters[temp].Name}를 공격합니다.", () => Player_Att(temp + 1)));
                    sb.AppendLine($"{temp + 1}.   이름 : {monsters[temp].Name}   |   레벨: {monsters[temp].Level}   |  HP : {monsters[temp].HP} / {monsters[temp].MaxHP}");
                    ScreenManager.instance.AsyncUnitVideo(monsters[temp].FilePath.idle, startX: startMonsterX+temp*24, startY: startMonsterY, videoSizeX: monsterSizeX, videoSizeY: monsterSizeY, _isContinue: true, _isReversal:true, _frame:100);
                    ScreenManager.instance.AsyncText($"Lv.{monsters[temp].Level} {monsters[temp].Name} ({monsters[temp].HP} / {monsters[temp].MaxHP})", _startX: startMonsterX+temp*24, _startY: startMonsterY+monsterSizeY+1);
                }
                else
                {
                    tempActions.Add(($"{monsters[temp].Name}", $"{monsters[temp].Name}는 이미 사망 했습니다.", null));
                    ScreenManager.instance.AsyncUnitVideo(monsters[temp].FilePath.die, startX: startMonsterX+temp*24, startY: startMonsterY, videoSizeX: monsterSizeX, videoSizeY: monsterSizeY, _isContinue: false, _isReversal:true, _frame:100);
                    ScreenManager.instance.AsyncText($"Lv.{monsters[temp].Level} {monsters[temp].Name} ({monsters[temp].HP} / {monsters[temp].MaxHP})", _startX: startMonsterX+temp*24, _startY: startMonsterY+monsterSizeY+1);
                }
            }

            tempActions.Add(($"돌아가기", "마을로 나갑니다.", () => {DungeonScene.instance.Dungeon_Title(); }));


            ScreenManager.instance.AsyncText($"Lv.{player.Name} ({player.Job})", _startX: 1, _startY: startMonsterY+monsterSizeY+1);
            ScreenManager.instance.AsyncText($"HP   {player.HP}/{player.MaxHP}", _startX: 1, _startY: startMonsterY+monsterSizeY+2,_color:ConsoleColor.Red);
            ScreenManager.instance.AsyncText($"MP   {player.MP}/{player.MaxMP}", _startX: 1, _startY: startMonsterY+monsterSizeY+3,_color:ConsoleColor.Blue);
            ScreenManager.instance.AsyncText($"Gold {player.Gold}G", _startX: 1, _startY: startMonsterY+monsterSizeY+4,_color:ConsoleColor.Yellow);
            

            InputKeyManager.instance.ArtMenu(tempActions.ToArray());
        }

        // 랜덤 사용해서 몬스터 랜덤 소환 및 공격시 치명타 및 회피 설정

        // 플레이어가 몬스터를 공격
        public void Player_Att(int input)
        {
            monsters[input - 1].HP = (int)(monsters[input - 1].HP - player.AttDamage);

            Utill.ColorWriteLine($"{player.Name} 공격", ConsoleColor.Blue);
            Utill.ColorWriteLine($"{monsters[input - 1].Name}는(은) {player.AttDamage}의 데미지를 받았다.\n");

            Thread.Sleep(1000);

            if(DeadCount())
            {
                SceneManager.instance.GoMenu(Stage_Clear);
            }
            else
            {
                Monster_Att();
            }
        }

        // 몬스터 공격
        public void Monster_Att()
        {
            for (int i = 0; i < monsters.Length; i++)
            {

                if (monsters[i].IsDead == false)
                {
                    // 플레이어 체력 깎아주기
                    player.HP = (int)(player.HP - monsters[i].AttDamage);

                    Utill.ColorWriteLine($"{monsters[i].Name} 공격", ConsoleColor.Red);
                    Utill.ColorWriteLine($"{player.Name}는(은) {monsters[i].AttDamage}의 데미지를 받았다.\n");

                    Thread.Sleep(1000);

                    if (player.IsDead == true)
                    {
                        break;                        
                    }

                }
            }

            if (player.IsDead == true)
            {
                SceneManager.instance.GoMenu(Stage_failed);
            }
            else
            {
                SceneManager.instance.GoMenu(DrawMonster_Info);
            }
        }

        // 몬스터가 전부 죽었는지 확인
        public bool DeadCount()
        {
            int deadCount = 0;

            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i].IsDead == true)
                {
                    deadCount++;
                }
            }

            if (monsters.Length == deadCount)
            {
                return true;
            }

            return false;
        }

        //패배 씬
        public void Stage_failed()
        {
            Console.WriteLine("Stage Failed...");

            Thread.Sleep(1000);

            SceneManager.instance.GoMenu(TownScene.instance.Game_Main);
        }

        // 스테이지 클리어 시 띄움 ( 보상 추가 )
        public void Stage_Clear()
        {
            Console.WriteLine("Stage Clear");

            Console.WriteLine("0. [ 나가기 ]");

            SceneManager.instance.Menu(Stage_Clear, Dungeon_Title);
        }
    }
}
