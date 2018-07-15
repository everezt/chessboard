using System.Collections.Generic;


namespace MaleNupuLiikumine
{
    class Knight : Piece
    {
        private int[] _movesX;
        private int[] _movesY;

        

        public List<PossibleMovesSquares> getPossibleMoves(int x, int y)
        {
            List<PossibleMovesSquares> possibleMoves = new List<PossibleMovesSquares>();

            // map the possible moves and return
            for (int i = 0; i < 8; i++)
            {
                possibleMoves.Add(new PossibleMovesSquares(x + this._movesX[i], y + this._movesY[i]));
            }

            return possibleMoves;
        }

        public Knight(int[] xy)
        {
            // possible moves with knight
            this._movesX = new int[] { -2, -1, 1, 2, -2, -1, 1, 2 };
            this._movesY = new int[] { -1, -2, -2, -1, 1, 2, 2, 1 };
            // set piece XY position
            this.setXY(xy);
        }
    }
}
