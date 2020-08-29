using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    /// <summary>
    /// Represents a Graph's Edge. 
    /// </summary>
    class Edge : IComparable<Edge>
    {
        readonly Vertex origin;
        readonly Vertex destination;
        readonly float fWeight;

        public Edge(Vertex ndOri, Vertex ndDest, float ndWeight)
        {
            origin = ndOri;
            destination = ndDest;
            fWeight = ndWeight;
        }

        internal Vertex Origin => origin;
        internal Vertex Destination => destination;
        internal float Weight => fWeight;

        public int CompareTo(Edge other)
        {
            if (this.fWeight < other.fWeight)
                return -1;
            else if (this.fWeight > other.fWeight)
                return 1;
            else
                return 0;
        }

        public override string ToString()
        {
            return "Origen: " + origin.Id + ", Destino: " + destination.Id;
        }
    }
}
