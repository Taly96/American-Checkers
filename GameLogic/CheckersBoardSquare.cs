using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class CheckersBoardSquare
    {
        public enum eSoldierDirectionInSquare
        {
            Down = 1,
            Up = -1,
            Left = -1,
        }

        public enum eSquareType
        {
            Free = ' ',
            SoldierX = 'X',
            SoldierO = 'O',
            KingK = 'K',
            KingU = 'U',
        }

        private readonly BoardPoint r_SquareLocation = null;
        private bool m_IsFree = true;
        private bool m_IsKing = false;
        private char m_SoldierType = (char)eSquareType.Free;

        public event EventHandler SquareModified;

        public CheckersBoardSquare(int i_RowLoc, int i_ColLoc)
        {
            r_SquareLocation = new BoardPoint(i_RowLoc, i_ColLoc);
        }

        public CheckersBoardSquare(CheckersBoardSquare i_PointToCopy)
        {
            r_SquareLocation = new BoardPoint(i_PointToCopy.SquareLocation);
            m_IsFree = i_PointToCopy.IsFreeSquare;
            m_IsKing = i_PointToCopy.IsKingSquare;
            m_SoldierType = i_PointToCopy.m_SoldierType;
        }

        public BoardPoint SquareLocation
        {
            get
            {
                return r_SquareLocation;
            }
        }

        public char SquareType
        {
            get
            {
                return m_SoldierType;
            }

            set
            {
                m_SoldierType = (char)value;
            }
        }

        public bool IsFreeSquare
        {
            get
            {
                return m_IsFree;
            }

            set
            {
                m_IsFree = value;
            }
        }

        public bool IsKingSquare
        {
            get
            {
                return m_IsKing;
            }

            set
            {
                m_IsKing = value;
            }
        }

        public void InitSquare()
        {
            m_IsFree = true;
            m_IsKing = false;
            m_SoldierType = (char)eSquareType.Free;
            OnSquareModified(new ModifiedSquareEventArgs(true, false, false));
        }

        public void SetValues(CheckersBoardSquare i_BoardSquare)
        {
            m_IsFree = i_BoardSquare.IsFreeSquare;
            m_IsKing = i_BoardSquare.IsKingSquare;
            m_SoldierType = i_BoardSquare.SquareType;
            bool isCurrentSquareXType = m_SoldierType == (char)eSquareType.SoldierX;
            bool isCurrentSquareKingXType = m_SoldierType == (char)eSquareType.KingK;
            bool isXSquare = isCurrentSquareXType || isCurrentSquareKingXType;

            OnSquareModified(new ModifiedSquareEventArgs(m_IsFree, isXSquare, m_IsKing));
        }

        protected virtual void OnSquareModified(ModifiedSquareEventArgs i_Ea)
        {
            if (SquareModified != null)
            {
                SquareModified(this, i_Ea);
            }
        }

        public void SetSoldierOnSquare(
            uint i_BoardSize,
            List<CheckersBoardSquare> i_FirstPlayersSoldiersOnBoard,
            List<CheckersBoardSquare> i_SecondPlayersSoldiersOnBoard)
        {
            m_IsFree = false;
            uint firstBoardHalf = (i_BoardSize - 2) / 2;
            uint secondBoardHalf = i_BoardSize - firstBoardHalf;
            bool isSoldierTypeX = false;

            if (r_SquareLocation.RowOnBoard < firstBoardHalf)
            {
                m_SoldierType = (char)eSquareType.SoldierO;
                i_SecondPlayersSoldiersOnBoard.Add(this);
            }
            else if (r_SquareLocation.RowOnBoard >= secondBoardHalf)
            {
                m_SoldierType = (char)eSquareType.SoldierX;
                isSoldierTypeX = true;
                i_FirstPlayersSoldiersOnBoard.Add(this);
            }
            else
            {
                m_IsFree = true;
            }

            OnSquareModified(new ModifiedSquareEventArgs(m_IsFree, isSoldierTypeX, false));
        }
    }
}