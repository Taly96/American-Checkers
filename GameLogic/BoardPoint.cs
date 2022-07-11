namespace GameLogic
{
    public class BoardPoint
    {
        private int? m_Row = null;
        private int? m_Col = null;

        public BoardPoint(BoardPoint i_BoardPointToCopy)
        {
            m_Row = i_BoardPointToCopy.RowOnBoard;
            m_Col = i_BoardPointToCopy.RowOnBoard;
        }

        public BoardPoint(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int RowOnBoard
        {
            get
            {
                return m_Row.Value;
            }

            set
            {
                m_Row = value;
            }
        }

        public int ColOnBoard
        {
            get
            {
                return m_Col.Value;
            }

            set
            {
                m_Col = value;
            }
        }

        public bool Equals(BoardPoint i_PointToCheck)
        {
            bool isEqual = m_Col.Equals(i_PointToCheck.ColOnBoard)
                           && m_Row.Equals(i_PointToCheck.RowOnBoard);

            return isEqual;
        }
    }
}