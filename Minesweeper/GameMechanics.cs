using System;
namespace Minesweeper
{
    //Part of the class Map that contains methods and properties that are useful to other classes
    partial class Map
    {
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
        private static Random MinePlacer = new Random();

        /// <summary>
        /// Prepares the map for a game. Mines = amount of mines in the field.
        /// </summary>
        public static void Prepare(int mines)
        {
            Fill(Map_Layout);
            Fill(Map_Visible);
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
}