using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class ShopScene : Helper.Singleton<ShopScene>
    {
        public void ShowShop ()
        {
            Console.WriteLine("0. 돌아가기");
            SceneManager.instance.Menu(ShowShop, TownScene.instance.Game_Main);
        }
    }
}
