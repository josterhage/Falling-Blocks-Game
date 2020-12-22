using System;
using System.Collections.Generic;
using System.Text;

namespace Falling_Blocks_Game
{
    class Field
    {
        public Square[,] Squares { get; private set; } = new Square[24, 12];
        private int lines = 0;

        public Field()
        {
            for (int r = 0; r < 24; r++)
            {
                for (int c = 0; c < 12; c++)
                {
                    Squares[r, c] = null;
                }
            }
            // what goes here?
        }

        public bool AddBlock(Block block)
        {
            for (int i = 0; i < 4; i++)
            {
                int row = block.Squares[i].Row;
                int col = block.Squares[i].Col;
                if (Squares[row, col] != null)
                {
                    return false;
                }
                Squares[row, col] = block.Squares[i];
            }

            ClearCompletes();
            return true;
        }

        private void ClearCompletes()
        {
            for (int r = 23; r > 0; r--)
            {
                if (RowComplete(r))
                {
                    lines++;
                    GamePage.GotLine();
                    GamePage.SetLevel(1 + (lines / 10));
                    ClearRow(r);
                    r++;
                }
            }
        }

        private void ClearRow(int r)
        {
            for (int c = 0; c < 12; c++)
            {
                Squares[r, c].DeleteSquare();
                Squares[r, c] = null;
            }

            for (int row = r; row > 0; row--)
            {
                for (int c = 0; c < 12; c++)
                {
                    if (Squares[row - 1, c] != null)
                    {
                        Squares[row, c] = Squares[row - 1, c];
                        Squares[row, c].SetRow(row);
                        Squares[row - 1, c] = null;
                    }
                }
            }
        }

        private bool RowComplete(int r)
        {
            for (int c = 0; c < 12; c++)
            {
                if (Squares[r, c] == null) return false;
            }
            return true;
        }

        public bool CheckBeneath(Block block)
        {
            //bool result = false;
            for (int i = 0; i < 4; i++)
            {
                int row = block.Squares[i].Row + 1;
                int col = block.Squares[i].Col;
                if (Squares[row, col] != null)
                {
                    return true;
                }
            }
            return false;
        }

        internal bool CanShift(bool v, Block block)
        {
            if (v)
            {
                for (int i = 0; i < 4; i++)
                {
                    int row = block.Squares[i].Row;
                    int col = block.Squares[i].Col + 1;
                    if (col == 12) return false;
                    if (Squares[row, col] != null)
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    int row = block.Squares[i].Row;
                    int col = block.Squares[i].Col - 1;
                    if (col < 0) return false;
                    if (Squares[row, col] != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
