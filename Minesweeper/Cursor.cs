using System;

namespace Minesweeper
{
    partial class Cursor
    {
        public static int Lives
        {
            get
            {
                return lives;
            }
            set
            {
                lives = value;
                if (lives < 1)
                {
                    Program.ProgramRun = false;
                }
            }
        }
        private static int lives = 0;
        public static byte Marks { get; set; } = 0;
        public static char Look { get; set; } = '＋'; // How the cursor looks like during the game.
        public static int XPosition { get; set; } = 0;
        public static int YPosition { get; set; } = 0;
        /// <summary>
        /// Reads keyboard input and makes actions based on that
        /// </summary>
        public static void Actions()
        {
            switch (Console.ReadKey().Key)
            {
                //Movement
                case ConsoleKey.UpArrow:
                    if (YPosition != 0)
                        YPosition--;
                    Look = 'へ';
                    MoveCursor(XPosition, YPosition + 1, XPosition, YPosition);
                    break;

                case ConsoleKey.DownArrow:
                    if (YPosition != Map.YLength - 1)
                        YPosition++;
                    Look = 'Ⅴ';
                    MoveCursor(XPosition, YPosition - 1, XPosition, YPosition);
                    break;

                case ConsoleKey.LeftArrow:
                    if (XPosition != 0)
                        XPosition--;
                    Look = '＜';
                    MoveCursor(XPosition + 1, YPosition, XPosition, YPosition);
                    break;

                case ConsoleKey.RightArrow:
                    if (XPosition != Map.XLength - 1)
                        XPosition++;
                    Look = '＞';
                    MoveCursor(XPosition - 1, YPosition, XPosition, YPosition);
                    break;

                case ConsoleKey.W:
                    goto case ConsoleKey.UpArrow;
                case ConsoleKey.S:
                    goto case ConsoleKey.DownArrow;
                case ConsoleKey.A:
                    goto case ConsoleKey.LeftArrow;
                case ConsoleKey.D:
                    goto case ConsoleKey.RightArrow;

                //Reveal
                case ConsoleKey.Enter:
                    switch (Map.Layout[XPosition, YPosition])
                    {
                        case 10: //Unmarked mine
                            Lives--;
                            Marks++;
                            Map.Layout[XPosition, YPosition] = 11;
                            break;
                        case 13: //Marked mine
                            Lives--;
                            Map.Layout[XPosition, YPosition] = 11;
                            break;
                        case 11: //Revealed mine
                            Lives--;
                            break;
                        case 12: //Unrevealed position without mines
                            if (Map.CheckMines(XPosition, YPosition) == 0) //Redisplays map if 0 mines found
                            {
                                Console.Clear();
                                Map.DisplayAll();
                            }
                            break;
                    }
                    break;

                //Mark
                case ConsoleKey.Spacebar:
                    switch (Map.Layout[XPosition, YPosition])
                    {
                        case 10: //Marking a mine
                            Map.Layout[XPosition, YPosition] = 13;
                            Marks++;
                            break;
                        case 13: //Unmarking a mine
                            Map.Layout[XPosition, YPosition] = 10;
                            Marks--;
                            break;

                        case 9: //Unmarking empty space
                            Map.Layout[XPosition, YPosition] = 12;
                            Marks--;
                            break;
                        case 12: //Marking empty space
                            Map.Layout[XPosition, YPosition] = 9;
                            Marks++;
                            break;
                    }
                    break;
            }

        }
        public static void MoveCursor(int previousX, int previousY, int newX, int newY)
        {
            int previousCursorLeft = Console.CursorLeft;
            int previousCursorTop = Console.CursorTop;

            Console.SetCursorPosition(newX * 2 + 2, newY + 1);
            Console.Write(Look);

            Console.SetCursorPosition(previousX * 2 + 2, previousY + 1);
            Map.DisplayOne(previousX, previousY);

            Console.SetCursorPosition(previousCursorLeft, previousCursorTop);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}