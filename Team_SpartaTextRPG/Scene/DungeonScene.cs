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
            tempActions.Add(DungeonScene.instance.Dungeon_Title);

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
                Console.WriteLine($"{i+1}.   {player.Inven_Usable_Item[i].Name}   |   {player.Inven_Usable_Item[i].Description}   |   {player.Inven_Usable_Item[i].HporMp()}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            SceneManager.instance.Menu(DungeonMenu_Use_Item, tempActions.ToArray());
        }

        // 랜덤 사용해서 몬스터 랜덤 소환
        public void Random_Monster()
        {
            Random ran = new Random();
            int monsterType = ran.Next(1, 5);

            // 랜덤 몬스터 스폰 개수
            // 스테이지마다 다르게 몬스터 생성
            // 매니저 5% 오크 15% 고블린 30% 슬라임 50%

            for (int i = 0; i < monsterType; i++)
            {
                int Rand = ran.Next(0, 100);

                if (Rand < 5)
                {
                    monsters[i] = new Monster("맼닠젘", 10, 50, 50, 10, 25);
                }
                else if (Rand < 20)
                {
                    monsters[i] = new Monster("오크", 5, 26, 26, 8, 4);
                }
                else if (Rand < 50)
                {
                    monsters[i] = new Monster("고블린", 3, 22, 22, 3, 5);
                }
                else
                {
                    monsters[i] = new Monster("슬라임", 1, 15, 15, 1, 1);
                }
            }
        }

        //공격시 치명타 및 회피 설정



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
                if(monsters[i] != null)
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

        public void DungeonMenu_Skill_Select ()
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

            SceneManager.instance.Menu(()=>DungeonMenu_Monster_Select(skillIndex), tempActions.ToArray());

        }

        public void DungeonMenu_Skill_Use(int targetIndex, int skillIndex)
        {
            float damage = SkillManager.instance.GetSkillDamage(player, player.SkillList[skillIndex]);

            if (damage >= 0)
            {
                SkillManager.instance.ExecuteSkillCost(player, player.SkillList[skillIndex]);

                
                monsters[targetIndex].HP -= (int)damage;

                Console.WriteLine($"{monsters[targetIndex].Name}에게 {damage} 데미지를 입혔습니다!");
            }

            
            SceneManager.instance.GoMenu(DungeonMenu);
        }
    }
}
