using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProyectoFinal
{
    /// <summary>
    /// Graph represented by adjacency list.
    /// </summary>
    class Graph
    {
        readonly List<Vertex> vertexList;
        Vertex objective;
        List<Prey> preys;
        List<Predator> predators;

        public Graph(List<Circle> cL, Bitmap bmp)
        {
            vertexList = new List<Vertex>();

            foreach (Circle c in cL)
                vertexList.Add(new Vertex(c, vertexList.Count));

            for (int i = 0; i < vertexList.Count; i++)
            {
                for (int j = i + 1; j < vertexList.Count; j++)
                {
                    if (AreAdjacent(vertexList[j], vertexList[i], bmp))
                    {
                        float x0 = vertexList[i].CircleData.X;
                        float y0 = vertexList[i].CircleData.Y;
                        float xf = vertexList[j].CircleData.X;
                        float yf = vertexList[j].CircleData.Y;

                        // Distancia Euclidiana
                        float fWeight = (float)Math.Sqrt((x0 - xf) * (x0 - xf) + (y0 - yf) * (y0 - yf));

                        Edge e1 = new Edge(vertexList[i], vertexList[j], fWeight);
                        Edge e2 = new Edge(vertexList[j], vertexList[i], fWeight);
                        vertexList[i].AddEdge(e1);
                        vertexList[j].AddEdge(e2);
                    }
                }
            }
            preys = new List<Prey>();
            predators = new List<Predator>();
            objective = null;
        }

        internal List<Vertex> Vertices => vertexList;

        internal Vertex Objective { get => objective; set => objective = value; }
        internal List<Prey> Preys => preys;
        internal List<Predator> Predators => predators;

        public int GetVertexCount()
        {
            return vertexList.Count;
        }

        public bool ContainsVertex(Vertex v)
        {
            return vertexList.Contains(v);
        }

        /// <summary>Determines wether (x, y) is in a vertex in the graph.</summary>
        public Vertex BelongsToVertex(int x, int y)
        {
            Circle c_i;
            float x_0;
            float y_0;
            float r;
            float k = 5;
            float s;
            for (int i = 0; i < vertexList.Count; i++)
            {
                c_i = vertexList[i].CircleData;
                x_0 = c_i.X;
                y_0 = c_i.Y;
                r = c_i.Radius + k;
                s = (x - x_0) * (x - x_0) + (y - y_0) * (y - y_0) - r * r;
                if (s <= 0)
                    return vertexList[i];
            }
            return null;
        }

        public void Draw(Graphics graphics)
        {
            Pen linePen = new Pen(Color.Black);

            for (int i = 0; i < GetVertexCount(); i++)
            {
                Vertex origin = this.Vertices[i];
                for (int j = 0; j < origin.Degree(); j++)
                {
                    Vertex destination = origin.Edges[j].Destination;
                    // No funciona para lazos
                    if (destination.Id > i)
                    {
                        float x0 = origin.CircleData.X;
                        float y0 = origin.CircleData.Y;

                        float xf = destination.CircleData.X;
                        float yf = destination.CircleData.Y;

                        graphics.DrawLine(linePen, x0, y0, xf, yf);
                    }
                }
            }

            if (objective != null)
            {
                Brush brush = new SolidBrush(Color.MediumOrchid);
                float x = objective.CircleData.X - objective.CircleData.Radius;
                float y = objective.CircleData.Y - objective.CircleData.Radius;

                graphics.FillEllipse(brush, (int)x, (int)y, objective.CircleData.Radius * 2, objective.CircleData.Radius * 2);
            }
        }

        public void DrawObjective(Graphics graphics)
        {
            if (objective != null)
            {
                Brush brush = new SolidBrush(Color.MediumOrchid);
                float x = objective.CircleData.X - objective.CircleData.Radius * 2;
                float y = objective.CircleData.Y - objective.CircleData.Radius * 2;

                graphics.FillEllipse(brush, (int)x, (int)y, objective.CircleData.Radius * 4, objective.CircleData.Radius * 4);
            }
        }

        /// <summary>
        ///  Determines wether v1 and v2 are adyacent in an image.
        /// </summary>
        bool AreAdjacent(Vertex v1, Vertex v2, Bitmap bmp)
        {
            float x1 = v1.CircleData.X;
            float y1 = v1.CircleData.Y;
            float x2 = v2.CircleData.X;
            float y2 = v2.CircleData.Y;

            bool bFlagFirst = false;
            bool bFlagSecond = false;

            float dx = x2 - x1;
            float dy = y2 - y1;
            float step;

            if (Math.Abs(dx) >= Math.Abs(dy))
                step = Math.Abs(dx);
            else
                step = Math.Abs(dy);

            dx /= step;
            dy /= step;

            float x;
            float y;

            x = x1;
            y = y1;

            for (int i = 0; i < step; i++)
            {
                Color color = bmp.GetPixel((int)Math.Round(x), (int)Math.Round(y));

                if (bFlagSecond && color.R == 255 && color.G == 255 && color.B == 255)
                {
                    return false;
                }
                else if (bFlagFirst && color.G != 255) // No funciona con obstaculos 255 en G (Verdes)
                {
                    bFlagSecond = true;
                }
                else if (!bFlagFirst && color.R == 255 && color.G == 255 && color.B == 255)
                {
                    bFlagFirst = true;
                }

                x += dx;
                y += dy;
            }

            return true;
        }
    }
}
