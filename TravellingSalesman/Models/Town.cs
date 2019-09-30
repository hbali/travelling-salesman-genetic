using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class Town
    {
        public float x;
        public float y;

        public float Distance(Town t)
        {
            float dx = x - t.x;
            float dy = y - t.y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public Town(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

    }
}
