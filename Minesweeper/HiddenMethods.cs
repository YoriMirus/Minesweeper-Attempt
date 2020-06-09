using Microsoft.VisualBasic.CompilerServices;
using System;

namespace Minesweeper
{
    //Class containing methods useful only inside the class
    partial class Map
    {
        private static Random MinePlacer = new Random();
        /// <summary>
        /// Fills the map with the default number 12.
        /// </summary>
        /// <param name="array"></param>
        private static void Fill()
        {
            for(int i = 0; i < YLength; i++)
            {
                for (int j = 0; j < XLength; j++)
                    Layout[j, i] = 12;
            }
        }
        private static void SetMines(int mines)
        {
            int i = 0;
            while(i < mines)
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
        /// Reveals field nearby that are zero. Requires x and y coordinates of the field that is also 0.
        /// </summary>
        private static void SafeReveal(int x, int y)
        {
            //This is gonna get long :(

            /*Explaination:
             * The program looks at the fields around the field.
             * First the program checks if the field wasnt looked at first. (unchecked field = 12)
             * If not, then it checks if it has a zero. (CheckMines method)
             * If it doesnt have a zero, then it reveals it and stops searching there.
             * If it does have zero, the CheckMines method will automatically start the SafeReveal method.
             * If the field wasn't checked if it was checked before. (field equal to 0 - 9), 
             * then StackOverflowException can sometimes happen.
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