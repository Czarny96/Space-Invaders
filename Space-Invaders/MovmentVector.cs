using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class MovmentVector
    {
        int x_movment;
        int y_movment;

        public MovmentVector(int x_movment, int y_movment)
        {
            this.X_movment = x_movment;
            this.Y_movment = y_movment;
        }

        public int X_movment { get => x_movment; set => x_movment = value; }
        public int Y_movment { get => y_movment; set => y_movment = value; }

        public void changeMovment(int new_x_movment, int new_y_movment)
        {
            this.X_movment = new_x_movment;
            this.Y_movment = new_y_movment;
        }
    }
}
