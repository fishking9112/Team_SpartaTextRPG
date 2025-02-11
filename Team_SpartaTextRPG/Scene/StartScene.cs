

using System.Text;

namespace Team_SpartaTextRPG
{
    internal class StartScene : Helper.Singleton<StartScene>
    {
        
        public void Game_FakeLoding()
        {
            TitleManager.instance.WriteTitle("강렬한 인상 is 게임 (로딩 중)");
            InputKeyManager.instance.isInput = true;
            ScreenManager.instance.AsyncVideo("resources/game_start.mp4", _isContinue: true, _isReversal: false);
            PanelManager.instance.DrawInputKeyPanel();

            int length = 58;
            for(int i = 0; i < length; i++)
            {
                if(i == 0)
                {
                    PanelManager.instance.DrawInputKeyPanel();
                    InputKeyManager.instance.MenuExplanation("9시 출석을 위해 패스 앱 인증중",true, ConsoleColor.Magenta);
                }
                else if(i == 10)
                {
                    PanelManager.instance.DrawInputKeyPanel();
                    InputKeyManager.instance.MenuExplanation("21조 스크럼을 위해 팀원 소집 중",true, ConsoleColor.Blue);
                }
                else if(i == 20)
                {
                    PanelManager.instance.DrawInputKeyPanel();
                    InputKeyManager.instance.MenuExplanation("그림 리소스 불러오는중",true, ConsoleColor.Yellow);
                }
                else if(i == 30)
                {
                    PanelManager.instance.DrawInputKeyPanel();
                    InputKeyManager.instance.MenuExplanation("스파르타 정신 가다듬는 중",true, ConsoleColor.Cyan);
                }
                else if(i == 40)
                {
                    PanelManager.instance.DrawInputKeyPanel();
                    InputKeyManager.instance.MenuExplanation("디스! 이즈! 스파르타!",true, ConsoleColor.Red);
                }

                StringBuilder sb = new StringBuilder();
                for(int j = 0; j < i+1; j++)
                {
                    sb.Append("■");
                }
                for(int j = i; j < length-1; j++)
                {
                    sb.Append("□");
                }
                ScreenManager.instance.AsyncText(sb, _startY:PanelManager.instance.screenPanelY+PanelManager.instance.titlePanelY+1);


                Thread.Sleep(200);
            }

            Thread.Sleep(2000);
            InputKeyManager.instance.isInput = false;
            // ScreenManager.instance.ClearScreen();
            // ScreenManager.instance.AsyncVideo("resources/title.mp4", _isContinue: false, _isReversal: true);

            InputKeyManager.instance.GoMenu(Game_Title);
        }

        public void Game_Title()
        {
            TitleManager.instance.WriteTitle("강렬한 인상 is 게임");
            ScreenManager.instance.AsyncVideo("resources/title.mp4", _isContinue: false, _isReversal: true);

            InputKeyManager.instance.ArtMenu(
            ("계속 하기", "저장된 게임을 불러옵니다.", () => { Select_Job("test",PLAYER_JOB.WARRIOR); }),
            ("게임 시작", "게임을 시작합니다.", () => { Game_Start(); }),
            ("게임 종료", "게임을 종료합니다.", () => { Game_Quit(); }));
        }

        public void Game_Start()
        {
            TitleManager.instance.WriteTitle("이름 정하기");

            StringBuilder sb = new();

            sb.Append("스파르타 던전에 오신 여러분 환영합니다.\n");
            sb.Append("원하시는 이름을 설정해주세요.");

            ScreenManager.instance.ClearScreen();
            ScreenManager.instance.AsyncText(sb);

            string name = InputKeyManager.instance.InputString("이름을 입력하세요(3 ~ 5 글자) >> ");
            
            if(3 <= name.Length && name.Length <= 5){
                InputKeyManager.instance.GoMenu(() => { Write_Name_Complete(name); });
            } else {
                TitleManager.instance.WriteTitle("이름 정하기");
                sb.Clear();
                sb.Append($"이름이 양식에 맞지 않습니다.");
                ScreenManager.instance.AsyncText(sb);
                Thread.Sleep(1000);
                InputKeyManager.instance.GoMenu(() => { Game_Start(); });
            }
        }

        public void Write_Name_Complete(string name)
        {
            TitleManager.instance.WriteTitle("이름 결정");

            ScreenManager.instance.AsyncVideo("resources/game_start.mp4", _isContinue: true);

            InputKeyManager.instance.ArtMenu(($"제 이름은 {name}(이)가 맞습니다", "게임을 시작합니다.", () => { Choose_Job(name); }), ("다시 설정", "다시 이름을 정합니다.", () => { Game_Start(); }));
        }

        public void Choose_Job(string _name)
        {
            TitleManager.instance.WriteTitle("직업 선택하기");

            ScreenManager.instance.AsyncUnitVideo("./resources/warrior.mp4", startX: 0, startY: 2, videoSizeX: 15, videoSizeY: 20, _isContinue: true, _isReversal:true);
            ScreenManager.instance.AsyncUnitVideo("./resources/thief.mp4", startX: 30, startY: 2, videoSizeX: 15, videoSizeY: 20, _isContinue: true, _isReversal:true);
            ScreenManager.instance.AsyncUnitVideo("./resources/archer.mp4", startX: 60, startY: 2, videoSizeX: 15, videoSizeY: 20, _isContinue: true, _isReversal:true);
            ScreenManager.instance.AsyncUnitVideo("./resources/wizard.mp4", startX: 90, startY: 2, videoSizeX: 15, videoSizeY: 20, _isContinue: true, _isReversal:true);
            
            InputKeyManager.instance.ArtMenu(
                ($"전사", "단단한 녀석입니다.", () => Select_Job(_name, PLAYER_JOB.WARRIOR)), 
                ($"도적", "얍삽한 녀석입니다.", () => Select_Job(_name, PLAYER_JOB.THIEF)),
                ($"궁수", "강력한 녀석입니다.", () => Select_Job(_name, PLAYER_JOB.ARCHER)),
                ($"법사", "초강력 녀석입니다.", () => Select_Job(_name, PLAYER_JOB.WIZARD)));
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
                    GameManager.instance.player.FilePath = Utill.WARRIOR_PATH;
                    break;

                case PLAYER_JOB.THIEF: // 도적 스킬 3개 습득
                    skillList.Add(Skill_Key.ThiefSkill01);
                    skillList.Add(Skill_Key.ThiefSkill02);
                    skillList.Add(Skill_Key.ThiefSkill03);
                    // 이름, 직업, 스킬목록 추가
                    GameManager.instance.player = new Player(_name, _job, skillList,
                    _level: 1, _maxExp: 100, _exp: 0, _maxHp: 100, _hp: 100, _maxMp: 70, _mp: 70, _attDamage: 13, _def: 7, _gold: 3000);
                    GameManager.instance.player.FilePath = Utill.THIEF_PATH;
                    break;

                case PLAYER_JOB.ARCHER: // 궁수 스킬 3개 습득
                    skillList.Add(Skill_Key.ArcherSkill01);
                    skillList.Add(Skill_Key.ArcherSkill02);
                    skillList.Add(Skill_Key.ArcherSkill03);
                    // 이름, 직업, 스킬목록 추가
                    GameManager.instance.player = new Player(_name, _job, skillList,
                    _level: 1, _maxExp: 100, _exp: 0, _maxHp: 150, _hp: 150, _maxMp: 80, _mp: 80, _attDamage: 20, _def: 5, _gold: 1500);
                    GameManager.instance.player.FilePath = Utill.ARCHER_PATH;
                    break;

                case PLAYER_JOB.WIZARD: // 법사 스킬 3개 습득
                    skillList.Add(Skill_Key.WizardSkill01);
                    skillList.Add(Skill_Key.WizardSkill02);
                    skillList.Add(Skill_Key.WizardSkill03);
                    // 이름, 직업, 스킬목록 추가
                    GameManager.instance.player = new Player(_name, _job, skillList,
                    _level: 1, _maxExp: 100, _exp: 0, _maxHp: 150, _hp: 150, _maxMp: 150, _mp: 150, _attDamage: 18, _def: 5, _gold: 1500);
                    GameManager.instance.player.FilePath = Utill.WIZARD_PATH;
                    break;

            }


            InputKeyManager.instance.GoMenu(TownScene.instance.Game_Main);
        }
        public void Game_Quit()
        {
            GameManager.instance.isPlaying = false;
        }
    }
}
