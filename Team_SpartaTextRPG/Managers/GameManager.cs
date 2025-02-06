using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class GameManager : Helper.Singleton<GameManager>
    {
        public Player player { get; set; } = new Player();  // 임시로 기본 생성자
    }
}
