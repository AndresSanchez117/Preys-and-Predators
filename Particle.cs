using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    class Particle
    {
        protected List<Point> path;
        protected int iPathPosition;
        protected int iDiameter;
        protected int iSpeed;
        protected Point origin;
        protected Point destination;

        public Particle(Point ndOri, Point ndDest)
        {
            origin = ndOri;
            destination = ndDest;
            iDiameter = 40;
            iSpeed = 2;
            path = MakePath(origin, destination);
        }

        internal int Diameter { get => iDiameter; set => iDiameter = value; }
        internal int Speed { get => iSpeed; set => iSpeed = value; }
        internal Point Origin => origin;
        internal Point Destination => destination;
        internal Point Position => new Point(path[iPathPosition].X, path[iPathPosition].Y);
        internal Point PreviousPosition=> new Point(path[iPathPosition - 1].X, path[iPathPosition - 1].Y);

        /// <summary>
        /// Particle walks one step in the path. Returns true if it can keep walking, returns false otherwise.
        /// </summary>
        public bool Walk()
        {
            if (iPathPosition + iSpeed < path.Count)
            {
                iPathPosition += iSpeed;
                return true;
            }
            else
            {
                iPathPosition = path.Count - 1;
                return false;
            }
        }

        public void SetNewPath(Point ndOri, Point ndDest)
        {
            iPathPosition = 0;
            origin = ndOri;
            destination = ndDest;
            path = MakePath(origin, destination);
        }

        /// <summary>
        /// Builds a list of points from the path of p1 to p2.
        /// </summary>
        protected List<Point> MakePath(Point p1, Point p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            float step;

            if (Math.Abs(dx) >= Math.Abs(dy))
                step = Math.Abs(dx);
            else
                step = Math.Abs(dy);

            dx /= step;
            dy /= step;

            float x = p1.X;
            float y = p1.Y;

            List<Point> path = new List<Point>();

            for (int i = 0; i < step; i++)
            {
                path.Add(new Point((int)x, (int)y));
                x += dx;
                y += dy;
            }
            return path;
        }

        public virtual void Draw(Graphics graphics)
        {
            Brush brush = new SolidBrush(Color.DeepSkyBlue);

            int x = this.Position.X - (iDiameter / 2);
            int y = this.Position.Y - (iDiameter / 2);

            graphics.FillEllipse(brush, x, y, iDiameter, iDiameter);
        }
    }
}
