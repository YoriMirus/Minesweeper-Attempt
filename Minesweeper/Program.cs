using System;

namespace Minesweeper
{
    class Program
    {
        public static bool ProgramRun = true; //Tells you, if the game has ended or not.
        static void Main(string[] args)
        {
            Map.Prepare(20);

            while (ProgramRun)
            {
                Map.Display(0);
                Cursor.ShowCursor();
                Cursor.Actions();
                Console.Clear();
            }
        }
    }
}
