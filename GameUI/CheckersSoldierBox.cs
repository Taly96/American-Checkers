using System.Drawing;
using System.Windows.Forms;

namespace GameUI
{
    public class CheckersSoldierBox : PictureBox
    {
        private Point m_LocationInMatrix;
        private bool m_IsEmpty = true;

        public CheckersSoldierBox(int i_Row, int i_Col)
        {
            m_LocationInMatrix = new Point(i_Row, i_Col);
        }

        public Point LocationInMatrix
        {
            get
            {
                return m_LocationInMatrix;
            }
        }

        public int RowInMatrix
        {
            get
            {
                return m_LocationInMatrix.X;
            }

            set
            {
                m_LocationInMatrix.X = value;
            }
        }

        public int ColInMatrix
        {
            get
            {
                return m_LocationInMatrix.Y;
            }

            set
            {
                m_LocationInMatrix.Y = value;
            }
        }

        public bool isEmptyBox
        {
            get
            {
                return m_IsEmpty;
            }

            set
            {
                m_IsEmpty = value;
            }
        }
    }
}
