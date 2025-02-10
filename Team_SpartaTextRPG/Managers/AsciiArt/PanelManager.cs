using System.Text;

namespace Team_SpartaTextRPG
{
    class PanelManager : Helper.Singleton<PanelManager>
    {
        // 전체 게임 panel의 X 크기
        public int gamePanelX = 60;

        // 다른 panel들의 크기        
        public int titlePanelY = 1; // 타이틀의 Y 크기
        public int screenPanelY = 25; // 스크린의 Y 크기
        public int inputPanelY = 12; // 입력창의 Y 크기

        public string title = "";

        // 맨 처음 그려야 함
        public void DrawAllPanel()
        {
            // 타이틀 부분 Panel
            DrawTitlePanel();
            // 스크린 부분 Panel
            DrawScreenPanel();
            // 입력창 부분 Panel
            DrawInputKeyPanel();
        }

        // 타이틀 부분 리셋
        public void DrawTitlePanel()
        {
            Panel(0, 0, gamePanelX, titlePanelY, true, false);
        }
        // 스크린 부분 리셋
        public void DrawScreenPanel()
        {
            Panel(0, titlePanelY + 1, gamePanelX, screenPanelY, false, false);
        }
        // 입력 부분 리셋
        public void DrawInputKeyPanel()
        {
            Panel(0, titlePanelY + screenPanelY + 2, gamePanelX, inputPanelY);
        }

        // 판넬 그리기
        public void Panel(int _startX, int _startY, int _width, int _height, bool _isStart = false, bool _isEnd = true)
        {
            // 가로길이 2배
            _width *= 2;

            // string 저장
            StringBuilder sb = new();

            for (int i = 0; i < _height + 2; i++)
            {
                if (i == 0)
                {
                    sb.Append(_isStart ? "┌" : "├");
                }
                else if (i == _height + 1)
                {
                    sb.Append(_isEnd ? "└" : "├");
                }
                else
                {
                    sb.Append("│");
                }

                for (int j = 0; j < _width; j++)
                {
                    if (i == 0 || i == _height + 1)
                    {
                        sb.Append("─");
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                }
                if (i == 0)
                {
                    sb.Append(_isStart ? "┐" : "┤");
                }
                else if (i == _height + 1)
                {
                    sb.Append(_isEnd ? "┘" : "┤");
                }
                else
                {
                    sb.Append("│");
                }
                sb.AppendLine();
            }

            // Panel 그리기
            CursorManager.instance.CurserPointUse(() =>
            {
                // X가 0이라면 자원 덜쓰게 하기 위해 한번에 그리기
                if (_startX == 0)
                {
                    if (!GameManager.instance.isPlaying) return;
                    Console.SetCursorPosition(_startX, _startY);
                    Console.Write(sb.ToString());
                }
                else
                {
                    var sbSplit = sb.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                    for (int i = 0; i < sbSplit.Length; i++)
                    {
                        if (!GameManager.instance.isPlaying) return;
                        Console.SetCursorPosition(_startX, i + _startY);
                        Console.Write(sbSplit[i].ToString());
                    }
                }
            });
        }
    }
}