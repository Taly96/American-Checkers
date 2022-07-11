using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class GamePlay
    {
        public enum eSoldierValue
        {
            KingVal = 4,
            SoldierVal = 1,
        }

        private readonly uint r_NumOfPlayers = 2;
        private readonly uint r_NumOfBoardSizeOptions = 3;
        private readonly uint[] r_BoardSizeOptions = null;
        private readonly Player[] r_CheckersPlayers = null;
        private readonly List<BoardMove> r_RegularMovesForCurrentPlayer = null;
        private readonly List<BoardMove> r_EatMovesForCurrentPlayer = null;
        private Board m_CheckersBoard = null;
        private Player m_GameWinner = null;
        private BoardMove m_CurrentPlayersMove = null;
        private bool m_IsGameOn = true;
        private bool m_IsRoundOn = true;
        private uint m_CurrentPlayer = 0;
        private bool m_CurrentPlayerHasToEat = false;
        private bool m_IsContinuousEatMoveDemanded = false;
        private bool m_IsWin = false;
        private bool m_IsDecidedToQuit = false;
        public EventHandler RoundEnded;
        public EventHandler GameUpdated;

        public GamePlay()
        {
            r_BoardSizeOptions = new uint[]
                                     {
                                         (int)Board.eBoardSizeOptions.Small,
                                         (int)Board.eBoardSizeOptions.Medium,
                                         (int)Board.eBoardSizeOptions.Large,
                                     };
            r_NumOfBoardSizeOptions = (uint)r_BoardSizeOptions.Length;
            r_CheckersPlayers = new Player[r_NumOfPlayers];
            r_EatMovesForCurrentPlayer = new List<BoardMove>();
            r_RegularMovesForCurrentPlayer = new List<BoardMove>();
        }

        public Player Player1
        {
            get
            {
                return r_CheckersPlayers[0];
            }
        }

        public Player Player2
        {
            get
            {
                return r_CheckersPlayers[1];
            }
        }

        private Player NextPlayer
        {
            get
            {
                return Players[(m_CurrentPlayer + 1) % r_NumOfPlayers];
            }
        }

        private int NextPlayersNumberOfKingsAndSoldiersOnBoard
        {
            get
            {
                return NextPlayer.NumberOKingsAndSoldiersOnBoard;
            }
        }

        private int CurrentPlayersNumberOfKingsAndSoldiersOnBoard
        {
            get
            {
                return CurrentPlayer.NumberOKingsAndSoldiersOnBoard;
            }
        }

        private Player CurrentPlayer
        {
            get
            {
                return Players[m_CurrentPlayer];
            }
        }

        public Player GameWinner
        {
            get
            {
                return m_GameWinner;
            }

            set
            {
                m_GameWinner = value;
            }
        }

        public bool IsWin
        {
            get
            {
                return m_IsWin;
            }

            set
            {
                m_IsWin = value;
            }
        }

        public CheckersBoardSquare[,] SoldiersBoard
        {
            get
            {
                return m_CheckersBoard.SoldiersBoard;
            }
        }

        private Player[] Players
        {
            get
            {
                return r_CheckersPlayers;
            }
        }

        public uint CurrentPlayerSoldierType
        {
            get
            {
                return Players[m_CurrentPlayer].PlayerSoldierType;
            }
        }

        public bool RoundOn
        {
            get
            {
                return m_IsRoundOn;
            }

            set
            {
                m_IsRoundOn = value;
            }
        }

        public uint CurrentBoardSize
        {
            get
            {
                return m_CheckersBoard.CurrentBoardSize;
            }
        }

        private List<CheckersBoardSquare> CurrentPlayersSoldiersOnBoard
        {
            get
            {
                return CurrentPlayer.PlayersSoldiersOnBoard;
            }
        }

        private char CurrentPlayersKingType
        {
            get
            {
                return CurrentPlayer.PlayerKingType;
            }
        }

        public void SetGameParams(uint i_UserBoardSizeInput, Player[] i_CheckersPlayers)
        {
            m_CheckersBoard = new Board(i_UserBoardSizeInput);
            r_CheckersPlayers[0] = new Player(i_CheckersPlayers[0]);
            r_CheckersPlayers[1] = new Player(i_CheckersPlayers[1]);

            m_CheckersBoard.InitGameBoard(
                r_CheckersPlayers[0].PlayersSoldiersOnBoard,
                r_CheckersPlayers[1].PlayersSoldiersOnBoard);
        }

        private bool isPointWithinBoardRange(BoardPoint i_PointToCheck)
        {
            int rowToCheck = i_PointToCheck.RowOnBoard;
            int colToCheck = i_PointToCheck.ColOnBoard;
            bool isPointWithinRange = isCoordWithinBoardRange(rowToCheck, colToCheck);

            return isPointWithinRange;
        }

        private bool isCoordWithinBoardRange(int i_RowToCheck, int i_ColToCheck)
        {
            bool isWithinRange = m_CheckersBoard.IsCoordWithinRange(i_RowToCheck, i_ColToCheck);

            return isWithinRange;
        }

        private int getPlayersDirection()
        {
            int currentDirection =
                (CurrentPlayerSoldierType == (int)CheckersBoardSquare.eSquareType.SoldierX
                 || CurrentPlayersKingType == (int)CheckersBoardSquare.eSquareType.KingK)
                    ? ((int)CheckersBoardSquare.eSoldierDirectionInSquare.Up)
                    : (int)CheckersBoardSquare.eSoldierDirectionInSquare.Down;

            return currentDirection;
        }

        private void addEatMoveBasedOnDirectionIfValid(
            int i_UpOrDown,
            int i_LeftOrRight,
            BoardPoint i_CurrentPlayersSquareLocation)
        {
            int rowToCheck = i_CurrentPlayersSquareLocation.RowOnBoard + i_UpOrDown;
            int colToCheck = i_CurrentPlayersSquareLocation.ColOnBoard + i_LeftOrRight;
            BoardPoint rivalsCoords = new BoardPoint(rowToCheck, colToCheck);
            BoardPoint movingTo = new BoardPoint(
                rowToCheck + i_UpOrDown,
                colToCheck + i_LeftOrRight);
            BoardPoint movingFrom = new BoardPoint(
                i_CurrentPlayersSquareLocation.RowOnBoard,
                i_CurrentPlayersSquareLocation.ColOnBoard);

            addToEatMovesIfValid(new BoardMove(movingFrom, movingTo, rivalsCoords));
        }

        private void addToRegMovesIfValid(BoardMove i_MoveToCheck)
        {
            bool isValidRegMove = isPointWithinBoardRange(i_MoveToCheck.ToPoint)
                                 && isTargetSquareEmpty(i_MoveToCheck.ToPoint);

            if (isValidRegMove)
            {
                r_RegularMovesForCurrentPlayer.Add(
                    new BoardMove(
                        i_MoveToCheck.FromPoint,
                        i_MoveToCheck.ToPoint));
            }
        }

        private void addToEatMovesIfValid(BoardMove i_MoveToCheck)
        {
            bool isValidEatMove = isRivalsSquare(i_MoveToCheck.RivalInRow, i_MoveToCheck.RivalInCol)
                                  && isTargetSquareEmpty(i_MoveToCheck.ToPoint);

            if (isValidEatMove)
            {
                r_EatMovesForCurrentPlayer.Add(
                    new BoardMove(
                        i_MoveToCheck.FromPoint,
                        i_MoveToCheck.ToPoint,
                        i_MoveToCheck.RivalsCoords));
            }
        }

        private void addRegMoveBasedOnDirectionIfValid(
            int i_UpOrDown,
            int i_LeftOrRight,
            BoardPoint i_CurrentPlayersSquare)
        {
            int rowToCheck = i_CurrentPlayersSquare.RowOnBoard + i_UpOrDown;
            int colToCheck = i_CurrentPlayersSquare.ColOnBoard + i_LeftOrRight;

            addToRegMovesIfValid(
                new BoardMove(
                    i_CurrentPlayersSquare.RowOnBoard,
                    i_CurrentPlayersSquare.ColOnBoard,
                    rowToCheck,
                    colToCheck));
        }

        private void addPossibleMovesFromSquare(CheckersBoardSquare i_SquareToCheck, int i_CurrentPlayersDirection)
        {
            int possibleDirectionsToMove = 4;
            int colDirectionToCheck = (int)CheckersBoardSquare.eSoldierDirectionInSquare.Left;

            for (int i = 0; i < possibleDirectionsToMove; i++)
            {
                addEatMoveBasedOnDirectionIfValid(
                    i_CurrentPlayersDirection,
                    colDirectionToCheck,
                    i_SquareToCheck.SquareLocation);
                addRegMoveBasedOnDirectionIfValid(
                    i_CurrentPlayersDirection,
                    colDirectionToCheck,
                    i_SquareToCheck.SquareLocation);
                colDirectionToCheck *= -1;
                if (i == 1 && i_SquareToCheck.IsKingSquare == true)
                {
                    i_CurrentPlayersDirection *= -1;
                    colDirectionToCheck *= -1;
                }
                else if (i == 1 && i_SquareToCheck.IsKingSquare == false)
                {
                    break;
                }
            }
        }

        private void generateEatAndRegularMovesForPlayer(int i_CurrentPlayersDirection)
        {
            r_EatMovesForCurrentPlayer.Clear();
            r_RegularMovesForCurrentPlayer.Clear();
            foreach (CheckersBoardSquare playersSquare in CurrentPlayersSoldiersOnBoard)
            {
                addPossibleMovesFromSquare(playersSquare, i_CurrentPlayersDirection);
            }
        }

        private bool isRivalsSquare(int i_RowToCheck, int i_ColToCheck)
        {
            bool isRivalsSquare = isCoordWithinBoardRange(i_RowToCheck, i_ColToCheck)
                                  && m_CheckersBoard[i_RowToCheck, i_ColToCheck].SquareType != CurrentPlayerSoldierType
                                  && m_CheckersBoard[i_RowToCheck, i_ColToCheck].SquareType != CurrentPlayersKingType
                                  && m_CheckersBoard[i_RowToCheck, i_ColToCheck].IsFreeSquare == false;

            return isRivalsSquare;
        }

        private bool isTargetSquareEmpty(BoardPoint i_TargetCoords)
        {
            bool isEmptyTargetSquare = isPointWithinBoardRange(i_TargetCoords)
                                       && m_CheckersBoard[i_TargetCoords].IsFreeSquare == true;

            return isEmptyTargetSquare;
        }

        private bool isValidEatMove()
        {
            bool isValidEatMove = false;

            foreach (BoardMove validEatMoveForPlayer in r_EatMovesForCurrentPlayer)
            {
                isValidEatMove = m_CurrentPlayersMove.FromPoint.Equals(validEatMoveForPlayer.FromPoint)
                                 && m_CurrentPlayersMove.ToPoint.Equals(validEatMoveForPlayer.ToPoint);
                if (isValidEatMove)
                {
                    m_IsContinuousEatMoveDemanded = false;
                    m_CurrentPlayersMove = validEatMoveForPlayer;
                    r_EatMovesForCurrentPlayer.Remove(validEatMoveForPlayer);

                    break;
                }
            }

            return isValidEatMove;
        }

        private bool isValidNotEatMove()
        {
            bool isValidNotEatMove = false;

            foreach (BoardMove validPlayerMove in r_RegularMovesForCurrentPlayer)
            {
                isValidNotEatMove = m_CurrentPlayersMove.FromPoint.Equals(validPlayerMove.FromPoint)
                                    && m_CurrentPlayersMove.ToPoint.Equals(validPlayerMove.ToPoint);
                if (isValidNotEatMove)
                {
                    m_CurrentPlayersMove = validPlayerMove;
                    r_RegularMovesForCurrentPlayer.Remove(validPlayerMove);

                    break;
                }
            }

            return isValidNotEatMove;
        }

        public void EndGame()
        {
            m_IsRoundOn = false;
            m_IsWin = true;
            m_GameWinner = NextPlayer;
        }

        public void ResetGame()
        {
            RoundOn = true;
            m_CurrentPlayer = 0;
            CurrentPlayer.Reset();
            NextPlayer.Reset();
            m_CheckersBoard.ResetBoard(
                CurrentPlayer.PlayersSoldiersOnBoard,
                NextPlayer.PlayersSoldiersOnBoard);
            GameWinner = null;
            m_CurrentPlayerHasToEat = false;
            m_CurrentPlayersMove = null;
            m_IsContinuousEatMoveDemanded = false;
            IsWin = false;
            m_IsDecidedToQuit = false;
            r_EatMovesForCurrentPlayer.Clear();
            r_RegularMovesForCurrentPlayer.Clear();
            m_CurrentPlayer = 1;
        }

        private void generatePossibleMovesForPlayer()
        {
            int currentPlayersDirection = getPlayersDirection();

            generateEatAndRegularMovesForPlayer(currentPlayersDirection);
            m_CurrentPlayerHasToEat = r_EatMovesForCurrentPlayer.Count != 0;
        }

        private bool isValidPlayerMove(out bool o_IsEatMove)
        {
            bool isValidPlayersMove = false;

            o_IsEatMove = false;
            if (m_IsContinuousEatMoveDemanded)
            {
                isValidPlayersMove = isValidEatMove();
                o_IsEatMove = true;
            }
            else
            {
                generatePossibleMovesForPlayer();
                if (m_CurrentPlayerHasToEat)
                {
                    isValidPlayersMove = isValidEatMove();
                    o_IsEatMove = m_CurrentPlayerHasToEat && isValidPlayersMove;
                }
                else
                {
                    isValidPlayersMove = isValidNotEatMove();
                }
            }

            return isValidPlayersMove;
        }

        private bool isCurrentPlayerHuman()
        {
            bool isHuman = CurrentPlayer.TypeOfPlayer == (uint)Player.eTypeOfPlayer.Human;

            return isHuman;
        }

        private void updateMoveOnBoard()
        {
            m_CheckersBoard.SwapSquares(m_CurrentPlayersMove);
            CurrentPlayer.RemoveSoldier(m_CheckersBoard[m_CurrentPlayersMove.FromPoint]);
            CurrentPlayer.AddSoldier(m_CheckersBoard[m_CurrentPlayersMove.ToPoint]);
        }

        private void updateScore()
        {
            int eatenSoldiersValue = m_CheckersBoard[m_CurrentPlayersMove.RivalsCoords].IsKingSquare
                                         ? (int)eSoldierValue.KingVal
                                         : (int)eSoldierValue.SoldierVal;

            CurrentPlayer.Score += eatenSoldiersValue;
        }

        private void updateScoreAndRivalsSoldiersOnBoard()
        {
            updateScore();
            m_CheckersBoard[m_CurrentPlayersMove.RivalsCoords].InitSquare();
            NextPlayer.RemoveSoldier(m_CheckersBoard[m_CurrentPlayersMove.RivalsCoords]);
        }

        private void checkForPossibleContinuousEatMove()
        {
            r_EatMovesForCurrentPlayer.Clear();
            int currentPlayersDirection = getPlayersDirection();

            addPossibleMovesFromSquare(m_CheckersBoard[m_CurrentPlayersMove.ToPoint], currentPlayersDirection);
            m_IsContinuousEatMoveDemanded = r_EatMovesForCurrentPlayer.Count != 0;
            m_CurrentPlayerHasToEat = true;
        }

        private void updateGameStatus(bool i_IsEatMove)
        {
            updateMoveOnBoard();
            if (i_IsEatMove)
            {
                m_CurrentPlayerHasToEat = false;
                updateScoreAndRivalsSoldiersOnBoard();
                checkForPossibleContinuousEatMove();
            }

            checkForPossibleKingCoronation();
            checkIfWinOrTie();
            if (!m_IsContinuousEatMoveDemanded)
            {
                m_CurrentPlayer = (m_CurrentPlayer + 1) % r_NumOfPlayers;
            }

            OnGameUpdated(new GameUpdatedEventArgs(Players[0].Score, Players[1].Score, m_CurrentPlayer));
        }

        private bool isGameWin(int i_NumOfRivalsEatMoves, int i_NumOfRivalsRegMoves)
        {
            bool isOnlyRivalLeftWithNoSoldiersOnBoard = CurrentPlayersNumberOfKingsAndSoldiersOnBoard != 0
                                                    && NextPlayersNumberOfKingsAndSoldiersOnBoard == 0;
            bool isCurrentPlayerAbleToMoveAtAll = r_EatMovesForCurrentPlayer.Count != 0
                                                     || r_RegularMovesForCurrentPlayer.Count != 0;
            bool isRivalNotAbleToMoveAtAll = i_NumOfRivalsEatMoves == 0 && i_NumOfRivalsRegMoves == 0;
            bool isWin = isOnlyRivalLeftWithNoSoldiersOnBoard
                         || (isCurrentPlayerAbleToMoveAtAll && isRivalNotAbleToMoveAtAll);

            m_GameWinner = isWin ? CurrentPlayer : null;

            return isWin;
        }

        protected virtual void OnRoundEnded(RoundEndedEventArgs i_Ea)
        {
            if (RoundEnded != null)
            {
                RoundEnded(this, i_Ea);
            }
        }

        protected virtual void OnGameUpdated(GameUpdatedEventArgs i_Ea)
        {
            if (GameUpdated != null)
            {
                GameUpdated(this, i_Ea);
            }
        }

        private bool isGameTie(int i_RivalsNumOfEatMoves, int i_RivalsNumOfRegMoves)
        {
            bool bothPlayersHaveSoldiersOnBoard = NextPlayersNumberOfKingsAndSoldiersOnBoard != 0
                                                  && CurrentPlayersNumberOfKingsAndSoldiersOnBoard != 0;
            bool isRivalANotAbleToMoveAtAll = i_RivalsNumOfEatMoves == 0
                                              && i_RivalsNumOfRegMoves == 0;
            bool isCurrentPlayerNotAbleToMoveAtAll = r_EatMovesForCurrentPlayer.Count == 0
                                                     && r_RegularMovesForCurrentPlayer.Count == 0;
            bool isTie = bothPlayersHaveSoldiersOnBoard && isCurrentPlayerNotAbleToMoveAtAll
                                                        && isRivalANotAbleToMoveAtAll;

            return isTie;
        }

        private void checkIfWinOrTie()
        {
            bool isTie = false;
            int playersDirection = 0;
            int rivalsNumOfEatMoves = 0;
            int rivalsNumOfRegMoves = 0;

            for (int i = 0; i < 2; i++)
            {
                m_CurrentPlayer = (m_CurrentPlayer + 1) % 2;
                playersDirection = getPlayersDirection();
                generateEatAndRegularMovesForPlayer(playersDirection);
                if (i == 1)
                {
                    break;
                }

                rivalsNumOfEatMoves = r_EatMovesForCurrentPlayer.Count;
                rivalsNumOfRegMoves = r_RegularMovesForCurrentPlayer.Count;
            }

            m_IsWin = isGameWin(rivalsNumOfEatMoves, rivalsNumOfRegMoves);
            isTie = isGameTie(rivalsNumOfEatMoves, rivalsNumOfRegMoves);
            m_IsRoundOn = !(m_IsWin || isTie);
            if (!m_IsRoundOn)
            {
                calcEndRoundScore();
            }
        }

        private void calcEndRoundScore()
        {
            Player gameLoser = CurrentPlayer == GameWinner ? NextPlayer : CurrentPlayer;
            int totalPointsForRivalsKings = gameLoser.NumberOfKingsOnBoard * (int)eSoldierValue.KingVal;
            int totalPointsForRivalsSoldiers = gameLoser.NumberOfSoldiersOnBoard * (int)eSoldierValue.SoldierVal;

            GameWinner.Score += totalPointsForRivalsSoldiers + totalPointsForRivalsKings;
            OnRoundEnded(new RoundEndedEventArgs(m_IsWin, GameWinner.PlayersName));
        }

        public bool TryToMakeMove(BoardMove i_CurrentPlayerMove)
        {
            bool isValidPlayersMove = false;
            bool isEatMove;

            m_CurrentPlayersMove = i_CurrentPlayerMove;
            isValidPlayersMove = m_IsDecidedToQuit;
            if (m_IsRoundOn)
            {
                isValidPlayersMove = isValidPlayerMove(out isEatMove);
                if (isValidPlayersMove)
                {
                    updateGameStatus(isEatMove);
                    if(!isCurrentPlayerHuman() && m_IsRoundOn)
                    {
                        playComputerMove(out isEatMove);
                        updateGameStatus(isEatMove);
                    }
                }
            }

            return isValidPlayersMove;
        }

        private void checkForPossibleKingCoronation()
        {
            int currentPlayersDirection = getPlayersDirection();
            int kingsRow = currentPlayersDirection == (int)CheckersBoardSquare.eSoldierDirectionInSquare.Down
                               ? (int)CurrentBoardSize - 1
                               : 0;
            CheckersBoardSquare newValuesForSquare = null;

            for (int i = 0; i < CurrentBoardSize; i++)
            {
                if (m_CurrentPlayersMove.ToPoint.Equals(new BoardPoint(kingsRow, i)))
                {
                    newValuesForSquare = new CheckersBoardSquare(
                        m_CurrentPlayersMove.ToPoint.RowOnBoard,
                        m_CurrentPlayersMove.ToPoint.ColOnBoard);
                    newValuesForSquare.IsFreeSquare = false;
                    newValuesForSquare.IsKingSquare = true;
                    newValuesForSquare.SquareType = CurrentPlayersKingType;
                    m_CheckersBoard[m_CurrentPlayersMove.ToPoint].SetValues(newValuesForSquare);
                    CurrentPlayer.NumberOfKingsOnBoard += 1;
                }
            }
        }

        private void generateEatMoveForComputer()
        {
            foreach (BoardMove validEatMove in r_EatMovesForCurrentPlayer)
            {
                m_CurrentPlayersMove = validEatMove;

                break;
            }
        }

        private void generateRegMoveForComputer()
        {
            Random randomEatMove = new Random();
            int randomMoveIndex = randomEatMove.Next(r_RegularMovesForCurrentPlayer.Count);

            m_CurrentPlayersMove = r_RegularMovesForCurrentPlayer[randomMoveIndex];
        }

        private void playComputerMove(out bool o_IsEatMove)
        {
            if (m_IsContinuousEatMoveDemanded)
            {
                generateEatMoveForComputer();
                o_IsEatMove = true;
            }
            else
            {
                generatePossibleMovesForPlayer();
                if (m_CurrentPlayerHasToEat)
                {
                    generateEatMoveForComputer();
                    o_IsEatMove = true;
                }
                else
                {
                    generateRegMoveForComputer();
                    o_IsEatMove = false;
                }
            }
        }
    }
}