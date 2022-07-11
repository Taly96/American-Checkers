using System.Drawing;

namespace GameLogic
{
    public class BoardMove
    {
        private BoardPoint m_MoveFrom = null;
        private BoardPoint m_MoveTo = null;
        private BoardPoint m_RivalsCoords = null;

        public BoardMove(int i_FromRow, int i_FromCol, int i_ToRow, int i_ToCol)
        {
            m_MoveFrom = new BoardPoint(i_FromRow, i_FromCol);
            m_MoveTo = new BoardPoint(i_ToRow, i_ToCol);
        }

        public BoardMove(BoardPoint i_MoveFrom, BoardPoint i_MoveTo)
        {
            m_MoveFrom = new BoardPoint(i_MoveFrom.RowOnBoard, i_MoveFrom.ColOnBoard);
            m_MoveTo = new BoardPoint(i_MoveTo.RowOnBoard, i_MoveTo.ColOnBoard);
        }

        public BoardMove(BoardPoint i_MoveFrom, BoardPoint i_MoveTo, BoardPoint i_RivalsCoords)
        {
            m_MoveFrom = new BoardPoint(i_MoveFrom.RowOnBoard, i_MoveFrom.ColOnBoard);
            m_MoveTo = new BoardPoint(i_MoveTo.RowOnBoard, i_MoveTo.ColOnBoard);
            m_RivalsCoords = new BoardPoint(i_RivalsCoords.RowOnBoard, i_RivalsCoords.ColOnBoard);
        }

        public BoardMove(Point i_FromPoint, Point i_ToPoint)
        {
            m_MoveFrom = new BoardPoint(i_FromPoint.X, i_FromPoint.Y);
            m_MoveTo = new BoardPoint(i_ToPoint.X, i_ToPoint.Y);
        }

        public int RivalInRow
        {
            get
            {
                return m_RivalsCoords.RowOnBoard;
            }

            set
            {
                m_RivalsCoords.RowOnBoard = value;
            }
        }

        public int RivalInCol
        {
            get
            {
                return m_RivalsCoords.ColOnBoard;
            }

            set
            {
                m_RivalsCoords.ColOnBoard = value;
            }
        }

        public BoardPoint RivalsCoords
        {
            get
            {
                return m_RivalsCoords;
            }
        }

        public BoardPoint FromPoint
        {
            get
            {
                return m_MoveFrom;
            }

            set
            {
                m_MoveFrom.ColOnBoard = value.ColOnBoard;
                m_MoveFrom.RowOnBoard = value.RowOnBoard;
            }
        }

        public BoardPoint ToPoint
        {
            get
            {
                return m_MoveTo;
            }

            set
            {
                m_MoveTo.ColOnBoard = value.ColOnBoard;
                m_MoveTo.RowOnBoard = value.RowOnBoard;
            }
        }

        public int MovingToCol
        {
            get
            {
                return ToPoint.ColOnBoard;
            }

            set
            {
                ToPoint.ColOnBoard = value;
            }
        }

        public int MovingToRow
        {
            get
            {
                return ToPoint.RowOnBoard;
            }

            set
            {
                ToPoint.RowOnBoard = value;
            }
        }

        public int MovingFromRow
        {
            get
            {
                return FromPoint.RowOnBoard;
            }
        }

        public int MovingFromCol
        {
            get
            {
                return FromPoint.ColOnBoard;
            }
        }
    }
}
