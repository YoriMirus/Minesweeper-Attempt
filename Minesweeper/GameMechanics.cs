using System;
using System.Threading;
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
         * 10 = Undiscovered mine
         * 11 = Discovered mine (in case I want to implement multiple lives)
         * 12 = Not yet revealed
         * 13 = Marked mine
         */
        public static int MinesAmount { get; set; }
        public static int XLength { get; set; }
        public static int YLength { get; set; }
        public static int[,] Layout { get; set; }

        /// <summary>
        /// Prepares the map and cursor for a game. Asks the player for amount of mines and lives.
        /// </summary>
        public static void Prepare()
        {
            int mines;
            Console.WriteLine("Welcome to minesweeper!");
            Console.WriteLine("Use arrows or WASD to move with a cursor.");
            Console.WriteLine("Once you choose a field press enter to reveal it.");
            Console.WriteLine("If you suspect there is a mine press space to mark it.");
            Console.WriteLine("If you see alot of question marks then change your console font!");
            Console.WriteLine("Test text: こんにちは！");
            Console.WriteLine("Press anything to continue.");
            Console.ReadKey();
            Console.Clear();

            while (true)
            {
                try
                {
                    Exception e = new Exception();
                    Console.WriteLine("How large do you want the field to be?");
                    Console.WriteLine("Each dimension can't be larger than 50.");

                    Console.Write("Left to right: ");
                    XLength = int.Parse(Console.ReadLine());

                    Console.Write("Top to bottom: ");
                    YLength = int.Parse(Console.ReadLine());

                    if ((XLength > 50 || YLength > 50) || (XLength < 1 || YLength < 1))
                        throw e;
                    break;
                }
                catch
                {
                    Console.WriteLine("Sorry. Either you didn't input a number,");
                    Console.WriteLine("or it didn't meet the requirements.");
                    Console.WriteLine("Please try again!");
                    Thread.Sleep(500);
                    Console.Clear();
                }
            }

            Console.Clear();

            while (true)
            {
                try
                {
                    Exception e = new Exception();
                    Console.WriteLine("How many lives do you want?(1 = fail after one mine)");
                    Console.Write("Lives: ");
                    Cursor.Lives = int.Parse(Console.ReadLine());
                    if (Cursor.Lives < 1)
                        throw e;
                    break;
                }
                catch
                {
                    Console.WriteLine("Sorry, but you can't put that in there.");
                    Console.WriteLine("Either you put a number too low or didn't type it properly.");
                    Console.WriteLine("Please try again");
                    Thread.Sleep(500);
                    Console.Clear();
                }
            }

            Console.Clear();

            while (true)
            {
                try
                {
                    Exception e = new Exception();
                    Console.WriteLine("How many mines do you want?");
                    Console.WriteLine("The number must fit the field!");
                    Console.Write("Mines: ");
                    mines = int.Parse(Console.ReadLine());

                    if (XLength * YLength < mines && mines > 0)
                        throw e;
                    break;
                }
                catch
                {
                    Console.WriteLine("Sorry! Seems like you didn't put a number,");
                    Console.WriteLine("or the number was too large!");
                    Console.WriteLine("Try again!");
                }
            }
            Console.Clear();

            Layout = new int[XLength, YLength];
            Fill();
            MinesAmount = mines;
            SetMines(mines);
        }
        public static void Display()
        {
            for (int i = 0; i < XLength + 2; i++)
                Console.Write("－");
            Console.WriteLine();

            for(int i = 0; i < YLength; i++)
            {
                Console.Write("｜");
                for (int j = 0; j < XLength; j++)
                {
                    switch(Layout[j, i])
                    {
                        case 0:
                            Console.Write("　");
                            break;
                        case 1:
                            Console.Write("１");
                            break;
                        case 2:
                            Console.Write("２");
                            break;
                        case 3:
                            Console.Write("３");
                            break;
                        case 4:
                            Console.Write("４");
                            break;
                        case 5:
                            Console.Write("５");
                            break;
                        case 6:
                            Console.Write("６");
                            break;
                        case 7:
                            Console.Write("７");
                            break;
                        case 8:
                            Console.Write("８");
                            break;
                        case  9: //Mark on empty space
                            Console.Write("＋");
                            break;
                        case 10: //Unrevealed unmarked mine
                            Console.Write("＃");
                            break;
                        case 11: //Revealed mine
                            Console.Write("＊");
                            break;
                        case 12: //Unrevealed position
                            goto case 10;
                        case 13: //Marked mine
                            goto case 9;
                        default:
                            Console.Write(Layout[j, i]);
                            break;
                    }
                }
                Console.WriteLine("｜");
            }

            for (int i = 0; i < XLength + 2; i++)
                Console.Write("－");
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
                if (Layout[0, 1] == 10 || Layout[0, 1] == 11 || Layout[0, 1] == 13)
                    mines++;
                if (Layout[1, 0] == 10 || Layout[1, 0] == 11 || Layout[1, 0] == 13)
                    mines++;
                if (Layout[1, 1] == 10 || Layout[1, 1] == 11 || Layout[1, 1] == 13)
                    mines++;
            }
            else if (x == XLength - 1 && y == 0) // Top right
            {
                if (Layout[XLength - 2, 0] == 10 || Layout[XLength - 2, 0] == 11 || Layout[XLength - 2, 0] == 13)
                    mines++;
                if (Layout[XLength - 2, 1] == 10 || Layout[XLength - 2, 1] == 11 || Layout[XLength - 2, 1] == 13)
                    mines++;
                if (Layout[XLength - 1, 1] == 10 || Layout[XLength - 1, 1] == 11 || Layout[XLength - 1, 1] == 13)
                    mines++;
            }
            else if (x == 0 && y == YLength - 1) // Bottom left
            {
                if (Layout[0, YLength - 2] == 10 || Layout[0, YLength - 2] == 11 || Layout[0, YLength - 2] == 13)
                    mines++;
                if (Layout[1, YLength - 2] == 10 || Layout[1, YLength - 2] == 11 || Layout[1, YLength - 2] == 13)
                    mines++;
                if (Layout[1, YLength - 1] == 10 || Layout[1, YLength - 1] == 11 || Layout[1, YLength - 1] == 13)
                    mines++;
            }
            else if (x == XLength - 1 && y == YLength - 1) // Bottom right
            {
                if (Layout[XLength - 1, YLength - 2] == 10 || Layout[XLength - 1, YLength - 2] == 11 || Layout[XLength - 1, YLength - 2] == 13)
                    mines++;
                if (Layout[XLength - 2, YLength - 2] == 10 || Layout[XLength - 2, YLength - 2] == 11 || Layout[XLength - 2, YLength - 2] == 13)
                    mines++;
                if (Layout[XLength - 2, YLength - 1] == 10 || Layout[XLength - 2, YLength - 1] == 11 || Layout[XLength - 2, YLength - 1] == 13)
                    mines++;
            }

            //Edges

            else if (y == 0) // Top edge
            {
                for(int i = -1; i < 2; i++) //X
                {
                    for (int j = 0; j < 2; j++) //Y
                    {
                        if (Layout[i + x, j + y] == 10 || Layout[i + x, j + y] == 11 || Layout[i + x, j + y] == 13)
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
                        if (Layout[i + x, j + y] == 10 || Layout[i + x, j + y] == 11 || Layout[i + x, j + y] == 13)
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
                        if (Layout[i, j + y] == 10 || Layout[i, j + y] == 11 || Layout[i, j + y] == 13)
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
                        if (Layout[XLength - 1 + i, j + y] == 10 || Layout[XLength - 1 + i, j + y] == 11 || Layout[XLength - 1 + i, j + y] == 13)
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
                        if (Layout[x + i, y + j] == 10 || Layout[x + i, y + j] == 11 || Layout[x + i, y + j] == 13)
                            mines++;
                    }
                }
            }
            return mines;
        }
    }
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
                if(lives < 1)
                {
                    Program.ProgramRun = false;
                }
            }
        }
        private static int lives = 1;
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
                    switch(Map.Layout[XPosition, YPosition])
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
            int previousCursorTop  = Console.CursorTop ;

            Console.SetCursorPosition(newX * 2 + 1, newY + 1);
            Console.Write(Look);

            Console.SetCursorPosition(previousX * 2 + 1, previousY + 1);

            switch(Map.Layout[previousX, previousY])
            {
                case 0:
                    Console.Write("　");
                    break;
                case 1:
                    Console.Write("１");
                    break;
                case 2:
                    Console.Write("２");
                    break;
                case 3:
                    Console.Write("３");
                    break;
                case 4:
                    Console.Write("４");
                    break;
                case 5:
                    Console.Write("５");
                    break;
                case 6:
                    Console.Write("６");
                    break;
                case 7:
                    Console.Write("７");
                    break;
                case 8:
                    Console.Write("８");
                    break;
                case 9:
                    Console.Write("＋");
                    break;
                case 10:
                    goto case 12;
                case 11:
                    Console.Write("＊");
                    break;
                case 12:
                    Console.Write("＃");
                    break;
                case 13:
                    goto case 9;
            }
            Console.SetCursorPosition(previousCursorLeft, previousCursorTop);
        }
    }
}