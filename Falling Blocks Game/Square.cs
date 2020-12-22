using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Falling_Blocks_Game
{
    class Square
    {
        public enum SquareTypes { Maroon, Silver, Purple, Blue, Green, Brown, Teal };

        private SolidColorBrush border = null;
        private SolidColorBrush fill = null;
        private Rectangle rect = null;
        private Canvas canvas = null;
        public int Row { get; private set; }
        public int Col { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Square(Canvas canvas, SquareTypes st, int row, int col)
        {
            this.canvas = canvas;
            Row = row;
            Col = col;

            NewSquare(st);
            DrawSquare();
        }

        private void NewSquare(SquareTypes st)
        {
            switch (st)
            {
                case SquareTypes.Maroon:
                    this.fill = new SolidColorBrush(Colors.Maroon);
                    this.border = new SolidColorBrush(Colors.LightGray);
                    break;
                case SquareTypes.Silver:
                    this.fill = new SolidColorBrush(Colors.Silver);
                    this.border = new SolidColorBrush(Colors.DarkGray);
                    break;
                case SquareTypes.Purple:
                    this.fill = new SolidColorBrush(Colors.Purple);
                    this.border = new SolidColorBrush(Colors.LightGray);
                    break;
                case SquareTypes.Blue:
                    this.fill = new SolidColorBrush(Colors.DarkBlue);
                    this.border = new SolidColorBrush(Colors.LightGray);
                    break;
                case SquareTypes.Green:
                    this.fill = new SolidColorBrush(Colors.Green);
                    this.border = new SolidColorBrush(Colors.DarkGray);
                    break;
                case SquareTypes.Brown:
                    this.fill = new SolidColorBrush(Colors.Brown);
                    this.border = new SolidColorBrush(Colors.White);
                    break;
                case SquareTypes.Teal:
                    this.fill = new SolidColorBrush(Colors.Teal);
                    this.border = new SolidColorBrush(Colors.White);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            this.rect = new Rectangle
            {
                Fill = this.fill,
                Stroke = this.border,
                Width = 20,
                Height = 20
            };
        }

        private void DrawSquare()
        {
            if (rect != null && canvas.Children.Contains(rect))
            {
                canvas.Children.Remove(rect);
            }

            Canvas.SetTop(rect, Row * 20);
            Canvas.SetLeft(rect, Col * 20);
            canvas.Children.Add(rect);
        }

        internal void DeleteSquare()
        {
            if (this.rect != null && canvas.Children.Contains(this.rect))
            {
                canvas.Children.Remove(this.rect);
            }
        }

        public void MoveSquare(bool right)
        {
            DeleteSquare();
            //The caller is responsible for wall checking
            if (right)
            {
                Col++;
            }
            else
            {
                Col--;
            }
            DrawSquare();
        }

        public void DropSquare()
        {
            DeleteSquare();
            //caller is responsible for bounds-checking
            Row++;
            DrawSquare();
        }

        public void SetRow(int row)
        {
            DeleteSquare();
            Row = row;
            DrawSquare();
        }

        public void SetLoc(int row, int col)
        {
            DeleteSquare();
            Row = row;
            Col = col;
            DrawSquare();
        }
    }
}
