using System;
namespace Minesweeper
{
    //Part of the class Map that contains methods and properties that are useful to other classes
    partial class Map
    {
        public static int MinesAmount { get; set; }
        public static int XLength { get; } = 20;
        public static int YLength { get; } = 10;
        /// <summary>
        /// Map that the player sees
        /// </summary>
        public static char[,] Map_Visible { get; set; } = new char[XLength, YLength];
        /// <summary>
        /// Layout of all mines.
        /// </summary>
        public static char[,] Map_Layout { get; set; } = new char[XLength, YLength];

        /// <summary>
        /// Prepares the map for a game. Mines = amount of mines in the field.
        /// </summary>
        public static void Prepare(int mines)
        {
            Fill(Map_Layout);
            Fill(Map_Visible);
            MinesAmount = mines;
            SetMines(mines);
        }

        /// <summary>
        /// Displays the map layout to the console. 0 = Map_Visible, 1 = Map_Layout.
        /// </summary>
        /// <param name="mapType"></param>
        public static void Display(int mapType)
        {
            if(mapType == 0)
                for(int i = 0; i < YLength; i++)
                {
                    for (int j = 0; j < XLength; j++)
                    {
                        Console.Write(Map_Visible[j, i]);
                    }
                    Console.WriteLine();
                }
            else if(mapType == 1)
                for (int i = 0; i < YLength; i++)
                {
                    for (int j = 0; j < XLength; j++)
                    {
                        Console.Write(Map_Layout[j, i]);
                    }
                    Console.WriteLine();
                }
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
            }

        }
        public static void ShowCursor()
        {
            int previousCursorLeft = Console.CursorLeft;
            int previousCursorTop  = Console.CursorTop ;
            Console.SetCursorPosition(XPosition, YPosition);
            Console.Write("+");
            Console.SetCursorPosition(previousCursorLeft, previousCursorTop);
        }
    }
}