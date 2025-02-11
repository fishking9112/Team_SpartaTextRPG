using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Team_SpartaTextRPG
{
    //Utill Class : 개발 하면서 도움되는 함수 , 값 들을 모아둔 클래스
    static class Utill
    {
        public static void ColorWriteLine(string _text,ConsoleColor _color = ConsoleColor.Yellow)
        {
            /* 색 참고하세요 
             * Black = 0,
             * DarkBlue = 1,
             * DarkGreen = 2,
             * DarkCyan = 3,
             * DarkRed = 4,
             * DarkMagenta = 5,
             * DarkYellow = 6,
             * Gray = 7,
             * DarkGray = 8,
             * Blue = 9,
             * Green = 10,
             * Cyan = 11,
             * Red = 12,
             * Magenta = 13,
             * Yellow = 14,
             * White = 15
            */

            Console.ForegroundColor = _color; //노란색으로 변경
            Console.WriteLine(_text);
            Console.ResetColor();
        }
        public static void ColorWrite(string _text, ConsoleColor _color = ConsoleColor.Yellow)
        {
            /* 색 참고하세요 
             * Black = 0,
             * DarkBlue = 1,
             * DarkGreen = 2,
             * DarkCyan = 3,
             * DarkRed = 4,
             * DarkMagenta = 5,
             * DarkYellow = 6,
             * Gray = 7,
             * DarkGray = 8,
             * Blue = 9,
             * Green = 10,
             * Cyan = 11,
             * Red = 12,
             * Magenta = 13,
             * Yellow = 14,
             * White = 15
            */

            Console.ForegroundColor = _color; //노란색으로 변경
            Console.Write(_text);
            Console.ResetColor();
        }

        // IDLE Player
        public static readonly (string idle,string die, string end) WARRIOR_PATH = ("./resources/warrior.mp4", "./resources/warrior.mp4", "./resources/warrior.mp4");
        public static readonly (string idle,string die, string end) THIEF_PATH = ("./resources/thief.mp4", "./resources/thief.mp4", "./resources/thief.mp4");
        public static readonly (string idle,string die, string end) ARCHER_PATH = ("./resources/archer.mp4", "./resources/archer.mp4", "./resources/archer.mp4");
        public static readonly (string idle,string die, string end) WIZARD_PATH = ("./resources/wizard.mp4", "./resources/wizard.mp4", "./resources/wizard.mp4");

        
        // IDLE, DIE, END Monster
        public static readonly (string idle,string die, string end) SKELETON_PATH = ("./resources/skeleton.gif", "./resources/skeleton_die.gif", "./resources/skeleton_die_end.gif");
        public static readonly (string idle,string die, string end) SLIME_PATH = ("./resources/slime.gif", "./resources/slime_die.gif", "./resources/slime_die_end.gif");
        public static readonly (string idle,string die, string end) ORK_PATH = ("./resources/ork.gif", "./resources/ork_die.gif", "./resources/ork_die_end.gif");
        public static readonly (string idle,string die, string end) NineToNine_PATH = ("./resources/9to9.gif", "./resources/9to9_die.gif", "./resources/9to9_die_end.gif");
        public static readonly (string idle,string die, string end) TIL_PATH = ("./resources/til_idle.gif", "./resources/til_die.gif", "./resources/til_die_end.gif");
        public static readonly (string idle,string die, string end) CS_PATH = ("./resources/CsIcon_idle.gif", "./resources/CsIcon_die.gif", "./resources/CsIcon_die_end.gif");
        public static readonly (string idle,string die, string end) Algorithm_PATH = ("./resources/algorithm_idle.gif", "./resources/algorithm_die.gif", "./resources/algorithm_die_end.gif");
        public static readonly (string idle,string die, string end) AssetStore_PATH = ("./resources/asset_store_idle.gif", "./resources/asset_store_die.gif", "./resources/asset_store_die_end.gif");
        public static readonly (string idle,string die, string end) Unity2D_PATH = ("./resources/unity2d_idle.gif", "./resources/unity2d_die.gif", "./resources/unity2d_die_end.gif");
        public static readonly (string idle,string die, string end) TeamProject_PATH = ("./resources/team_project_idle.gif", "./resources/team_project_die.gif", "./resources/team_project_die_end.gif");
        public static readonly (string idle,string die, string end) Unity3D_PATH = ("./resources/unity3d_idle.gif", "./resources/unity3d_die.gif", "./resources/unity3d_die_end.gif");
        public static readonly (string idle,string die, string end) RealProject_PATH = ("./resources/real_project_idle.gif", "./resources/real_project_die.gif", "./resources/real_project_die_end.gif");
        public static readonly (string idle,string die, string end) BOSS_PATH = ("./resources/boss_idle.gif", "./resources/boss_die.gif", "./resources/boss_die_end.gif");
        // ScreenManager.instance.AsyncUnitVideo("./resources/warrior.mp4", startX: 0, startY: 1, videoSizeX: 12, videoSizeY: 15, _isContinue: true, _isReversal:true);
        // ScreenManager.instance.AsyncText("Lv.1 플레이어(전사)", _startX: 1, _startY: 17);
        // ScreenManager.instance.AsyncText("HP 100/100", _startX: 1, _startY: 18,_color:ConsoleColor.Red);
        // ScreenManager.instance.AsyncText("MP 100/100", _startX: 1, _startY: 19,_color:ConsoleColor.Blue);

        // ScreenManager.instance.AsyncUnitVideo("./resources/skeleton_die.gif",_color:ConsoleColor.DarkGray, startX: 24, startY: 1, videoSizeX: 12, videoSizeY: 15, _isContinue: false, _isReversal:true);
        // ScreenManager.instance.AsyncText("Lv.1 해골 (100/100)",_color:ConsoleColor.DarkGray, _startX: 24, _startY: 17);
        
        // ScreenManager.instance.AsyncUnitVideo("./resources/skeleton.gif", startX: 48, startY: 1, videoSizeX: 12, videoSizeY: 15, _isContinue: true, _isReversal:true, _frame:100);
        // ScreenManager.instance.AsyncText("Lv.1 해골 (100/100)", _startX: 48, _startY: 17);
        
        // ScreenManager.instance.AsyncUnitVideo("./resources/slime_die_end.gif",_color:ConsoleColor.DarkGray, startX: 72, startY: 1, videoSizeX: 12, videoSizeY: 15, _isContinue: false, _isReversal:true);
        // ScreenManager.instance.AsyncText("Lv.1 슬라임 (100/100)",_color:ConsoleColor.DarkGray, _startX: 72, _startY: 17);

        // ScreenManager.instance.AsyncUnitVideo("./resources/slime.gif", startX: 96, startY: 1, videoSizeX: 12, videoSizeY: 15, _isContinue: true, _isReversal:true);
        // ScreenManager.instance.AsyncText("Lv.1 슬라임 (100/100)", _startX: 96, _startY: 17);
    }
}
