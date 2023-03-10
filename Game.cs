using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SłabySaper
{
    class Game: Panel
    {
        bool gameOver = false;
        const int SIZE = 30;
        ButtonField[,] board;
        int X, Y;

        
        public Game(int x, int y, int flags)
        {
           
            this.AutoSize = true;
            X = x;
            Y = y;
            
           
            
            board = new ButtonField[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    board[i,j] = new ButtonField();
                    board[i,j].Size = new System.Drawing.Size(SIZE,SIZE);
                    board[i, j].Location = new System.Drawing.Point(i * SIZE, j * SIZE);
                    board[i, j].Click += Game_Click;
                    board[i, j].Name =$"{i}.{j}";
                    board[i, j].BackColor = Color.Black;
                    this.Controls.Add(board[i,j]);

                }
            }
            Random generator = new Random();
            int howManyFlags = 0;
            do
            {
                int x1 = generator.Next(x);
                int y1 = generator.Next(y);
                if (board[x1, y1].Name == "flag") continue;
                board[x1, y1].Name = "flag";
                howManyFlags++;
            } while (howManyFlags < flags);
        }

        private void Game_Click(object sender, EventArgs e)
        {
            if (gameOver) return;
            if (sender is ButtonField)
            {
                    for (int i = 0; i < X; i++)
                    {
                        for (int j = 0; j < Y; j++)
                        {

                        if ((sender as ButtonField).Name == $"{i}.{j}")
                        {
                            (sender as ButtonField).Text = policz(i, j).ToString();
                            (sender as ButtonField).BackColor = Color.White;
                            if ((sender as ButtonField).Text == 0.ToString()) odblokujSasiadow(i, j);

                        }

                        if ((sender as ButtonField).Name == "flag")
                        { 
                            (sender as ButtonField).Text = "#";
                            (sender as ButtonField).BackColor = Color.White;
                            gameOver = true;
                            DialogResult result;
                            result = MessageBox.Show("Przegrałeś","Skończono", MessageBoxButtons.RetryCancel);
                            if (result == DialogResult.Retry)
                            {
                                return;
                            }
                            if (result == DialogResult.Cancel)
                            {
                                for(int xi = 0; xi < X; xi++)
                                {
                                for(int yi = 0; yi < Y; yi++)
                                    {
                                        int wynik = policz(xi,yi);
                                        board[xi, yi].BackColor = Color.Red;
                                        board[xi, yi].ForeColor = Color.White;
                                        if (wynik == 9) board[xi, yi].Text = "#";
                                        else board[xi, yi].Text = wynik.ToString();
                                        
                                    }

                                }
                                return;
                            }


                        }
                    }
                    }
          }
            
        }

        private int policz(int i, int j)
        {
            int sasiedzi = 0;
            if (board[i, j].Name == "flag") return 9;
            if(i-1>=0)if(board[i - 1, j].Name== "flag") sasiedzi++;
            if(i-1>=0&&j-1>=0)if (board[i - 1, j-1].Name == "flag") sasiedzi++;
            if (i - 1 >= 0 && j + 1 < Y) if (board[i - 1, j+1].Name == "flag") sasiedzi++;
            if (j - 1>=0) if (board[i , j-1].Name == "flag") sasiedzi++;
            if (j + 1 <Y) if (board[i , j+1].Name == "flag") sasiedzi++;
            if (i + 1 < X) if (board[i + 1, j].Name == "flag") sasiedzi++;
            if (i + 1 < X&&j-1 >=0) if (board[i + 1, j-1].Name == "flag") sasiedzi++;
            if (i + 1 < X&&j+1<Y) if (board[i + 1, j+1].Name == "flag") sasiedzi++;

            
            return sasiedzi;
            
        }

        private void odblokujSasiadow(int i,int j)
        {
         for(int x=i-1;x<=i+1; x++)
               {
                for (int y = j - 1; y <= j + 1; y++)
                        {
                             if (y == j && x == i) continue;
                             if (x >= 0 && x < X)
                                {
                        if (y >= 0 && y < Y)
                        {
                            int c = policz(x, y);
                            if (c==0&&board[x, y].Text =="")
                            {
                                board[x, y].Text = c.ToString();
                                board[x, y].BackColor = Color.White;
                                odblokujSasiadow(x, y);
                            }
                            else
                            {
                                board[x, y].Text = c.ToString();
                                board[x, y].BackColor = Color.White;
                            }
                            
                        }
                                 }
                        }
              }
        }
    }
}
