using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class GameManager : Helper.Singleton<GameManager>
    {
        public bool isPlaying = true;
        public Player player { get; set; }  // 플레이어 임시로 기본 생성자
    }
}
