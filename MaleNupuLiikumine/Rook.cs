using System.Collections.Generic;
using System;

namespace MaleNupuLiikumine
{
    class Rook : Piece
    {
        private int _boardRows;
        private int _boardColumns;

        public List<PossibleMovesSquares> getPossibleMoves(int x, int y)
        {
            List<PossibleMovesSquares> possibleMoves = new List<PossibleMovesSquares>();

            for (int i = x; i < this._boardRows; i++)
            {
                // possible moves UP
                possibleMoves.Add(new PossibleMovesSquares(x + (i - x), y, "UP"));
            }

            for (int i = x; i >= 0; i--)
            {
                // possible moves DOWN
                possibleMoves.Add(new PossibleMovesSquares( x + (i - x), y, "DOWN"));
            }

            for (int i = y; i < this._boardColumns; i++)
            {
                // possible moves RIGHT
                possibleMoves.Add(new PossibleMovesSquares(x, y + (i - y), "RIGHT"));
            }

            for (int i = y; i >= 0; i--)
            {
                // possible moves LEFT
                possibleMoves.Add(new PossibleMovesSquares(x, y + (i - y), "LEFT"));
            }

            return possibleMoves;
        }

        public Rook(int[] xy, int rowsCount, int columnsCount)
        {       
            this._boardRows = rowsCount;
            this._boardColumns = columnsCount;
            this.setXY(xy);

        }
    }
}
