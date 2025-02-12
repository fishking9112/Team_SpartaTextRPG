using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    enum QUEST_TYPE { Item_Collection , Kill_Count }
    enum QUEST_PROGRESS { Before, Inprogress, Obtainable, Complete }
    internal class Quest
    {
        public Quest(){}

        public Quest(string _name , string _Des , string _Target , int _Count , int _Reward, int _Level) 
        {
            Name = _name;
            Description = _Des;
            TargetName = _Target;
            CurCount = 0;
            MaxCount = _Count;
            Reward = _Reward;
            Level = _Level;
            QuestProgress = QUEST_PROGRESS.Before;
        }

        //퀘스트에 필요한 것 ?
        // 퀘스트 제목
        public string Name { get; set; }
        // 퀘스트 내용
        public string Description { get; set; }
        // 목표 이름
        public string TargetName { get; set; }
        // 현재 갯수
        public int CurCount {  get; set; }
        // 목표 갯수
        public int MaxCount { get; set; }
        // 보상
        public int Reward { get; set; }
        // 난이도
        public int Level { get; set; }
        // 진행도
        public QUEST_PROGRESS QuestProgress { get; set; }
        // 퀘스트 타입 ( 아이템 수집 or 킬 미션 )
        // 고민중임
    }
}
