using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class QuestBoardScene : Helper.Singleton<QuestBoardScene>
    {
        public QuestBoardScene()
        {
            Init_QuestList();
        }
        public List<Quest> questList = new List<Quest>();

        void Init_QuestList()
        {
            questList.Add(new Quest("9to9", "9to9 5마리 처치", "9to9", 5, 1000, 1));
            questList.Add(new Quest("TIL", "TIL 5마리 처치", "TIL", 5, 1000, 1));
            questList.Add(new Quest("C# 기초, 심화", "C# 기초, 심화 5마리 처치", "C# 기초, 심화", 5, 1000, 1));
            questList.Add(new Quest("Unity 2D 처치", "Unity 2D 5마리 처치", "Unity 2D", 3, 2000, 2));
            questList.Add(new Quest("Unity 입문 처치", "Unity 입문 5마리 처치", "Unity 입문", 3, 2000, 2));
            questList.Add(new Quest("자료구조 처치", "자료구조 5마리 처치", "자료구조", 3, 2000, 2));
            questList.Add(new Quest("실전 프로젝트 처치", "실전 프로젝트 5마리 처치", "실전 프로젝트", 2, 3000, 3));
            questList.Add(new Quest("Unity 3D 처치", "Unity 3D 5마리 처치", "Unity 3D", 2, 3000, 3));
            questList.Add(new Quest("팀 프로젝트 처치", "팀 프로젝트 3마리 처치", "팀 프로젝트", 2, 3000, 3));
            questList.Add(new Quest("면접 처치", "면접 1마리 처치", "면접", 1, 5000, 4));
        }
        public void Show_Quest_Board()
        {
            TitleManager.instance.WriteTitle("퀘스트 목록", ConsoleColor.Yellow);

            List<Action> tempActions = new List<Action>();
            tempActions.Add(TownScene.instance.Game_Main);

            int nameX = 1;
            int descriptionX = 25;
            int rewardX = 52;
            int levelX = 70;
            int processX = 85;

            for (int i = 0; i < questList.Count; i++)
            {
                StringBuilder sbLevel = new();
                int temp = i;
                tempActions.Add( () => Quest_Accept(questList[temp]));

                switch(questList[i].QuestProgress)
                {
                    case QUEST_PROGRESS.Before: // 받기 전
                        for (int j = 1; j <= questList[i].Level; j++)
                            sbLevel.Append("★");
                        ScreenManager.instance.AsyncText($"{i + 1}. {questList[i].Name}", nameX, i + 1);
                        ScreenManager.instance.AsyncText($"{questList[i].Description}", descriptionX, i + 1);
                        ScreenManager.instance.AsyncText($"보상 : {questList[i].Reward}", rewardX, i + 1);
                        ScreenManager.instance.AsyncText(sbLevel, levelX, i + 1, ConsoleColor.Yellow);
                        break;
                    case QUEST_PROGRESS.Inprogress: // 진행 중
                        for (int j = 1; j <= questList[i].Level; j++)
                            sbLevel.Append("★");
                        ScreenManager.instance.AsyncText($"{i + 1}. {questList[i].Name}", nameX, i + 1);
                        ScreenManager.instance.AsyncText($"{questList[i].Description}", descriptionX, i + 1);
                        ScreenManager.instance.AsyncText($"보상 : {questList[i].Reward}", rewardX, i + 1);
                        ScreenManager.instance.AsyncText(sbLevel, levelX, i + 1, ConsoleColor.Yellow);
                        ScreenManager.instance.AsyncText($"[ 진행 중 ( {questList[i].CurCount} / {questList[i].MaxCount} ) ]", processX, i + 1, ConsoleColor.Cyan);
                        break;
                    case QUEST_PROGRESS.Obtainable: // 보상 획득 가능
                        for (int j = 1; j <= questList[i].Level; j++)
                            sbLevel.Append("★");
                        ScreenManager.instance.AsyncText($"{i + 1}. {questList[i].Name}", nameX, i + 1);
                        ScreenManager.instance.AsyncText($"{questList[i].Description}", descriptionX, i + 1);
                        ScreenManager.instance.AsyncText($"보상 : {questList[i].Reward}", rewardX, i + 1);
                        ScreenManager.instance.AsyncText(sbLevel, levelX, i + 2, ConsoleColor.Yellow);
                        ScreenManager.instance.AsyncText($"[ 보상 획득 가능 ]", processX, i + 1, ConsoleColor.Green);
                        break;
                    case QUEST_PROGRESS.Complete: // 이미 완료
                        for (int j = 1; j <= questList[i].Level; j++)
                            sbLevel.Append("★");
                        ScreenManager.instance.AsyncText($"{i + 1}. {questList[i].Name}", nameX, i + 1, ConsoleColor.DarkGray);
                        ScreenManager.instance.AsyncText($"{questList[i].Description}", descriptionX, i + 1, ConsoleColor.DarkGray);
                        ScreenManager.instance.AsyncText($"보상 : {questList[i].Reward}", rewardX, i + 1, ConsoleColor.DarkGray);
                        ScreenManager.instance.AsyncText(sbLevel, levelX, i + 1, ConsoleColor.DarkGray);
                        ScreenManager.instance.AsyncText($"[ 완료 ]", processX, i + 1, ConsoleColor.DarkGray);
                        break;
                }
            }

            ScreenManager.instance.AsyncText("0. 나가기", 1, questList.Count + 2);


            InputKeyManager.instance.InputMenu(QuestBoardScene.instance.Show_Quest_Board, "행동할 퀘스트 번호를 입력해주세요 >> ", tempActions.ToArray());
            // SceneManager.instance.Menu(QuestBoardScene.instance.Show_Quest_Board, tempActions.ToArray());
        }
        
        // 퀘스트 받기
        void Quest_Accept(Quest _quest)
        {
            switch(_quest.QuestProgress)
            {
                case QUEST_PROGRESS.Before: // 받기 전 - 수락가능
                    //퀘스트 받기
                    Quest_Receive(_quest);
                    break;

                case QUEST_PROGRESS.Inprogress: // 진행중
                    //암것도 안해요
                    break;

                case QUEST_PROGRESS.Obtainable: // 보상 획득 가능
                    Quest_Complete(_quest);
                    break;

                case QUEST_PROGRESS.Complete:   // 이미 완료한 퀘스트
                    //암것도 안해요
                    break;
            }

            SceneManager.instance.GoMenu(Show_Quest_Board);
            InputKeyManager.instance.GoMenu(Show_Quest_Board);
        }
        void Quest_Receive(Quest _quest)
        {
            QuestManager.instance.AddQuest(_quest);
        }

        void Quest_Complete(Quest _quest)
        {
            QuestManager.instance.QuestClear(_quest);
        }

    }
}
