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
    internal class DungeonScene : Helper.Singleton<DungeonScene>
    {
        Player player = GameManager.instance.player;
        Monster[] monsters = new Monster[4];
        private int MonsterCount = 0;

        // 던전 화면
        public void Dungeon_Title()
        {
            Console.WriteLine("1. [ Stage 입장 ]");
            Console.WriteLine("0. [ 돌아가기 ]");

            SceneManager.instance.Menu(Dungeon_Title, TownScene.instance.Game_Main, () => Select_Stage(1));
        }

        public void Select_Stage(int _index)
        {
            if (_index == 1)
            {
                //몬스터 Init
                InitStage();
                DungeonMenu();
            }
        }

        // 층에 나오기 전에 나오는 몬스터 설정
        public void InitStage()
        {
            for(int i = 0; i < monsters.Length; i++) { monsters[i] = null; }
            Random_Monster();
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
            List<Action> tempActions = new List<Action>();
            tempActions.Add(DungeonScene.instance.DungeonMenu);

            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i] != null)
                {
                    if (monsters[i].IsDead == false)
                    {
                        int temp = i;
                        tempActions.Add(() => Player_Att(temp + 1));
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

            SceneManager.instance.Menu(DungeonMenu_Fight, tempActions.ToArray());

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
        public void Random_Monster()
        {
            Random ran = new Random();
            MonsterCount = ran.Next(1, 5);

            // 랜덤 몬스터 스폰 개수
            // 스테이지마다 다르게 몬스터 생성
            // 매니저 5% 오크 15% 고블린 30% 슬라임 50%

            for (int i = 0; i < MonsterCount; i++)
            {
                int Rand = ran.Next(0, 100);

                if (Rand < 5)
                {
                    monsters[i] = new Monster("면접", 12, 50, 50, 30, 25);
                }
                else if (Rand < 20)
                {
                    monsters[i] = new Monster("실전 프로젝트", 7, 26, 26, 20, 4);
                }
                else if (Rand < 50)
                {
                    monsters[i] = new Monster("Unity 3D", 5, 22, 22, 10, 5);
                }
                else if (Rand < 50)
                {
                    monsters[i] = new Monster("팀 프로젝트​​", 3, 15, 15, 8, 1);
                }
                else if (Rand < 50)
                {
                    monsters[i] = new Monster("Unity 2D", 3, 15, 15, 8, 1);
                }
                else if (Rand < 50)
                {
                    monsters[i] = new Monster("Unity 입문", 3, 15, 15, 8, 1);
                }
                else if (Rand < 50)
                {
                    monsters[i] = new Monster("C# 기초, 심화", 3, 15, 15, 8, 1);
                }
                else if (Rand < 50)
                {
                    monsters[i] = new Monster("자료구조", 3, 15, 15, 8, 1);
                }
                else if (Rand < 50)
                {
                    monsters[i] = new Monster("TIL", 3, 15, 15, 8, 1);
                }
                else
                {
                    monsters[i] = new Monster("9to9", 3, 15, 15, 8, 1);
                }
            }
        }

        // 플레이어가 몬스터를 공격
        public void Player_Att(int input)
        {

            //플레이어가 몬스터를 공격시 치명타 함수호출
            bool isCritical = false;
            float Criticaldamage = player.CriticalAttack(player.FinalDamage(), ref isCritical);

            if (monsters[input - 1].IsAvoid(10.0f) == true) // 회피를 하면 ?
            {
                // 회피
                Utill.ColorWriteLine($"{monsters[input - 1].Name}는(은) {player.Name}의 공격을 회피했다 !.\n", ConsoleColor.Cyan);
            }
            else
            {
                // 맞음
                monsters[input - 1].HP = (int)(monsters[input - 1].HP - Criticaldamage);

                if (monsters[input - 1].HP <= 0)
                {
                    QuestManager.instance.MonsterCount(monsters[input - 1].Name);
                }

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
            Console.WriteLine("Stage Clear");

            Console.WriteLine("0. [ 나가기 ]");

            SceneManager.instance.Menu(Stage_Clear, Dungeon_Title);
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

                Console.WriteLine($"{i + 1}. {skillDescription}");

                skillActions.Add(() =>
                {

                    DungeonMenu_Monster_Select(index);
                });
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            SceneManager.instance.Menu(DungeonMenu, skillActions.ToArray());
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
            float Criticaldamage = player.CriticalAttack(baseDamage, ref isCritical);

            float finalDamage = baseDamage;

            SkillManager.instance.ExecuteSkillCost(player, player.SkillList[skillIndex]);

            if (isCritical)
            {
                finalDamage = Criticaldamage;
            }
            monsters[targetIndex].HP = (int)(monsters[targetIndex].HP - finalDamage);
            Utill.ColorWriteLine($"{player.Name} 스킬 사용", ConsoleColor.Blue);

            if (monsters[targetIndex].HP <= 0)
            {
                QuestManager.instance.MonsterCount(monsters[targetIndex].Name);
            }

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
