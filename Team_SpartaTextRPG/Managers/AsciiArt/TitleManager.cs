using System.Text;

namespace Team_SpartaTextRPG
{
    class TitleManager : Helper.Singleton<TitleManager>
    {
        string title = "";
        public void WriteTitle(string _title = "", ConsoleColor _color = ConsoleColor.Gray)
        {
            // 이전에 썻던 타이틀 이름 지우기
            PanelManager.instance.DrawTitlePanel();

            //! Screen 일일이 지우기 힘들어서 여기 둠 (결합도 올라가서 안좋지만 일단 쓰자)
            ScreenManager.instance.ClearScreen();

            // 타이틀에 아무것도 쓰지 않았다면 전에 썻던 title 그대로 가져옴
            title = _title == "" ? title : _title;

            // 한글이 아닌 경우 0.5칸 취급
            float textWidth = title.Sum(c => c == ' ' || c == '!' || c == '(' || c == ')' ||('0' <= c && c <= '9') ||('a' <= c && c <= 'z') ||('A' <= c && c <= 'Z') ? 0.5f : 1f);


            // 게임 판넬의 전체 길이를 가져와서 중간값으로 바꿈
            int middle = PanelManager.instance.gamePanelX - (int)(textWidth + 0.5f);

            // 판넬 중간으로 커서 옮겨서 글 쓰기
            CursorManager.instance.CurserPointUse(() =>
            {
                Console.ForegroundColor = _color; //그림 색상 변경
                if (!GameManager.instance.isPlaying) return;
                Console.SetCursorPosition(middle, 1);
                Console.Write(title);
                Console.ResetColor();
            });
        }
    }
}