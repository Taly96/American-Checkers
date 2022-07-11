using System.Collections.Generic;

namespace GameLogic
{
    public class Player
    {
        public enum eTypeOfPlayer
        {
            Human = 1,
            Computer,
        }

        private readonly uint? r_TypeOfPlayer = null;
        private readonly char? r_SoldierType = null;
        private readonly char? r_KingType = null;
        private readonly string r_PlayersFirstName = null;
        private readonly List<CheckersBoardSquare> r_PlayersSoldiersOnBoard = null;
        private int m_NumOfKings = 0;
        private int m_Score = 0;

        public Player(Player i_PlayerToCopy)
        {
            r_TypeOfPlayer = i_PlayerToCopy.TypeOfPlayer;
            r_SoldierType = i_PlayerToCopy.PlayerSoldierType;
            r_KingType = i_PlayerToCopy.PlayerKingType;
            r_PlayersFirstName = i_PlayerToCopy.PlayersName;
            r_PlayersSoldiersOnBoard = new List<CheckersBoardSquare>();
        }

        public Player(string i_PlayersFirstName, uint i_PlayersType, char i_PlayersSoldierType)
        {
            r_TypeOfPlayer = i_PlayersType;
            r_PlayersFirstName = i_PlayersFirstName;
            r_SoldierType = i_PlayersSoldierType;
            r_PlayersSoldiersOnBoard = new List<CheckersBoardSquare>();
            r_KingType = i_PlayersSoldierType == (char)CheckersBoardSquare.eSquareType.SoldierX
                             ? (char)CheckersBoardSquare.eSquareType.KingK
                             : (char)CheckersBoardSquare.eSquareType.KingU;
        }

        public int NumberOfKingsOnBoard
        {
            get
            {
                return m_NumOfKings;
            }

            set
            {
                m_NumOfKings = value;
            }
        }

        public List<CheckersBoardSquare> PlayersSoldiersOnBoard
        {
            get
            {
                return r_PlayersSoldiersOnBoard;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public char PlayerSoldierType
        {
            get
            {
                return r_SoldierType.Value;
            }
        }

        public string PlayersName
        {
            get
            {
                return r_PlayersFirstName;
            }
        }

        public int NumberOKingsAndSoldiersOnBoard
        {
            get
            {
                return r_PlayersSoldiersOnBoard.Count;
            }
        }

        public int NumberOfSoldiersOnBoard
        {
            get
            {
                return r_PlayersSoldiersOnBoard.Count - m_NumOfKings;
            }
        }

        public uint TypeOfPlayer
        {
            get
            {
                return r_TypeOfPlayer.Value;
            }
        }

        public char PlayerKingType
        {
            get
            {
                return r_KingType.Value;
            }
        }

        public void Reset()
        {
            m_NumOfKings = 0;

            r_PlayersSoldiersOnBoard.Clear();
        }

        public void RemoveSoldier(CheckersBoardSquare i_NoLongerPlayersSquare)
        {
            r_PlayersSoldiersOnBoard.Remove(i_NoLongerPlayersSquare);
        }

        public void AddSoldier(CheckersBoardSquare i_NewSquareForPlayer)
        {
            r_PlayersSoldiersOnBoard.Add(i_NewSquareForPlayer);
        }
    }
}
