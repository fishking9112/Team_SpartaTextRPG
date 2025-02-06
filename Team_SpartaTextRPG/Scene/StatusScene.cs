using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class StatusScene : Helper.Singleton<StatusScene>
    {
        public void Game_Stats()
        {
            Console.Clear();
            ShowHighlightText("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");

        }

        private static void ShowHighlightText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public void Game_Quit()
        {
            GameManager.instance.isPlaying = false;
        }
    }

}
