

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
            Console.WriteLine("캐릭터 선택!");

            Console.WriteLine("[1. 전사]");
            Console.WriteLine("[2. 궁수]");
            Console.WriteLine("[3. 뭐시기]");

            
            SceneManager.instance.Menu(Game_Start, null, ()=>Select_Job(1),()=>Select_Job(2),()=>Select_Job(3));
        }

        public void Select_Job(int _index){
            if(_index == 1){
                // 전사 선택
            } else {
                // 궁수 선택
            }

            SceneManager.instance.GoMenu(TownScene.instance.Game_Main);
        }
        public void Game_Quit()
        {
            GameManager.instance.isPlaying = false;
        }
    }
}
