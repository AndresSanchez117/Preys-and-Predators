using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProyectoFinal
{
    class Predator : Particle
    {
        readonly int iId;
        readonly Graph scene;
        int iRange;
        int iCollisionRange;
        Prey prey;
        Vertex originVertex;
        Vertex destinationVertex;
        Image icon;

        public Predator(int ndId, Graph sceneGraph, Vertex ndOrigin) : base(new Point((int)ndOrigin.CircleData.X, (int)ndOrigin.CircleData.Y), new Point((int)ndOrigin.CircleData.X + 1, (int)ndOrigin.CircleData.Y + 1))
        {
            iId = ndId;
            scene = sceneGraph;
            iCollisionRange = (int)ndOrigin.CircleData.Radius;
            originVertex = ndOrigin;
            destinationVertex = ndOrigin;
            iRange = 300;
            //iSpeed += 1;

            // Icon made by Freepik from www.flaticon.com
            icon = Image.FromFile("../../Icons/dog.png");
        }

        internal int Id => iId;
        internal Vertex OriginVertex => originVertex;
        internal Vertex DestinationVertex => destinationVertex;
        internal Prey Prey { get => prey; set => prey = value; }
        internal int CollisionRange => iCollisionRange;
        internal int VisionRange => iRange;

        public void Update()
        {
            if (scene.Objective != null && scene.Preys.Count > 0)
            {
                prey = FindNewPrey();
                if (prey != null)
                {
                    prey.Predator = this;
                    // Detect Prey colision
                    if (EuclideanDistance(this.Position, prey.Position) < iCollisionRange && !prey.IsSafe)
                    {
                        // Can only eliminate prey if it is in the same edge
                        if ((originVertex == prey.CurrentVertex && destinationVertex == prey.DestinationVertex) || (originVertex == prey.DestinationVertex && destinationVertex == prey.CurrentVertex))
                        {
                            // Eliminate Prey
                            scene.Preys.Remove(prey);
                            prey = null;
                        }
                    }
                }

                if (!Walk())
                {
                    originVertex = destinationVertex;
                    if (prey == null)
                    {
                        // Random Walk
                        Random random = new Random();
                        int iDest = random.Next(originVertex.Degree());
                        destinationVertex = originVertex.Edges[iDest].Destination;
                    }
                    else
                    {
                        // Move greedily, determine closest adjacency to prey

                        destinationVertex = originVertex.Edges[0].Destination;
                        foreach (Edge e in originVertex.Edges)
                        {
                            if (e.Destination == prey.DestinationVertex)
                            {
                                destinationVertex = e.Destination;
                                break;
                            }

                            int iVertexX = (int)e.Destination.CircleData.X;
                            int iVertexY = (int)e.Destination.CircleData.Y;
                            int iPreyDestX = (int)prey.DestinationVertex.CircleData.X;
                            int iPreyDestY = (int)prey.DestinationVertex.CircleData.Y;
                            int x = (int)destinationVertex.CircleData.X;
                            int y = (int)destinationVertex.CircleData.Y;
                            if (EuclideanDistance(new Point(iVertexX, iVertexY), new Point(iPreyDestX, iPreyDestY)) < EuclideanDistance(new Point(x, y), new Point(iPreyDestX, iPreyDestY)))
                                destinationVertex = e.Destination;
                        }

                        // If destination is not the same as prey, check if can move to prey origin
                        if (destinationVertex != prey.DestinationVertex)
                        {
                            foreach (Edge e in originVertex.Edges)
                            {
                                if (e.Destination == prey.CurrentVertex)
                                {
                                    destinationVertex = e.Destination;
                                    break;
                                }
                            }
                        }
                    }
                    int x0 = (int)originVertex.CircleData.X;
                    int y0 = (int)originVertex.CircleData.Y;
                    int xf = (int)destinationVertex.CircleData.X;
                    int yf = (int)destinationVertex.CircleData.Y;
                    SetNewPath(new Point(x0, y0), new Point(xf, yf));
                }
            }
        }

        public override void Draw(Graphics graphics)
        {
            // Draw Range
            Pen rangePen = new Pen(Color.DarkRed, 4);
            graphics.DrawEllipse(rangePen, Position.X - iRange, Position.Y - iRange, iRange * 2, iRange * 2);

            Brush brush = null;

            if (prey == null)
                brush = new SolidBrush(Color.LightBlue);
            else
            {
                graphics.DrawLine(rangePen, Position, prey.Position);
                brush = new SolidBrush(Color.CornflowerBlue);
            }

            int x = Position.X - (iDiameter / 2);
            int y = Position.Y - (iDiameter / 2);
            graphics.FillEllipse(brush, x, y, iDiameter, iDiameter);
            graphics.DrawImage(icon, x, y, iDiameter, iDiameter);
        }

        Prey FindNewPrey()
        {
            if (prey != null)
                prey.Predator = null;

            Prey newPrey = null;
            foreach (Prey p in scene.Preys)
            {
                if (p.Predator == null || p == prey) // If prey has no predator
                {
                    if (EuclideanDistance(this.Position, p.Position) <= iRange) // If prey is in range
                        if (newPrey == null || EuclideanDistance(this.Position, p.Position) <= EuclideanDistance(this.Position, newPrey.Position))
                            newPrey = p;
                }
            }

            return newPrey;
        }

        /// <summary>
        /// Returns Euclidean distance between points p and q.
        /// </summary>
        double EuclideanDistance(Point p, Point q)
        {
            return Math.Sqrt((q.X - p.X) * (q.X - p.X) + (q.Y - p.Y) * (q.Y - p.Y));
        }
    }
}
