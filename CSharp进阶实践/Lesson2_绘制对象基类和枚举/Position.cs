using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp进阶实践
{
    internal struct Position
    {
        public Position(int x,int y)
        {
            this.x = x; this.y = y;
        }

        public static bool operator ==(Position left, Position right)
        {
            if(left.x==right.x && left.y==right.y)
                return true;
            return false;
        }
        public static bool operator !=(Position left,Position right)
        {
            return !(left == right);
        }
        public static Position operator +(Position left, Position right)
        {
            Position pos = new Position(left.x+right.x,left.y+right.y);
            return pos;
        }

        public int x;
        public int y;

    }
}
