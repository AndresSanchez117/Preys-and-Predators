using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    /// <summary>
    /// A circle with a position and radius.
    /// </summary>
    class Circle
    {
        readonly float x;
        readonly float y;
        readonly float radius;

        public Circle(float x, float y, float radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

        internal float X => x;
        internal float Y => y;
        internal float Radius => radius;

        public override string ToString()
        {
            return "Centro: (" + x + "," + y + ") Radio: " + radius;
        }
    }
}
