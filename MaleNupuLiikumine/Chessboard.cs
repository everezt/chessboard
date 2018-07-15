using System;
using System.Collections.Generic;
using System.Linq;

namespace MaleNupuLiikumine
{
    class Chessboard
    {
        // Chessboard grid
        private bool[,] _chessboardPositions;

        private int _chessboardRows;
        private int _chessboardColumns;



        public Chessboard(int rows = 8, int columns = 8)
        {
            // initialize board, sizes are optional
            this._chessboardPositions = new bool[rows, columns];
            this._chessboardRows = rows;

            if (columns > 26)
            {
                throw new Exception("Viga: Veerge ei saa olla rohkem kui 26!");
            }

            this._chessboardColumns = columns;
        }

        // Check if row and column values are within range of the board
        private bool isPositionInRange(int x, int y)
        {
            if ( x > _chessboardRows - 1 || y > _chessboardColumns - 1 || x < 0 || y < 0 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Check if row and colum value in board grid is already filled
        private bool isPositionFilled(int x, int y)
        {
            if (_chessboardPositions[x, y])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Fill the blocked positions imported from file
        public void fillBlocks(string[] blocks)
        {
            foreach (string block in blocks)
            {
                int[] xy = textToCord(block.Trim());

                this.fillPosition(xy[0], xy[1]);
            }
        }


        // Lock the positions in board grid
        private void fillPosition(int x, int y)
        {
           
            if (!isPositionInRange(x, y))
            {
                throw new Exception("Viga: Postitsioon mida üritatakse täita, asub mänguväljast väljas!");
            }

            if (isPositionFilled(x, y))
            {
                throw new Exception("Viga: Positsioon mida üritatakse täita on juba täidetud!");
            }

            _chessboardPositions[x, y] = true;

        }

        // Check if position is free
        private bool isMovePossible(int x, int y)
        {
            if (isPositionInRange(x, y))
            {
                if (!isPositionFilled(x, y))
                {
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            else
            {
                return false;
            }       
        }

        // to keep track of our distance from source
        // and the next squares to check
        private struct positionsToVisit
        {
            public int X;
            public int Y;
            public int distance;
            public string path;

            public positionsToVisit(int x, int y, int d, string p)
            {
                X = x;
                Y = y;
                distance = d;
                path = p;
            }
        }


        // Char A in ASCII table is at 65
        // So since arrays start with 0, then A = 65, B = 66 etc.
        private char cordToChar(int num)
        {
            return (char)(num + 65);
        }


        public int[] textToCord(string cord)
        {
            int y = (int)cord[0] - 65;
            int x;
            // Limit current position formating to A-Z
            if (!int.TryParse(cord.Remove(0, 1), out x) || y < 0 || y > 90)
            {
                throw new Exception("Viga: Positsiooni formaat on vale!");
            }
            // additional -1 comes since array starts with 0
            return new int[] { (x - 1), y };
        }

        public string getMovementInfo(int[] startXY, int[] endXY, string piece = "ratsu")
        {

            // Check for faults

            if (!isPositionInRange(startXY[0], startXY[1]))
            {
                throw new Exception("Viga: Nupu algpositsioon asub väljaspool mänguvälja!");
            }

            if (!isPositionInRange(endXY[0], endXY[1]))
            {
                throw new Exception("Viga: Nupu lõpp-positsioon asub väljaspool mänguvälja!");
            }

            if (!isMovePossible(endXY[0], endXY[1]))
            {
                throw new Exception("Viga: Ei saa määrata lõpp-positsiooni juba kinnisele alale!");
            }

            if (!isMovePossible(startXY[0], startXY[1]))
            {
                throw new Exception("Viga: Ei saa määrata algpositsiooni juba kinnisele alale!");
            }

            if (startXY == endXY)
            {
                throw new Exception("Viga: Algus ja lõpp ei saa asuda samal positsioonil!");
            }

            // format piece name
            piece = piece.ToLower();

            // pieces            
            Knight knight = new Knight(startXY);
            Pawn pawn = new Pawn(startXY);
            Rook rook = new Rook(startXY, this._chessboardRows, this._chessboardColumns);
            Bishop bishop = new Bishop(startXY, this._chessboardRows, this._chessboardColumns);

            // setup end position
            int endPositionX = endXY[0];
            int endPositionY = endXY[1];

            // format piece name to lower
            piece = piece.ToLower();

        
            // initialize already visited squares grid, by default all positions are false
            bool[,] visited = new bool[this._chessboardRows, this._chessboardColumns];
            // intialize list of queue, including path, distance and X, Y position
            List<positionsToVisit> toVisit = new List<positionsToVisit>();
            // did we reach end by going through all possible moves
            bool endReached = false;
             
            // Set first position maually
            visited[startXY[0], startXY[1]] = true;
            toVisit.Add(new positionsToVisit(startXY[0], startXY[1], 0, ""));

            while(toVisit.Count() != 0)
            {
                // Select first square we haven't visited
                positionsToVisit currentSquare = toVisit[0];
                // Remove it from queue since we're visiting it right now
                toVisit.RemoveAt(0);
                   

                if (currentSquare.X == endPositionX && currentSquare.Y == endPositionY)
                {       
                    endReached = true;
                    string outputContent = string.Format("{0}\r\n{1}", currentSquare.distance, currentSquare.path.Remove(currentSquare.path.Count() - 2, 1));
                    // Found target square, return info
                    return outputContent;
                }

                List<Piece.PossibleMovesSquares> possibleMoves = new List<Piece.PossibleMovesSquares>();

                // Select which class will produce our movements
                switch (piece)
                {
                    case "ratsu":
                        possibleMoves = knight.getPossibleMoves(currentSquare.X, currentSquare.Y);
                        break;
                    case "ettur":
                        possibleMoves = pawn.getPossibleMoves(currentSquare.X, currentSquare.Y);
                        break;
                    case "kahur":
                        possibleMoves = rook.getPossibleMoves(currentSquare.X, currentSquare.Y);
                        break;
                    case "oda":
                        possibleMoves = bishop.getPossibleMoves(currentSquare.X, currentSquare.Y);
                        break;
                    default:
                        possibleMoves = knight.getPossibleMoves(currentSquare.X, currentSquare.Y);
                        break;
                }

                string ignoreDirection = "";

                foreach (Piece.PossibleMovesSquares possibleMove in possibleMoves)
                {
                    // Rook and Bishop can move all over the grid with 1 move, so if we have blockage
                    // We'll ignore all possible moves towards that direction during that move cycle
                    if (!isMovePossible(possibleMove.X, possibleMove.Y) && possibleMove.Direction != null)
                    {
                        ignoreDirection = possibleMove.Direction;
                    }

                    // If the calculated move position allows us to move on the designated square and we haven't visited that square yet, add to queue
                    if (isMovePossible(possibleMove.X, possibleMove.Y) && !visited[possibleMove.X, possibleMove.Y] && possibleMove.Direction != ignoreDirection )
                    {
                        toVisit.Add(new positionsToVisit(possibleMove.X, possibleMove.Y, currentSquare.distance + 1, currentSquare.path + string.Format("{0}{1}, ", cordToChar(possibleMove.Y), possibleMove.X + 1, possibleMove.Direction)));
                        visited[currentSquare.X, currentSquare.Y] = true;
                        visited[possibleMove.X, possibleMove.Y] = true;

                    }
                   
                }
            }   
            

            

            if (!endReached)
            {                
                throw new Exception("Viga: Võimalikku teekonda ei ole!");
            }

            // dummy return
            return "";
           
        }
    }
}

