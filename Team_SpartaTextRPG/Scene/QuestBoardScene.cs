﻿using System;
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
        List<Quest> questList = new List<Quest>();

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

            StringBuilder sb = new();
            List<Action> tempActions = new List<Action>();
            tempActions.Add(TownScene.instance.Game_Main);

            for (int i = 0; i < questList.Count; i++)
            {
                int temp = i;
                tempActions.Add( () => Quest_Accept(questList[temp]));

                switch(questList[i].QuestProgress)
                {
                    case QUEST_PROGRESS.Before: // 받기 전
                        sb.Append($"{i + 1}. {questList[i].Name} | {questList[i].Description} | 보상 : {questList[i].Reward} | ");
                        sb.Append("난이도 : ");
                        for (int j = 1; j <= questList[i].Level; j++)
                            sb.Append("★");
                        sb.AppendLine();
                        break;
                    case QUEST_PROGRESS.Inprogress: // 진행 중
                        sb.Append($"{i + 1}. {questList[i].Name} | {questList[i].Description} | 보상 : {questList[i].Reward} | ");
                        sb.Append("난이도 : ");
                        for (int j = 1; j <= questList[i].Level; j++)
                            sb.Append("★");
                        sb.Append(" | ");
                        sb.AppendLine("[진행 중]");
                        break;
                    case QUEST_PROGRESS.Obtainable: // 보상 획득 가능
                        sb.Append($"{i + 1}. {questList[i].Name} | {questList[i].Description} | 보상 : {questList[i].Reward} | ");
                        sb.Append("난이도 : ");
                        for (int j = 1; j <= questList[i].Level; j++)
                            sb.Append("★");
                        sb.Append(" | ");
                        sb.AppendLine("[보상 획득 가능]");
                        break;
                    case QUEST_PROGRESS.Complete: // 이미 완료
                        sb.Append($"{i + 1}. {questList[i].Name} | {questList[i].Description} | 보상 : {questList[i].Reward} | " );
                        sb.Append("난이도 : ");
                        for (int j = 1; j <= questList[i].Level; j++)
                            sb.Append("★");
                        sb.AppendLine();
                        break;
                }
            }

            sb.AppendLine();
            sb.AppendLine("0. 나가기");

            ScreenManager.instance.AsyncText(sb);


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
