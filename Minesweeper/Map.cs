using System;
using System.Diagnostics.Tracing;
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
            Console.WriteLine("Welcome to minesweeper!");
            Console.WriteLine("Use arrows or WASD to move with a cursor.");
            Console.WriteLine("Once you choose a field press enter to reveal it.");
            Console.WriteLine("If you suspect there is a mine press space to mark it.");
            Console.WriteLine("If you see alot of question marks then change your console font!");
            Console.WriteLine("Test text: こんにちは！");
            Console.WriteLine("Press anything to continue.");
            Console.ReadKey();
            Console.Clear();

            int x;
            int y;
            while (true)
            {

                Console.WriteLine("How large do you want the field to be?");
                Console.WriteLine("Each dimension can't be larger than 50.");

                Console.Write("Left to right: ");
                int.TryParse(Console.ReadLine(), out x);
                Console.Write("Top to bottom: ");
                int.TryParse(Console.ReadLine(), out y);

                XLength = x;
                YLength = y;
                if ((XLength > 50 || YLength > 50) || (XLength < 1 || YLength < 1))
                {
                    Console.WriteLine("Sorry. Either you didn't input a number,");
                    Console.WriteLine("or it didn't meet the requirements.");
                    Console.WriteLine("Please try again!");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                    break;
            }

            Console.Clear();

            int lives;
            while (true)
            {

                Console.WriteLine("How many lives do you want?");
                Console.WriteLine("(1 = fail after one revealed mine)");
                Console.Write("Lives: ");
                int.TryParse(Console.ReadLine(), out lives);
                Cursor.Lives = lives;
                if (Cursor.Lives < 1)
                {
                    Console.WriteLine("Sorry, but you can't put that in there.");
                    Console.WriteLine("Either you put a number too low or didn't type it properly.");
                    Console.WriteLine("Please try again");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                    break;
            }

            Console.Clear();

            int mines;
            while (true)
            {
                Console.WriteLine("How many mines do you want?");
                Console.Write("Mines: ");
                int.TryParse(Console.ReadLine(), out mines);

                if (XLength * YLength < mines && mines > 0)
                {
                    Console.WriteLine("Sorry! Seems like you didn't put a number,");
                    Console.WriteLine("or the number was too large!");
                    Console.WriteLine("Try again!");
                    Thread.Sleep(1000);
                }
                else
                    break;
            }
            Console.Clear();

            Layout = new int[XLength, YLength];
            Fill();
            MinesAmount = mines;
            SetMines(mines);
        }
        /// <summary>
        /// Displays the whole map
        /// </summary>
        public static void DisplayAll()
        {
            for (int i = 0; i < XLength + 2; i++)
                Console.Write("－");
            Console.WriteLine();

            for(int i = 0; i < YLength; i++)
            {
                Console.Write("｜");
                for (int j = 0; j < XLength; j++)
                {
                    DisplayOne(j, i);
                }
                Console.WriteLine("｜");
            }

            for (int i = 0; i < XLength + 2; i++)
                Console.Write("－");
            Console.WriteLine();
        }
        /// <summary>
        /// Displays one position of a map. Coordinates are required
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void DisplayOne(int x, int y)
        {
            switch (Layout[x, y])
            {
                case 0:
                    Console.Write("　");
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("１");
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("２");
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("３");
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("４");
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("５");
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("６");
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("７");
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("８");
                    break;
                case 9: //Mark on empty space
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("＋");
                    break;
                case 10: //Unrevealed unmarked mine
                    Console.Write("＃");
                    break;
                case 11: //Revealed mine
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("＊");
                    break;
                case 12: //Unrevealed position
                    goto case 10;
                case 13: //Marked mine
                    goto case 9;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        /// <summary>
        /// Fills the map with the default number 12.
        /// </summary>
        /// <param name="array"></param>
        private static void Fill()
        {
            for (int i = 0; i < YLength; i++)
            {
                for (int j = 0; j < XLength; j++)
                    Layout[j, i] = 12;
            }
        }
        /// <summary>
        /// Places mines at random positions
        /// </summary>
        /// <param name="mines"></param>
        private static void SetMines(int mines)
        {
            Random MinePlacer = new Random();
            int i = 0;
            while (i <= mines)
            {
                int x = MinePlacer.Next(0, XLength); //X position of a mine
                int y = MinePlacer.Next(0, YLength); //Y position of a mine

                if (Layout[x, y] != 10)
                {
                    Layout[x, y] = 10;
                    i++;
                }
            }
        }
        /// <summary>
        /// Reveals a random position that is equal to zero, so player has a place to start.
        /// Places the cursor inside the safe area as well.
        /// </summary>
        public static void FirstReveal()
        {
            Random rdNum = new Random();
            bool numChosen = false;

            while (!numChosen)
            {
                int x = rdNum.Next(0, XLength);
                int y = rdNum.Next(0, YLength);

                if (CheckMines(x, y) != 0)
                    Layout[x, y] = 12;
                else
                    numChosen = true;
                Console.Clear();
                Cursor.XPosition = x;
                Cursor.YPosition = y;
                Cursor.MoveCursor(0, 0, x, y);
            }
        }
        /// <summary>
        /// Checks for mines around a position. If there are no mines nearby, reveals nearby positions.
        /// The number is automatically assigned to the position.
        /// Has the ability to return how many mines around the position were found.
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

            Layout[x, y] = mines;

            if(mines == 0)
            {
                SafeReveal(x, y);
            }
            return mines;
        }
        /// <summary>
        /// Reveals field nearby that are zero. Requires x and y coordinates of the position which = 0.
        /// </summary>
        private static void SafeReveal(int x, int y)
        {
            /*Explaination:
             * The program looks at the positions around the position.
             * First the program checks if the position wasnt looked at first. (unchecked position = 12)
             * If not, then it checks if it has a zero. (CheckMines method)
             * If it doesnt have a zero, then it reveals it and stops searching there.
             * If it does have zero, the CheckMines method will automatically start the SafeReveal method.
             * If the field doesn't check if the position was checked before, (position equal to 0 - 9), 
             * then StackOverflowException might happen.
             * 
             * Tho it still seems to happen if the area is too large. No idea how to fix that.
             */

            //Top left corner

            if (x == 0 && y == 0)
            {
                if (Layout[1, 0] == 12)
                    CheckMines(1, 0);

                if (Layout[0, 1] == 12)
                    CheckMines(0, 1);

                if (Layout[1, 1] == 12)
                    CheckMines(1, 1);

            }

            //Top right corner

            else if (x == XLength - 1 && y == 0)
            {
                if (Layout[XLength - 2, 0] == 12)
                    CheckMines(XLength - 2, 0);

                if (Layout[XLength - 1, 1] == 12)
                    CheckMines(XLength - 1, 1);

                if (Layout[XLength - 2, 1] == 12)
                    CheckMines(XLength - 2, 1);
            }

            //Bottom left corner

            else if (x == 0 && y == YLength - 1)
            {
                if (Layout[0, YLength - 2] == 12)
                    CheckMines(0, YLength - 2);

                if (Layout[1, YLength - 2] == 12)
                    CheckMines(1, YLength - 2);

                if (Layout[1, YLength - 1] == 12)
                    CheckMines(1, YLength - 1);
            }

            //Bottom right corner

            else if (x == XLength - 1 && y == YLength - 1)
            {
                if (Layout[XLength - 1, YLength - 2] == 12)
                    CheckMines(XLength - 1, YLength - 2);

                if (Layout[XLength - 2, YLength - 2] == 12)
                    CheckMines(XLength - 2, YLength - 2);

                if (Layout[XLength - 2, YLength - 1] == 12)
                    CheckMines(XLength - 2, YLength - 1);
            }



            //Top edge

            else if (y == 0)
            {
                for (int i = -1; i < 2; i++)//X
                    for (int j = 0; j < 2; j++)//Y
                        if (Layout[x + i, y + j] == 12)
                            CheckMines(x + i, y + j);
            }

            //Bottom edge

            else if (y == YLength - 1)
            {
                for (int i = -1; i < 2; i++)
                    for (int j = -1; j < 1; j++)
                        if (Layout[x + i, y + j] == 12)
                            CheckMines(x + i, y + j);
            }

            //Left edge

            else if (x == 0)
            {
                for (int i = 0; i < 2; i++)
                    for (int j = -1; j < 2; j++)
                        if (Layout[x + i, y + j] == 12)
                            CheckMines(x + i, y + j);
            }

            //Right edge

            else if (x == XLength - 1)
            {
                for (int i = -1; i < 1; i++)
                    for (int j = -1; j < 2; j++)
                        if (Layout[x + i, y + j] == 12)
                            CheckMines(x + i, y + j);
            }

            //Center

            else
            {
                for (int i = -1; i < 2; i++)
                    for (int j = -1; j < 2; j++)
                        if (Layout[x + i, y + j] == 12)
                            CheckMines(x + i, y + j);
            }
        }
    }
   
}