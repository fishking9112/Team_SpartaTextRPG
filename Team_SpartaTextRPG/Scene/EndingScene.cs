using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class EndingScene : Helper.Singleton<EndingScene>
    {
        public void End()
        {
            TitleManager.instance.WriteTitle("엔딩 씬");
            
            ScreenManager.instance.AsyncVideo("./resources/ending.gif", _isContinue: false, _isReversal: true, _frame: 33);

            Utill.Sleep(33*320);

            InputKeyManager.instance.ArtMenu(("게임 종료", "플레이 해주셔서 감사합니다!", StartScene.instance.Game_Quit));
        }
    }
}
