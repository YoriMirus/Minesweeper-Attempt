using System;

namespace Minesweeper
{
    class Program
    {
        public static bool ProgramRun = true; //Tells you, if the game has ended or not.
        /// <summary>
        /// Writes into the console the GUI, that the user sees and interacts with.
        /// </summary>
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Map.Prepare();
            Console.Clear();
            Map.Display();
            Console.WriteLine("Lives left: {0}   ", Cursor.Lives);
            Console.WriteLine("Marks: {0}/{1}", Cursor.Marks, Map.MinesAmount);

            while (ProgramRun)
            {
                Cursor.Actions();
                Console.SetCursorPosition(12, Map.YLength + 2);
                Console.WriteLine(Cursor.Lives + "   ");
                Console.SetCursorPosition(8, Console.CursorTop);
                Console.Write("{0}/{1}", Cursor.Marks, Map.MinesAmount);
            }
        }
    }
}
