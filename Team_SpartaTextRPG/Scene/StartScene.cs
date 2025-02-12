

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

            int length = 117;
            for(int i = 0; i < length; i+=2)
            {
                if(i <= 20)
                {
                    PanelManager.instance.DrawInputKeyPanel();
                    InputKeyManager.instance.MenuExplanation("9시 출석을 위해 패스 앱 인증중",true, ConsoleColor.Magenta);
                }
                else if(i <= 40)
                {
                    PanelManager.instance.DrawInputKeyPanel();
                    InputKeyManager.instance.MenuExplanation("21조 스크럼을 위해 팀원 소집 중",true, ConsoleColor.Blue);
                }
                else if(i <= 60)
                {
                    PanelManager.instance.DrawInputKeyPanel();
                    InputKeyManager.instance.MenuExplanation("그림 리소스 불러오는중",true, ConsoleColor.Yellow);
                }
                else if(i <= 80)
                {
                    PanelManager.instance.DrawInputKeyPanel();
                    InputKeyManager.instance.MenuExplanation("스파르타 정신 가다듬는 중",true, ConsoleColor.Cyan);
                }
                else if(i <= 100)
                {
                    PanelManager.instance.DrawInputKeyPanel();
                    InputKeyManager.instance.MenuExplanation("디스! 이즈! 스파르타!",true, ConsoleColor.Red);
                }

                StringBuilder sb = new StringBuilder();
                for(int j = 0; j < i+1; j++)
                {
                    sb.Append("=");
                }
                for(int j = i; j < length-1; j++)
                {
                    sb.Append("-");
                }
                ScreenManager.instance.AsyncText(sb, _startY:PanelManager.instance.screenPanelY+PanelManager.instance.titlePanelY+1);


                Utill.Sleep(200);
            }

            Utill.Sleep(2000);
            Console.Clear(); // Win11 오류
            InputKeyManager.instance.isInput = false;
            // ScreenManager.instance.ClearScreen();
            // ScreenManager.instance.AsyncVideo("resources/title.mp4", _isContinue: false, _isReversal: true);

            InputKeyManager.instance.GoMenu(Game_Title);
        }

        public void Game_Title()
        {
            TitleManager.instance.WriteTitle("강렬한 인상 is 게임");
            ScreenManager.instance.AsyncVideo("resources/title.mp4", _isContinue: false, _isReversal: true);

            var player = SaveLoadManager.instance.LoadFromJson<Player>();
            var dungeonData = SaveLoadManager.instance.LoadFromJson<DungeonData>();

            if(player != null && dungeonData != null){
                // 계속 하기는 저장된 데이터만 있을 때 실행할 수 있도록 비활성화
                InputKeyManager.instance.ArtMenu(
                ("계속 하기", "저장된 게임을 불러옵니다.", () => { Game_Continue(player, dungeonData); }),
                ("새 게임 시작", "새로운 게임을 시작합니다.", () => { Game_Start(); }),
                ("게임 종료", "게임을 종료합니다.", () => { Game_Quit(); }));
            } else {
                // 계속 하기는 저장된 데이터만 있을 때 실행할 수 있도록 비활성화
                InputKeyManager.instance.ArtMenu(
                ("새 게임 시작", "새로운 게임을 시작합니다.", () => { Game_Start(); }),
                ("게임 종료", "게임을 종료합니다.", () => { Game_Quit(); }));
            }
        }

        public void Game_Start()
        {
            TitleManager.instance.WriteTitle("이름 정하기");

            StringBuilder sb = new();

            sb.Append("스파르타 던전에 오신 여러분 환영합니다.\n");
            sb.Append("원하시는 이름을 설정해주세요.");

            ScreenManager.instance.ClearScreen();
            ScreenManager.instance.AsyncText(sb);

            ScreenManager.instance.AsyncImage("./resources/dungeon.png",_startX:80, _startY:2, imageSizeX:20, imageSizeY:20);

            string name = InputKeyManager.instance.InputString("이름을 입력하세요(3 ~ 5 글자) >> ");
            
            if(3 <= name.Length && name.Length <= 5){
                InputKeyManager.instance.GoMenu(() => { Write_Name_Complete(name); });
            } else {
                TitleManager.instance.WriteTitle("이름 정하기");
                sb.Clear();
                sb.Append($"이름이 양식에 맞지 않습니다.");
                ScreenManager.instance.AsyncText(sb);
                Utill.Sleep(1000);
                InputKeyManager.instance.GoMenu(() => { Game_Start(); });
            }
        }

        public void Write_Name_Complete(string name)
        {
            TitleManager.instance.WriteTitle("이름 결정");

            ScreenManager.instance.AsyncVideo("resources/giphy.gif", _isContinue: false);

            InputKeyManager.instance.ArtMenu(($"제 이름은 {name}(이)가 맞습니다", "게임을 시작합니다.", () => { Choose_Job(name); }), ("다시 설정", "다시 이름을 정합니다.", () => { Game_Start(); }));
        }

        public void Choose_Job(string _name)
        {
            TitleManager.instance.WriteTitle("직업 선택하기");

            ScreenManager.instance.AsyncUnitVideo("./resources/warrior.mp4", startX: 0, startY: 2, videoSizeX: 15, videoSizeY: 20, _isContinue: true, _isReversal:true);
            ScreenManager.instance.AsyncUnitVideo("./resources/wizard.mp4", startX: 30, startY: 2, videoSizeX: 15, videoSizeY: 20, _isContinue: true, _isReversal:true);
            // ScreenManager.instance.AsyncUnitVideo("./resources/archer.mp4", startX: 60, startY: 2, videoSizeX: 15, videoSizeY: 20, _isContinue: true, _isReversal:true);
            // ScreenManager.instance.AsyncUnitVideo("./resources/wizard.mp4", startX: 90, startY: 2, videoSizeX: 15, videoSizeY: 20, _isContinue: true, _isReversal:true);
            
            InputKeyManager.instance.ArtMenu(
                ($"프로그래머", "프로그래머를 희망합니다.", () => Select_Job(_name, PLAYER_JOB.Programmer)),
                ($"기획자", "기획자를 희망합니다.", () => Select_Job(_name, PLAYER_JOB.Planner)));
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
                    GameManager.instance.player.FilePath = Utill.WARRIOR_PATH;
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
                    GameManager.instance.player.FilePath = Utill.WIZARD_PATH;
                    break;

            }


            InputKeyManager.instance.GoMenu(TownScene.instance.Game_Main);
        }
        
        public void Game_Continue(Player player, DungeonData dungeonData)
        {
            GameManager.instance.player = player;
            DungeonScene.instance.dungeonData = dungeonData;

            InputKeyManager.instance.GoMenu(TownScene.instance.Game_Main);
        }

        public void Game_Quit()
        {
            GameManager.instance.isPlaying = false;
        }
    }
}
