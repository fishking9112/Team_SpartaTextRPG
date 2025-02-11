

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

            Console.WriteLine("[1. 프로그래머]");
            Console.WriteLine("[2. 기획자]");

            SceneManager.instance.Menu(Game_Start, null,
            () => Select_Job(_name, PLAYER_JOB.Programmer),
            () => Select_Job(_name, PLAYER_JOB.Planner)
            );
        }

        public void Select_Job(string _name, PLAYER_JOB _job)
        {
            List<Skill_Key> skillList = new List<Skill_Key>();

            // 직업 별 스킬 분배
            switch (_job)
            {
                case PLAYER_JOB.Programmer: // 프로그래머 스킬 5개 습득
                    skillList.Add(Skill_Key.ProgrammerSkill01);
                    skillList.Add(Skill_Key.ProgrammerSkill02);
                    skillList.Add(Skill_Key.ProgrammerSkill03);
                    skillList.Add(Skill_Key.ProgrammerSkill04);
                    skillList.Add(Skill_Key.ProgrammerSkill05);
                    // 이름, 직업, 스킬목록 추가
                    GameManager.instance.player = new Player(_name, _job, skillList,
                    _level: 1, _maxExp: 100, _exp: 0, _maxHp: 150, _hp: 150, _maxMp: 60, _mp: 60, _attDamage: 9, _def: 6, _gold: 1500);
                    break;

                case PLAYER_JOB.Planner: // 기획자 스킬 5개 습득
                    skillList.Add(Skill_Key.PlannerSkill01);
                    skillList.Add(Skill_Key.PlannerSkill02);
                    skillList.Add(Skill_Key.PlannerSkill03);
                    skillList.Add(Skill_Key.PlannerSkill04);
                    skillList.Add(Skill_Key.PlannerSkill05);
                    // 이름, 직업, 스킬목록 추가
                    GameManager.instance.player = new Player(_name, _job, skillList,
                    _level: 1, _maxExp: 100, _exp: 0, _maxHp: 100, _hp: 100, _maxMp: 100, _mp: 100, _attDamage: 6, _def: 8, _gold: 1500);
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
