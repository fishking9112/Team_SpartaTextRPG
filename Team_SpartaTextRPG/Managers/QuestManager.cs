using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class QuestManager : Helper.Singleton<QuestManager>
    {
        Player player = GameManager.instance.player;
        //public List<Quest> Questlist = new List<Quest> ();

        public void AddQuest(Quest _quest)
        {
            _quest.QuestProgress = QUEST_PROGRESS.Inprogress;
            player.QuestList.Add(_quest);
        }

        public void MonsterCount(string _name)
        {
            foreach (var item in player.QuestList)
            {
                if(item.TargetName == _name)
                {
                    item.CurCount++;

                    if (item.CurCount >= item.MaxCount)
                    {
                        //최대값
                        item.CurCount = item.MaxCount;
                        item.QuestProgress = QUEST_PROGRESS.Obtainable;
                    }
                }
            }
        }
        public void QuestClear(Quest _quest)
        {
            _quest.QuestProgress = QUEST_PROGRESS.Complete;
            player.Gold += _quest.Reward;
        }
    }
}
