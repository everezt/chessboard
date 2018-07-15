using System.Collections.Generic;

namespace MaleNupuLiikumine
{
    class Bishop : Piece
    {
        private int _boardRows;
        private int _boardColumns;


        public List<PossibleMovesSquares> getPossibleMoves(int x, int y)
        {
            List<PossibleMovesSquares> possibleMoves = new List<PossibleMovesSquares>();

            int limiter;


            // due bishop can only move diagonally then maximum amount of squares it can move
            // during one move cycle is limited by the smallest side
            if (this._boardRows < this._boardColumns)
            {
                limiter = this._boardRows;
            }
            else
            {
                limiter = this._boardColumns;
            }
           

            int xOffset = 0;
            int yOffset = 0;

            for (int i = 0; i < limiter; i++)
            {
                // possible moves north-east    
                possibleMoves.Add(new PossibleMovesSquares(x + xOffset, y + yOffset, "NE"));
                xOffset++;
                yOffset++;
            }

            xOffset = 0;
            yOffset = 0;

            for (int i = 0; i < limiter; i++)
            {
                // possible moves south-west
                possibleMoves.Add(new PossibleMovesSquares(x + xOffset, y + yOffset, "SW"));
                xOffset--;
                yOffset--;
            }

            xOffset = 0;
            yOffset = 0;

            for (int i = 0; i < limiter; i++)
            {
                // possible moves north-wst
                possibleMoves.Add(new PossibleMovesSquares(x + xOffset, y + yOffset, "NW"));
                xOffset--;
                yOffset++;
            }


            xOffset = 0;
            yOffset = 0;

            for (int i = 0; i < limiter; i++)
            {
                // possible moves south-east
                possibleMoves.Add(new PossibleMovesSquares(x + xOffset, y + yOffset, "SE"));
                xOffset++;
                yOffset--;
            }


            return possibleMoves;
        }

        public Bishop(int[] xy, int rowsCount, int columnsCount)
        {
            this._boardRows = rowsCount;
            this._boardColumns = columnsCount;
            this.setXY(xy);
        }
    }
}
