using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sand_game
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    public class sand
    {

        // makes the grid,
        public static int[,] GridMake(int c, int r)
        {
            int[,] Grid = new int[c, r];
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    Grid[i, j] = 0;

                }
            }
            return Grid;
        }
        // returns true if the index is within the grid and false if it isnt
        public static bool withinCols(int i)
        {
            return i >= 0 && i <= 100 - 1;
        }
        public static bool withinRows(int j)
        {
            return j >= 0 && j <= 50 - 1;
        }

    }
}

