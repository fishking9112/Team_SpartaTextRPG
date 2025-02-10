using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class QuestBoardScene : Helper.Singleton<QuestBoardScene>
    {
        void InitQuestList()
        {

        }
        void ShowMenu()
        {
            Console.WriteLine("1. 퀘스트 받기");
            Console.WriteLine("2. 퀘스트 완료");
            Console.WriteLine("0. 돌아가기");
            SceneManager.instance.Menu(ShowMenu, TownScene.instance.Game_Main, Quest_Board, Quest_Complete);
        }


        void Quest_Board()
        {
            Console.WriteLine("퀘스트 목록");
            Console.WriteLine("============");
            Console.WriteLine();


        }
        void Quest_Complete()
        {

        }

    }
}
