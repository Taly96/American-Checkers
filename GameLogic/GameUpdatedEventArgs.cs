using System;

namespace GameLogic
{
    public class GameUpdatedEventArgs : EventArgs
    {
        private readonly int r_Player1Score = 0;
        private readonly int r_Player2Score = 0;
        private readonly uint r_CurrentPlayer = 0;

        public GameUpdatedEventArgs(int i_Player1Score, int i_Player2Score, uint i_CurrentPlayer)
        {
            r_CurrentPlayer = i_CurrentPlayer;
            r_Player1Score = i_Player1Score;
            r_Player2Score = i_Player2Score;
        }

        public int Player1Score
        {
            get
            {
                return r_Player1Score;
            }
        }

        public int Player2Score
        {
            get
            {
                return r_Player2Score;
            }
        }

        public uint CurrentPlayer
        {
            get
            {
                return r_CurrentPlayer;
            }
        }
    }
}
