using System.Collections.Generic;

namespace GameLogic
{
    public class Board
    {
        public enum eBoardSizeOptions
        {
            Small = 6,
            Medium = 8,
            Large = 10,
        }

        private readonly uint r_BoardSize;
        private readonly CheckersBoardSquare[,] r_SoldiersBoard = null;

        public Board(uint i_UserInputBoardSize)
        {
            r_BoardSize = i_UserInputBoardSize;
            r_SoldiersBoard = new CheckersBoardSquare[r_BoardSize, r_BoardSize];
            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    r_SoldiersBoard[i, j] = new CheckersBoardSquare(i, j);
                }
            }
        }

        public CheckersBoardSquare[,] SoldiersBoard
        {
            get
            {
                return r_SoldiersBoard;
            }
        }

        public uint CurrentBoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }

        public CheckersBoardSquare this[int i_RowToCheck, int i_ColToCheck]
        {
            get
            {
                return r_SoldiersBoard[i_RowToCheck, i_ColToCheck];
            }
        }

        public CheckersBoardSquare this[BoardPoint i_BoardPoint]
        {
            get
            {
                return r_SoldiersBoard[i_BoardPoint.RowOnBoard, i_BoardPoint.ColOnBoard];
            }

            set
            {
                r_SoldiersBoard[i_BoardPoint.RowOnBoard, i_BoardPoint.ColOnBoard] = value;
            }
        }

        public void SwapSquares(BoardMove i_MoveToUpdateOnBoard)
        {
            CheckersBoardSquare temp = new CheckersBoardSquare(this[i_MoveToUpdateOnBoard.FromPoint]);

            this[i_MoveToUpdateOnBoard.FromPoint].SetValues(this[i_MoveToUpdateOnBoard.ToPoint]);
            this[i_MoveToUpdateOnBoard.ToPoint].SetValues(temp);
        }

        private void initSoldiersBoard()
        {
            foreach (CheckersBoardSquare checkersSquare in r_SoldiersBoard)
            {
                if(checkersSquare.SquareType != (char)CheckersBoardSquare.eSquareType.Free)
                {
                    checkersSquare.InitSquare();
                }
            }
        }

        public bool IsCoordWithinRange(int i_RowToCheck, int i_ColToCheck)
        {
            bool isWithinValidRangeOfBoard = i_RowToCheck >= 0 &&
                                             i_RowToCheck < r_BoardSize
                                             && i_ColToCheck >= 0
                                             && i_ColToCheck < r_BoardSize;

            return isWithinValidRangeOfBoard;
        }

        public void ResetBoard(
            List<CheckersBoardSquare> i_Player1SoldiersOnBoard,
            List<CheckersBoardSquare> i_Player2SoldiersOnBoard)
        {
            initSoldiersBoard();
            InitGameBoard(i_Player1SoldiersOnBoard, i_Player2SoldiersOnBoard);
        }

        public void InitGameBoard(
            List<CheckersBoardSquare> i_FirstPlayersSoldiersOnBoard,
            List<CheckersBoardSquare> i_SecondPlayersSoldiersOnBoard)
        {
            bool isValidSquareForSoldier = false;

            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    isValidSquareForSoldier = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                    if (isValidSquareForSoldier)
                    {
                        r_SoldiersBoard[i, j].SetSoldierOnSquare(
                            r_BoardSize,
                            i_FirstPlayersSoldiersOnBoard,
                            i_SecondPlayersSoldiersOnBoard);
                    }
                }
            }
        }
    }
}