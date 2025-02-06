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
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 돌아가기");
            SceneManager.instance.Menu(ShowShop, TownScene.instance.Game_Main);
        }

        public void selectMenu (int _input)
        {
            if (_input == 0)
            {

            }
            else if (_input == 1)
            {

            }
            else if ( _input == 2)
            {

            }
        }
    }
}
