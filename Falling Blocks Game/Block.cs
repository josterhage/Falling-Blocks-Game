using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Falling_Blocks_Game
{
    class Block
    {
        private int[] relRow = new int[4];
        private int[] relCol = new int[4];
        private readonly Square.SquareTypes st;
        private Position position = Position.north;

        //public
        public enum Position { north, east, south, west };
        public int Row { get; private set; }
        public int Col { get; private set; }
        public int BottomRow { get; private set; }
        public int RightCol { get; private set; }
        public Square[] Squares { get; private set; } = new Square[4];

        //Constructor takes the type of block, a handle to the gameField, and the x,y coordinates of this block
        public Block(Square.SquareTypes st, Canvas canvas, int row, int col)
        {
            Row = row;
            Col = col;
            this.st = st;
            CreateBlock(canvas);
        }

        private void CreateBlock(Canvas canvas)
        {
            SetBlock();
            for (int i = 0; i < 4; i++)
            {
                this.Squares[i] = new Square(canvas, st, Row + relRow[i], Col + relCol[i]);
            }
        }

        private void SetBlock()
        {
            switch (st)
            {
                case Square.SquareTypes.Maroon:
                    MaroonBlock();
                    break;
                case Square.SquareTypes.Silver:
                    SilverBlock();
                    break;
                case Square.SquareTypes.Purple:
                    PurpleBlock();
                    break;
                case Square.SquareTypes.Blue:
                    BlueBlock();
                    break;
                case Square.SquareTypes.Green:
                    GreenBlock();
                    break;
                case Square.SquareTypes.Brown:
                    BrownBlock();
                    break;
                case Square.SquareTypes.Teal:
                    TealBlock();
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public void ShiftBlock(bool right)
        {
            if (right)
            {
                if ((Col + RightCol) < 11)
                {
                    Col++;
                    for (int i = 0; i < 4; i++)
                    {
                        Squares[i].MoveSquare(true);
                    }
                }
            }
            else
            {
                if (Col > 0)
                {
                    Col--;
                    for (int i = 0; i < 4; i++)
                    {
                        Squares[i].MoveSquare(false);
                    }
                }
            }
        }

        public void RotateBlock(bool right)
        {
            if (right)
            {
                switch (position)
                {
                    case Position.north:
                    case Position.south:
                        switch (st)
                        {
                            case Square.SquareTypes.Maroon:
                                if ((Col + RightCol) < 9)
                                {
                                    position++;
                                }
                                else
                                {
                                    Col = 8;
                                    position++;
                                }
                                break;

                            case Square.SquareTypes.Brown:
                            case Square.SquareTypes.Green:
                            case Square.SquareTypes.Purple:
                            case Square.SquareTypes.Silver:
                            case Square.SquareTypes.Teal:

                                if ((Col + RightCol) < 10)
                                {
                                    position++;
                                }
                                else
                                {
                                    Col = 9;
                                    position++;
                                }
                                break;
                            case Square.SquareTypes.Blue:
                                position++;
                                break;
                            default:
                                throw new InvalidOperationException();
                        }
                        break;
                    case Position.east:
                        switch (st)
                        {
                            case Square.SquareTypes.Maroon:
                                if (Row < 20)
                                {
                                    position++;
                                }
                                break;
                            case Square.SquareTypes.Brown:
                            case Square.SquareTypes.Green:
                            case Square.SquareTypes.Purple:
                            case Square.SquareTypes.Silver:
                            case Square.SquareTypes.Teal:
                                if (Row < 21)
                                {
                                    position++;
                                }
                                break;
                            case Square.SquareTypes.Blue:
                                position++;
                                break;
                            default:
                                throw new InvalidOperationException();
                        }
                        break;
                    case Position.west:
                        switch (st)
                        {
                            case Square.SquareTypes.Maroon:
                                if (Row < 20)
                                {
                                    position = Position.north;
                                }
                                break;
                            case Square.SquareTypes.Brown:
                            case Square.SquareTypes.Green:
                            case Square.SquareTypes.Purple:
                            case Square.SquareTypes.Silver:
                            case Square.SquareTypes.Teal:
                                if (Row < 21)
                                {
                                    position = Position.north;
                                }
                                break;
                            case Square.SquareTypes.Blue:
                                position = Position.north;
                                break;
                            default:
                                throw new InvalidOperationException();
                        }
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            else
            {
                if (position > Position.north)
                {
                    position--;
                }
                else
                {
                    position = Position.west;
                }
            }
            SetBlock();
            DrawBlock();
        }

        //private methods
        private void DrawBlock()
        {
            for (int i = 0; i < 4; i++)
            {
                Squares[i].SetLoc(Row + relRow[i], Col + relCol[i]);
            }
        }

        private void MaroonBlock()
        {
            switch (position)
            {
                case Position.north:
                case Position.south:

                    for (int i = 0; i < 4; i++)
                    {

                        relRow[i] = i;
                        relCol[i] = 0;

                    }

                    BottomRow = 3;
                    RightCol = 0;

                    break;

                case Position.east:
                case Position.west:

                    for (int i = 0; i < 4; i++)
                    {

                        relRow[i] = 0;
                        relCol[i] = i;

                    }

                    BottomRow = 0;
                    RightCol = 3;

                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        private void SilverBlock()
        {
            switch (position)
            {
                case Position.north:

                    relRow[0] = relRow[1] = 2;
                    relRow[2] = 1;
                    relRow[3] = 0;

                    relCol[0] = 0;
                    relCol[1] = relCol[2] = relCol[3] = 1;

                    RightCol = 1;
                    BottomRow = 2;

                    break;
                case Position.east:

                    relCol[0] = relCol[1] = 0;
                    relCol[2] = 1;
                    relCol[3] = 2;

                    relRow[0] = 0;
                    relRow[1] = relRow[2] = relRow[3] = 1;

                    RightCol = 2;
                    BottomRow = 1;

                    break;
                case Position.south:

                    relCol[1] = relCol[2] = relCol[3] = 0;
                    relCol[0] = 1;

                    relRow[0] = relRow[1] = 0;
                    relRow[2] = 1;
                    relRow[3] = 2;

                    RightCol = 1;
                    BottomRow = 2;

                    break;
                case Position.west:

                    relRow[1] = relRow[2] = relRow[3] = 0;
                    relRow[0] = 1;

                    relCol[3] = 0;
                    relCol[2] = 1;
                    relCol[1] = relCol[0] = 2;

                    RightCol = 2;
                    BottomRow = 1;

                    break;
                default:
                    throw new InvalidOperationException();

            }
        }

        private void PurpleBlock()
        {
            switch (position)
            {
                case Position.north:

                    relCol[0] = relCol[1] = relCol[2] = 0;
                    relCol[3] = 1;

                    relRow[0] = 0;
                    relRow[1] = 1;
                    relRow[2] = relRow[3] = 2;

                    RightCol = 1;
                    BottomRow = 2;

                    break;
                case Position.east:

                    relRow[0] = relRow[1] = relRow[2] = 0;
                    relRow[3] = 1;

                    relCol[2] = relCol[3] = 0;
                    relCol[1] = 1;
                    relCol[0] = 2;

                    RightCol = 2;
                    BottomRow = 1;

                    break;
                case Position.south:

                    relCol[3] = 0;
                    relCol[0] = relCol[1] = relCol[2] = 1;

                    relRow[3] = relRow[2] = 0;
                    relRow[1] = 1;
                    relRow[0] = 2;

                    RightCol = 1;
                    BottomRow = 2;

                    break;
                case Position.west:

                    relRow[3] = 0;
                    relRow[0] = relRow[1] = relRow[2] = 1;

                    relCol[0] = 0;
                    relCol[1] = 1;
                    relCol[2] = relCol[3] = 2;

                    RightCol = 2;
                    BottomRow = 1;

                    break;
                default:
                    throw new InvalidOperationException();

            }
        }

        private void BlueBlock()
        {

            relCol[0] = relCol[2] = relRow[0] = relRow[1] = 0;
            relCol[1] = relCol[3] = relRow[2] = relRow[3] = 1;

            RightCol = 1;
            BottomRow = 1;

        }

        private void GreenBlock()
        {
            switch (position)
            {
                case Position.north:
                case Position.south:

                    relCol[0] = relCol[1] = 0;
                    relCol[2] = relCol[3] = 1;

                    relRow[0] = 0;
                    relRow[1] = relRow[2] = 1;
                    relRow[3] = 2;

                    RightCol = 1;
                    BottomRow = 2;

                    break;

                case Position.west:
                case Position.east:

                    relRow[0] = relRow[1] = 0;
                    relRow[2] = relRow[3] = 1;

                    relCol[3] = 0;
                    relCol[1] = relCol[2] = 1;
                    relCol[0] = 2;

                    RightCol = 2;
                    BottomRow = 1;

                    break;
                default:
                    throw new InvalidOperationException();

            }
        }

        private void BrownBlock()
        {
            switch (position)
            {
                case Position.north:

                    relCol[0] = relCol[1] = relCol[2] = 1;
                    relCol[3] = 0;

                    relRow[0] = 0;
                    relRow[1] = relRow[3] = 1;
                    relRow[2] = 2;

                    RightCol = 1;
                    BottomRow = 2;

                    break;

                case Position.east:

                    relRow[0] = relRow[1] = relRow[2] = 1;
                    relRow[3] = 0;

                    relCol[2] = 0;
                    relCol[1] = relCol[3] = 1;
                    relCol[0] = 2;

                    RightCol = 2;
                    BottomRow = 1;

                    break;

                case Position.south:

                    relCol[2] = relCol[1] = relCol[0] = 0;
                    relCol[3] = 1;

                    relRow[2] = 0;
                    relRow[3] = relRow[1] = 1;
                    relRow[0] = 2;

                    RightCol = 1;
                    BottomRow = 2;

                    break;
                case Position.west:

                    relRow[0] = relRow[1] = relRow[2] = 0;
                    relRow[3] = 1;

                    relCol[0] = 0;
                    relCol[1] = relCol[3] = 1;
                    relCol[2] = 2;

                    RightCol = 2;
                    BottomRow = 1;

                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        private void TealBlock()
        {
            switch (position)
            {
                case Position.north:
                case Position.south:

                    relCol[2] = relCol[3] = 0;
                    relCol[0] = relCol[1] = 1;

                    relRow[0] = 0;
                    relRow[1] = relRow[2] = 1;
                    relRow[3] = 2;

                    RightCol = 1;
                    BottomRow = 2;

                    break;

                case Position.west:
                case Position.east:

                    relRow[0] = relRow[1] = 0;
                    relRow[2] = relRow[3] = 1;

                    relCol[0] = 0;
                    relCol[1] = relCol[2] = 1;
                    relCol[3] = 2;

                    RightCol = 2;
                    BottomRow = 1;

                    break;
                default:
                    throw new InvalidOperationException();

            }
        }

        public void Fall()
        {
            //Caller is responsible for bounds-checking
            Row++;

            for (int i = 0; i < 4; i++)
            {
                Squares[i].DropSquare();
            }
        }

        public void Bottomout()
        {
            Row = 23 - BottomRow;
            for (int i = 0; i < 4; i++)
            {
                Squares[i].SetRow(Row + relRow[i]);
            }
        }
    }
}
