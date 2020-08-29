using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    class DijkstraElement
    {
        readonly Vertex vertex;
        float weight;
        bool isDefinitive;
        Vertex predecesor;

        public DijkstraElement(Vertex ndVertex)
        {
            vertex = ndVertex;
            weight = float.PositiveInfinity;
            isDefinitive = false;
            predecesor = null;
        }

        internal float Weight { get => weight; set => weight = value; }
        internal bool IsDefinitive { get => isDefinitive; set => isDefinitive = value; }
        internal Vertex Predecesor { get => predecesor; set => predecesor = value; }
        internal Vertex Vertex => vertex;
    }
}
