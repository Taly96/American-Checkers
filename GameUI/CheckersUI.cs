using System;
using System.Drawing;
using System.Windows.Forms;
using GameLogic;

namespace GameUI
{
    public class CheckersUI
    {
        private readonly FormGamePlay r_FormCheckersGame = null;
        private readonly FormGameSettings r_FormCheckersSettings = null;
        private readonly GamePlay r_CheckersGame = null;

        public CheckersUI()
        {
            r_FormCheckersGame = new FormGamePlay();
            r_FormCheckersSettings = new FormGameSettings();
            r_CheckersGame = new GamePlay();
            r_FormCheckersGame.MoveMade += r_FormGame_MoveMade;
            r_CheckersGame.RoundEnded += r_CheckersGame_RoundEnded;
            r_CheckersGame.GameUpdated += r_CheckersGame_GameUpdated;
        }

        private void setGameParams()
        {
            uint requestedBoardSize = r_FormCheckersSettings.BoardSize;
            string player1Name = r_FormCheckersSettings.player1Name;
            string player2Name = r_FormCheckersSettings.player2Name;
            uint player1Type = (uint)Player.eTypeOfPlayer.Human;
            char player1SoldierType = (char)CheckersBoardSquare.eSquareType.SoldierX;
            bool isPlayer2Human = r_FormCheckersSettings.isPlayer2Human;
            uint player2Type = isPlayer2Human ? (uint)Player.eTypeOfPlayer.Human : (uint)Player.eTypeOfPlayer.Computer;
            char player2SoldierType = (char)CheckersBoardSquare.eSquareType.SoldierO;

            r_FormCheckersGame.SetParamsToForm(
                requestedBoardSize,
                r_FormCheckersSettings.player1Name,
                r_FormCheckersSettings.player2Name);
            Player[] checkersPlayers = new Player[2];

            checkersPlayers[0] = new Player(player1Name, player1Type, player1SoldierType);
            checkersPlayers[1] = new Player(player2Name, player2Type, player2SoldierType);
            r_CheckersGame.SetGameParams(requestedBoardSize, checkersPlayers);
            for (int i = 0; i < requestedBoardSize; i++)
            {
                for (int j = 0; j < requestedBoardSize; j++)
                {
                    r_CheckersGame.SoldiersBoard[i, j].SquareModified += r_FormGame_SquareModified;
                }
            }
        }

        private void endGame()
        {
            r_CheckersGame.EndGame();
            string winnerName = r_CheckersGame.IsWin ? r_CheckersGame.GameWinner.PlayersName : "No one";

            MessageBox.Show(winnerName + " has won this time. Thank you for playing, see you next time!", "Game ended", MessageBoxButtons.OK);
        }

        public void RunGame()
        {
            if(r_FormCheckersSettings.ShowDialog() == DialogResult.OK)
            {
                setGameParams();
                r_FormCheckersGame.ShowDialog();
                endGame();
            }
            else
            {
                MessageBox.Show("Leaving so soon? Fine, see you next time!", "Game ended");
            }
        }

        private void r_CheckersGame_GameUpdated(object i_Sender, EventArgs i_E)
        {
            GameUpdatedEventArgs ea = i_E as GameUpdatedEventArgs;
            string player1ScoreToDisplay = r_CheckersGame.Player1.PlayersName;
            string player2ScoreToDisplay = r_CheckersGame.Player2.PlayersName;

            player1ScoreToDisplay += ": " + ea.Player1Score;
            player2ScoreToDisplay += ": " + ea.Player2Score;
            r_FormCheckersGame.Player1Score = player1ScoreToDisplay;
            r_FormCheckersGame.Player2Score = player2ScoreToDisplay;
            r_FormCheckersGame.MarkCurrentPlayerLabel(ea.CurrentPlayer);
        }

        private void r_CheckersGame_RoundEnded(object i_Sender, EventArgs i_E)
        {
            RoundEndedEventArgs ea = i_E as RoundEndedEventArgs;
            string winnersName = ea.IsWin ? ea.WinnersName : "No one";
            string msgToShow = winnersName;

            msgToShow += " has won!" + " Care for another round?";
            DialogResult res
                = MessageBox.Show(msgToShow, "Round ended", MessageBoxButtons.YesNo);

            if(res == DialogResult.Yes)
            {
                r_CheckersGame.ResetGame();
            }
            else
            {
                r_FormCheckersGame.Close();
            }
        }

        private void r_FormGame_SquareModified(object i_Sender, EventArgs i_E)
        {
            CheckersBoardSquare currentModifiedBoardSquare = i_Sender as CheckersBoardSquare;
            int row = currentModifiedBoardSquare.SquareLocation.RowOnBoard;
            int col = currentModifiedBoardSquare.SquareLocation.ColOnBoard;
            CheckersSoldierBox currentBox = r_FormCheckersGame[col, row];
            ModifiedSquareEventArgs ea = i_E as ModifiedSquareEventArgs;

            if(ea.ToEmptySquare)
            {
                currentBox.Image = null;
            }
            else if(ea.ToKing)
            {
                currentBox.Image = ea.ToSoldierX
                                       ? global::GameUI.Properties.Resources.BlackPieceKing
                                       : global::GameUI.Properties.Resources.WhitePieceKing;
            }
            else if(ea.ToSoldierX)
            {
                currentBox.Image = global::GameUI.Properties.Resources.BlackPiece;
            }
            else
            {
                currentBox.Image = global::GameUI.Properties.Resources.WhitePiece;
            }

            currentBox.BackColor = Color.White;
        }

        private void r_FormGame_MoveMade(object i_Sender, EventArgs i_E)
        {
            FormGamePlay checkersGame = i_Sender as FormGamePlay;
            Point moveFrom = checkersGame.PressedBoxes[0];
            Point moveTo = checkersGame.PressedBoxes[1];
            BoardMove currentPlayersMove = new BoardMove(moveFrom, moveTo);
            bool isValidPlayerMove = false;
            CheckersSoldierBox boxOnBard = null;

            isValidPlayerMove = r_CheckersGame.TryToMakeMove(currentPlayersMove);
            if (!isValidPlayerMove)
            {
                MessageBox.Show("Invalid move");
                boxOnBard = r_FormCheckersGame[moveFrom.Y, moveFrom.X];
                boxOnBard.BackColor = Color.White;
                boxOnBard = r_FormCheckersGame[moveTo.Y, moveTo.X];
                boxOnBard.BackColor = Color.White;
            }
        }
    }
}
