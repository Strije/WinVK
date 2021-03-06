﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VK.GenericUI
{
    public static class RoundRect
    {
        public static void FillRoundRectangle(this Graphics g, Brush brush, int x, int y, int width, int height, int radius)
        {
            float fx = Convert.ToSingle(x);
            float fy = Convert.ToSingle(y);
            float fwidth = Convert.ToSingle(width);
            float fheight = Convert.ToSingle(height);
            float fradius = Convert.ToSingle(radius);

            g.FillRoundRectangle(brush, fx, fy, fwidth, fheight, fradius);
        }
        public static void FillRoundRectangle(this Graphics g, Brush brush, float x, float y, float width, float height, float radius)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = GetRoundedRect(rectangle, radius);
            g.FillPath(brush, path);
        }
        public static void FillRoundRectangle(this Graphics g, Brush brush, Rectangle rect, int radius)
        {
            RectangleF rectF = rect;
            float fradius = Convert.ToSingle(radius);

            g.FillRoundRectangle(brush, rectF, fradius);
        }
        public static void FillRoundRectangle(this Graphics g, Brush brush, RectangleF rectangle, float radius)
        {
            GraphicsPath path = GetRoundedRect(rectangle, radius);
            g.FillPath(brush, path);
        }

        public static void DrawRoundRectangle(this Graphics g, Pen pen, int x, int y, int width, int height, int radius)
        {
            float fx = Convert.ToSingle(x);
            float fy = Convert.ToSingle(y);
            float fwidth = Convert.ToSingle(width);
            float fheight = Convert.ToSingle(height);
            float fradius = Convert.ToSingle(radius);

            g.DrawRoundRectangle(pen, fx, fy, fwidth, fheight, fradius);
        }
        public static void DrawRoundRectangle(this Graphics g, Pen pen, float x, float y, float width, float height, float radius)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = GetRoundedRect(rectangle, radius);
            g.DrawPath(pen, path);
        }
        public static void DrawRoundRectangle(this Graphics g, Pen pen, Rectangle rect, float radius)
        {
            g.DrawRoundRectangle(pen, (RectangleF) rect, radius);
        }
        public static void DrawRoundRectangle(this Graphics g, Pen pen, RectangleF rectangle, float radius)
        {
            GraphicsPath path = GetRoundedRect(rectangle, radius);
            g.DrawPath(pen, path);
        }


        #region Get the desired Rounded Rectangle path.
        private static GraphicsPath GetRoundedRect(RectangleF baseRect, float radius)
        {
            // if corner radius is less than or equal to zero, 
            // return the original rectangle 
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            // if the corner radius is greater than or equal to 
            // half the width, or height (whichever is shorter) 
            // then return a capsule instead of a lozenge 
            if (radius >= (Math.Min(baseRect.Width, baseRect.Height)) / 2.0)
                return GetCapsule(baseRect);

            // create the arc for the rectangle sides and declare 
            // a graphics path object for the drawing 
            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new GraphicsPath();

            // top left arc 
            path.AddArc(arc, 180, 90);

            // top right arc 
            arc.X = baseRect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc 
            arc.Y = baseRect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc
            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
        #endregion

        #region Gets the desired Capsular path.
        private static GraphicsPath GetCapsule(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    // return horizontal capsule 
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    // return vertical capsule 
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    // return circle 
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception ex)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion
    }
}
