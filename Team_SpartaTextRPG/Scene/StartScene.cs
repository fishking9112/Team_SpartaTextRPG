

namespace Team_SpartaTextRPG
{
    internal class StartScene : Helper.Singleton<StartScene>
    {
        public void Game_Title()
        {
            Console.WriteLine("강렬한 인상 is 게임");

            Console.WriteLine("[1. 게임 시작]");
            Console.WriteLine("[2. 게임 종료]");

            SceneManager.instance.Menu(Game_Title, null, Game_Start, Game_Quit);
        }

        public void Game_Start()
        {

            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.\n");

            string name = Console.ReadLine() ?? "Chad";


            Console.WriteLine($"\n입력하신 이름은 {name} 입니다.\n");

            Console.WriteLine("1. 저장\n2. 취소\n");
            SceneManager.instance.Menu(Game_Start, null, () => { Choose_Job(name); }, Game_Start);

        }

        public void Choose_Job(string _name)
        {
            Console.WriteLine("캐릭터 선택!");

            Console.WriteLine("[1. 전사]");
            Console.WriteLine("[2. 도적]");
            Console.WriteLine("[3. 궁수]");
            Console.WriteLine("[4. 법사]");

            SceneManager.instance.Menu(Game_Start, null,
            () => Select_Job(_name, PLAYER_JOB.WARRIOR),
            () => Select_Job(_name, PLAYER_JOB.THIEF),
            () => Select_Job(_name, PLAYER_JOB.ARCHER),
            () => Select_Job(_name, PLAYER_JOB.WIZARD)
            );
        }

        public void Select_Job(string _name, PLAYER_JOB _job)
        {
            List<Skill_Key> skillList = new List<Skill_Key>();

            // 직업 별 스킬 분배
            switch (_job)
            {
                case PLAYER_JOB.WARRIOR: // 전사 스킬 3개 습득
                    skillList.Add(Skill_Key.WarriorSkill01);
                    skillList.Add(Skill_Key.WarriorSkill02);
                    skillList.Add(Skill_Key.WarriorSkill03);
                    // 이름, 직업, 스킬목록 추가
                    GameManager.instance.player = new Player(_name, _job, skillList,
                    _level: 1, _maxExp: 100, _exp: 0, _maxHp: 200, _hp: 200, _maxMp: 50, _mp: 50, _attDamage: 15, _def: 10, _gold: 1500);
                    break;

                case PLAYER_JOB.THIEF: // 도적 스킬 3개 습득
                    skillList.Add(Skill_Key.ThiefSkill01);
                    skillList.Add(Skill_Key.ThiefSkill02);
                    skillList.Add(Skill_Key.ThiefSkill03);
                    // 이름, 직업, 스킬목록 추가
                    GameManager.instance.player = new Player(_name, _job, skillList,
                    _level: 1, _maxExp: 100, _exp: 0, _maxHp: 100, _hp: 100, _maxMp: 70, _mp: 70, _attDamage: 13, _def: 7, _gold: 3000);
                    break;

                case PLAYER_JOB.ARCHER: // 궁수 스킬 3개 습득
                    skillList.Add(Skill_Key.ArcherSkill01);
                    skillList.Add(Skill_Key.ArcherSkill02);
                    skillList.Add(Skill_Key.ArcherSkill03);
                    // 이름, 직업, 스킬목록 추가
                    GameManager.instance.player = new Player(_name, _job, skillList,
                    _level: 1, _maxExp: 100, _exp: 0, _maxHp: 150, _hp: 150, _maxMp: 80, _mp: 80, _attDamage: 20, _def: 5, _gold: 1500);
                    break;

                case PLAYER_JOB.WIZARD: // 법사 스킬 3개 습득
                    skillList.Add(Skill_Key.WizardSkill01);
                    skillList.Add(Skill_Key.WizardSkill02);
                    skillList.Add(Skill_Key.WizardSkill03);
                    // 이름, 직업, 스킬목록 추가
                    GameManager.instance.player = new Player(_name, _job, skillList,
                    _level: 1, _maxExp: 100, _exp: 0, _maxHp: 150, _hp: 150, _maxMp: 150, _mp: 150, _attDamage: 18, _def: 5, _gold: 1500);
                    break;

            }


            SceneManager.instance.GoMenu(TownScene.instance.Game_Main);
        }
        public void Game_Quit()
        {
            GameManager.instance.isPlaying = false;
        }
    }
}
