using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Team_SpartaTextRPG
{
    enum Dungeon_Level { Level_1 , Level_2, Level_3, Level_Boss }
    internal class DungeonScene : Helper.Singleton<DungeonScene>
    {
        Player player = GameManager.instance.player;
        Monster[] monsters = new Monster[4];
        private int MonsterCount = 0;

        Dungeon_Level DungeonLevel = Dungeon_Level.Level_1;
        private int Dungeon_ClearCount = 0;
        private int Dungeon_MaxCount = 3;
        private int total_ClearCount = 0;

        // 던전 화면
        public void Dungeon_Title()
        {
            if(DungeonLevel == Dungeon_Level.Level_Boss)
            {
                TitleManager.instance.WriteTitle($"던전 (!! BOSS STAGE !!)", ConsoleColor.DarkRed);
                ScreenManager.instance.AsyncVideo("./resources/dungeon.gif",_frame:100, _color: ConsoleColor.DarkRed);
            }
            else
            {
                TitleManager.instance.WriteTitle($"던전 ({(int)DungeonLevel + 1} - {Dungeon_ClearCount + 1})", ConsoleColor.DarkRed);
                ScreenManager.instance.AsyncVideo("./resources/dungeon.gif",_frame:100, _color: ConsoleColor.Magenta);
            }

            InputKeyManager.instance.ArtMenu(
                ($"Stage 입장", $"던전으로 입장합니다.", () => Select_Stage(1)), 
                ($"돌아가기", "마을로 나갑니다.", () => {TownScene.instance.Game_Main(); }));
        }

        public void Select_Stage(int _index)
        {
            if (_index == 1)
            {
                //몬스터 Init
                InitStage(DungeonLevel);
                DungeonMenu();
            }
        }

        // 층에 나오기 전에 나오는 몬스터 설정
        public void InitStage(Dungeon_Level _DungeonLevel)
        {
            for(int i = 0; i < monsters.Length; i++) { monsters[i] = null; }
            Random_Monster(_DungeonLevel);
        }

        private void DungeonMenu()
        {
            // 1. 싸우기
            // 2. 아이템 사용
            // 0. 후퇴
            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i] != null)
                {
                    if (monsters[i].IsDead == false)
                    {
                        Console.WriteLine($"{i + 1}.   이름 : {monsters[i].Name}   |   레벨: {monsters[i].Level}   |  HP : {monsters[i].HP} / {monsters[i].MaxHP}");
                    }
                    else
                    {
                        Utill.ColorWrite($"{i + 1}.   이름 : {monsters[i].Name}   |   레벨: {monsters[i].Level}   |  HP : {monsters[i].HP} / {monsters[i].MaxHP} |", ConsoleColor.DarkGray);
                        Utill.ColorWriteLine("\tDead", ConsoleColor.Red);
                    }
                }
            }
            Console.WriteLine("===========================================================\n");
            Console.WriteLine($"이름 : {player.Name}  |  Lv. {player.Level}  |  플레이어의 체력 : {player.HP}  |  플레이어의 마나 : {player.MP}");
            Console.WriteLine();
            Console.WriteLine("1. [ 공격 ]");
            Console.WriteLine("2. [ 아이템 사용 ]");
            Console.WriteLine("3. [ 스킬 사용 ]");
            Console.WriteLine("0. [ 도망가기 ]");
            SceneManager.instance.Menu(DungeonMenu, Dungeon_Title, DungeonMenu_Fight, DungeonMenu_Use_Item, DungeonMenu_Skill_Select);
        }

        // 현재 몬스터를 Select_Stage로 출력
        public void DungeonMenu_Fight()
        {
            TitleManager.instance.WriteTitle("던전 - 전투");

            StringBuilder sb = new();

            List<(string _menuName, string? _explanation, Action? _action)> tempActions = new List<(string _menuName, string? _explanation, Action? _action)>();

            int startMonsterX = 24;
            int startMonsterY = 3;
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
                    ScreenManager.instance.AsyncText($"Lv.{monsters[temp].Level} {monsters[temp].Name} ({monsters[temp].HP} / {monsters[temp].MaxHP})", _startX: startMonsterX+temp*24, _startY: startMonsterY+monsterSizeY+1, _color:ConsoleColor.Cyan);
                }
                else
                {
                    tempActions.Add(($"{monsters[temp].Name}", $"{monsters[temp].Name}는 이미 사망 했습니다.", null));
                    ScreenManager.instance.AsyncUnitVideo(monsters[temp].FilePath.die, startX: startMonsterX+temp*24, startY: startMonsterY, videoSizeX: monsterSizeX, videoSizeY: monsterSizeY, _isContinue: false, _isReversal:true, _frame:100);
                    ScreenManager.instance.AsyncText($"Lv.{monsters[temp].Level} {monsters[temp].Name} ({monsters[temp].HP} / {monsters[temp].MaxHP})", _startX: startMonsterX+temp*24, _startY: startMonsterY+monsterSizeY+1, _color:ConsoleColor.DarkGray);
                    ScreenManager.instance.AsyncText("\tDead", _startX: startMonsterX+temp*24, _startY: startMonsterY+monsterSizeY+2, _color:ConsoleColor.Red);
                }
            }

            tempActions.Add(($"돌아가기", "마을로 나갑니다.", () => {DungeonScene.instance.Dungeon_Title(); }));

            ScreenManager.instance.AsyncText($"{player.Name} ({player.Job})", _startX: 1, _startY: startMonsterY+monsterSizeY+1, _color:ConsoleColor.Green);
            ScreenManager.instance.AsyncText($"Lv.{player.Level}", _startX: 1, _startY: startMonsterY+monsterSizeY+2);
            ScreenManager.instance.AsyncText($"HP   {player.HP}/{player.MaxHP}", _startX: 1, _startY: startMonsterY+monsterSizeY+3,_color:ConsoleColor.Red);
            ScreenManager.instance.AsyncText($"MP   {player.MP}/{player.MaxMP}", _startX: 1, _startY: startMonsterY+monsterSizeY+4,_color:ConsoleColor.Blue);
            ScreenManager.instance.AsyncText($"Gold {player.Gold}G", _startX: 1, _startY: startMonsterY+monsterSizeY+5,_color:ConsoleColor.Yellow);


            InputKeyManager.instance.ArtMenu(tempActions.ToArray());
        }
        //아이템 사용
        private void DungeonMenu_Use_Item()
        {
            Usable_Item usable_Item;
            List<Action> tempActions = new List<Action>();
            tempActions.Add(DungeonScene.instance.DungeonMenu);
            Console.WriteLine();
            Console.WriteLine("[소비 아이템 목록]");
            Console.WriteLine();
            if (player.Inven_Usable_Item.Count <= 0)
            {
                Console.WriteLine("보유 중인 소비 아이템이 없습니다.");
            }
            for (int i = 0; i < player.Inven_Usable_Item.Count; i++)
            {
                Usable_Item currentItem = player.Inven_Usable_Item[i];
                tempActions.Add(() =>
                {
                    currentItem.Use(currentItem);
                    SceneManager.instance.GoMenu(DungeonMenu);
                });
                Console.WriteLine($"{i + 1}.   {player.Inven_Usable_Item[i].Name}   |   {player.Inven_Usable_Item[i].Description}   |   {player.Inven_Usable_Item[i].HporMp()}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            SceneManager.instance.Menu(DungeonMenu_Use_Item, tempActions.ToArray());
        }

        // 랜덤 사용해서 몬스터 랜덤 소환
        public void Random_Monster(Dungeon_Level _DungeonLevel)
        {
            Random ran = new Random();
            MonsterCount = ran.Next(1, 5);

            // 랜덤 몬스터 스폰 개수
            // 스테이지마다 다르게 몬스터 생성

            switch(_DungeonLevel)
            {
                case Dungeon_Level.Level_1:
                    for (int i = 0; i < MonsterCount; i++)
                    {
                        int Rand = ran.Next(0, 100);

                        if (Rand < 33)
                        {
                            monsters[i] = new Monster("C# 기초, 심화", 3, 15, 15, 8, 1, Utill.CS_PATH);
                        }
                        else if (Rand < 66)
                        {
                            monsters[i] = new Monster("TIL", 3, 15, 15, 8, 1, Utill.TIL_PATH);
                        }
                        else
                        {
                            monsters[i] = new Monster("9to9", 3, 15, 15, 8, 1, Utill.NineToNine_PATH);
                        }
                    }
                    break;
                case Dungeon_Level.Level_2:
                    for (int i = 0; i < MonsterCount; i++)
                    {
                        int Rand = ran.Next(0, 100);

                        if (Rand < 33)
                        {
                            monsters[i] = new Monster("Unity 2D", 3, 15, 15, 8, 1, Utill.Unity2D_PATH);
                        }
                        else if (Rand < 66)
                        {
                            monsters[i] = new Monster("Unity 입문", 3, 15, 15, 8, 1, Utill.AssetStore_PATH);
                        }
                        else
                        {
                            monsters[i] = new Monster("자료구조", 3, 15, 15, 8, 1, Utill.Algorithm_PATH);
                        }
                    }
                    break;
                case Dungeon_Level.Level_3:
                    for (int i = 0; i < MonsterCount; i++)
                    {
                        int Rand = ran.Next(0, 100);


                        if (Rand < 33)
                        {
                            monsters[i] = new Monster("실전 프로젝트", 7, 26, 26, 20, 4, Utill.RealProject_PATH);
                        }
                        else if (Rand < 66)
                        {
                            monsters[i] = new Monster("Unity 3D", 5, 22, 22, 10, 5, Utill.Unity3D_PATH);
                        }
                        else
                        {
                            monsters[i] = new Monster("팀 프로젝트", 3, 15, 15, 8, 1, Utill.TeamProject_PATH);
                        }

                    }
                    break;
                case Dungeon_Level.Level_Boss:
                    monsters[0] = new Monster("면접", 12, 50, 50, 30, 25, Utill.BOSS_PATH);
                    MonsterCount = 1;
                    break;
            }
        }

        // 플레이어가 몬스터를 공격
        public void Player_Att(int input)
        {
            //플레이어가 몬스터를 공격시 치명타 함수호출
            bool isCritical = false;
            int Criticaldamage = player.CriticalAttack(player.FinalDamage(), ref isCritical);

            if (monsters[input - 1].IsAvoid(10.0f) == true) // 회피를 하면 ?
            {
                // 회피
                Utill.ColorWriteLine($"{monsters[input - 1].Name}는(은) {player.Name}의 공격을 회피했다 !.\n", ConsoleColor.Cyan);
            }
            else
            {
                // 맞음
                monsters[input - 1].HP = monsters[input - 1].HP - Criticaldamage;

                MonsterDeadCheck(monsters[input - 1]);

                Utill.ColorWriteLine($"{player.Name} 공격", ConsoleColor.Blue);
                if (isCritical)
                {
                    Utill.ColorWriteLine($"{monsters[input - 1].Name}는(은) 강력한{Criticaldamage}의 데미지를 받았다.\n", ConsoleColor.Magenta);
                }
                else
                {
                    Utill.ColorWriteLine($"{monsters[input - 1].Name}는(은) {Criticaldamage}의 데미지를 받았다.\n");
                }
            }

            player.CountBuff();

            Thread.Sleep(1000);

            if (DeadCount())
            {
                SceneManager.instance.GoMenu(Stage_Clear);
            }
            else
            {
                Monster_Att();
            }
        }
        private void MonsterDeadCheck(Monster _monster)
        {
            // 몬스터 잡았을 때
            if (_monster.HP <= 0)
            {
                QuestManager.instance.MonsterCount(_monster.Name);
                Random rand = new Random();
                int rewardGold = rand.Next((total_ClearCount + 1) * 100, (total_ClearCount + 1) * 200);
                int rewardExp = rand.Next((total_ClearCount + 1) * 30, (total_ClearCount + 1) * 60);

                player.Gold += rewardGold;
                player.Exp += rewardExp;

                player.LevelUp();
            }
        }

        // 몬스터 공격
        public void Monster_Att()
        {
            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i] != null)
                {
                    if (monsters[i].IsDead == false)
                    {
                        // 여기에 몬스터 오차 범위 넣기
                        float AttRangeDamage = monsters[i].Monster_AttDamage_Range();
                        int totalDamage = (int)Math.Round(AttRangeDamage) - player.FinalDefense();

                        if (totalDamage < 0)
                        {
                            totalDamage = 0;
                        }

                        // 플레이어 체력 깎아주기
                        player.HP = (int)(player.HP - (totalDamage));

                        Utill.ColorWriteLine($"{monsters[i].Name} 공격", ConsoleColor.Red);
                        Utill.ColorWriteLine($"{player.Name}는(은) {totalDamage}의 데미지를 받았다.\n");

                        Thread.Sleep(1000);

                        if (player.IsDead == true)
                        {
                            break;
                        }
                    }
                }
            }

            if (player.IsDead == true)
            {
                SceneManager.instance.GoMenu(Stage_failed);
            }
            else
            {
                SceneManager.instance.GoMenu(DungeonMenu);
            }
        }

        // 몬스터가 전부 죽었는지 확인  지금 4마리 중 2마리가 null이라 deadCount에 포함이 안됨
        public bool DeadCount()
        {
            int deadCount = 0;

            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i] != null)
                {
                    if (monsters[i].IsDead == true)
                    {
                        deadCount++;
                    }
                }
            }

            if (MonsterCount == deadCount)
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
            total_ClearCount++;

            //보스 클리어
            if (DungeonLevel == Dungeon_Level.Level_Boss)
            {
                SceneManager.instance.GoMenu(EndingScene.instance.End);
            }

            Console.WriteLine("Stage Clear");
            Dungeon_Reward();
            Console.WriteLine("0. [ 나가기 ]");

            //보상

            DungeonLevelUp();
            SceneManager.instance.Menu(Stage_Clear, Dungeon_Title);
        }
        private void Dungeon_Reward()
        {
            player.Gold += total_ClearCount * 1000;
            Console.Write("클리어로 획득한 금화 : ");
            Utill.ColorWriteLine($"{total_ClearCount * 1000}");
            Console.Write("현재 보유 금화 : ");
            Utill.ColorWriteLine($"{player.Gold}");
            Console.WriteLine();

            /* 
             * 1 - 1 : 1000
             * 1 - 2 : 2000
             * 1 - 3 : 3000
             * 2 - 1 : 4000
             * 2 - 2 : 5000
             * 2 - 3 : 6000
             * 3 - 1 : 7000
             * ...
             */
            player.Exp += total_ClearCount * 100;
            player.LevelUp();

            Console.Write("클리어로 획득한 경험치 : ");
            Utill.ColorWriteLine($"{total_ClearCount * 100}" , ConsoleColor.DarkCyan);

            Console.Write("Lv : ");
            Utill.ColorWriteLine($"{player.Level}", ConsoleColor.Blue);

            Console.Write($"경험치 : ");
            Utill.ColorWriteLine($"{player.Exp} / {player.MaxExp}", ConsoleColor.DarkCyan);
            Console.WriteLine();
            Console.WriteLine("==============================");
        }
        private void DungeonLevelUp()
        {
            Dungeon_ClearCount++;

            if (Dungeon_ClearCount >= Dungeon_MaxCount)
            {
                Dungeon_ClearCount = 0;
                
                if(DungeonLevel < Dungeon_Level.Level_Boss)
                    DungeonLevel++;
            }
        }

        public void DungeonMenu_Skill_Select()
        {
            List<Action> skillActions = new List<Action>();
            skillActions.Add(DungeonScene.instance.DungeonMenu);
            Console.WriteLine("[사용 가능한 스킬 목록]");

            for (int i = 0; i < player.SkillList.Count; i++)
            {
                int index = i;
                    string skillDescription = SkillManager.instance.GetSkillDescription(player, player.SkillList[i]);

                if(SkillManager.instance.GetSkillDamage(player, player.SkillList[i]) >= 0){

                    Console.WriteLine($"{i + 1}. {skillDescription}");

                    skillActions.Add(() =>{ DungeonMenu_Monster_Select(index);});
                } else {
                    Utill.ColorWriteLine($"{i + 1}. {skillDescription}", ConsoleColor.DarkGray);

                    skillActions.Add(null);
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            SceneManager.instance.Menu(DungeonMenu_Skill_Select, skillActions.ToArray());
        }

        public void DungeonMenu_Monster_Select(int skillIndex)
        {
            List<Action> tempActions = new List<Action>();
            tempActions.Add(DungeonScene.instance.DungeonMenu);

            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i] != null)
                {
                    if (!monsters[i].IsDead)
                    {
                        int temp = i;
                        tempActions.Add(() => DungeonMenu_Skill_Use(temp, skillIndex));
                        Console.WriteLine($"{i + 1}.   이름 : {monsters[i].Name}   |   레벨: {monsters[i].Level}   |  HP : {monsters[i].HP} / {monsters[i].MaxHP}");
                    }
                    else
                    {
                        tempActions.Add(null);
                        Utill.ColorWrite($"{i + 1}.   이름 : {monsters[i].Name}   |   레벨: {monsters[i].Level}   |  HP : {monsters[i].HP} / {monsters[i].MaxHP} |", ConsoleColor.DarkGray);
                        Utill.ColorWriteLine("\tDead", ConsoleColor.Red);
                    }
                }
            }

            Console.WriteLine("===========================================================\n");
            Console.WriteLine($"이름 : {player.Name}  |  Lv. {player.Level}  |  플레이어의 체력 : {player.HP}  |  플레이어의 마나 : {player.MP}");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            SceneManager.instance.Menu(() => DungeonMenu_Monster_Select(skillIndex), tempActions.ToArray());

        }

        public void DungeonMenu_Skill_Use(int targetIndex, int skillIndex)
        {
            float baseDamage = SkillManager.instance.GetSkillDamage(player, player.SkillList[skillIndex]);

            bool isCritical = false;
            int Criticaldamage = player.CriticalAttack(baseDamage, ref isCritical);

            int finalDamage = (int)Math.Round(baseDamage);

            SkillManager.instance.ExecuteSkillCost(player, player.SkillList[skillIndex]);

            if (isCritical)
            {
                finalDamage = Criticaldamage;
            }
            monsters[targetIndex].HP = monsters[targetIndex].HP - finalDamage;
            Utill.ColorWriteLine($"{player.Name} 스킬 사용", ConsoleColor.Blue);

            MonsterDeadCheck(monsters[targetIndex]);

            if (isCritical)
            {
                Utill.ColorWriteLine($"{monsters[targetIndex].Name}는(은) 강력한 {finalDamage}의 데미지를 받았다.\n", ConsoleColor.Magenta);
            }
            else
            {
                Utill.ColorWriteLine($"{monsters[targetIndex].Name}는(은) {finalDamage}의 데미지를 받았다.\n");
            }

            player.CountBuff();

            Thread.Sleep(1000);

            if (DeadCount())
            {
                SceneManager.instance.GoMenu(Stage_Clear);
            }
            else
            {
                Monster_Att();
            }
        }
    }
}
