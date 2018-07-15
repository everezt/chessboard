using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaleNupuLiikumine
{
    class Piece
    {

        int[] _pieceXY;

        // Possible move squares transfer struct
        public struct PossibleMovesSquares
        {
            public int X;
            public int Y;
            public string Direction;

            public PossibleMovesSquares(int x, int y, string direction = null)
            {
                X = x;
                Y = y;
                Direction = direction;
            }
        }

        // set pieces XY position
        protected void setXY(int[] xy)
        {
            this._pieceXY = xy;
        }

        // get position 
        public int[] getPosition()
        {
            return _pieceXY;
        }

    }
}