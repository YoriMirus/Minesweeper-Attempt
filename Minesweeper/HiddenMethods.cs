﻿using Microsoft.VisualBasic.CompilerServices;
using System;

namespace Minesweeper
{
    //Class containing methods useful only inside the class
    partial class Map
    {
        /// <summary>
        /// Fills the map with a default character - O.
        /// </summary>
        /// <param name="array"></param>
        private static void Fill(char[,] array)
        {
            for(int i = 0; i < YLength; i++)
            {
                for (int j = 0; j < XLength; j++)
                    array[j, i] = 'O';
            }
        }
        private static void SetMines(int mines)
        {
            int i = 0;
            while(i < mines)
            {
                int x = MinePlacer.Next(0, XLength); //X position of a mine
                int y = MinePlacer.Next(0, YLength); //Y position of a mine

                if (Map_Layout[x, y] != '*')
                {
                    Map_Layout[x, y] = '*';
                    i++;
                }
            }
        }
    }
}