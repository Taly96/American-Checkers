using System;

namespace GameLogic
{
    public class ModifiedSquareEventArgs : EventArgs
    {
        private readonly bool r_ToEmpty = false;
        private readonly bool r_ToSoldierX = false;
        private readonly bool r_ToKing = false;

        public ModifiedSquareEventArgs(bool i_ToEmpty, bool i_ToSoldierX, bool i_ToKing)
        {
            r_ToEmpty = i_ToEmpty;
            r_ToKing = i_ToKing;
            r_ToSoldierX = i_ToSoldierX;
        }

        public bool ToEmptySquare
        {
            get
            {
                return r_ToEmpty;
            }
        }

        public bool ToSoldierX
        {
            get
            {
                return r_ToSoldierX;
            }
        }

        public bool ToKing
        {
            get
            {
                return r_ToKing;
            }
        }
    }
}
