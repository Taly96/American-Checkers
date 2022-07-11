using System;

namespace GameLogic
{
    public class RoundEndedEventArgs : EventArgs
    {
        private readonly bool r_IsWin = false;
        private readonly string r_WinnersName = null;

        public RoundEndedEventArgs(bool i_IsWin, string i_WinnersName)
        {
            r_IsWin = i_IsWin;
            r_WinnersName = i_WinnersName;
        }

        public bool IsWin
        {
            get
            {
                return r_IsWin;
            }
        }

        public string WinnersName
        {
            get
            {
                return r_WinnersName;
            }
        }
    }
}
