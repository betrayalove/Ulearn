// Вставьте сюда финальное содержимое файла DrawingProgram.cs

// Советую сделать самостоятельно, так как это самая полезная практика из всего курса))
// Качайте Resharper и Alt + Enter сделают все за вас)

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace RefactorMe
{
    class Painter
    {
        static float x;
        static float y;
        static Graphics graphic;

        public static void Initialize(Graphics newGraphic)
        {
            graphic = newGraphic;
            graphic.SmoothingMode = SmoothingMode.None;
            graphic.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void DrawTrajectory(Pen pen, double length, double angle)
        {
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphic.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        static float small = 0.375f;
        static float reallsmall = 0.04f;

        public static void DrawSide(int sz, double angle)
        {
            Painter.DrawTrajectory(Pens.Yellow, sz * small, angle);
            Painter.DrawTrajectory(Pens.Yellow, sz * reallsmall * Math.Sqrt(2), angle + Math.PI / 4);
            Painter.DrawTrajectory(Pens.Yellow, sz * small, angle + Math.PI);
            Painter.DrawTrajectory(Pens.Yellow, sz * small - sz * reallsmall, angle + Math.PI / 2);
            Painter.Change(sz * reallsmall, angle + (-Math.PI));
            Painter.Change(sz * reallsmall * Math.Sqrt(2), angle + 3 * Math.PI / 4);
        }

        public static void Draw(int width, int height, double angleOfTurn, Graphics graphic)
        {
            Painter.Initialize(graphic);
            var sz = Math.Min(width, height);
            var diagonalLength = Math.Sqrt(2) * (sz * small + sz * reallsmall) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;
            Painter.SetPosition(x0, y0);
            DrawSquare(sz);
        }

        public static void DrawSquare(int sz)
        {
            var angle = 0.0;
            DrawSide(sz, angle);
            angle = -Math.PI / 2;
            DrawSide(sz, angle);
            angle = Math.PI;
            DrawSide(sz, angle);
            angle = Math.PI / 2;
            DrawSide(sz, angle);
        }
    }
}