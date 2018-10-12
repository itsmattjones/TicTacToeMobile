using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models;

namespace TicTacToe
{
    public class GameEngine
    {
        public GameModel TakePlayerTurn(GameModel gameModel, string cellId, out bool gameIsWon, out bool gameIsDraw)
        {
            bool result = int.TryParse(cellId, out int CellId);
            gameIsWon = false;
            gameIsDraw = false;

            // If not a valid cellId abort.
            if (!result || cellId == string.Empty)
                return gameModel;

            // If the cell's already claimed abort.
            if (gameModel.Board[CellId].CellState != 0)
                return gameModel;
                
            IPlayerModel PlayerTurn = GetTurnPlayer(gameModel);

            // Set the cell to this player.
            gameModel.Board[CellId].CellState = PlayerTurn.PlayerAvatar;

            // Check if player has won, if has end game. Also check for draw.
            gameIsWon = GameIsWon(gameModel.Board, PlayerTurn);
            gameIsDraw = GameIsDraw(gameModel.Board);

            if (gameIsWon || gameIsDraw)
                return gameModel;

            // No Win or Draw? Rotate.
            gameModel.PlayerOne.IsPlayerTurn = !gameModel.PlayerOne.IsPlayerTurn;
            gameModel.PlayerTwo.IsPlayerTurn = !gameModel.PlayerTwo.IsPlayerTurn;

            return gameModel;
        }

        public GameModel TakeAITurn(GameModel gameModel, out bool gameIsWon, out bool gameIsDraw)
        {
            gameIsWon = false;
            gameIsDraw = false;
            int cellId;

            AIDifficulty difficulty = gameModel.PlayerOne.PlayerType == PlayerType.AI ? 
                gameModel.PlayerOne.Difficulty : gameModel.PlayerTwo.Difficulty;

            // Make selection based on difficulty.
            switch(difficulty)
            {
                case AIDifficulty.Easy:
                    cellId = EasyAITurn(gameModel);
                    break;
                case AIDifficulty.Medium:
                    cellId = MediumAITurn(gameModel);
                    break;
                case AIDifficulty.Hard:
                    // fall through.
                default: // Medium
                    cellId = MediumAITurn(gameModel);
                    break;
            }

            // If not a valid cellId or if the cell is taken abort.
            if (cellId == -1 || gameModel.Board[cellId].CellState != 0)
                return gameModel;
                
            IPlayerModel PlayerTurn = GetTurnPlayer(gameModel);

            // Set the cell to this player.
            gameModel.Board[cellId].CellState = PlayerTurn.PlayerAvatar;

            // Check if player has won, if has game will end. Also check for draw.
            gameIsWon = GameIsWon(gameModel.Board, PlayerTurn);
            gameIsDraw = GameIsDraw(gameModel.Board);

            if (gameIsWon || gameIsDraw)
                return gameModel;

            // No Win or Draw? Rotate.
            gameModel.PlayerOne.IsPlayerTurn = !gameModel.PlayerOne.IsPlayerTurn;
            gameModel.PlayerTwo.IsPlayerTurn = !gameModel.PlayerTwo.IsPlayerTurn;

            return gameModel;
        }


        public int EasyAITurn(GameModel gameModel)
        {
            foreach (var cell in gameModel.Board)
            {
                if (cell.CellState == 0)
                    return gameModel.Board.IndexOf(cell);
            }
            return -1;
        }

        public int MediumAITurn(GameModel gameModel)
        {
            Dictionary<List<int>, int> winningCombinations = new Dictionary<List<int>, int>
            {
                { new List<int> { 0, 1, 2 }, 0 }, { new List<int> { 3, 4, 5 }, 0 }, { new List<int> { 6, 7, 8 }, 0 },
                { new List<int> { 0, 3, 6 }, 0 }, { new List<int> { 1, 4, 7 }, 0 }, { new List<int> { 2, 5, 8 }, 0 },
                { new List<int> { 0, 4, 8 }, 0 }, { new List<int> { 2, 4, 6 }, 0 }
            };

            IPlayerModel otherPlayer = gameModel.PlayerOne.IsPlayerTurn ? gameModel.PlayerTwo : gameModel.PlayerOne;

            // Create a new dictionary containing the solution, and how many cells of the solution is taken by the other player.
            Dictionary<List<int>, int> winningCombinationsNew = new Dictionary<List<int>, int>();
            foreach (var solution in winningCombinations)
            {
                var cellTakenCount = 0;

                foreach(var cellId in solution.Key)
                {
                    if (gameModel.Board[cellId].CellState == otherPlayer.PlayerAvatar)
                        cellTakenCount++;
                }

                winningCombinationsNew.Add(solution.Key, cellTakenCount);
            }

            foreach(var solution in winningCombinationsNew.OrderByDescending(key => key.Value))
            {
                // Block the solution
                foreach (var cellid in solution.Key)
                {
                    if (gameModel.Board[cellid].CellState == 0)
                        return cellid;
                }
            }


            // If it fails to block any solutions just pick a cell to take.
            foreach (var cell in gameModel.Board)
            {
                if (cell.CellState == 0)
                    return gameModel.Board.IndexOf(cell);
            }

            return -1;
        }

        private bool GameIsWon(List<CellModel> board, IPlayerModel player)
        {
            return CheckForVictory(board, player) ? true : false;
        }

        private bool GameIsDraw(List<CellModel> board)
        {
            return CheckBoardFull(board) ? true : false;
        }

        #region Helper Methods

        public IPlayerModel GetTurnPlayer(GameModel gameModel)
        {
            return gameModel.PlayerOne.IsPlayerTurn ? gameModel.PlayerOne : gameModel.PlayerTwo;
        }

        private bool CheckBoardFull(List<CellModel> board)
        {
            foreach (var cell in board)
            {
                if (cell.CellState == 0)
                    return false;
            }

            return true;
        }

        private bool CheckForVictory(List<CellModel> board, IPlayerModel player)
        {
            var pAvatar = player.PlayerAvatar;

            // Check horizontal
            if (IsBoardMatch(pAvatar, board[0]) && IsBoardMatch(pAvatar, board[1]) && IsBoardMatch(pAvatar, board[2]))
                return true;

            if (IsBoardMatch(pAvatar, board[3]) && IsBoardMatch(pAvatar, board[4]) && IsBoardMatch(pAvatar, board[5]))
                return true;

            if (IsBoardMatch(pAvatar, board[6]) && IsBoardMatch(pAvatar, board[7]) && IsBoardMatch(pAvatar, board[8]))
                return true;


            // Check vertical
            if (IsBoardMatch(pAvatar, board[0]) && IsBoardMatch(pAvatar, board[3]) && IsBoardMatch(pAvatar, board[6]))
                return true;

            if (IsBoardMatch(pAvatar, board[1]) && IsBoardMatch(pAvatar, board[4]) && IsBoardMatch(pAvatar, board[7]))
                return true;

            if (IsBoardMatch(pAvatar, board[2]) && IsBoardMatch(pAvatar, board[5]) && IsBoardMatch(pAvatar, board[8]))
                return true;


            // Check diagonal
            if (IsBoardMatch(pAvatar, board[0]) && IsBoardMatch(pAvatar, board[4]) && IsBoardMatch(pAvatar, board[8]))
                return true;

            if (IsBoardMatch(pAvatar, board[2]) && IsBoardMatch(pAvatar, board[4]) && IsBoardMatch(pAvatar, board[6]))
                return true;


            return false;
        }

        private bool IsBoardMatch(int avatar, CellModel boardCell)
        {
            var cellState = boardCell.CellState;
            return avatar == cellState ? true : false;
        }

        #endregion
    }
}
