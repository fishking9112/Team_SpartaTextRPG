namespace Team_SpartaTextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // 콘솔 크기 설정 (버퍼 크기를 화면 크기와 동일하게)
            // Console.SetBufferSize(600, 500);

            Console.Clear();

            Console.SetWindowSize(125, 43);

            Console.CursorVisible = false; // 커서 숨기기
            PanelManager.instance.DrawAllPanel();
            InputKeyManager.instance.AsyncAction();

            // InputKeyManager.instance.ArtMenu(("시작", null, () => StartScene.instance.Game_FakeLoding()));
            InputKeyManager.instance.ArtMenu(("시작", null, () => StartScene.instance.Game_Title()));

            Progress();

            // 종료 안되게 함
            while (GameManager.instance.isPlaying)
            {
                Thread.Sleep(1000); // cpu 자원 아끼기
            }

            // 종료되면 깔끔하게 정리
            Console.Clear();
        }
        public static async void Progress()
        {
            var TaskScene = Task.Run(ScreeneProgress);
            var TaskAction = Task.Run(InputProgress);

            await Task.WhenAll(TaskScene, TaskAction);
        }

        public static void ScreeneProgress()
        {
            while (GameManager.instance.isPlaying)
            {
                // 엔터를 누를 때 까지 대기
                while (!InputKeyManager.instance.isEnter)
                {
                    Thread.Sleep(50); // cpu 자원 아끼기
                }

                // 엔터를 눌렀다면 다시 enter초기화 하고 select번호 출력
                InputKeyManager.instance.isEnter = false;
                int selectInput = InputKeyManager.instance.selectInput;

                // select 번호에 따른 menu 실행
                if (InputKeyManager.instance.menus[selectInput]._action != null)
                {
                    InputKeyManager.instance.selectInput = 0;
                    InputKeyManager.instance.menus[selectInput]._action();
                }
            }
        }

        public static void InputProgress()
        {
            while (GameManager.instance.isPlaying)
            {
                // 액션
                while (InputKeyManager.instance.nextActon == null)
                {
                    Thread.Sleep(50); // cpu 자원 아끼기
                }

                int selectInput = InputKeyManager.instance.selectInput;
                InputKeyManager.instance.selectInput = 0;
                // Thread.Sleep(1000);
                Task.Run(InputKeyManager.instance.nextActon);
                InputKeyManager.instance.nextActon = null;
                InputKeyManager.instance.isInput = false;
            }
        }
    }
}
