using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProyectoFinal
{
    class Prey : Particle
    {
        readonly int iId;
        bool isSafe;
        readonly Graph scene;
        Predator predator;
        Vertex currentVertex;
        Vertex destinationVertex;
        DijkstraElement[] DijkstraArray;
        List<Vertex> shortestPath;
        int iAtPath;
        bool bInDanger;
        Image icon;
        bool bInitialPathSet;

        public Prey(int ndId, Graph sceneGraph, Vertex ndInitial) : base(new Point((int)ndInitial.CircleData.X, (int)ndInitial.CircleData.Y), new Point((int)ndInitial.CircleData.X + 1, (int)ndInitial.CircleData.Y + 1))
        {
            iId = ndId;
            scene = sceneGraph;
            currentVertex = ndInitial;
            isSafe = true;
            bInDanger = false;
            bInitialPathSet = false;
            //iSpeed += 1;

            // Icon made by Freepik from www.flaticon.com
            icon = Image.FromFile("../../Icons/bone.png");
        }

        internal int Id => iId;
        internal bool IsSafe => isSafe;
        internal Predator Predator { get => predator; set => predator = value; }
        internal Vertex CurrentVertex => currentVertex;
        internal Vertex NextVertex { get => shortestPath[iAtPath + 1]; set => shortestPath[iAtPath + 1] = value; }
        internal Vertex DestinationVertex => destinationVertex;
        internal bool InitialPathSet => bInitialPathSet;

        public void Update()
        {
            if (scene.Objective != null)
            {
                if (predator != null && shortestPath == null)
                {
                    // If predator is going to next vertex, go back
                    if (destinationVertex == predator.DestinationVertex || destinationVertex == predator.OriginVertex)
                    {
                        // If predator is not in the same edge, go back, else keep running
                        if (currentVertex != predator.OriginVertex)
                        {
                            GoBack();
                            Console.WriteLine("[ARISTA] Depredador sin camino, da la vuelta");
                        }
                    }
                }
                // Check if predator is in path
                else if (predator != null && shortestPath.Contains(predator.DestinationVertex) && !bInDanger)
                {
                    // If predator is going to next vertex, go back
                    if (NextVertex == predator.DestinationVertex || NextVertex == predator.OriginVertex)
                    {
                        // But if predator is right behind, keep moving
                        if (currentVertex != predator.OriginVertex)
                        {
                            GoBack();
                            bInDanger = true;
                            shortestPath = null;
                            Console.WriteLine("[ARISTA] Depredador con camino, da la vuelta, en peligro");
                        }
                    }
                }
                // Check if predator is close enough, if it is move
                else if (predator != null && EuclideanDistance(Position, predator.Position) < predator.CollisionRange * 2)
                {
                    if (currentVertex != predator.DestinationVertex && destinationVertex != predator.DestinationVertex)
                    {
                        if (currentVertex != predator.OriginVertex && destinationVertex != predator.OriginVertex)
                        {
                            GoBack();
                            bInDanger = true;
                            shortestPath = null;
                            Console.WriteLine("[ARISTA] Depredador cercano, da la vuelta, en peligro");
                        }
                    }
                }

                if (!Walk())
                {
                    iAtPath++;
                    currentVertex = destinationVertex;

                    // If not in danger, continue normally
                    if (!bInDanger)
                    {
                        ContinueOnPath();
                        Console.WriteLine("[VERTICE] Continua normalmente");
                    }
                    // if in danger, try to find a new path
                    else
                    {
                        Vertex avoid = null;
                        if (predator != null)
                            avoid = predator.OriginVertex; // avoid = predator.DesinationVertex ?? 
                        MakeNewPath(avoid);
                        // If successful in finding a new path, move trough that path
                        if (shortestPath != null)
                        {
                            if (predator == null || (EuclideanDistance(Position, predator.Position) > predator.CollisionRange * 4))
                            {
                                ContinueOnPath();
                                Console.WriteLine("[VERTICE] Se ha encontrado un nuevo camino evitando al depredador.");
                                bInDanger = false;
                            }
                        }
                        // If not successful in finding a new path, move away to try again
                        else
                        {
                            Console.WriteLine("[VERTICE] Camino mas corto nulo.");
                            foreach (Edge e in currentVertex.Edges)
                            {
                                int iVertexX = (int)e.Destination.CircleData.X;
                                int iVertexY = (int)e.Destination.CircleData.Y;
                                int iDestX = (int)destinationVertex.CircleData.X;
                                int iDestY = (int)destinationVertex.CircleData.Y;

                                if (EuclideanDistance(new Point(iVertexX, iVertexY), predator.Position) > EuclideanDistance(new Point(iDestX, iDestY), predator.Position))
                                    destinationVertex = e.Destination;
                            }

                            // If option is to move to predator, stay but wait until predator is far enough to move
                            if (destinationVertex != predator.OriginVertex && destinationVertex != predator.DestinationVertex)
                            {
                                if (EuclideanDistance(Position, predator.Position) > predator.CollisionRange * 4)
                                {
                                    int xf = (int)destinationVertex.CircleData.X;
                                    int yf = (int)destinationVertex.CircleData.Y;
                                    SetNewPath(Position, new Point(xf, yf));
                                    Console.WriteLine("[VERTICE] Alejarse, no se tiene un camino");
                                }
                                else
                                    destinationVertex = currentVertex;
                            }
                            else
                                destinationVertex = currentVertex;
                        }
                    }
                }

                // Determine if got to objective
                int iObjectiveX = (int)scene.Objective.CircleData.X;
                int iObjectiveY = (int)scene.Objective.CircleData.Y;
                if (EuclideanDistance(Position, new Point(iObjectiveX, iObjectiveY)) <= iDiameter / 2)
                {
                    scene.Objective = null;
                    if (predator != null)
                        predator.Prey = null;
                }

                // Determine if safe
                Vertex safe = scene.BelongsToVertex(Position.X, Position.Y);
                if (safe == null)
                    isSafe = false;
                else
                    isSafe = true;
            }
        }

        public override void Draw(Graphics graphics)
        {
            Brush brush;
            if (predator == null)
            {
                brush = new SolidBrush(Color.PaleGreen);
            }
            else
            {
                brush = new SolidBrush(Color.LightGoldenrodYellow);
            }

            int x = Position.X - (iDiameter / 2);
            int y = Position.Y - (iDiameter / 2);
            graphics.FillEllipse(brush, x, y, iDiameter, iDiameter);
            graphics.DrawImage(icon, x, y, iDiameter, iDiameter);
        }

        public void SetInitialPath()
        {
            // Set initial shortest path
            if (!bInitialPathSet)
            {
                MakeNewPath();
                destinationVertex = NextVertex;
                int xf = (int)NextVertex.CircleData.X;
                int yf = (int)NextVertex.CircleData.Y;
                SetNewPath(Position, new Point(xf, yf));

                bInitialPathSet = true;
            }
            else
            {
                bInDanger = true;
                shortestPath = null;
            }
        }

        /// <summary>
        /// Build shortest path from current vertex to objective. Can avoid a vertex.
        /// </summary>
        void MakeNewPath(Vertex avoid = null)
        {
            DijkstraArray = Dijkstra(scene, currentVertex, scene.Objective, avoid);

            // Build shortest path from current vertex to objective
            shortestPath = new List<Vertex>();
            shortestPath.Add(scene.Objective);
            Vertex aux = DijkstraArray[scene.Objective.Id].Predecesor;

            // Can not reach objective.
            if (aux == null)
            {
                shortestPath = null;
                return;
            }

            do
            {
                shortestPath.Add(aux);
                aux = DijkstraArray[aux.Id].Predecesor;
                if (aux == null)
                    break;
            } while (true);


            shortestPath.Reverse();

            iAtPath = 0;

            if (shortestPath[0] != scene.BelongsToVertex(Position.X, Position.Y))
            {
                shortestPath = null;
                currentVertex = scene.BelongsToVertex(Position.X, Position.Y);
                Console.WriteLine("Camino mas corto invalido");
            }
        }

        /// <summary>
        /// Runs Dijkstra's algorithm, stops when a path for objective is found. Returns an array of Dijkstra elements.
        /// </summary>
        DijkstraElement[] Dijkstra(Graph g, Vertex s, Vertex objective, Vertex avoid = null)
        {
            // Initialize Dijkstra vector
            DijkstraElement[] vector = InitializeDijkstra(g, s, avoid);

            while (!DijkstraSolution(vector, objective))
            {
                DijkstraElement definitive = SelectDefinitive(vector);
                vector = UpdateDijkstra(vector, definitive);
            }

            return vector;
        }

        DijkstraElement[] InitializeDijkstra(Graph g, Vertex s, Vertex avoid)
        {
            int n = g.GetVertexCount();

            DijkstraElement[] vector = new DijkstraElement[n];
            for (int i = 0; i < n; i++)
            {
                vector[i] = new DijkstraElement(g.Vertices[i]);
                if (g.Vertices[i] == s)
                {
                    vector[i].Weight = 0;
                }
                // Mark vertex to avoid as definitive
                if (g.Vertices[i] == avoid)
                    vector[i].IsDefinitive = true;
            }

            return vector;
        }

        bool DijkstraSolution(DijkstraElement[] vector, Vertex objective)
        {
            foreach (DijkstraElement d in vector)
            {
                if (d.Vertex == objective && d.IsDefinitive)
                    return true;

                if (!d.IsDefinitive)
                    if (!float.IsInfinity(d.Weight))
                        return false;
            }
            return true; // ??
        }

        DijkstraElement SelectDefinitive(DijkstraElement[] vector)
        {
            DijkstraElement minElement = null;
            float fMin = float.PositiveInfinity;

            foreach (DijkstraElement d in vector)
            {
                if (!d.IsDefinitive && d.Weight < fMin)
                {
                    fMin = d.Weight;
                    minElement = d;
                }
            }
            if (minElement != null)
                minElement.IsDefinitive = true;

            return minElement;
        }

        DijkstraElement[] UpdateDijkstra(DijkstraElement[] vector, DijkstraElement definitive)
        {
            foreach (Edge e in definitive.Vertex.Edges)
            {
                int iDestinationID = e.Destination.Id;
                if (vector[iDestinationID].IsDefinitive)
                    continue;
                if (definitive.Weight + e.Weight < vector[iDestinationID].Weight)
                {
                    vector[iDestinationID].Weight = definitive.Weight + e.Weight;
                    vector[iDestinationID].Predecesor = definitive.Vertex;
                }
            }

            return vector;
        }

        void GoBack()
        {
            Vertex aux = currentVertex;
            currentVertex = destinationVertex;
            destinationVertex = aux;
            int xf = (int)destinationVertex.CircleData.X;
            int yf = (int)destinationVertex.CircleData.Y;
            SetNewPath(Position, new Point(xf, yf));
        }

        void ContinueOnPath()
        {
            destinationVertex = NextVertex;
            int xf = (int)destinationVertex.CircleData.X;
            int yf = (int)destinationVertex.CircleData.Y;
            SetNewPath(Position, new Point(xf, yf));
        }

        double EuclideanDistance(Point p, Point q)
        {
            return Math.Sqrt((q.X - p.X) * (q.X - p.X) + (q.Y - p.Y) * (q.Y - p.Y));
        }
    }
}
