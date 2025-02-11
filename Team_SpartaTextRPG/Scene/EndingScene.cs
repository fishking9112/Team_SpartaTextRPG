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
            Console.WriteLine("-완-");

            SceneManager.instance.Menu(End , StartScene.instance.Game_Quit);
        }
    }
}
