﻿using System;

namespace Minesweeper
{
    class Program
    {
        public static bool ProgramRun = true; //If set to false, the game stops
        /// <summary>
        /// Writes into the console the GUI, that the user sees and interacts with.
        /// </summary>
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Map.Prepare();
            Map.FirstReveal();
            Console.Clear();
            Map.DisplayAll();
            Console.WriteLine("Lives left: {0}   ", Cursor.Lives);
            Console.WriteLine("Marks: {0}/{1}", Cursor.Marks, Map.MinesAmount);

            while (ProgramRun)
            {
                Cursor.Actions();
                Console.SetCursorPosition(0, Map.YLength + 2);
                Console.WriteLine("Lives left: {0}   ", Cursor.Lives);
                Console.WriteLine("Marks: {0}/{1}", Cursor.Marks, Map.MinesAmount);
            }
        }
    }
}
