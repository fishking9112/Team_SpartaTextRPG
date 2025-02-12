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
    public enum Dungeon_Level { Level_1 , Level_2, Level_3, Level_Boss }
    
    public class DungeonData{
        public Dungeon_Level DungeonLevel = Dungeon_Level.Level_1;
        public int Dungeon_ClearCount = 0;

        public DungeonData(){}
    }

    internal class DungeonScene : Helper.Singleton<DungeonScene>
    {
        Player player = GameManager.instance.player;
        Monster[] monsters = new Monster[4];
        private int MonsterCount = 0;

        public DungeonData dungeonData = new DungeonData();
        private int Dungeon_MaxCount = 3;
        private int total_ClearCount = 0;


        // 입장 던전 확인용 (클리어 시 Dungeon_ClearCount 이 ++ 되는지 확인)
        Dungeon_Level currentDungeonLevel = Dungeon_Level.Level_1;

        // Art용
        int startMonsterX = 24;
        int startMonsterY = 3;
        int monsterSizeX = 12;
        int monsterSizeY = 15;

        // 던전 화면
        public void Dungeon_Title()
        {
            if(dungeonData.DungeonLevel == Dungeon_Level.Level_Boss)
            {
                TitleManager.instance.WriteTitle($"던전 (!! BOSS STAGE !!)", ConsoleColor.DarkRed);
                ScreenManager.instance.AsyncVideo("./resources/dungeon.gif",_frame:100, _color: ConsoleColor.DarkRed);
            }
            else
            {
                TitleManager.instance.WriteTitle($"던전 ({(int)dungeonData.DungeonLevel + 1} - {dungeonData.Dungeon_ClearCount + 1})", ConsoleColor.Cyan);
                ScreenManager.instance.AsyncVideo("./resources/dungeon.gif",_frame:100, _color: ConsoleColor.Cyan);
            }

            var tempActions = new List<(string _menuName, string? _explanation, Action? _action)>();

            if(player.HP == 0){
                InputKeyManager.instance.ArtMenu(($"돌아가기", $"체력이 0인 상태로는 던전에 입장할 수 없습니다...", TownScene.instance.Game_Main));
            } else {
                if(dungeonData.DungeonLevel >= Dungeon_Level.Level_Boss){
                    tempActions.Add(("보스 스테이지","", () => { Select_Stage(Dungeon_Level.Level_Boss); }));
                }
                
                if(dungeonData.DungeonLevel >= Dungeon_Level.Level_3){
                    tempActions.Add(("3Stage 입장","", () => { Select_Stage(Dungeon_Level.Level_3); }));
                }
                
                if(dungeonData.DungeonLevel >= Dungeon_Level.Level_2){
                    tempActions.Add(("2Stage 입장","", () => { Select_Stage(Dungeon_Level.Level_2); }));
                }
                
                if(dungeonData.DungeonLevel >= Dungeon_Level.Level_1){
                    tempActions.Add(("1Stage 입장","", () => { Select_Stage(Dungeon_Level.Level_1); }));
                }
                
                tempActions.Add(($"돌아가기", "마을로 나갑니다.", () => { TownScene.instance.Game_Main(); }));
                
                InputKeyManager.instance.ArtMenu(tempActions.ToArray());
            }
        }

        public void Select_Stage(Dungeon_Level _DungeonLevel)
        {
            currentDungeonLevel = _DungeonLevel;
            InitStage(_DungeonLevel);
            DungeonMenu();
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
            
            if(dungeonData.DungeonLevel == Dungeon_Level.Level_Boss)
            {
                TitleManager.instance.WriteTitle($"던전 (!! BOSS STAGE !!) - 전투", ConsoleColor.DarkRed);
            }
            else
            {
                TitleManager.instance.WriteTitle($"던전 ({(int)dungeonData.DungeonLevel + 1} - {dungeonData.Dungeon_ClearCount + 1}) - 전투", ConsoleColor.Cyan);
            }


            ArtUnitShow();
            

            InputKeyManager.instance.ArtMenu(
                ($"공격", "지정된 대상을 공격합니다.", DungeonMenu_Fight),
                ($"스킬 사용", "지정된 대상에게 스킬을 사용합니다.", DungeonMenu_Skill_Select),
                ($"아이템 사용", "인벤토리에서 아이템을 사용합니다.", DungeonMenu_Use_Item),
                ($"도망가기", "당신은 겁쟁이 입니다! 무서워서 도망칩니다!", Dungeon_Title));
        }

        // 현재 몬스터를 Select_Stage로 출력
        public void DungeonMenu_Fight()
        {
            if(dungeonData.DungeonLevel == Dungeon_Level.Level_Boss)
            {
                TitleManager.instance.WriteTitle($"던전 (!! BOSS STAGE !!) - 전투 - 공격", ConsoleColor.DarkRed);
            }
            else
            {
                TitleManager.instance.WriteTitle($"던전 ({(int)dungeonData.DungeonLevel + 1} - {dungeonData.Dungeon_ClearCount + 1}) - 전투 - 공격", ConsoleColor.Cyan);
            }

            ArtUnitShow();

            var tempActions = new List<(string _menuName, string? _explanation, Action? _action)>();
            
            for (int i = 0; i < monsters.Length; i++)
            {
                int temp = i;
                if (monsters[i] != null)
                {
                    if (monsters[i].IsDead == false)
                    {
                        tempActions.Add(($"{monsters[temp].Name}", $"{monsters[temp].Name}를 공격합니다.", () => Player_Att(temp + 1)));
                    }
                    else
                    {
                        tempActions.Add(($"{monsters[temp].Name}", $"{monsters[temp].Name}는 이미 사망 했습니다.", null));
                    }
                }
            }

            tempActions.Add(($"취소", "돌아갑니다", DungeonMenu));

            InputKeyManager.instance.ArtMenu(tempActions.ToArray());
        }
        //아이템 사용
        private void DungeonMenu_Use_Item()
        {
            if(dungeonData.DungeonLevel == Dungeon_Level.Level_Boss)
            {
                TitleManager.instance.WriteTitle($"던전 (!! BOSS STAGE !!) - 전투 - 아이템 사용", ConsoleColor.DarkRed);
            }
            else
            {
                TitleManager.instance.WriteTitle($"던전 ({(int)dungeonData.DungeonLevel + 1} - {dungeonData.Dungeon_ClearCount + 1}) - 전투 - 아이템 사용", ConsoleColor.Cyan);
            }


            StringBuilder sb = new();

            Usable_Item usable_Item;
            List<Action> tempActions = new List<Action>();
            tempActions.Add(DungeonScene.instance.DungeonMenu);


            sb.AppendLine();
            sb.AppendLine("[소비 아이템 목록]");
            sb.AppendLine();
            if (player.Inven_Usable_Item.Count <= 0)
            {
                sb.AppendLine("보유 중인 소비 아이템이 없습니다.");
            }
            for (int i = 0; i < player.Inven_Usable_Item.Count; i++)
            {
                Usable_Item currentItem = player.Inven_Usable_Item[i];
                tempActions.Add(() =>
                {
                    currentItem.Use(currentItem);
                    InputKeyManager.instance.GoMenu(DungeonMenu);
                });
                sb.AppendLine($"{i + 1}.   {player.Inven_Usable_Item[i].Name}   |   {player.Inven_Usable_Item[i].Description}   |   {player.Inven_Usable_Item[i].HporMp()}");
            }
            sb.AppendLine();
            sb.AppendLine("0. 나가기");

            ScreenManager.instance.AsyncText(sb);


            InputKeyManager.instance.InputMenu(DungeonMenu_Use_Item, "사용할 아이템 번호를 입력하세요 >> ", tempActions.ToArray());
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
                            monsters[i] = new Monster("C# 기초, 심화", 3, 15, 15, 10, 1, Utill.CS_PATH);
                        }
                        else if (Rand < 66)
                        {
                            monsters[i] = new Monster("TIL", 4, 20, 20, 15, 1, Utill.TIL_PATH);
                        }
                        else
                        {
                            monsters[i] = new Monster("9to9", 5, 25, 25, 20, 1, Utill.NineToNine_PATH);
                        }
                    }
                    break;
                case Dungeon_Level.Level_2:
                    for (int i = 0; i < MonsterCount; i++)
                    {
                        int Rand = ran.Next(0, 100);

                        if (Rand < 33)
                        {
                            monsters[i] = new Monster("Unity 2D", 6, 30, 30, 40, 5, Utill.Unity2D_PATH);
                        }
                        else if (Rand < 66)
                        {
                            monsters[i] = new Monster("Unity 입문", 8, 40, 40, 50, 5, Utill.AssetStore_PATH);
                        }
                        else
                        {
                            monsters[i] = new Monster("자료구조", 10, 50, 50, 80, 5, Utill.Algorithm_PATH);
                        }
                    }
                    break;
                case Dungeon_Level.Level_3:
                    for (int i = 0; i < MonsterCount; i++)
                    {
                        int Rand = ran.Next(0, 100);


                        if (Rand < 33)
                        {
                            monsters[i] = new Monster("실전 프로젝트", 15, 100, 100, 100, 30, Utill.RealProject_PATH);
                        }
                        else if (Rand < 66)
                        {
                            monsters[i] = new Monster("Unity 3D", 18, 150, 150, 120, 30, Utill.Unity3D_PATH);
                        }
                        else
                        {
                            monsters[i] = new Monster("팀 프로젝트", 20, 200, 200, 150, 30, Utill.TeamProject_PATH);
                        }

                    }
                    break;
                case Dungeon_Level.Level_Boss:
                    monsters[0] = new Monster("면접", 50, 5000, 5000, 300, 100, Utill.BOSS_PATH);
                    MonsterCount = 1;
                    break;
            }
        }

        // 플레이어가 몬스터를 공격
        public void Player_Att(int input)
        {
            if(dungeonData.DungeonLevel == Dungeon_Level.Level_Boss)
            {
                TitleManager.instance.WriteTitle($"던전 (!! BOSS STAGE !!) - 전투 - 공격 (결과창)", ConsoleColor.DarkRed);
            }
            else
            {
                TitleManager.instance.WriteTitle($"던전 ({(int)dungeonData.DungeonLevel + 1} - {dungeonData.Dungeon_ClearCount + 1}) - 전투 - 공격 (결과창)", ConsoleColor.Cyan);
            }
            
            //플레이어가 몬스터를 공격시 치명타 함수호출
            bool isCritical = false;
            int Criticaldamage = player.CriticalAttack(player.FinalDamage(), ref isCritical);

            StringBuilder sb = new();

            if (monsters[input - 1].IsAvoid(10.0f) == true) // 회피를 하면 ?
            {
                // 회피
                sb.AppendLine($"{monsters[input - 1].Name}는(은) {player.Name}의 공격을 회피했다...!\n");
                InputKeyManager.instance.MenuExplanation(sb.ToString(), _color: ConsoleColor.Magenta);
            }
            else
            {
                // 맞음
                monsters[input - 1].HP = monsters[input - 1].HP - Criticaldamage;

                MonsterDeadCheck(monsters[input - 1]);

                sb.AppendLine($"{player.Name} 공격");
                if (isCritical)
                {
                    sb.AppendLine($"{monsters[input - 1].Name}에게 {Criticaldamage}의 강력한 데미지를 입혔다.\n");
                }
                else
                {
                    sb.AppendLine($"{monsters[input - 1].Name}에게 {Criticaldamage}의 데미지를 입혔다.\n");
                }
                InputKeyManager.instance.MenuExplanation(sb.ToString(), _color: ConsoleColor.Green);
            }

            player.CountBuff();

            
            // 그래픽
            ArtUnitShow(input - 1);


            Utill.Sleep(1000);

            if (DeadCount())
            {
                InputKeyManager.instance.ArtMenu(("적을 모두 죽였습니다.",sb.ToString(),Stage_Clear));
            }
            else
            {
                InputKeyManager.instance.ArtMenu(("몬스터 턴으로...",sb.ToString(),Monster_Att));
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
            if(dungeonData.DungeonLevel == Dungeon_Level.Level_Boss)
            {
                TitleManager.instance.WriteTitle($"던전 (!! BOSS STAGE !!) - 전투 - 몬스터 턴", ConsoleColor.DarkRed);
            }
            else
            {
                TitleManager.instance.WriteTitle($"던전 ({(int)dungeonData.DungeonLevel + 1} - {dungeonData.Dungeon_ClearCount + 1}) - 전투 - 몬스터 턴", ConsoleColor.Cyan);
            }

            StringBuilder sb = new();
            
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
                        ScreenManager.instance.ClearScreen();

                        // 플레이어 체력 깎아주기
                        player.HP = (int)(player.HP - (totalDamage));
                        sb.AppendLine($"{monsters[i].Name}의 공격! => {totalDamage}의 데미지를 받았다!");
                        InputKeyManager.instance.MenuExplanation(sb.ToString(), _color:ConsoleColor.Red);

                        ArtUnitShow();

                        Utill.Sleep(1000);

                        if (player.IsDead == true)
                        {
                            break;
                        }
                    }
                }
            }


            if (player.IsDead == true)
            {
                InputKeyManager.instance.ArtMenu(("사망하셨습니다.",sb.ToString(),Stage_failed));
            }
            else
            {
                InputKeyManager.instance.ArtMenu(("다시 나의 턴으로...",sb.ToString(),DungeonMenu));
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
            // Console.WriteLine("Stage Failed...");

            // Utill.Sleep(1000);

            InputKeyManager.instance.GoMenu(TownScene.instance.Game_Main);
            // SceneManager.instance.GoMenu(TownScene.instance.Game_Main);
        }

        // 스테이지 클리어 시 띄움 ( 보상 추가 )
        public void Stage_Clear()
        {
            if(dungeonData.DungeonLevel == Dungeon_Level.Level_Boss)
            {
                TitleManager.instance.WriteTitle($"던전 (!! BOSS STAGE !!) - 클리어!", ConsoleColor.DarkRed);
            }
            else
            {
                TitleManager.instance.WriteTitle($"던전 ({(int)dungeonData.DungeonLevel + 1} - {dungeonData.Dungeon_ClearCount + 1}) - 클리어!", ConsoleColor.Cyan);
            }

            total_ClearCount++;

            //보스 클리어
            if (dungeonData.DungeonLevel == Dungeon_Level.Level_Boss && currentDungeonLevel == Dungeon_Level.Level_Boss)
            {
                InputKeyManager.instance.GoMenu(EndingScene.instance.End);
                return;
            }

            //던전 레벨업 (현재 입장한 던전이 현재 레벨과 맞다면 라운드 업)
            if(currentDungeonLevel == dungeonData.DungeonLevel){
                dungeonData.Dungeon_ClearCount++;
            }

            if (dungeonData.Dungeon_ClearCount >= Dungeon_MaxCount)
            {
                dungeonData.Dungeon_ClearCount = 0;
                
                if(dungeonData.DungeonLevel < Dungeon_Level.Level_Boss)
                    dungeonData.DungeonLevel++;
            }

            StringBuilder sb = new();
            //보상
            player.Gold += total_ClearCount * 1000;
            sb.Append("클리어로 획득한 금화 : ");
            sb.AppendLine($"{total_ClearCount * 1000}");
            sb.Append("현재 보유 금화 : ");
            sb.AppendLine($"{player.Gold}");
            sb.AppendLine();

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


            sb.Append("클리어로 획득한 경험치 : ");
            sb.AppendLine($"{total_ClearCount * 100}");

            sb.Append("Lv : ");
            sb.AppendLine($"{player.Level}");

            sb.Append($"경험치 : ");
            sb.AppendLine($"{player.Exp} / {player.MaxExp}");

            // 가방 이미지 보여주기
            ScreenManager.instance.AsyncImage("./resources/bag_open.png",_startX:80, _startY:2, imageSizeX:20, imageSizeY:20);

            // 클리어 보상 보여주기
            ScreenManager.instance.AsyncText(sb, _color:ConsoleColor.Green);


            InputKeyManager.instance.ArtMenu(("완료","스테이지 클리어 완료하였습니다.\n던전을 나갑니다.",Dungeon_Title));
        }

        public void DungeonMenu_Skill_Select()
        {
            if(dungeonData.DungeonLevel == Dungeon_Level.Level_Boss)
            {
                TitleManager.instance.WriteTitle($"던전 (!! BOSS STAGE !!) - 전투 - 스킬 사용!", ConsoleColor.DarkRed);
            }
            else
            {
                TitleManager.instance.WriteTitle($"던전 ({(int)dungeonData.DungeonLevel + 1} - {dungeonData.Dungeon_ClearCount + 1}) - 전투 - 스킬 사용!", ConsoleColor.Cyan);
            }

            List<(string _menuName, string? _explanation, Action? _action)> skillActions = new List<(string _menuName, string? _explanation, Action? _action)>();
            
            for (int i = 0; i < player.SkillList.Count; i++)
            {
                int index = i;
                string skillDescription = SkillManager.instance.GetSkillDescription(player, player.SkillList[i]);
                var sbSplit = skillDescription.Split(new[] { " : "}, StringSplitOptions.None);

                if(SkillManager.instance.GetSkillDamage(player, player.SkillList[i]) >= 0){
                    skillActions.Add((sbSplit[0],sbSplit[1],() =>{ DungeonMenu_Monster_Select(index);}));
                } else {
                    skillActions.Add((sbSplit[0],sbSplit[1], null));
                }
            }

            ArtUnitShow();

            skillActions.Add(("뒤로","뒤로 돌아갑니다.", DungeonMenu));

            InputKeyManager.instance.ArtMenu(skillActions.ToArray());
        }

        public void DungeonMenu_Monster_Select(int skillIndex)
        {
            if(dungeonData.DungeonLevel == Dungeon_Level.Level_Boss)
            {
                TitleManager.instance.WriteTitle($"던전 (!! BOSS STAGE !!) - 전투 - 적 선택", ConsoleColor.DarkRed);
            }
            else
            {
                TitleManager.instance.WriteTitle($"던전 ({(int)dungeonData.DungeonLevel + 1} - {dungeonData.Dungeon_ClearCount + 1}) - 전투 - 적 선택", ConsoleColor.Cyan);
            }

            // 그래픽
            ArtUnitShow();

            var tempActions = new List<(string _menuName, string? _explanation, Action? _action)>();
            
            for (int i = 0; i < monsters.Length; i++)
            {
                int temp = i;
                if (monsters[i] != null)
                {
                    if (monsters[i].IsDead == false)
                    {
                        tempActions.Add(($"{monsters[temp].Name}", $"{monsters[temp].Name}를 공격합니다.",() => DungeonMenu_Skill_Use(temp, skillIndex)));
                    }
                    else
                    {
                        tempActions.Add(($"{monsters[temp].Name}", $"{monsters[temp].Name}는 이미 사망 했습니다.", null));
                    }
                }
            }


            tempActions.Add(("취소","선택을 취소합니다.", DungeonMenu));

            InputKeyManager.instance.ArtMenu(tempActions.ToArray());
        }

        public void DungeonMenu_Skill_Use(int targetIndex, int skillIndex)
        {
            if(dungeonData.DungeonLevel == Dungeon_Level.Level_Boss)
            {
                TitleManager.instance.WriteTitle($"던전 (!! BOSS STAGE !!) - 전투 - 스킬 사용 (결과창)", ConsoleColor.DarkRed);
            }
            else
            {
                TitleManager.instance.WriteTitle($"던전 ({(int)dungeonData.DungeonLevel + 1} - {dungeonData.Dungeon_ClearCount + 1}) - 전투 - 스킬 사용 (결과창)", ConsoleColor.Cyan);
            }

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


            StringBuilder sb = new();

            string description = SkillManager.instance.GetSkillDescription(player, player.SkillList[skillIndex]).Split(" : ")[0];
            sb.AppendLine($"{description} << 스킬 사용!");

            MonsterDeadCheck(monsters[targetIndex]);

            if (isCritical)
            {
                sb.AppendLine($"{monsters[targetIndex].Name}에게 {finalDamage}의 강력한 데미지를 입혔다!!!\n");
            }
            else
            {
                sb.AppendLine($"{monsters[targetIndex].Name}에게 {finalDamage}의 데미지를 입혔다!\n");
            }

            player.CountBuff();

            InputKeyManager.instance.MenuExplanation(sb.ToString(), _color:ConsoleColor.Green);
            
            // 그래픽
            ArtUnitShow(targetIndex);

            Utill.Sleep(1000);

            if (DeadCount())
            {
                InputKeyManager.instance.ArtMenu(("적을 모두 죽였습니다.",sb.ToString(),Stage_Clear));
            }
            else
            {
                InputKeyManager.instance.ArtMenu(("몬스터 턴으로...",sb.ToString(),Monster_Att));
            }
        }

        public void ArtUnitShow(int _dieActionNum = -1){
            
            for (int i = 0; i < monsters.Length; i++)
            {
                int temp = i;
                if (monsters[i] != null)
                {
                    if (monsters[i].IsDead == false)
                    {
                        ScreenManager.instance.AsyncUnitVideo(monsters[temp].FilePath.idle, startX: startMonsterX+temp*24, startY: startMonsterY, videoSizeX: monsterSizeX, videoSizeY: monsterSizeY, _isContinue: true, _isReversal:true, _frame:100);
                        ScreenManager.instance.AsyncText($"Lv.{monsters[temp].Level} {monsters[temp].Name}", _startX: startMonsterX+temp*24, _startY: startMonsterY+monsterSizeY+1);
                        ScreenManager.instance.AsyncText($"({monsters[temp].HP} / {monsters[temp].MaxHP})", _startX: startMonsterX+temp*24, _startY: startMonsterY+monsterSizeY+2, _color:ConsoleColor.Green);
                    }
                    else
                    {
                        if (temp == _dieActionNum)
                        {
                            ScreenManager.instance.AsyncUnitVideo(monsters[temp].FilePath.die, startX: startMonsterX+temp*24, startY: startMonsterY, videoSizeX: monsterSizeX, videoSizeY: monsterSizeY, _isContinue: false, _isReversal:true, _color:ConsoleColor.DarkGray, _frame:100);
                        } else 
                        {
                            ScreenManager.instance.AsyncUnitVideo(monsters[temp].FilePath.end, startX: startMonsterX+temp*24, startY: startMonsterY, videoSizeX: monsterSizeX, videoSizeY: monsterSizeY, _isContinue: false, _isReversal:true, _color:ConsoleColor.DarkGray, _frame:100);
                        }
                        ScreenManager.instance.AsyncText($"Lv.{monsters[temp].Level} {monsters[temp].Name}", _startX: startMonsterX+temp*24, _startY: startMonsterY+monsterSizeY+1, _color:ConsoleColor.DarkGray);
                        ScreenManager.instance.AsyncText("Dead", _startX: startMonsterX+temp*24, _startY: startMonsterY+monsterSizeY+2, _color:ConsoleColor.Red);

                    }
                }
            }

            ScreenManager.instance.AsyncUnitVideo(player.FilePath.idle, startX: 0, startY: startMonsterY, videoSizeX: monsterSizeX, videoSizeY: monsterSizeY, _isContinue: true, _isReversal:true, _frame:33);

            ScreenManager.instance.AsyncText($"{player.Name} ({player.Job})", _startX: 1, _startY: startMonsterY+monsterSizeY+1, _color:ConsoleColor.Green);
            ScreenManager.instance.AsyncText($"Lv.{player.Level}", _startX: 1, _startY: startMonsterY+monsterSizeY+2);
            ScreenManager.instance.AsyncText($"HP   {player.HP}/{player.MaxHP}", _startX: 1, _startY: startMonsterY+monsterSizeY+3,_color:ConsoleColor.Red);
            ScreenManager.instance.AsyncText($"MP   {player.MP}/{player.MaxMP}", _startX: 1, _startY: startMonsterY+monsterSizeY+4,_color:ConsoleColor.Blue);
            ScreenManager.instance.AsyncText($"Gold {player.Gold}G", _startX: 1, _startY: startMonsterY+monsterSizeY+5,_color:ConsoleColor.Yellow);
        }
    }
}
