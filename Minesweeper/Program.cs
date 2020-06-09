using System;

namespace Minesweeper
{
    class Program
    {
        public static bool ProgramRun = true; //Tells you, if the game has ended or not.
        static void Main()
        {
            Map.Prepare();

            while (ProgramRun)
            {
                Map.Display();
                Cursor.ShowCursor();
                Console.WriteLine("Lives left: {0}", Cursor.Lives);
                Console.WriteLine("Marks: {0}/{1}", Cursor.Marks, Map.MinesAmount);
                Cursor.Actions();
                Console.Clear();
            }
        }
    }
}
