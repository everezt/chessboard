using System.Collections.Generic;


namespace MaleNupuLiikumine
{
    class Pawn : Piece
    {
        private int[] _movesX;
        private int[] _movesY;
        private bool _firstMove;


        public List<PossibleMovesSquares> getPossibleMoves(int x, int y)
        {
            List<PossibleMovesSquares> possibleMoves = new List<PossibleMovesSquares>();


            for (int i = 0; i < 1; i++)
            {
                if (_firstMove)
                {
                    // First move of pawn can be 2 squares
                    x++;

                    this._firstMove = false;
                }

                possibleMoves.Add(new PossibleMovesSquares(x + this._movesX[i], y + this._movesY[i]));
            }

            return possibleMoves;
        }

        public Pawn(int[] xy)
        {
            // possible moves with pawn
            this._movesX = new int[] { 1 };
            this._movesY = new int[] { 0 };
            // set piece position
            this.setXY(xy);
            this._firstMove = true;
        }
    }
}
