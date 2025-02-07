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
    }
}
