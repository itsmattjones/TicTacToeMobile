using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public class GameEngine : IGameEngine
    {
        public GameType GameType { get; }
        public List<IBoardCell> Board { get; }
        public List<IPlayer> Players { get; }

        public GameEngine(GameType gameType, int boardSize)
        {
            if (boardSize % 9 != 0 || boardSize == 0)
                throw new ArgumentException("The board size must be an equal grid.");

            for (var i = 0; i < boardSize; i++)
                Board.Add(new BoardCell());

            Players.Add(new Player { PlayerId = 1, IsPlayerTurn = true, PlayerAvatar = 1 });
            Players.Add(new AIPlayer { PlayerId = 2, IsPlayerTurn = false, PlayerAvatar = 2, Difficulty = AiDifficulty.Medium });

            if (gameType == GameType.Singleplayer)
            {
                Players[1].PlayerType = PlayerType.Ai;
                Players[1].Difficulty = AiDifficulty.Medium;
            }

            GameType = gameType;
        }

        public EngineTickResult TickPlayerTurn(int chosenCell)
        {
            return DoTurn(GetCurrentPlayer(), chosenCell);
        }

        public EngineTickResult TickAIPlayerTurn()
        {
            var aiPlayer = GetCurrentPlayer();
            if (aiPlayer.PlayerType != PlayerType.Ai)
                throw new Exception("Cannot tick AI player turn of a non-ai player");

            // Make selection based on difficulty.
            int cell;
            switch (aiPlayer.Difficulty)
            {
                case AiDifficulty.Easy:
                    cell = EasyAiTurn(Board);
                    break;
                case AiDifficulty.Medium:
                    cell = MediumAiTurn(Board, aiPlayer);
                    break;
                case AiDifficulty.Hard:
                    cell = HardAiTurn(Board, aiPlayer);
                    break;
                default: // Default to medium
                    cell = MediumAiTurn(Board, aiPlayer);
                    break;
            }

            return DoTurn(aiPlayer, cell);
        }

        private int EasyAiTurn(List<IBoardCell> gameBoard)
        {
            foreach (var cell in gameBoard)
            {
                if (cell.State == CellState.Available)
                    return gameBoard.IndexOf(cell);
            }
            return -1;
        }

        private int MediumAiTurn(List<IBoardCell> gameBoard, IPlayer player)
        {
            var cellsToSearch = player.PlayerId == 1 ? CellState.TakenByPlayer2 : CellState.TakenByPlayer1;
            var solutions = GetPopulatedWinningCombinations(gameBoard, cellsToSearch);

            // Attempt to block solutions with 2 cells taken by the other player.
            var possibleSolutions = solutions.OrderByDescending(x => x[1]).ToList();
            if (possibleSolutions.Any(x => x[1] > 1)) 
            {
                var solution = possibleSolutions.Where(x => x[1] > 1).OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                if (solution != null)
                {
                    foreach (var cellId in solution.GetRange(2, 3))
                    {
                        if (gameBoard[cellId].State == CellState.Available)
                            return cellId;
                    }   
                }
            }

            // Attempt to block solutions with 1 cell taken by the other player.
            if(possibleSolutions.Any(x => x[1] > 0))
            {
                var freeCellIds = new List<int>();
                var solution = possibleSolutions.Where(x => x[1] > 0).OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (solution != null)
                {
                    foreach (var cellId in solution.GetRange(2, 3))
                    {
                        if (gameBoard[cellId].State == CellState.Available)
                            freeCellIds.Add(cellId);
                    }   
                }

                if (freeCellIds.Count > 0)
                    return freeCellIds.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            }

            // If all fails, revert to previous difficulty.
            return EasyAiTurn(gameBoard);
        }

        /// <summary>
        /// Choose a cell under Hard difficulty.
        /// </summary>
        /// <returns>The chosen cell to select.</returns>
        /// <param name="gameModel">Game model.</param>
        private int HardAiTurn(List<IBoardCell> gameBoard, IPlayer player)
        {
            var cellsToSearch = player.PlayerId == 1 ? CellState.TakenByPlayer1 : CellState.TakenByPlayer2;
            var solutions = GetPopulatedWinningCombinations(gameBoard, cellsToSearch);

            // Attempt to win the game.
            if (solutions.Any(x => x[1] > 1))
            {
                var solution = solutions.FirstOrDefault(x => x[1] > 1);
                if (solution != null)
                {
                    foreach (var cellId in solution.GetRange(2, 3))
                    {
                        if (gameBoard[cellId].State == CellState.Available)
                            return cellId;
                    }   
                }
            }

            // If central cell is not taken, take it.
            if (gameBoard[4].State == CellState.Available)
                return 4;

            // If it can't win, revert to using medium to block.
            return MediumAiTurn(gameBoard, player);
        }

        private EngineTickResult DoTurn(IPlayer player, int cell)
        {
            if (cell == -1 || Board[cell].State != CellState.Available)
                return new EngineTickResult() { SuccessfulTick = false };

            // Select cell
            Board[cell].State = player.PlayerId == 1 ? CellState.TakenByPlayer1 : CellState.TakenByPlayer2;
            player.TurnsTaken++;

            // Check for game win or draw
            if (GameIsWon(Board, player))
            {
                return new EngineTickResult()
                {
                    SuccessfulTick = true,
                    GameFinished = true,
                    Results = new GameResults { GameHasWinner = true, Winner = player }
                };
            }
            else if (GameIsDraw(Board))
            {
                return new EngineTickResult()
                {
                    SuccessfulTick = true,
                    GameFinished = true,
                    Results = new GameResults { GameHasWinner = false }
                };
            }

            // Swap player turns
            player.IsPlayerTurn = false;
            if (player.PlayerId == Players.Count)
                Players[0].IsPlayerTurn = true;
            else
                Players[player.PlayerId].IsPlayerTurn = true;

            return new EngineTickResult() { SuccessfulTick = true };
        }

        private IPlayer GetCurrentPlayer()
        {
            foreach (IPlayer player in Players)
            {
                if (player.IsPlayerTurn)
                    return player;
            }

            throw new Exception("Cannot find the player whose turn it is.");
        }

        private bool GameIsWon(List<IBoardCell> board, IPlayer player)
        {
            var cellStateToFind = player.PlayerId == 1 ? CellState.TakenByPlayer1 : CellState.TakenByPlayer2;

            var solutions = GenerateWinningCombinationsList();
            foreach (var solution in solutions)
            {
                var cellsTaken = 0;
                foreach (var cellId in solution.GetRange(2, 3))
                {
                    if (cellStateToFind == board[cellId].State)
                        cellsTaken++;
                }

                if (cellsTaken == 3)
                    return true;
            }

            return false;
        }

        private bool GameIsDraw(List<IBoardCell> board)
        {
            foreach (var cell in board)
            {
                if (cell.State == CellState.Available)
                    return false;
            }

            return true;
        }

        private List<List<int>> GenerateWinningCombinationsList()
        {
            return new List<List<int>>
            {
                new List<int> { 3, 0, 0, 3, 6 }, new List<int> { 3, 0, 1, 4, 7 }, new List<int> { 3, 0, 2, 5, 8 },
                new List<int> { 2, 0, 0, 1, 2 }, new List<int> { 2, 0, 3, 4, 5 }, new List<int> { 2, 0, 6, 7, 8 },
                new List<int> { 1, 0, 0, 4, 8 }, new List<int> { 1, 0, 2, 4, 6 }
            };
        }

        private List<List<int>> GetPopulatedWinningCombinations(List<IBoardCell> gameBoard, CellState cellState)
        {
            var solutions = GenerateWinningCombinationsList();
            foreach (var solution in solutions)
            {
                var solutionCellIds = solution.GetRange(2, 3);
                foreach (var cellId in solutionCellIds)
                {
                    if (gameBoard[cellId].State == cellState)
                        solutions[solutions.IndexOf(solution)][1]++;
                }
            }
            return solutions;
        }
    }
}
