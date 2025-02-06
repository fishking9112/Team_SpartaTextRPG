

namespace Team_SpartaTextRPG
{
    class SceneManager : Helper.Singleton<SceneManager>
    {
        public Action? nextScene;

        // origin, 0 ~ ...
        public void Menu(Action _origin, params Action?[] _actions){

            Console.Write("원하시는 행동을 입력해주세요\n>>");
            int.TryParse(Console.ReadLine(), out int input);

            Console.WriteLine($"입력 된 행동>> {input}");

            // 입력 범위 값이 이 안에 없으면 오류와 함께 다시 origin 시작
            if(input < 0 || _actions.Length <= input || _actions[input] == null){
                Console.WriteLine($"\n잘못된 입력입니다. (입력된 값 : {input})\n");
                Thread.Sleep(1000);
                nextScene = _origin;
            } else {
                nextScene = _actions[input];
            }
        }

        public void GoMenu(Action _action){
            nextScene = _action;
        }
    }
}