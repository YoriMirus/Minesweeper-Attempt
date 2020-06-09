using System;
namespace Minesweeper
{
    //Part of the class Map that contains methods and properties that are useful to other classes
    partial class Map
    {
        /*
         * 0 = 0
         * 1 = 1
         * 2 = 2
         * 3 = 3
         * 4 = 4
         * 5 = 5
         * 6 = 6
         * 7 = 7
         * 8 = 8
         * 
         * 9 = Marker
         * 10 = Undiscovered mine
         * 11 = Discovered mine
         * 12 = Not yet revealed
         */
        public static int MinesAmount { get; set; }
        public static int XLength { get; } = 20;
        public static int YLength { get; } = 10;
        /// <summary>
        /// Map that the player sees
        /// </summary>
        public static int[,] Layout { get; set; } = new int[XLength, YLength];

        /// <summary>
        /// Prepares the map for a game. Mines = amount of mines in the field.
        /// </summary>
        public static void Prepare(int mines)
        {
            Fill(Layout);
            MinesAmount = mines;
            SetMines(mines);
        }
        public static void Display()
        {
            for (int i = 0; i < XLength + 2; i++)
                Console.Write("-");
            Console.WriteLine();

            for(int i = 0; i < YLength; i++)
            {
                Console.Write("|");
                for (int j = 0; j < XLength; j++)
                {
                    switch(Layout[j, i])
                    {
                        case  9:
                            Console.Write("+");
                            break;
                        case 10:
                            Console.Write(" ");
                            break;
                        case 11:
                            Console.Write("*");
                            break;
                        case 12:
                            goto case 10;
                    }
                }
                Console.WriteLine("|");
            }

            for (int i = 0; i < XLength + 2; i++)
                Console.Write("-");
            Console.WriteLine();
        }
    }
    partial class Cursor
    {
        public static int XPosition { get; set; } = 0;
        public static int YPosition { get; set; } = 0;

        public static void Actions()
        {
            switch (Console.ReadKey().Key)
            {
                //Movement
                case ConsoleKey.UpArrow:
                    if (YPosition != 0)
                        YPosition--;
                    break;
                case ConsoleKey.DownArrow:
                    if (YPosition != Map.YLength - 1)
                        YPosition++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (XPosition != 0)
                        XPosition--;
                    break;
                case ConsoleKey.RightArrow:
                    if (XPosition != Map.XLength - 1)
                        XPosition++;
                    break;
                case ConsoleKey.W:
                    goto case ConsoleKey.UpArrow;
                case ConsoleKey.S:
                    goto case ConsoleKey.DownArrow;
                case ConsoleKey.A:
                    goto case ConsoleKey.LeftArrow;
                case ConsoleKey.D:
                    goto case ConsoleKey.RightArrow;

                //Other actions

                //Reveal
                case ConsoleKey.Enter:

            }

        }
        public static void ShowCursor()
        {
            int previousCursorLeft = Console.CursorLeft;
            int previousCursorTop  = Console.CursorTop ;
            Console.SetCursorPosition(XPosition + 1, YPosition + 1);
            Console.Write("+");
            Console.SetCursorPosition(previousCursorLeft, previousCursorTop);
        }
    }
}