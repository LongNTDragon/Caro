using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Caro
{
    class RenderUICaro
    {
        private Control ctr;
        private bool flag = true;
        private Tuple<int, int>[,] positions;
        private int n;
        private int caseWin;
        
        public RenderUICaro(Control ctr, int n)
        {
            this.ctr = ctr;
            this.n = n;
            this.caseWin = n < 5 ? n : 5;
        }

        public void renderMatrix()
        {
            positions = new Tuple<int,int>[n, n];
            int topPos = 50;

            for (int i = 0; i < n; i++)
            {
                int leftPos = 50;
                for (int j = 0; j < n; j++)
                {
                    Button btn = new Button();
                    btn.Width = 30;
                    btn.Height = 30;
                    btn.Top = topPos;
                    btn.Left = leftPos;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.Font = new Font("Arial", 14);
                    leftPos += 30;
                    btn.Click += new EventHandler(btn_Click);
                    ctr.Controls.Add(btn);
                    positions[i, j] = new Tuple<int, int>(btn.Location.X, btn.Location.Y);
                    Console.WriteLine("pos[" + i + "," + j + "]=" + positions[i, j]);
                }
                topPos += 30;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            foreach(Control item in ctr.Controls)
            {
                if(item.GetType()==typeof(Button))
                {
                    Button btn = (Button)item;
                    int row = findRow(btn.Location.X, btn.Location.Y);
                    int col = findCol(btn.Location.X, btn.Location.Y);

                    if(btn.Focused == true && btn.Text == "")
                    {
                        if (flag)
                        {
                            btn.ForeColor = Color.Blue;
                            btn.Text = "x";
                            flag = false;
                            if (checkWinOnRow(row, col, "x") || checkWinOnCol(row, col, "x") || checkWinOnMainDiagonal(row, col, "x") || checkWinOnAuxiliaryDiagonal(row, col, "x"))
                            {
                                MessageBox.Show("X Win");
                                ctr.Hide();
                            }
                        }
                        else
                        {
                            btn.ForeColor = Color.Red;
                            btn.Text = "o";
                            flag = true;
                            if (checkWinOnRow(row, col, "o") || checkWinOnCol(row, col, "o") || checkWinOnMainDiagonal(row, col, "o") || checkWinOnAuxiliaryDiagonal(row, col, "o"))
                            {
                                MessageBox.Show("O Win");
                                ctr.Hide();
                            }
                        }
                    }
                    
                }
            }
        }

        private int findRow(int x, int y)
        {
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (positions[i, j].Item1 == x && positions[i, j].Item2 == y)
                        return i;
                }
            }
            return -1;
        }

        private int findCol(int x, int y)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (positions[i, j].Item1 == x && positions[i, j].Item2 == y)
                        return j;
                }
            }
            return -1;
        }

        private bool checkWinOnRow(int row,int col, string text)
        {
            int countL = countElementLeft(row, col, text);
            int countR = countElementRight(row, col, text);

            if((countL + countR + 1) == caseWin)
                return true;
            return false;
        }

        private int countElementLeft(int row,int col, string text)
        {
            int count = 0;
            for (int j = col - 1; j >= 0; j--)
            {
                foreach (Control item in ctr.Controls)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        Button btn = (Button)item;
                        if (btn.Location.X == positions[row, j].Item1 && btn.Location.Y == positions[row, j].Item2)
                        {
                            if (btn.Text == text)
                                count++;
                            else
                                return count;
                        }
                    }
                }
            }
            return count;
        }

        private int countElementRight(int row, int col, string text)
        {
            int count = 0;
            for (int j = col + 1; j < n; j++)
            {
                foreach (Control item in ctr.Controls)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        Button btn = (Button)item;
                        if (btn.Location.X == positions[row, j].Item1 && btn.Location.Y == positions[row, j].Item2)
                        {
                            if (btn.Text == text)
                                count++;
                            else
                                return count;
                        }
                    }
                }
            }
            return count;
        }

        private bool checkWinOnCol(int row, int col, string text)
        {
            int countT = countElementAbove(row, col, text);
            int countB = countElementBelow(row, col, text);

            if ((countT + countB + 1) == caseWin)
                return true;
            return false;
        }

        private int countElementAbove(int row, int col, string text)
        {
            int count = 0;
            for (int i = row - 1; i >= 0; i--)
            {
                foreach (Control item in ctr.Controls)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        Button btn = (Button)item;
                        if (btn.Location.X == positions[i, col].Item1 && btn.Location.Y == positions[i, col].Item2)
                        {
                            if (btn.Text == text)
                                count++;
                            else
                                return count;
                        }
                    }
                }
            }
            return count;
        }

        private int countElementBelow(int row, int col, string text)
        {
            int count = 0;
            for (int i = row + 1; i < n; i++)
            {
                foreach (Control item in ctr.Controls)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        Button btn = (Button)item;
                        if (btn.Location.X == positions[i, col].Item1 && btn.Location.Y == positions[i, col].Item2)
                        {
                            if (btn.Text == text)
                                count++;
                            else
                                return count;
                        }
                    }
                }
            }
            return count;
        }

        private bool checkWinOnMainDiagonal(int row,int col, string text)
        {
            int countTL = countElementTopLeft(row, col, text);
            int countBR = countElementBottomRight(row, col, text);

            if ((countTL + countBR + 1) == caseWin)
                return true;
            return false;
        }

        private int countElementTopLeft(int row, int col, string text)
        {
            int count = 0;
            for (int i = row - 1; i >= 0; i--)
            {
                int j = --col >= 0 ? col : 0;
                foreach (Control item in ctr.Controls)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        Button btn = (Button)item;
                        if (btn.Location.X == positions[i, j].Item1 && btn.Location.Y == positions[i, j].Item2)
                        {
                            if (btn.Text == text)
                                count++;
                            else
                                return count;
                        }
                    }
                }
            }
            return count;
        }

        private int countElementBottomRight(int row, int col, string text)
        {
            int count = 0;
            for (int i = row + 1; i < n; i++)
            {
                int j = ++col < n ? col : (n-1);
                foreach (Control item in ctr.Controls)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        Button btn = (Button)item;
                        if (btn.Location.X == positions[i, i].Item1 && btn.Location.Y == positions[i, i].Item2)
                        {
                            if (btn.Text == text)
                                count++;
                            else
                                return count;
                        }
                    }
                }
            }
            return count;
        }

        private bool checkWinOnAuxiliaryDiagonal(int row, int col, string text)
        {
            int countTR = countElementTopRight(row, col, text);
            int countBL = countElementBottomLeft(row, col, text);

            if ((countTR + countBL + 1) == caseWin)
                return true;
            return false;
        }

        private int countElementTopRight(int row, int col, string text)
        {
            int count = 0;
            for (int j = col + 1; j < n; j++)
            {
                int i = --row >= 0 ? row : 0;
                foreach (Control item in ctr.Controls)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        Button btn = (Button)item;
                        if (btn.Location.X == positions[i, j].Item1 && btn.Location.Y == positions[i, j].Item2)
                        {
                            if (btn.Text == text)
                                count++;
                            else
                                return count;
                        }
                    }
                }
            }
            return count;
        }

        private int countElementBottomLeft(int row, int col, string text)
        {
            int count = 0;
            for (int j = col - 1; j >= 0; j--)
            {
                int i = ++row < n ? row : (n - 1);
                foreach (Control item in ctr.Controls)
                {
                    if (item.GetType() == typeof(Button))
                    {
                        Button btn = (Button)item;
                        if (btn.Location.X == positions[i, j].Item1 && btn.Location.Y == positions[i, j].Item2)
                        {
                            if (btn.Text == text)
                                count++;
                            else
                                return count;
                        }
                    }
                }
            }
            return count;
        }
    }
}
