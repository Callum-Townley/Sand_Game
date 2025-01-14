using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Sand_game
{
    public partial class Form1 : Form
    {
        //states the width of each pixel,colour of the sand itself,and the size of the grid
        int w = 20;

        private Color colour = Color.Peru;
        int[,] grid = sand.GridMake(100, 50);
        int[,] nextGrid = sand.GridMake(100, 50);
        public Form1()
        {
            InitializeComponent();
            //creates the window, sets the tick speed of the two timers and starts one of them.
            timer1.Interval = 50;
            timer1.Enabled = true;
            timer2.Interval = 50;
            timer2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // calculates the amount of pixels in a column/row
            int cols = Width / w;
            int rows = Height / w;
            // gets the co-ordinates of the mouse cursor
            int X = Cursor.Position.X / w;
            int Y = Cursor.Position.Y / w;
            // creates the canvas to be drawn to
            Graphics g = e.Graphics;
            Pen pen = new Pen(colour);
            Brush mybrush = new SolidBrush(colour);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (grid[i, j] == 1)
                    {
                        g.FillRectangle(mybrush, i * w, j * w - w, w, w);
                    }
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            timer2.Enabled = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            timer2.Enabled = false;
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 99; i >= 0; i--)
            {
                for (int j = 41; j > 0; j--)
                {
                    int state = grid[i, j];
                    int belowA = 0;
                    int belowB = 0;
                    if (state == 1)
                    {
                        int below = grid[i, j + 1];
                        //selects a random number,which determines if the sand will fall to the left or right(only if the spot below is already filled)
                        Random rnd = new Random();
                        int num = rnd.Next(2);
                        int direction = 1;
                        if (num < 1)
                        {
                            direction *= -1;
                        }
                        if (sand.withinCols(i + direction))
                        {
                            belowA = grid[i + direction, j + 1];
                        }
                        if (sand.withinCols(i - direction))
                        {
                            belowB = grid[i - direction, j + 1];
                        }
                        //checks if the sand should move either down,diagonally right,or diagonally left
                        if (below == 0)
                        {
                            nextGrid[i, j] = 0;
                            nextGrid[i, j + 1] = state;
                        }
                        else if (belowA == 0 && i != 0 && i < 80)
                        {
                            nextGrid[i + direction, j + 1] = state;
                            nextGrid[i, j] = 0;
                        }
                        else if (belowB == 0 && i != 0 && i < 80)
                        {
                            nextGrid[i - direction, j + 1] = state;
                            nextGrid[i, j] = 0;
                        }
                        //ensures the sand does not try and fall to the left or right,when its at the edge of the grid
                        //this would cause an index out of range error
                        else if (i == 0 && grid[i + 1, j + 1] == 0)
                        {
                            nextGrid[i + 1, j + 1] = state;
                            nextGrid[i, j] = 0;
                        }
                        else if (i == 80 && grid[i - 1, j + 1] == 0)
                        {
                            nextGrid[i - 1, j + 1] = state;
                            nextGrid[i, j] = 0;
                        }

                    }
                }
            }
            //loads the next frame of the grid
            grid = nextGrid;
            //signals a paint event,causing the canvas to be repainted
            Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //draws pixels at the mouse position,even when the button is held
            int X = Cursor.Position.X / w;
            int Y = Cursor.Position.Y / w;
            if (X < 100 && Y < 50)
                if (grid[X, Y] == 0)
                {
                    grid[X, Y] = 1;
                    Invalidate(new Rectangle(X * w, Y * w - w, w, w));
                }
                else
                {

                }
        }
    }
}
