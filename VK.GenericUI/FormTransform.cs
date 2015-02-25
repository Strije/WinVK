using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace VK.GenericUI
{
    public static class FormTransform
    {
        public static void Transform(Form frm, int newWidth, int newHeight)
        {
            Transform(frm, new Size(newWidth, newHeight));
        }

        public static void Transform(Form frm, Size newSize)
        {
            Transform(frm, newSize, frm.Location);
        }

        public static void Transform(Form frm, Size newSize, Point newPos)
        {
            ParameterizedThreadStart threadStart = new ParameterizedThreadStart(RunTransformation);
            Thread transformThread = new Thread(threadStart);

            transformThread.Start(new object[] { frm, newSize, newPos });
        }

        private delegate void RunTransformationDelegate(object paramaters);
        private static void RunTransformation(object parameters)
        {
            Form frm = (Form)((object[])parameters)[0];
            if (frm.InvokeRequired)
            {
                RunTransformationDelegate del = new RunTransformationDelegate(RunTransformation);
                frm.Invoke(del, parameters);
            }
            else
            {
                //Animation variables
                double FPS = 300.0;
                long interval = (long)(Stopwatch.Frequency / FPS);
                long ticks1 = 0;
                long ticks2 = 0;

                //Dimension transform variables
                Size size = (Size)((object[])parameters)[1];
                Point loc = (Point)((object[])parameters)[2];

                int locStep = 5;
                int step = 10;

                int xDirection = frm.Width < size.Width ? 1 : -1;
                int yDirection = frm.Height < size.Height ? 1 : -1;

                int xStep = step * xDirection;
                int yStep = step * yDirection;

                int locXDirection = frm.Left < loc.X ? 1 : -1;
                int locYDirection = frm.Top < loc.Y ? 1 : -1;

                int locXStep = locStep * locXDirection;
                int locYStep = locStep * locYDirection;

                bool widthOff = IsVarOff(frm.Width, size.Width, xStep);
                bool heightOff = IsVarOff(frm.Height, size.Height, yStep);

                bool xOff = IsVarOff(frm.Left, loc.X, locXStep);
                bool yOff = IsVarOff(frm.Top, loc.Y, locYStep);

                while (widthOff || heightOff || xOff || yOff)
                {
                    //Get current timestamp
                    ticks2 = Stopwatch.GetTimestamp();

                    if (ticks2 >= ticks1 + interval) //only run logic if enough time has passed "between frames"
                    {
                        //Adjust the Form dimensions
                        if (widthOff)
                            frm.Width += xStep;

                        if (heightOff)
                            frm.Height += yStep;

                        if (xOff)
                            frm.Left += locXStep;

                        if (yOff)
                            frm.Top += locYStep;

                        widthOff = IsVarOff(frm.Width, size.Width, xStep);
                        heightOff = IsVarOff(frm.Height, size.Height, yStep);

                        xOff = IsVarOff(frm.Left, loc.X, locXStep);
                        yOff = IsVarOff(frm.Top, loc.Y, locYStep);

                        //Allows the Form to refresh
                        Application.DoEvents();

                        //Save current timestamp
                        ticks1 = Stopwatch.GetTimestamp();
                    }

                    Thread.Sleep(1);
                }

                frm.Size = size;
                frm.Location = loc;
            }
        }

        private static bool IsVarOff(int current, int target, int step)
        {
            //Do avoid uneven jumps, do not change the var if it is
            //within the step amount
            if (Math.Abs(current - target) <= Math.Abs(step)) return false;

            return (step > 0 && current < target) || //increasing direction - keep going if still too small
                   (step < 0 && current > target);   //decreasing direction - keep going if still too large
        }
    }
}
