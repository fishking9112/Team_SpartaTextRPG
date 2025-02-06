using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_SpartaTextRPG
{
    internal class Maingame : Helper.Singleton<Maingame>
    {
        public void StartGame()
        {
            // Init
            SceneManager.instance.nextScene = TownScene.instance.Draw_Menu;
            // 매니저 Init
            // Player Load
            // Bgm Play


            //Title
        }

        public void Update()
        {
            // Render
            Console.Clear();
            // Scene Loop
            SceneManager.instance.nextScene();
            // Input
        }
    }
}
