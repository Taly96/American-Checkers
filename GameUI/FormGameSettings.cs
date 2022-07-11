using System;
using System.Windows.Forms;

namespace GameUI
{
    public partial class FormGameSettings : Form
    {
        public FormGameSettings()
        {
            InitializeComponent();
        }

        public uint BoardSize
        {
            get
            {
                uint boardSize = 0;

                if (radioButtonSizeSmall.Checked)
                {
                    boardSize = 6;
                }
                else if (radioButtonSizeMedium.Checked)
                {
                    boardSize = 8;
                }
                else if (radioButtonSizeLarge.Checked)
                {
                    boardSize = 10;
                }

                return boardSize;
            }
        }

        public string player1Name
        {
            get
            {
                return textBoxPlayer1.Text;
            }
        }

        public string player2Name
        {
            get
            {
                return textBoxPlayer2.Text;
            }
        }

        public bool isPlayer2Human
        {
            get
            {
                return this.checkBoxPlayer2.Checked;
            }
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (!textBoxPlayer2.Enabled)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = string.Empty;
                textBoxPlayer2.TextAlign = HorizontalAlignment.Left;
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            bool isValidPlayer1Name = !string.IsNullOrEmpty(player1Name);
            bool isValidPlayer2Name = !string.IsNullOrEmpty(player2Name);

            if (isValidPlayer1Name && isValidPlayer2Name)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("A girl has no name?");
            }
        }
    }
}
