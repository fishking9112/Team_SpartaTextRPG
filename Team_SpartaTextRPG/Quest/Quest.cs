using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    enum QUEST_TYPE { Item_Collection , Kill_Count }
    internal class Quest
    {
        public Quest(string _name , string _Des , string _Target , int _Count , int _Price) 
        {
            Name = _name;
            Description = _Des;
            TargetName = _Target;
            MaxCount = _Count;
            Price = _Price;
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
        public int Price { get; set; }
        // 퀘스트 타입 ( 아이템 수집 or 킬 미션 )
        // 고민중임
    }
}
