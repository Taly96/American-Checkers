using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GameUI
{
    public partial class FormGamePlay : Form
    {
        private const int k_PictureBoxWidth = 60;
        private const int k_PictureBoxHeight = 60;
        private const int k_ToolImageDimensions = 50;
        private const int k_FormHeightOffset = 90;
        private const int k_FormWidthOffset = 35;
        private readonly List<Point> r_PressedBoxes = null;
        private CheckersSoldierBox[,] m_CheckersBoard = null;

        public event EventHandler MoveMade;

        public FormGamePlay()
        {
            InitializeComponent();
            r_PressedBoxes = new List<Point>();
        }

        public List<Point> PressedBoxes
        {
            get
            {
                return r_PressedBoxes;
            }
        }

        public string Player1Score
        {
            set
            {
                this.labelScorePlayer1.Text = value;
            }
        }

        public string Player2Score
        {
            set
            {
                this.labelScorePlayer2.Text = value;
            }
        }

        public CheckersSoldierBox this[int i_Row, int i_Col]
        {
            get
            {
                return m_CheckersBoard[i_Row, i_Col];
            }
        }

        public void MarkCurrentPlayerLabel(uint i_CurrentPlayer)
        {
            if(i_CurrentPlayer == 0)
            {
                this.labelScorePlayer1.Font = new Font(this.labelScorePlayer1.Font, FontStyle.Bold);
                this.labelScorePlayer2.Font = new Font(this.labelScorePlayer2.Font, FontStyle.Regular);
            }
            else
            {
                this.labelScorePlayer2.Font = new Font(this.labelScorePlayer1.Font, FontStyle.Bold);
                this.labelScorePlayer1.Font = new Font(this.labelScorePlayer2.Font, FontStyle.Regular);
            }
        }

        private void setLabels(uint i_RequestedBoardSize, string i_Player1Name, string i_Player2Name)
        {
            this.labelScorePlayer1.Text = i_Player1Name + ": 0";
            this.labelScorePlayer1.Font = new Font(this.labelScorePlayer1.Font, FontStyle.Bold);
            this.labelScorePlayer2.Text = i_Player2Name + ": 0";
            int labelOffsetY = 30;
            int labelOffsetX = (this.Width / 2) - 1;
            Point middle = new Point(labelOffsetX, labelOffsetY);
            Point player1LabelLocation = middle, player2LabelLocation = middle;

            player1LabelLocation.Offset(-k_PictureBoxWidth * 2, 0);
            player2LabelLocation.Offset(k_PictureBoxWidth, 0);
            this.labelScorePlayer1.Location = player1LabelLocation;
            this.labelScorePlayer1.AutoSize = true;
            this.labelScorePlayer2.Location = player2LabelLocation;
            this.labelScorePlayer1.AutoSize = true;
        }

        private void setSoldierOnBoard(uint i_RequestedBoardSize, PictureBox i_CurrentPictureBox, int i_RowOnBoard)
        {
            int firstBoardHalf = ((int)i_RequestedBoardSize - 2) / 2;
            int secondBoardHalf = (int)i_RequestedBoardSize - firstBoardHalf;

            i_CurrentPictureBox.Enabled = true;
            i_CurrentPictureBox.Click += CurrentPictureBox_OnClick;
            i_CurrentPictureBox.Padding = new Padding(
                (i_CurrentPictureBox.Width - k_ToolImageDimensions ) / 2,
                (i_CurrentPictureBox.Width - k_ToolImageDimensions) / 2,
                0,
                0);
            i_CurrentPictureBox.BackColor = Color.White;
            if (i_RowOnBoard < firstBoardHalf)
            {
                i_CurrentPictureBox.Image = global::GameUI.Properties.Resources.WhitePiece;
            }
            else if (i_RowOnBoard >= secondBoardHalf)
            {
                i_CurrentPictureBox.Image = global::GameUI.Properties.Resources.BlackPiece;
            }
        }

        private void setBoard(uint i_RequestedBoardSize)
        {
            int pictureBoxOffsetX = 15;
            int pictureBoxOffsetY = this.labelScorePlayer1.Bottom + 10;
            PictureBox currentPictureBox = null;
            bool isValidSquareForSoldier = false;

            for (int i = 0; i < i_RequestedBoardSize; i++)
            {
                for (int j = 0; j < i_RequestedBoardSize; j++)
                {
                    m_CheckersBoard[i, j] = new CheckersSoldierBox(j, i);
                    Controls.Add(m_CheckersBoard[i, j]);
                    currentPictureBox = m_CheckersBoard[i, j];
                    currentPictureBox.Location = new Point(pictureBoxOffsetX, pictureBoxOffsetY);
                    currentPictureBox.Width = k_PictureBoxWidth;
                    currentPictureBox.Height = k_PictureBoxHeight;
                    currentPictureBox.Enabled = false;
                    pictureBoxOffsetY += k_PictureBoxWidth;
                    isValidSquareForSoldier = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                    if (isValidSquareForSoldier)
                    {
                        setSoldierOnBoard(i_RequestedBoardSize, currentPictureBox, j);
                    }
                    else
                    {
                        currentPictureBox.Image = global::GameUI.Properties.Resources.DisabledSquare;
                    }
                }

                pictureBoxOffsetY = this.labelScorePlayer1.Bottom + 10;
                pictureBoxOffsetX += k_PictureBoxHeight;
            }
        }

        public void SetParamsToForm(
            uint i_RequestedBoardSize,
            string i_Player1Name,
            string i_Player2Name)
        {
            this.Height = ((int)i_RequestedBoardSize * k_PictureBoxHeight) + k_FormHeightOffset;
            this.Width = ((int)i_RequestedBoardSize * k_PictureBoxWidth) + k_FormWidthOffset;
            m_CheckersBoard = new CheckersSoldierBox[i_RequestedBoardSize, i_RequestedBoardSize];
            setLabels(i_RequestedBoardSize, i_Player1Name, i_Player2Name);
            setBoard(i_RequestedBoardSize);
        }

        private void CurrentPictureBox_OnClick(object i_Sender, EventArgs i_E)
        {
            CheckersSoldierBox pressedBox = i_Sender as CheckersSoldierBox;

            if (pressedBox.BackColor == Color.White)
            {
                pressedBox.BackColor = Color.Aquamarine;
                r_PressedBoxes.Add(pressedBox.LocationInMatrix);
            }
            else
            {
                pressedBox.BackColor = Color.White;
                r_PressedBoxes.Remove(pressedBox.LocationInMatrix);
            }

            if (r_PressedBoxes.Count == 2)
            {
                OnMoveMade();
                r_PressedBoxes.Clear();
            }
        }

        protected virtual void OnMoveMade()
        {
            if (MoveMade != null)
            {
                MoveMade(this, EventArgs.Empty);
            }
        }
    }
}
