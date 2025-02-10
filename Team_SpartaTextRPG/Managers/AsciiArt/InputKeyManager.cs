

using System.Text;

namespace Team_SpartaTextRPG
{
    class InputKeyManager : Helper.Singleton<InputKeyManager>
    {
        public (string _menuName, string? _explanation, Action? _action)[] menus;
        public Action? nextActon;

        // 화살표로 selectInput 이동
        public int selectInput = 0;

        // 엔터 입력 시 
        public bool isEnter = true;
        public bool isInput = false;

        public void AsyncAction()
        {
            Task.Run(ReadInput); // 비동기 입력 감지 시작
        }

        private async Task ReadInput()
        {
            while (true)
            {
                if (Console.KeyAvailable) // 키 입력이 있는 경우만 처리
                {
                    if (isEnter || isInput) continue; // 엔터 입력 됬을 거나 Input 입력 중에는 잠시 기능 멈추기
                    ConsoleKeyInfo InputKey = Console.ReadKey(true);

                    switch (InputKey.Key)
                    {
                        case ConsoleKey.LeftArrow: // 왼쪽 방향키 입력 시
                            selectInput--; // 왼쪽으로 한칸 이동
                            if (selectInput < 0) // 입력 범위 값이 왼쪽이 없으면 맨 오른쪽 메뉴로 이동
                                selectInput = menus.Length - 1;
                            DrawArtMenus(); // 메뉴 다시 그리기
                            break;
                        case ConsoleKey.RightArrow: // 오른쪽 방향키 입력 시
                            selectInput++; // 오른쪽으로 한칸 이동
                            if (selectInput >= menus.Length) // 입력 범위 값이 오른쪽이 없으면 맨 왼쪽으로 메뉴이동
                                selectInput = 0;
                            DrawArtMenus(); // 메뉴 다시 그리기
                            break;
                        case ConsoleKey.Enter: // 엔터 입력 시 
                            // if (isInput) continue; // Input 입력 중에는 enter가 눌려지지 않게 조치
                            isEnter = true;
                            break;
                    }
                }
                await Task.Delay(50); // CPU 사용률 조절
            }
        }

        // 메뉴 등록한 뒤 그리기
        public void ArtMenu(params (string _menuName, string? _explanation, Action? _action)[] _menus)
        {
            // 방향키로 움직이는 메뉴 정보 가져오기 
            menus = _menus;
            // 메뉴 정보를 토대로 메뉴 Panel과 함께 그리기
            DrawArtMenus();
        }

        // 메뉴 그리기만
        private void DrawArtMenus()
        {
            // 그리기 전 InputKey Panel 초기화
            PanelManager.instance.DrawInputKeyPanel();

            // 메뉴 그림 시작 위치 x, y값
            int x = 2;
            int y = PanelManager.instance.titlePanelY + PanelManager.instance.screenPanelY + 3;

            for (int i = 0; i < menus.Length; i++)
            {
                string _temp;
                if (selectInput == i)
                {
                    _temp = " ■  " + menus[i]._menuName;
                    if (menus[i]._explanation != null)
                    {
                        MenuExplanation(menus[i]._explanation);
                    }
                }
                else
                {
                    _temp = " □  " + menus[i]._menuName;
                }
                // 한글이 아닌 경우 0.5칸 취급
                float textWidth = _temp.Sum(c => c == ' ' || c == '!' || c == '(' || c == ')' ||('0' <= c && c <= '9') ||('a' <= c && c <= 'z') ||('A' <= c && c <= 'Z') ? 0.5f : 1f);

                DrawMenu(x, y, _temp, (int)(textWidth + 0.5f));
                x += ((int)(textWidth + 0.5f) * 2) + 3;
            }
        }

        // 단일 메뉴 그리기
        private void DrawMenu(int _startX, int _startY, string _menuName, int _textWidth)
        {
            CursorManager.instance.CurserPointUse(() =>
            {
                if (!GameManager.instance.isPlaying) return;
                PanelManager.instance.Panel(_startX, _startY, _textWidth, 1, true);
                Console.SetCursorPosition(_startX + 1, _startY + 1);
                Console.WriteLine(_menuName);
            });
        }

        // 메뉴 설명
        private void MenuExplanation(string? _explanation)
        {
            // 설명 시작 위치
            int x = 3;
            int y = PanelManager.instance.titlePanelY + PanelManager.instance.screenPanelY + 7;

            CursorManager.instance.CurserPointUse(() =>
            {
                if (!GameManager.instance.isPlaying) return;

                var sbSplit = _explanation?.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                for (int i = 0; i < sbSplit?.Length; i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    Console.Write(sbSplit[i].ToString());
                }
            });
        }

        //! ※ 주의사항) InputMenu 실행 중에는 커서가 옮겨지면 안되기 때문에 사용 시 영상 금지
        public void InputMenu(Action _origin, string _explanation, params Action[] _actions)
        {
            // 그리기 전 InputKey Panel 초기화
            PanelManager.instance.DrawInputKeyPanel();

            isInput = true;
            int x = 3;
            int y = PanelManager.instance.titlePanelY + PanelManager.instance.screenPanelY + 4; // 원래는 13

            // 입력 완료 시 입력에 대한 결과 값
            CursorManager.instance.CurserPointUse(() =>
            {
                Console.SetCursorPosition(x, y);
                // 대부분 "원하시는 행동을 입력해주세요 >> "로 할 것 같긴 함
                Console.Write($"{_explanation}");

                //! InputMenu는 멀티 스레드가 아니라 여기서 멈추기 때문에 영상 실행 불가능
                int.TryParse(Console.ReadLine(), out int input);

                Console.SetCursorPosition(x, y+2);

                // 입력 범위 값이 이 안에 없으면 오류와 함께 다시 origin 시작
                if (input < 0 || _actions.Length <= input || _actions[input] == null)
                {
                    Console.WriteLine($"잘못된 입력입니다. (입력된 값 : {input})");
                    Thread.Sleep(1000);
                    nextActon = _origin;
                }
                else
                {
                    // string explanation = _actions[input]._explanation ?? input.ToString();
                    // Console.WriteLine($"{explanation}"); // 행동에 대한 설명
                    // Thread.Sleep(1000);
                    nextActon = _actions[input];
                }
            });
        }

        //! ※ 주의사항) InputString 실행 중에는 커서가 옮겨지면 안되기 때문에 사용 시 영상 금지
        public string InputString(string _explanation)
    {
            // 그리기 전 InputKey Panel 초기화
            PanelManager.instance.DrawInputKeyPanel();

            isInput = true;
            int x = 3;
            int y = PanelManager.instance.titlePanelY + PanelManager.instance.screenPanelY + 4; // 원래는 13

            string inputString = "";
            CursorManager.instance.CurserPointUse(() =>
            {
                Console.SetCursorPosition(x, y);
                // 대부분 "원하시는 행동을 입력해주세요 >> "로 할 것 같긴 함
                Console.Write($"{_explanation}");
                //! InputString은 멀티 스레드가 아니라 여기서 멈추기 때문에 영상 실행 불가능
                inputString = Console.ReadLine() ?? "";
            });

            return inputString;
        }

        //! ※ 주의사항) InputString 실행 중에는 커서가 옮겨지면 안되기 때문에 사용 시 영상 금지
        public void GoMenu(Action _action)
        {
            nextActon = _action;
        }
    }
}