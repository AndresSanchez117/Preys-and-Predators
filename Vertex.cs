using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    /// <summary>
    /// Represents a Graph's Vertex.
    /// </summary>
    class Vertex
    {
        List<Edge> edgeList;
        Circle circleData;
        int iId;

        public Vertex(Circle c, int ndId)
        {
            iId = ndId;
            circleData = c;
            edgeList = new List<Edge>();
        }

        public int Id { get => iId; set => iId = value; }
        internal List<Edge> Edges => edgeList;
        internal Circle CircleData { get => circleData; set => circleData = value; }

        public void AddEdge(Edge e)
        {
            edgeList.Add(e);
        }

        public int Degree()
        {
            return edgeList.Count;
        }
    }
}
