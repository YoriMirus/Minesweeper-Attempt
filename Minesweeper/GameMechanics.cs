using System;
namespace Minesweeper
{
    //Part of the class Map that contains methods and properties that are useful to other classes
    partial class Map
    {
        /*Meaning of a value!!!
         * 
         * Mines nearby
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
         * Other
         * 
         * 9 = Marked empty space
         * 13 = Marked mine
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
                        case  9: //Mark on empty space
                            Console.Write("+");
                            break;
                        case 10: //Unrevealed unmarked mine
                            Console.Write(" ");
                            break;
                        case 11: //Revealed mine
                            Console.Write("*");
                            break;
                        case 12: //Unrevealed position
                            goto case 10;
                            break;
                        case 13: //Marked mine
                            goto case 9;
                        default:
                            Console.Write(Layout[j, i]);
                            break;
                    }
                }
                Console.WriteLine("|");
            }

            for (int i = 0; i < XLength + 2; i++)
                Console.Write("-");
            Console.WriteLine();
        }
        /// <summary>
        /// Checks for mines around a position
        /// </summary>
        /// <returns></returns>
        public static int CheckMines(int x, int y)
        {
            int mines = 0;

            //Corners
            if(x == 0 && y == 0) //Top left
            {
                if (Layout[0, 1] == 10)
                    mines++;
                if (Layout[1, 0] == 10)
                    mines++;
                if (Layout[1, 1] == 10)
                    mines++;
            }
            else if (x == XLength - 1 && y == 0) // Top right
            {
                if (Layout[XLength - 2, 0] == 10)
                    mines++;
                if (Layout[XLength - 2, 1] == 10)
                    mines++;
                if (Layout[XLength - 1, 1] == 10)
                    mines++;
            }
            else if (x == 0 && y == YLength - 1) // Bottom left
            {
                if (Layout[0, YLength - 2] == 10)
                    mines++;
                if (Layout[1, YLength - 2] == 10)
                    mines++;
                if (Layout[1, YLength - 1] == 10)
                    mines++;
            }
            else if (x == XLength - 1 && y == YLength - 1) // Bottom right
            {
                if (Layout[XLength - 1, YLength - 2] == 10)
                    mines++;
                if (Layout[XLength - 2, YLength - 2] == 10)
                    mines++;
                if (Layout[XLength - 2, YLength - 1] == 10)
                    mines++;
            }

            //Edges

            else if (y == 0) // Top edge
            {
                for(int i = -1; i < 2; i++) //X
                {
                    for (int j = 0; j < 2; j++) //Y
                    {
                        if (Layout[i + x, j + y] == 10)
                            mines++;
                    }
                }
            }
            else 
            if (y == YLength - 1) // Bottom edge
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = 0; j > -2; j--)
                    {
                        if (Layout[i + x, j + y] == 10)
                            mines++;
                    }
                }
            }
            else if (x == 0) // Left edge
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (Layout[i, j + y] == 10)
                            mines++;
                    }
                }
            }
            else if (x == XLength - 1)
            {
                for (int i = -1; i < 1; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (Layout[XLength - 1 + i, j + y] == 10)
                            mines++;
                    }
                }
            }
            //Middle
            else
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (Layout[x + i, y + j] == 10)
                            mines++;
                    }
                }
            }
            return mines;
        }
    }
    partial class Cursor
    {
        public static char Look { get; set; } = '+'; // How the cursor looks like during the game.
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
                    Look = '^';
                    break;
                case ConsoleKey.DownArrow:
                    if (YPosition != Map.YLength - 1)
                        YPosition++;
                    Look = 'v';
                    break;
                case ConsoleKey.LeftArrow:
                    if (XPosition != 0)
                        XPosition--;
                    Look = '<';
                    break;
                case ConsoleKey.RightArrow:
                    if (XPosition != Map.XLength - 1)
                        XPosition++;
                    Look = '>';
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
                    switch(Map.Layout[XPosition, YPosition])
                    {
                        case 10: //Unmarked mine
                            Program.ProgramRun = false;
                            break;
                        case 13: //Marked mine
                            goto case 10;
                        case 11: //Revealed mine
                            goto case 10;
                        case 12: //Unrevealed position without mines
                            Map.Layout[XPosition, YPosition] = Map.CheckMines(XPosition, YPosition);
                            break;
                    }
                    break;

                //Mark
                case ConsoleKey.Spacebar:
                    switch(Map.Layout[XPosition, YPosition])
                    {
                        case 10: //Marking a mine
                            Map.Layout[XPosition, YPosition] = 13;
                            break;
                        case 13: //Unmarking a mine
                            Map.Layout[XPosition, YPosition] = 10;
                            break;

                        case 9: //Unmarking empty space
                            Map.Layout[XPosition, YPosition] = 12;
                            break;
                        case 12: //Unmarking empty space
                            Map.Layout[XPosition, YPosition] = 9;
                            break;
                    }
                    break;
            }

        }
        public static void ShowCursor()
        {
            int previousCursorLeft = Console.CursorLeft;
            int previousCursorTop  = Console.CursorTop ;
            Console.SetCursorPosition(XPosition + 1, YPosition + 1);
            Console.Write(Look);
            Console.SetCursorPosition(previousCursorLeft, previousCursorTop);
        }
    }
}